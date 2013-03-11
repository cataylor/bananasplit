using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Data;
using BananaSplit.Data.Models;
using BananaSplit.Service;

namespace BananaSplit.Controllers
{
    public class GameController : BaseController
    {
        private GameRepository repo;
        public GameController()
        {
            this.repo = new GameRepository();
        }

        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View();
        }

 
        //
        // GET: /Game/Create

        public ActionResult Create()
        {
            ViewBag.Seasons = this.GetActiveSeasons();
            ViewBag.TeamTypes = this.GetTeamTypes();
            return View();
        }

        //
        // POST: /Game/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var game = new Game();
            try
            {
                var now = DateTime.Now;
                game.EventStartDate = CombineDateAndTime(collection.Get("EventStartDate"), collection.Get("EventStartTime"));
                game.EventEndDate = CombineDateAndTime(collection.Get("EventEndDate"), collection.Get("EventEndTime"));
                var loc = collection.Get("EventLocation");
                if (!String.IsNullOrEmpty(loc))
                {
                    game.EventLocation = collection.Get("EventLocation");
                }
                game.IsActive = true;
                game.VisitingTeamId = Convert.ToInt64(collection.Get("VisitingTeamId"));
                game.TeamId = Convert.ToInt64(collection.Get("TeamId"));
                game.TimeZone = collection.Get("TimeZone");
                game.SeasonId = Convert.ToInt64(collection.Get("SeasonId"));
                game.DateCreated = now;
                game.DateUpdated = now;

                this.repo.Save(game);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Seasons = this.GetActiveSeasons();
                ViewBag.TeamTypes = this.GetTeamTypes();
                return View(game);
            }
        }

        //
        // GET: /Game/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.TeamTypes = this.GetTeamTypes();
            ViewBag.Season = this.GetActiveSeasons();
            var game = this.repo.GetById(id);
            var teamRepo = new TeamRepository();
            ViewBag.TeamList = teamRepo.GetAllTeamsByTeamType(game.Team.TeamTypeId);
            return View(game);
        }

        //
        // POST: /Game/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            var gameId = Convert.ToInt64(collection.Get("GameId"));
            var game = new Game();
            try
            {
                var now = DateTime.Now;

                game = this.repo.GetById(gameId);

                game.EventStartDate = CombineDateAndTime(collection.Get("EventStartDate"), collection.Get("EventStartTime"));
                game.EventEndDate = CombineDateAndTime(collection.Get("EventEndDate"), collection.Get("EventEndTime"));
                var loc = collection.Get("EventLocation");
                if (!String.IsNullOrEmpty(loc))
                {
                    game.EventLocation = collection.Get("EventLocation");
                }
                var isActive = (collection.Get("IsActive") != null);
                game.IsActive = isActive;
                game.VisitingTeamId = Convert.ToInt64(collection.Get("VisitingTeamId"));
                game.TeamId = Convert.ToInt64(collection.Get("TeamId"));
                game.TimeZone = collection.Get("TimeZone");
                game.SeasonId = Convert.ToInt64(collection.Get("SeasonId"));
                game.DateUpdated = now;

                this.repo.Save(game);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.TeamTypes = this.GetTeamTypes();
                ViewBag.SeasonTypes = this.GetSeasonTypes();
                return View(game);
            }
        }

        //
        // GET: /Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int gameId)
        {
            var result = new ApiResult();

            try
            {
                var game = this.repo.GetAll().SingleOrDefault(g => g.GameId == gameId);
                if (null != game)
                {
                    game.IsActive = false;
                    this.repo.Save(game);
                }
                result.Description = "Game successfully deleted!";
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Description = "Failed to delete game";
                result.Success = false;
            }

            return this.ToJson(result);
        }
        

        private List<TeamType> GetTeamTypes()
        {
            var teamTypeRepo = new TeamTypeRepository();
            return teamTypeRepo.GetAllTeamTypes();
        }

        private List<Season> GetActiveSeasons()
        {
            var seasonRepo = new SeasonRepository();
            return seasonRepo.GetAllSeasons();
        }

        private List<SeasonType> GetSeasonTypes()
        {
            var seasonTypeRepo = new SeasonTypeRepository();
            return seasonTypeRepo.GetAllSeasonTypes();
        }

        private DateTime CombineDateAndTime(string theDate, string theTime)
        {
            var datePart = DateTime.ParseExact(theDate, "MM/dd/yyyy", null);
            var dateString = String.Format("{0} {1}", datePart.ToString("MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")), theTime);
            var newDate = DateTime.Parse(dateString, CultureInfo.CreateSpecificCulture("en-US"));
            return newDate;
            
        }



    }
}
