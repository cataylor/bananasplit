using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BananaSplit.Service;

namespace BananaSplit.Filters
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        private const String AUTH_RESULT = "AuthenticateResult";
        private const String AUTH_TOKEN = "AuthToken";

        private ApiAccessRepository repo;

        /// <summary>
        /// CTOR
        /// </summary>
        /// 
        public AuthenticateAttribute()
        {
            this.repo = new ApiAccessRepository();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        /// 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var encryptedToken = filterContext.HttpContext.Request[AUTH_TOKEN]; //Get Token from client app

            var authService = new AuthenticationService();
            var userAuthInfo = authService.GetUserAuthInfo(encryptedToken); //Decrypt token

            var validApiKey = this.repo.GetMemberByPublicApiKey(userAuthInfo.AppKey); //Get appkey and see if it's valid before proceeding
            
            filterContext.HttpContext.Items[AUTH_RESULT] = (null != validApiKey) ? userAuthInfo : null;

        }
        
    }
}