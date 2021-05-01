using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Labo1.Controllers
{
    public class VoitureController : Controller
    {
        // GET: Voiture
        public ActionResult Index()
        {
            return View();
        }

        // GET: Voiture/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Voiture/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            String ligne = "";
            try
            {
                using (StreamWriter ficVoiture = new StreamWriter(Server.MapPath(@"~/App_Data/voiture.txt"), true))
                {
                    ligne += Convert.ToString(collection["Numero"]) + ":";
                    ligne += Convert.ToString(collection["Sorte"]) + ":";
                    ligne += Convert.ToString(collection["Annee"]) + ":";
                    ligne += Convert.ToString(collection["Prix"]) ;
                    ficVoiture.WriteLine(ligne);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
