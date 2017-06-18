using BLL.Interfaces.Services;
using System.Linq;
using Quartz;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System;
using MvcPL.Properties;
using ILogger;
using BLL.Interfaces.Entities;

namespace MvcPL.Jobs
{
    public class EmailSender : IJob
    {
        private readonly ILotService lotService;
        private readonly IUserService userService;
        private readonly ILog log;

        public EmailSender(ILotService lotService, IUserService userService, ILog log)
        {
            this.lotService = lotService;
            this.userService = userService;
            this.log = log;
        }

        public Task SendAsync(string to, string subject, string body)
        {
            var from = Settings.Default.MailBox;
            var pass = Settings.Default.MailBoxPassword;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            var mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }

        private void ProcessLot(LotEntity lot)
        {
            try
            {
                lot.IsActive = false;
                lotService.Update(lot);
                if (lot.Bids.Count > 0)
                {
                    var seller = userService.GetOneByPredicate(u => u.ProfileId == lot.ProfileId);
                    int max = lot.Bids.Max(b => b.Price);
                    var winnerProfileId = lot.Bids.First(b => b.Price == max).ProfileId;
                    var winner = userService.GetOneByPredicate(u => u.ProfileId == winnerProfileId);
                    if ((winner != null) && (seller != null))
                    {
                        SendAsync(winner.Email, "Action: You are winner!", "Seller is " + seller.Email);
                        SendAsync(seller.Email, "Auction: Your item is sold out", "Winner is " + winner.Email);
                        Debug.Print("Email");
                        log.Info(string.Format("Auction (LotId: {0}, Name: {1}): Emails were sended successfully!", lot.Id, lot.Name));
                    }                                  
                }
            }
            catch (ArgumentException ae)
            {
                log.Error("Error (mail sending iteration): wrong parameters " + ae.ToString());
            }
            catch (FormatException fe)
            {
                log.Error("Error (mail sending iteration): wrong string of address " + fe.ToString());
            }
            catch (Exception e)
            {
                log.Error("Error (mail sending iteration): " + e.ToString());
            }        
        }

        public void Execute(IJobExecutionContext context)
        {
            Debug.Print("Background");
            log.Debug("Background");
            try
            {
                var lots = lotService.GetAll();
                foreach (var lot in lots)
                {
                    if ((DateTime.Now >= lot.EndDate) && (lot.IsActive == true) && (lot.IsChecked == true))
                    {
                        ProcessLot(lot);                  
                    }
                }
            }
            catch (SystemException ae)
            {
                log.Error("Error (mail sending): "+ ae.ToString());
            }
        }
    }
}