using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class PartnerTicketRepository : BaseRepository<PartnerTicket>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public PartnerTicket GetById(long id)
        {
            return GetAll().SingleOrDefault(pt => pt.PartnerTicketId == id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public List<PartnerTicket> GetAllTicketsByMemberId(long id)
        {
            IQueryable<PartnerTicket> allPartnerTickets = null;
            try
            {
                allPartnerTickets = GetAll().Where(mm => mm.MemberId == id);
            }
            catch (Exception e)
            {
                //Do nothing
            }
            return (null != allPartnerTickets && allPartnerTickets.Any()) ? allPartnerTickets.ToList() : null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Boolean Remove(PartnerTicket entity)
        {
            return Delete(entity);
        }

        

    }
}
