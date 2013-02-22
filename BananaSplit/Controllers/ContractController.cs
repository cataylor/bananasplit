using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BananaSplit.Controllers
{
    public class ContractController : Controller
    {
        //
        // GET: /Contract/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Contract/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Contract/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contract/Create

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
        // GET: /Contract/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Contract/Edit/5

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
        // GET: /Contract/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contract/Delete/5

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
