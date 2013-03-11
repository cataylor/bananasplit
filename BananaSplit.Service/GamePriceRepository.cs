using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class GamePriceRepository : BaseRepository<GamePrice>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamePriceId"></param>
        /// <returns></returns>
        /// 
        public GamePrice GetById(long gamePriceId)
        {
            return GetAll().SingleOrDefault(s => s.GamePriceId == gamePriceId);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public List<GamePrice> GetAllGamePrices()
        {
            return GetAll().ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(GamePrice entity)
        {
            if (entity.GamePriceId == 0)
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
        public Boolean Remove(GamePrice entity)
        {
            return Delete(entity);
        }
    }
}
