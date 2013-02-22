using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Data;
using BananaSplit.Service;

namespace BananaSplit.Controllers
{
    
    public class TeamController : BaseController
    {
        private TeamRepository repo;

        public TeamController()
        {
            this.repo = new TeamRepository();
        }
        //
        // GET: /Team/

        public ActionResult Index(int teamTypeId = 1)
        {
            ViewBag.TeamTypes = this.GetTeamTypes();
            ViewBag.SelectedTeamType = this.GetTeamType(teamTypeId);
            return View(this.repo.GetAllTeams());
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
                var team = new Team {TeamName = collection.Get("TeamName")};

                var locationRepo = new LocationRepository();
                
                var location = new Location();
                location.City = collection.Get("City");
                location.StateId = Convert.ToInt32(collection.Get("StateId"));

                locationRepo.Add(location);

                team.LocationId = location.LocationId;
                
                this.repo.Add(team);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.States = this.GetStates();
                return View();
            }
        }

        //
        // GET: /Team/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.States = this.GetStates();
            var team = this.repo.GetById(id);
            return View(team);
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var team = this.repo.GetById(Convert.ToInt32("TeamId"));
                team.TeamName = collection.Get("TeamName");

                var locationRepo = new LocationRepository();

                var city = collection.Get("City");
                var stateId = Convert.ToInt32(collection.Get("StateId"));

                var location = locationRepo.GetByCityStateId(city, stateId);

                if (null == location)
                {
                    location = new Location();
                    location.City = city;
                    location.StateId = stateId;
                }
                locationRepo.Save(location);

                team.LocationId = location.LocationId;


                this.repo.Add(team);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Team/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var team = this.repo.GetById(id);

            if (null != team)
            {
                this.repo.Remove(team);
            }

            return RedirectToAction("Index");
        }


        private List<TeamType> GetTeamTypes()
        {
            var teamTypeRepo = new TeamTypeRepository();
            return teamTypeRepo.GetAllTeamTypes();
        }

        private TeamType GetTeamType(int id)
        {
            var teamTypeRepo = new TeamTypeRepository();
            return teamTypeRepo.GetById(id);
        }


        private List<State> GetStates()
        {
            var stateRepo = new StateRepository();
            var states = stateRepo.GetAllStates();
            return states;
        }
    }
}
