using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskScheduling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //RemoteServerExample.Run();
            //RemoteClientExample.Run();
            //LoadDLL.Run();
            //JobDel.Run();
            return View();
        }

        [HttpPost]
        public JsonResult Start()
        {
            var json = new JsonResult();
            try
            {
                RemoteServerExample.Run();
            }
            catch (Exception ex)
            {
            }
            json.Data = "success";
            return json;
        }

        [HttpPost]
        public JsonResult AddJob()
        {
            var json = new JsonResult();
            try
            {
                RemoteClientExample.Run();
            }
            catch (Exception ex)
            {
            }
            json.Data = "success";
            return json;
        }

        [HttpPost]
        public JsonResult LoadJob()
        {
            var json = new JsonResult();
            try
            {
                LoadDLL.Run();
            }
            catch (Exception ex)
            {
            }
            json.Data = "success";
            return json;
        }

        [HttpPost]
        public JsonResult DelJob()
        {
            var json = new JsonResult();
            try
            {
                JobDel.Run();
            }
            catch (Exception ex)
            {
            }
            json.Data = "success";
            return json;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}