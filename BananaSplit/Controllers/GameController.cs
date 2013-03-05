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
        // GET: /Game/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Game/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Game/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Game/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.TeamTypes = this.GetTeamTypes();
            ViewBag.Season = this.GetActiveSeasons();
            var game = this.repo.GetById(id);
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

                game.EventStartDate = Convert.ToDateTime(collection.Get("EventStartDate"));
                game.EventEndDate = Convert.ToDateTime(collection.Get("EventEndDate"));
                game.EventLocation = collection.Get("EventLocation");
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
                ViewBag.TeamTypes = this.GetTeamTypes();
                ViewBag.SeasonTypes = this.GetTeamTypes();
                return View(game);
            }
        }

        //
        // GET: /Game/Delete/5

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

        //
        // POST: /Game/Delete/5

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



    }
}
