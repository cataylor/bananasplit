using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class LocationRepository : BaseRepository<Location>
    {
        public Location GetById(int id)
        {
            return GetAll().SingleOrDefault(u => u.LocationId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public List<Location> GetAllLocations()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(Location entity)
        {
            if (entity.LocationId == 0)
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
        public Boolean Remove(Location entity)
        {
            return Delete(entity);
        }
    }
}
