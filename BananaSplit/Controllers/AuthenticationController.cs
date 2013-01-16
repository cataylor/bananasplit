using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Security;
using BananaSplit.Data;
using BananaSplit.Service;
using Facebook;
using Newtonsoft.Json.Linq;

namespace BananaSplit.Controllers
{
    public class AuthenticationController : Controller
    {
        /*
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
         */


        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = BananaSplit.Core.Utility.Config.FacebookId,
                client_secret = BananaSplit.Core.Utility.Config.FacebookSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            
            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email");
            string email = me.email;
            string fbId = me.id;
            var now = DateTime.Now;

            var memberRepo = new MemberRepository();

            var member = memberRepo.GetByFacebookKey(fbId);

            if (null == member)
            {
                member = new Member();
                member.FirstName = me.first_name;
                member.LastName = me.last_name;
                member.Email = email;
                member.FacebookId = fbId;
                member.DateCreated = now;
                member.MemberTypeId = 1;
            }
            member.DateUpdated = now;
            member.DateLastAccessed = now;

            memberRepo.Save(member);

            //TODO: Check via email to see if this user is authorized to be added. NEED A PAGE FOR THIS

            // Store the access token in the session
            Session["FBAccessToken"] = accessToken;
            Session["FBId"] = fbId;

            // Set the auth cookie
            //FormsAuthentication.SetAuthCookie(email, false);

            return RedirectToAction("Index", "Home");
        }


        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }


        public ActionResult FacebookLogin()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = BananaSplit.Core.Utility.Config.FacebookId,
                client_secret = BananaSplit.Core.Utility.Config.FacebookSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }


    }
}
