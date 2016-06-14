using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reuse2.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult sendMessage()
        {
            var nome = Request.Params["nome"];
            var email = Request.Params["email"];
            var mensagem = Request.Params["mensagem"];

            EmailService es = new EmailService();
            es.sendContactMessage(nome, email, mensagem);

            return RedirectToAction("Contact", new { message = "emailSended" });
        }
    }
}