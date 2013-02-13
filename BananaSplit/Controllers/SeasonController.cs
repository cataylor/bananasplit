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
            var seasonRepo = new SeasonRepository();
            return seasonRepo.GetAllSeasons();
        }

        //
        // GET: /Season/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Season/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Season/Create

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
        // GET: /Season/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Season/Edit/5

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

        //
        // POST: /Season/Delete/5

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
    }
}
