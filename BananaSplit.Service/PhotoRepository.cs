using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class PhotoRepository : BaseRepository<Photo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public Photo GetById(long id)
        {
            return this.GetAll().SingleOrDefault(p => p.PhotoId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(Photo entity)
        {
            if (entity.PhotoId == 0)
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
        /// 
        public Boolean Remove(Photo entity)
        {
            entity.IsActive = false;
            return Update(entity);
        }
    }
}
