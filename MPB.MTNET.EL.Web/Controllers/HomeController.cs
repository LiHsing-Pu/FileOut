using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPB.MTNET.EL.BusinessLogicLayer.Interface;
using MPB.MTNET.EL.BusinessLogicLayer.Service;
using MPB.MTNET.EL.Web.Helpers;
using MPB.MTNET.EL.Parameter;
using MPB.MTNET.EL.Parameter.ViewModel;
using MPB.MTNET.EL.Web.Controllers;
using Template.Web;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using MPB.MTNET.Library;
using System.Net.Http;
using MPB.MTNET.EL.Parameter.ResultModel;
using static MPB.MTNET.EL.Parameter.ViewModel.EL010101ViewModel;


namespace MPB.MTNET.EL.Web.Controllers
{
    public class HomeController : BaseController
    {

        #region 公用參數、建構式
        IEL010101Service _IEL010101Service;
        public HomeController()
        {
            _IEL010101Service = new EL010101Service();
        }
        #endregion


        public ActionResult Index()
        {
            EL010101ViewModel ViewModel = new EL010101ViewModel();
            ViewData["ViewModel"] = ViewModel;
            return View();
        }



        #region table查詢

        //[AuthorizeFilter(Order = 1, userAction = UserAction.Read)]
        //[LogFilterAttibute(Order = 2, userAction = UserAction.Read)]
        public ActionResult Table(int limit, int offset, EL010101ViewModel model)
        {
            try
            {
                model = _IEL010101Service.GetViewCWB01Table(model);
            }
            catch (Exception ex)
            {
                return Json("");
            }
            ViewData["ViewModel"] = model;
            var total = model.resultModel.Count;
            var rows = model.resultModel.Skip(offset).Take(limit).ToList();

            return Json(new { total, rows }, JsonRequestBehavior.AllowGet);
        }

        #endregion table查詢

    }

}

