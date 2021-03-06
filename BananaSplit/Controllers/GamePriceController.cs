﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Data;
using BananaSplit.Service;

namespace BananaSplit.Controllers
{
    public class GamePriceController : BaseController
    {
        private GamePriceRepository repo;

        public GamePriceController()
        {
            this.repo = new GamePriceRepository();
        }

        //
        // GET: /GamePrice/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /GamePrice/Create

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
                        var gamePriceName = String.Format("game_price_name_{0}", item);
                        var gPrice = String.Format("game_price_price_{0}", item);
                        var name = collection.Get(gamePriceName);
                        var price = collection.Get(gPrice);
                        var gamePrice = new GamePrice();
                        gamePrice.Price = Convert.ToDecimal(price);
                    }
                }


                return RedirectToAction("Index", "Team");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /GamePrice/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /GamePrice/Edit/5

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
        // GET: /GamePrice/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /GamePrice/Delete/5

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
