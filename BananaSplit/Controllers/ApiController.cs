using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BananaSplit.Controllers
{
    public class ApiController : Controller
    {
        private const String AUTH_ITEM = "AuthenticateResult";

        public ActionResult Index()
        {
            //var authResult = HttpContext.Items[AUTH_ITEM] as DetachedCriteria; //Get the result of the Authenticate attribute
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="facebookKey"></param>
        /// <param name="email"></param>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        /// 
        public ActionResult RegisterFacebookMember(string facebookKey, string email, string mobilePhone = null)
        {
            return new ContentResult();
        }


        public ActionResult AuthenticateMember(string facebookKey)
        {
            return new ContentResult();
        }




    }
}
