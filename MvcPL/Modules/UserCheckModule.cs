using BLL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;

namespace MvcPL.Modules
{

    public class UserCheckModule : IHttpModule
    {
        public UserCheckModule ()
        {
        }
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);
        }

        public void Dispose() { }

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {          
            try
            {
                string name = null;
                name = HttpContext.Current.User.Identity.Name;
                if ((name != null) && (name != string.Empty))
                {
                    var user = Membership.GetUser(name, false);
                    if (user == null)
                    {
                        HttpContext.Current.Session.Abandon();
                        FormsAuthentication.SignOut();
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    Debug.Print(name);
                }              
            }
            catch
            {
                Debug.Print("Error: http context");
            }
        }
    }
}