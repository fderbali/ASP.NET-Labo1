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

        // GET: Voiture/Rapport
        public ActionResult Rapport()
        {
            var listVoitures = new List<Models.Voiture> { };
            if (System.IO.File.Exists(Server.MapPath(@"~/App_Data/voiture.txt"))){
                System.IO.StreamReader ficVoitures = new System.IO.StreamReader(Server.MapPath(@"~/App_Data/voiture.txt"));
                var ligne = "";
                var delimiteur = ':';
                Double TotalDesVentes = 0;
                Int16 NbVoitures = 0;
                Int16 NbVoituresAmericainesDePlusDe2000 = 0;
                Int16 NbVoituresRecentes = 0;
                Int16 NbVoituresJaponaise = 0;
                Double PrixVoituresJaponaise = 0;
                Double PrixAmericaineLaMoinsChere = 99999999;
                String ImmatriculationVoitureAmericaineLaMoinsChere = "";
                while ((ligne = ficVoitures.ReadLine()) != null)
                {
                    var attributs = ligne.Split(delimiteur);
                    var uneVoiture = new Models.Voiture() { Numero = attributs[0],  Sorte = Convert.ToInt16(attributs[1]), Annee = attributs[2], Prix = Convert.ToDouble(attributs[3]) };
                    listVoitures.Add(uneVoiture);
                    NbVoitures++;
                    TotalDesVentes += uneVoiture.Prix;
                    if(uneVoiture.Sorte == 1 && uneVoiture.Prix > 2000)
                    {
                        NbVoituresAmericainesDePlusDe2000++;
                    }
                    if( Convert.ToInt16(uneVoiture.Annee) >= 1991)
                    {
                        NbVoituresRecentes++;
                    }
                    if (uneVoiture.Sorte == 2)
                    {
                        NbVoituresJaponaise++;
                        PrixVoituresJaponaise += uneVoiture.Prix;
                    }
                    if (uneVoiture.Sorte == 2 && uneVoiture.Prix < PrixAmericaineLaMoinsChere)
                    {
                        PrixAmericaineLaMoinsChere = uneVoiture.Prix;
                        ImmatriculationVoitureAmericaineLaMoinsChere = uneVoiture.Numero;
                    }

                }
                ViewData["NbVoitures"] = NbVoitures;
                ViewData["TotalDesVentes"] = TotalDesVentes;
                ViewData["NbVoituresAmericainesDePlusDe2000"] = NbVoituresAmericainesDePlusDe2000;
                ViewData["NbVoituresRecentes"] = NbVoituresRecentes;
                ViewData["PrixMoyenVoitureJaponaise"] = PrixVoituresJaponaise / NbVoituresJaponaise;
                ViewData["PrixAmericaineLaMoinsChere"] = PrixAmericaineLaMoinsChere;
                ViewData["ImmatriculationVoitureAmericaineLaMoinsChere"] = ImmatriculationVoitureAmericaineLaMoinsChere;
            }
            return View(listVoitures);
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
