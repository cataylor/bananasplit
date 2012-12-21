using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BananaSplit.Model
{
    /// <summary>
    /// Stores information on the logged in member's current session
    /// </summary>
    /// 
    public class UserAuthenticationKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="appKey"></param>
        /// <param name="numOfHoursForSession"></param>
        /// 
        public UserAuthenticationKey(int memberId, string appKey, double numOfHoursForSession)
        {
            this.MemberId = memberId;
            this.AppKey = appKey;
            this.SessionExpiration = DateTime.Now.AddHours(numOfHoursForSession);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="sessionTicks"></param>
        /// <param name="appKey"></param>
        /// <param name="encryptedAppKey"></param>
        /// 
        public UserAuthenticationKey(int memberId, long sessionTicks, string appKey, string encryptedAppKey)
        {
            this.MemberId = memberId;
            this.AppKey = appKey;
            this.SessionExpiration = new DateTime(sessionTicks);
            this.EncryptedAuthenticationKey = encryptedAppKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedAuthKeyDelegate"></param>
        /// 
        public UserAuthenticationKey(Func<UserAuthenticationKey> encryptedAuthKeyDelegate)
        {
            var apiUser = encryptedAuthKeyDelegate.Invoke();
            this.MemberId = apiUser.MemberId;
            this.SessionExpiration = apiUser.SessionExpiration;
            this.AppKey = apiUser.AppKey;
            this.EncryptedAuthenticationKey = apiUser.EncryptedAuthenticationKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public Int32 MemberId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public DateTime SessionExpiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public String AppKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public String EncryptedAuthenticationKey { get; set; }
    }
}
