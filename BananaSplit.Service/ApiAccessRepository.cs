using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using BananaSplit.Core.Extensions.Encryption;
using BananaSplit.Core.Utility;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class ApiAccessRepository : BaseRepository<ApiAccess>
    {

        private string doubleUC = "__";
        private string encryptPass = Config.EncryptPass;
        private string encryptSalt = Config.EncryptSalt;

        public ApiAccess GetById(int id)
        {
            return GetAll().SingleOrDefault(u => u.ApiAccessId == id);
        }


        public ApiAccess CreateNewApiMember(long memberId, string companyName)
        {
            ApiAccess apiMember = null;
            try
            {
                var appKey = GetGUIDWithoutDashes(); //public appKey can be exposed
                //Secret key must be encrypted in case the database is compromised
                var secretKey = GetGUIDWithoutDashes().EncryptSymmetric<RijndaelManaged>(encryptPass, encryptSalt);

                var now = DateTime.Now;

                apiMember = new ApiAccess
                                {
                                    CompanyName = companyName,
                                    AppKey = appKey,
                                    IsActive = true,
                                    MemberId = memberId,
                                    SecretKey = secretKey,
                                    DateCreated = now,
                                    DateUpdated = now
                                };
                //Save the apiMember
                Save(apiMember);
            }
            catch (Exception ex)
            {
                 //Do nothing for now
            }

            return apiMember;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        /// 
        public ApiAccess GetMemberByPublicApiKey(string apiKey)
        {
            return GetAll().SingleOrDefault(am => am.AppKey.Equals(apiKey, StringComparison.OrdinalIgnoreCase));
        }


        //Gets a new GUID
        private String GetGUIDWithoutDashes()
        {
            return Convert.ToString(Guid.NewGuid()).Replace("-", string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(ApiAccess entity)
        {
            if (entity.ApiAccessId == 0)
            {
                //Insert
                return Add(entity);
            }
            else
            {
                //Update
                return Update(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Boolean Remove(ApiAccess entity)
        {
            return Delete(entity);
        }
    }
}
