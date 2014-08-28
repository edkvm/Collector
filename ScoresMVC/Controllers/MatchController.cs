using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoresMVC.Models;

namespace ScoresMVC.Controllers
{
    public class MatchController : Controller
    {
        //
        // GET: /Match/

        public ActionResult Index()
        {
            using (var db = new EddyScoresDBEntities()) {
                return View(db.tblMatches.ToList());
            }
        }

    }
}
