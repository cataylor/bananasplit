using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class GameRepository : BaseRepository<Game>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// 
        public Game GetById(long gameId)
        {
            return GetAll().SingleOrDefault(s => s.GameId == gameId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        /// 
        public List<Game> GetGamesByTeamId(long teamId)
        {
            return GetAll().Where(t => t.TeamId == teamId && t.IsActive == true).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public List<Game> GetAllSeasons()
        {
            return GetAll().Where(s => s.IsActive).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(Game entity)
        {
            if (entity.GameId == 0)
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
        public Boolean Remove(Game entity)
        {
            entity.IsActive = false;
            return Update(entity);
        }
    }
}


