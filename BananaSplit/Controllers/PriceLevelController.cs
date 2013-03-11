using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Data;
using BananaSplit.Service;

namespace BananaSplit.Controllers
{
    public class PriceLevelController : BaseController
    {
        private PriceLevelRepository repo;

        public PriceLevelController()
        {
            this.repo = new PriceLevelRepository();
        }
        //
        // GET: /PriceLevel/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /PriceLevel/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PriceLevel/Create

        public ActionResult Create(long seasonId, long teamId)
        {
            var pricelevel = new PriceLevel();
            pricelevel.SeasonId = seasonId;
            pricelevel.TeamId = teamId;
            return View(pricelevel);
        }

        //
        // POST: /PriceLevel/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var idList = collection.Get("ids");
                var idArray = idList.Split(",".ToCharArray());

                if (idArray.Length > 0)
                {
                    foreach (var item in idArray)
                    {
                        var priceLevelName = String.Format("price_level_name_{0}", item);
                        var gPrice = String.Format("price_level_price_{0}", item);
                        var name = collection.Get(priceLevelName);
                        var price = collection.Get(gPrice);
                        var priceLevel = new PriceLevel();
                        priceLevel.PriceLevelName = name;
                        priceLevel.Price = Convert.ToDecimal(price);
                        priceLevel.TeamId = Convert.ToInt64(collection.Get("TeamId"));
                        priceLevel.SeasonId = Convert.ToInt64(collection.Get("SeasonId"));
                        this.repo.Save(priceLevel);
                    }
                }


                return RedirectToAction("Index", "PriceLevel");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PriceLevel/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PriceLevel/Edit/5

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
        // GET: /PriceLevel/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PriceLevel/Delete/5

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
