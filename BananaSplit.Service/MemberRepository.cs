using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaSplit.Data;


namespace BananaSplit.Service
{
    public class MemberRepository : BaseRepository<Member>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public Member GetById(long id)
        {
            return GetAll().SingleOrDefault(m => m.MemberId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facebookKey"></param>
        /// <returns></returns>
        /// 
        public Member GetByFacebookKey(string facebookKey)
        {
            return GetAll().SingleOrDefault(m => m.FacebookId.Equals(facebookKey, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(Member entity)
        {
            if (entity.MemberId == 0)
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
        public Boolean Remove(Member entity)
        {
            entity.IsActive = false;
            return Update(entity);
        }
    }
}
