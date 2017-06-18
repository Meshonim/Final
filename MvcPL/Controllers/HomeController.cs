using BLL.Interfaces.Services;
using BLL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILotService service;
        private readonly ITagService srv;
        private readonly IUserService userService;

        public HomeController(ILotService service, ITagService srv, IUserService userService)
        {
            this.service = service;
            this.srv = srv;
            this.userService = userService;
        }

        [Authorize(Roles = "admin, moder")]
        public ActionResult ShowUnchecked()
        {
            var lots = service.GetAllByPredicate(l => l.IsChecked == false);
            var list = new ListLotViewModel();
            list.lots = new List<LotViewModel>();
            foreach (LotEntity lot in lots)
            {
                list.lots.Add(lot.ToLotViewModel());
            }
            list.lots = list.lots.OrderBy(e => e.EndDate).ToList();
            ViewBag.lots = list;
            return View();
        }

        public ActionResult Index(string word)
        {
            IEnumerable<LotEntity> lots = null;        
            if (word != null)
            {
                lots = service.GetAllByPredicate(l => l.IsChecked == true && l.Name.Contains(word));
            }
            else
            {
                lots = service.GetAllByPredicate(l => l.IsChecked == true);
            }
            var list = new ListLotViewModel();
            list.lots = new List<LotViewModel>();
            foreach (LotEntity lot in lots)
            {
                list.lots.Add(lot.ToLotViewModel());
            }
            ViewBag.lots = list;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page";

            return View();
        }
    }
}