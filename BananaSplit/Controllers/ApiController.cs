using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Helpers;
using BananaSplit.Data;
using BananaSplit.Data.Models;
using BananaSplit.Service;

namespace BananaSplit.Controllers
{
    public class ApiController : BaseController
    {
        private const String AUTH_ITEM = "AuthenticateResult";
        
        
        #region Authentication/Login

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="facebookKey"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="deviceId"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="phoneModel"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult RegisterFacebookMember(string appKey, string facebookKey, string firstName, string lastName, string email, string deviceId, string mobilePhoneNumber = null, string phoneModel = "iPhone")
        {
            var apiAccessRepo = new ApiAccessRepository();
            var apiAccess = apiAccessRepo.GetMemberByPublicApiKey(appKey);
            var apiAuthResult = new ApiAuthResult();

            if (null != apiAccess)
            {

                try
                {
                    var memberRepo = new MemberRepository();
                    var now = DateTime.Now;

                    var member = memberRepo.GetByFacebookKey(facebookKey);

                    if (null == member)
                    {
                        member = new Member();
                        member.FirstName = firstName;
                        member.LastName = lastName;
                        member.Email = email;
                        member.FacebookId = facebookKey;
                        member.DateCreated = now;
                        member.MemberTypeId = 1;
                    }
                    member.DateUpdated = now;
                    member.DateLastAccessed = now;

                    memberRepo.Save(member);

                    //Now Get User Auth Token
                    var authService = new AuthenticationService();
                    var auth = authService.GetAuthenticatedMemberLoginToken(apiAccess, member);
                    apiAuthResult.Success = auth.Success;
                    apiAuthResult.Description = auth.Reason;
                    apiAuthResult.AuthKey = auth.AuthKey;
                }catch(Exception e)
                {
                    apiAuthResult.Success = false;
                    apiAuthResult.Description = "Failed to save member and/or generate auth token";
                    apiAuthResult.AuthKey = "";
                }


            }else
            {
                apiAuthResult.Success = false;
                apiAuthResult.Description = "Invalid AppKey";
                apiAuthResult.AuthKey = "";
            }

            return this.ToXml(apiAuthResult);


        }


        #endregion


        #region Teams

        #endregion

        #region Games

        #endregion


        #region Season


        #endregion


        public ActionResult SendSms()
        {
            var member = new Member();
            member.FirstName = "Joe";
            member.LastName = "Rosenblum";

            Communication.SendSmsLoginResponse(member);

            var apiResult = new ApiResult {Success = true, Description = ""};

            return this.ToXml(apiResult);
        }




    }
}
