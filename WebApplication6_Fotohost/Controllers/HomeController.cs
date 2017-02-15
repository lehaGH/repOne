using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6_Fotohost.Controllers
{
    public class HomeController : Controller
    {
        Random rnd = new Random();
     
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Upload(string texts, HttpPostedFileBase uploadImage)
        {
            if (uploadImage == null) return RedirectToRoute(new { controller = "Home", action = "ErrorPage" });

            try
            {
                var buff = new Models.DataImg();
                buff.GreatUnit(texts, uploadImage, rnd.Next(111111, 999999), rnd.Next(111111, 999999));
                return RedirectToRoute(new { controller = "Home", action = "AdminPage", adminlink = buff.adminLink });
            }
            catch (Exception e) { return RedirectToRoute(new { controller = "Home", action = "ErrorPage" }); }         
        }

        public ActionResult AdminPage(string adminlink)
        {
            Models.DataImg buff;
            try
            {buff = new Models.DataImg().findByAdminLink(adminlink);}
            catch (Exception e)
            { buff = new Models.DataImg().FindByID();}
            
            return View(buff);
        }

        public FileContentResult GetImage(string link)
        {
            Models.DataImg buff;
            try
            {buff = new Models.DataImg().findByLink(link);}
            catch (Exception e) { buff = new Models.DataImg().FindByID(); }
            return File(buff.data, buff.type);
        }

        public ActionResult ErrorPage()
        {
            return View();
        }
        
        [HandleError(ExceptionType=typeof(Exception), View="ErrorPage")]
        public ActionResult Remove(string adminLink)
        {
            ViewBag.status = "Удаленно";
            new Models.DataImg().RemoveByAdminLink(adminLink);
            return PartialView();
        }
        //public FileContentResult GetImageS(string link, int w,int h)
        //{
        //    var buff = new Models.DataImg().findByLink(link);
        //    var img = new Bitmap(buff.GetBitmap(), w, h);
        //    MemoryStream str=new MemoryStream();
        //    img.Save(str,System.Drawing.Imaging.ImageFormat.Jpeg);
        //    return File(str.ToArray(), buff.type);
        //}
    }
}