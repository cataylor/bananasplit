using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BananaSplit.Controllers
{
    public class SeasonController : Controller
    {
        //
        // GET: /Season/

        public ActionResult Index()
        {
            return View();
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

        public ActionResult Delete(int id)
        {
            return View();
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
