using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Data;
using BananaSplit.Service;

namespace BananaSplit.Controllers
{
    public class TeamController : Controller
    {
        //
        // GET: /Team/

        public ActionResult Index()
        {
            return View();
        }

        
        //
        // GET: /Team/Create

        public ActionResult Create()
        {
            ViewBag.States = this.GetStates();
            return View(new Team());
        }

        //
        // POST: /Team/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var team = new Team();
                team.TeamName = collection.Get("TeamName");


                var locationRepo = new LocationRepository();
                var location = new Location();
                location.City = collection.Get("City");
                location.StateId = Convert.ToInt32(collection.Get("StateId"));

                locationRepo.Add(location);

                team.LocationId = location.LocationId;


                var teamRepo = new TeamRepository();
                teamRepo.Add(team);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Team/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Team/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Team/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        private List<State> GetStates()
        {
            var stateRepo = new StateRepository();
            var states = stateRepo.GetAllStates();
            return states;
        }
    }
}
