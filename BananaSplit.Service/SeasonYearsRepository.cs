using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class SeasonYearsRepository : BaseRepository<SeasonYear>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seasonYearsId"></param>
        /// <returns></returns>
        /// 
        public String GetSeasonYearsBySeasonId(int seasonYearsId)
        {
            var seasonYear = this.GetAll().SingleOrDefault(s => s.SeasonYearsId == seasonYearsId);
            return seasonYear.Years;
        }

        public int GetSeasonYearsBySeasonString(string years)
        {
            int id = 0;
            var seasonYear =
                this.GetAll().SingleOrDefault(s => s.Years.Equals(years, StringComparison.OrdinalIgnoreCase));
            if (null != seasonYear)
            {
                id = seasonYear.SeasonYearsId;
            }
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(SeasonYear entity)
        {
            if (entity.SeasonYearsId == 0)
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
    }
}
