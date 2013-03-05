using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using BananaSplit.Core.Extensions.Encryption;
using BananaSplit.Core.Utility;
using BananaSplit.Data;
using BananaSplit.Model;

namespace BananaSplit.Service
{
    public class AuthenticationService
    {

        private string doubleUC = "__";
        private string encryptPass = Config.EncryptPass;
        private string encryptSalt = Config.EncryptSalt;
        

        /// <summary>
        /// Logs a member and returns authentication token
        /// </summary>
        /// <param name="apiMember"></param>
        /// <param name="facebookKey"></param>
        /// <returns></returns>
        /// 
        public Authentication GetAuthenticatedMemberLoginToken(ApiAccess apiMember, string facebookKey)
        {
            if (String.IsNullOrEmpty(facebookKey))
            {
                return GetBadAuthentication("Missing or Invalid facebook key");
            }
            Authentication auth = null;
            try
            {
                var memberRepo = new MemberRepository();
                var member = memberRepo.GetByFacebookKey(facebookKey);
                if (null == member || !member.IsActive)
                {
                    return GetBadAuthentication("Member not found or is inactive");
                }

                const string hoursAhead = "1000";
                var sessionEnds = DateTime.Now.AddHours(Convert.ToDouble(hoursAhead)).Ticks;

                var plainAuthKey = String.Concat(Convert.ToString(member.MemberId), doubleUC,
                                                 Convert.ToString(sessionEnds), doubleUC, apiMember.AppKey);
                var generatedAuthKey = plainAuthKey.EncryptSymmetric<RijndaelManaged>(encryptPass, encryptSalt);
                auth = new Authentication {Reason = String.Empty, Success = true, AuthKey = generatedAuthKey};
            }
            catch (Exception e)
            {
                auth = GetBadAuthentication("Invalid facebook key and/or member not found");
            }

            return auth;
        }



        /// <summary>
        /// Logs a member and returns authentication token
        /// </summary>
        /// <param name="apiMember"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        /// 
        public Authentication GetAuthenticatedMemberLoginToken(ApiAccess apiMember, Member member)
        {
            Authentication auth = null;
            try
            {
                if (null == member || !member.IsActive)
                {
                    return GetBadAuthentication("Member not found or is inactive");
                }

                const string hoursAhead = "1000";
                var sessionEnds = DateTime.Now.AddHours(Convert.ToDouble(hoursAhead)).Ticks;

                var plainAuthKey = String.Concat(Convert.ToString(member.MemberId), doubleUC,
                                                 Convert.ToString(sessionEnds), doubleUC, apiMember.AppKey);
                var generatedAuthKey = plainAuthKey.EncryptSymmetric<RijndaelManaged>(encryptPass, encryptSalt);
                auth = new Authentication { Reason = String.Empty, Success = true, AuthKey = generatedAuthKey };
            }
            catch (Exception e)
            {
                auth = GetBadAuthentication("Invalid facebook key and/or member not found");
            }

            return auth;
        }



        /// <summary>
        /// This method decrypts and breaks apart the BananaSplit API 3rd Party Company's Auth Key into 
        /// it's relevant data and fills the ApiUserAuthenticationKey with that data.
        /// Use this anytime you need to get at the user's details to validate this user
        /// for an operation like rating, comments, etc.
        /// This authKey should be passed up for all operations that require saves to the database
        /// and should be sent with every request after the initial login
        /// </summary>
        /// <param name="encryptedAuthKey"></param>
        /// <returns></returns>
        ///
        public UserAuthenticationKey GetUserAuthInfo(string encryptedAuthKey)
        {
            return new UserAuthenticationKey(() =>
            {
                var plainKey = encryptedAuthKey.FromBase64().DecryptSymmetric<RijndaelManaged>(encryptPass, encryptSalt);
                var keyArray = plainKey.Split(doubleUC.ToCharArray());
                return new UserAuthenticationKey(
                                            Convert.ToInt32(keyArray[0])
                                            , Convert.ToInt64(keyArray[1])
                                            , keyArray[2]
                                            , encryptedAuthKey);
            });
        }


        private Boolean IsValidUser(string base64Password, string dbPassword, string secretKey)
        {
            string privateKey = secretKey.DecryptSymmetric<RijndaelManaged>(encryptPass, encryptSalt);
            string hashFromDatabasePass = dbPassword.ComputeHash<MD5CryptoServiceProvider>(privateKey);
            return (0 == StringComparer
                            .OrdinalIgnoreCase
                                .Compare(base64Password, hashFromDatabasePass));
        }


        //Helper Method if the Authentication Fails
        private Authentication GetBadAuthentication(string reason)
        {
            return new Authentication()
            {
                AuthKey = String.Empty,
                Success = false,
                Reason = reason
            };
        }
    }
}

