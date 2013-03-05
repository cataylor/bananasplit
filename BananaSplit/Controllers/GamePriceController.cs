using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BananaSplit.Controllers
{
    public class GamePriceController : BaseController
    {
        
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
                // TODO: Add insert logic here

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
