using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Data;
using BananaSplit.Data.Models;
using BananaSplit.Service;

namespace BananaSplit.Controllers
{
    public class SeasonController : BaseController
    {
        private SeasonRepository seasonRepo;
        public SeasonController()
        {
            this.seasonRepo = new SeasonRepository();
        }
        //
        // GET: /Season/

        public ActionResult Index()
        {
            ViewBag.Teams = this.GetAllTeams();
            return View(this.GetAllSeasonData());
        }

        private List<Team> GetAllTeams()
        {
            var teamRepo = new TeamRepository();
            return teamRepo.GetAllTeams();
        }

        private List<Season> GetAllSeasonData()
        {
            return this.seasonRepo.GetAll().Where(s => s.IsActive).ToList();
        }
        
        //
        // GET: /Season/Create

        public ActionResult Create()
        {
            ViewBag.TeamTypes = this.GetTeamTypes();
            ViewBag.SeasonTypes = this.GetTeamTypes();
            return View(new Season());
        }

        //
        // POST: /Season/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var season = new Season();
            try
            {
                var now = DateTime.Now;

                season.EventStartDate = Convert.ToDateTime(collection.Get("EventStartDate"));
                season.EventEndDate = Convert.ToDateTime(collection.Get("EventEndDate"));
                season.EventLocation = collection.Get("EventLocation");
                season.IsActive = true;
                season.IsArchived = false;
                season.Opponent = collection.Get("Opponent");
                season.SeasonTypeId = Convert.ToInt32(collection.Get("SeasonTypeId"));
                season.TeamId = Convert.ToInt64(collection.Get("TeamId"));
                season.TimeZone = collection.Get("TimeZone");
                season.SeasonYearsId = this.GetSeasonYearsId(collection.Get("SeasonYearsId"));
                season.DateCreated = now;
                season.DateUpdated = now;

                this.seasonRepo.Save(season);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.TeamTypes = this.GetTeamTypes();
                ViewBag.SeasonTypes = this.GetTeamTypes();
                return View(season);
            }
        }

        private int GetSeasonYearsId(string seasonYears)
        {
            var seasonYearsRepo = new SeasonYearsRepository();

            int existingYears = seasonYearsRepo.GetSeasonYearsBySeasonString(seasonYears);

            if (existingYears == 0)
            {
                var seasonYear = new SeasonYear();
                seasonYear.Years = seasonYears;
                seasonYearsRepo.Save(seasonYear);
                existingYears = seasonYear.SeasonYearsId;
            }

            return existingYears;

        }

        //
        // GET: /Season/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.TeamTypes = this.GetTeamTypes();
            ViewBag.SeasonTypes = this.GetSeasonTypes();
            var season = this.seasonRepo.GetById(id);
            return View(season);
        }

        //
        // POST: /Season/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            var seasonId = Convert.ToInt64(collection.Get("SeasonId"));
            var season = new Season();
            try
            {
                var now = DateTime.Now;

                season = this.seasonRepo.GetById(seasonId);
                
                season.EventStartDate = Convert.ToDateTime(collection.Get("EventStartDate"));
                season.EventEndDate = Convert.ToDateTime(collection.Get("EventEndDate"));
                season.EventLocation = collection.Get("EventLocation");
                season.IsActive = true;
                season.IsArchived = false;
                season.Opponent = collection.Get("Opponent");
                season.SeasonTypeId = Convert.ToInt32(collection.Get("SeasonTypeId"));
                season.TeamId = Convert.ToInt64(collection.Get("TeamId"));
                season.TimeZone = collection.Get("TimeZone");
                season.SeasonYearsId = this.GetSeasonYearsId(collection.Get("SeasonYearsId"));
                season.DateCreated = now;
                season.DateUpdated = now;

                this.seasonRepo.Save(season);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.TeamTypes = this.GetTeamTypes();
                ViewBag.SeasonTypes = this.GetTeamTypes();
                return View(season);
            }
        }

        //
        // GET: /Season/Delete/5
        [HttpPost]
        public ActionResult Delete(int seasonId)
        {
            var seasonRepo = new SeasonRepository();
            var result = new ApiResult();

            try
            {
                var season = seasonRepo.GetAll().SingleOrDefault(sr => sr.SeasonId == seasonId);
                if (null != season)
                {
                    season.IsActive = false;
                    seasonRepo.Save(season);
                }
                result.Description = "Season successfully deleted!";
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Description = "Failed to delete season";
                result.Success = false;
            }

            return this.ToJson(result);
        }

        private List<TeamType> GetTeamTypes()
        {
            var teamTypeRepo = new TeamTypeRepository();
            return teamTypeRepo.GetAllTeamTypes();
        }

        private List<SeasonType> GetSeasonTypes()
        {
            var seasonTypeRepo = new SeasonTypeRepository();
            return seasonTypeRepo.GetAllSeasonTypes();
        }
    }
}
