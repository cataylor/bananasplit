using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Security;
using Newtonsoft.Json.Linq;

namespace BananaSplit.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public ActionResult FacebookLogin(string token)
        {
            var client = new WebClient();
            string JsonResult = client.DownloadString(string.Concat("https://graph.facebook.com/me?access_token=", token));

            var jsonUserInfo = JObject.Parse(JsonResult);
            // you can get more user's info here. Please refer to:
            //     http://developers.facebook.com/docs/reference/api/user/
            string username = jsonUserInfo.Value<string>("username");
            string email = jsonUserInfo.Value<string>("email");
            string locale = jsonUserInfo.Value<string>("locale");
            string facebook_userID = jsonUserInfo.Value<string>("id");

            // store user's information here...
            FormsAuthentication.SetAuthCookie(username, true);
            return RedirectToAction("Index", "Home");
        }

    }
}
