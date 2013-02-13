using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class SeasonRepository : BaseRepository<Season>
    {

        public List<Season> GetSeasonByTeamId(long teamId)
        {
            return GetAll().Where(t => t.TeamId == teamId && t.IsActive == true).ToList();
        }

        public List<Season> GetAllSeasons()
        {
            return GetAll().Where(s => s.IsActive).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(Season entity)
        {
            if (entity.SeasonId == 0)
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

        public Boolean Remove(Season entity)
        {
            entity.IsActive = false;
            return Update(entity);
        }
    }
}
