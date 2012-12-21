
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class InvoiceRepository : BaseRepository<Invoice>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public Invoice GetById(long id)
        {
            return GetAll().SingleOrDefault(i => i.InvoiceId == id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        /// 
        public List<Invoice> GetAllInvoicesByMemberId(long memberId)
        {
            var invoices = GetAll().Where(i => i.MemberId == memberId);
            return (invoices.Any()) ? invoices.ToList() : null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="managingMemberId"></param>
        /// <returns></returns>
        /// 
        public List<Invoice> GetAllInvoicesByManagingMemberId(long managingMemberId)
        {
            var invoices = GetAll().Where(i => i.ManagingMemberId == managingMemberId);
            return (invoices.Any()) ? invoices.ToList() : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(Invoice entity)
        {
            if (entity.InvoiceId == 0)
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
        public Boolean Remove(Invoice entity)
        {
            entity.IsActive = false;
            return Update(entity);
        }
    }
}
