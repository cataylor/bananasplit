using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class MemberTypeRepository : BaseRepository<MemberType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public MemberType GetById(int id)
        {
            return GetAll().SingleOrDefault(mt => mt.MemberTypeId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public List<MemberType> GetAllMemberTypes()
        {
            return GetAll().ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Boolean Remove(MemberType entity)
        {
            var memberRepo = new MemberRepository();
            IQueryable<Member> memberTypeUsed = null;
            try
            {
                memberTypeUsed = memberRepo.GetAll().Where(m => m.MemberTypeId == entity.MemberTypeId);

            }catch(Exception e)
            {
                //Do nothing for now
            }

            if (null != memberTypeUsed && memberTypeUsed.Any())
            {
                entity.IsActive = false;
                return Update(entity);
            }else
            {
                return Delete(entity);
            }

            
        }

    }
}
