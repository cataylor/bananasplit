using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class TicketPriceRepository : BaseRepository<TicketPrice>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public TicketPrice GetById(long id)
        {
            return GetAll().SingleOrDefault(t => t.TicketPriceId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public List<TicketPrice> GetAllTicketPricesByManagingMemberId(long id)
        {
            IQueryable<TicketPrice> allTicketPrices = null;
            try
            {
                allTicketPrices = GetAll().Where(mm => mm.ManagingMemberId == id);
            }catch(Exception e)
            {
                //Do nothing
            }
            return (null != allTicketPrices && allTicketPrices.Any()) ? allTicketPrices.ToList() : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Boolean Remove(TicketPrice entity)
        {
            return Delete(entity);
        }
    }
}
