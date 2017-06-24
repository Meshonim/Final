using BLL.Interfaces.Services;
using MvcPL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Helpers;
using BLL.Interfaces.Entities;

namespace MvcPL.Controllers
{
    public class LotController : Controller
    {
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        private readonly ILotService lotService;
        private readonly IBidService bidService;

        public LotController(IUserService userService, ILotService lotService, IProfileService profileService, IBidService bidService)
        {
            this.userService = userService;
            this.lotService = lotService;
            this.profileService = profileService;
            this.bidService = bidService;
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult BidsIndex(int? id)
        {
            var lot = lotService.GetById(id.Value);
            var model = lot.ToLotViewModel();
            model.Bids = GetBidsList(lot);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_BidsList", model.Bids);
            }
            else
            {
                var user = userService.GetOneByPredicate(u => u.Name == User.Identity.Name);
                ViewBag.isBidPossible = CheckBidPossibility(user, lot);  
                return View("Details", model);
            }
        }

        [NonAction]

        public ListBidViewModel GetBidsList(LotEntity lot)
        {
            ListBidViewModel model = new ListBidViewModel();
            model.List = new List<BidViewModel>();
            if (lot != null)
            {                            
                foreach (BidEntity bidEntity in lot.Bids)
                {
                    BidViewModel bidViewModel = bidEntity.ToBidViewModel();
                    var profile = profileService.GetOneByPredicate(p => p.Id == bidEntity.ProfileId);
                    if (profile != null)
                    {
                        bidViewModel.Name = profile.Name;
                    }
                    else
                    {
                        bidViewModel.Name = "Unknown";
                    }
                    model.List.Add(bidViewModel);
                }             
            }
            return model;
        }

        [NonAction]
        private bool CheckBidPossibility (UserEntity user, LotEntity lot)
        {
            bool result = true;
            if ((lot.IsActive == false) || (lot.IsChecked == false))
            {
                result = false;
            }
            else
            {
                if (user != null)
                {
                    if (user.ProfileId == lot.ProfileId)
                        result = false;
                }
            }
            return result;
        }

        [Authorize]
        public ActionResult Details(int? id)
        {        
            var lot = lotService.GetById(id.Value);
            if ((lot.IsChecked == false) && !(User.IsInRole("admin") || User.IsInRole("moder")))
                return RedirectToAction("Index", "Home");
            var user = userService.GetOneByPredicate(u => u.Name == User.Identity.Name);
            ViewBag.isBidPossible = CheckBidPossibility(user, lot);         
            var model = lot.ToLotViewModel();
            model.Bids = GetBidsList(lot);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles="moder, admin")]
        public ActionResult Verify(int? id)
        {
            var lot = lotService.GetById(id.Value);
            lot.IsChecked = true;
            lot.IsActive = true;
            lotService.Update(lot);
            return RedirectToAction("ShowUnchecked", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "moder, admin")]
        public ActionResult Delete(int? id)
        {
            var lot = lotService.GetById(id.Value);
            lotService.Delete(lot);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLotModel model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Tags != null)
                model.Tags = model.Tags.Trim();

            var result = new LotViewModel();
            using (var img = Image.FromStream(model.Picture.InputStream))
            {
                result.Picture = ImageHelper.ImageToByteArray(img);
            }        
            result.Name = model.Name;
            result.Description = model.Description;
            var user = userService.GetOneByPredicate(u => u.Name == User.Identity.Name);
            if (user != null)
                result.ProfileId = user.ProfileId;
            result.StartDate = DateTime.Now;
            result.EndDate = result.StartDate + TimeSpan.FromDays(model.Days);

            var tags = model.Tags != null ? model.Tags.Split(' ') : new string[0];
            result.Tags = new List<TagViewModel>();
            foreach (var tag in tags)
            {
                result.Tags.Add(new TagViewModel
                {
                    Name = tag
                });
            }

           lotService.Create(result.ToLotEntity());
           return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateBid(CreateBidModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var lot = lotService.GetById(model.LotId);
            var user = userService.GetOneByPredicate(u => u.Name == User.Identity.Name);
            if (!CheckBidPossibility(user, lot))
            {
                return RedirectToAction("Details", "Lot", new { id = model.LotId });
            }
            if (lot.Bids.Count > 0)
            {
                int max = lot.Bids.Max(e => e.Price);
                if (model.Price <= max)
                {
                    ModelState.AddModelError("Price", "Your price is less than others");
                    return View(model);
                }
            }           
            var bid = new BidEntity();
            if (user != null)
                bid.ProfileId = user.ProfileId;          
            bid.LotId = model.LotId;
            bid.Price = model.Price;
            bid.Date = DateTime.Now;
            bidService.Create(bid);
            return RedirectToAction("Details", "Lot", new { id = model.LotId });
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateBid(int? id)
        {
            var lot = lotService.GetById(id.Value);
            var user = userService.GetOneByPredicate(u => u.Name == User.Identity.Name);
            if (!CheckBidPossibility(user, lot))
            {
                return RedirectToAction("Details", "Lot", new { id = id });
            }
            var model = new CreateBidModel();
            model.LotId = 0;
            if (id.HasValue)
            {
                model.LotId = id.Value;
            }          
            return View(model);
        }

        

    }
}