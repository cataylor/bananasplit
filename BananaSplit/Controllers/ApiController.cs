using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Helpers;
using BananaSplit.Data;
using BananaSplit.Data.Models;

namespace BananaSplit.Controllers
{
    public class ApiController : BaseController
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


        public ActionResult SendSms()
        {
            var member = new Member();
            member.FirstName = "Joe";
            member.LastName = "Rosenblum";

            Communication.SendSmsLoginResponse(member);

            var apiResult = new ApiResult();
            apiResult.Success = true;
            apiResult.Description = "";

            return this.ToXml(apiResult);
        }




    }
}
