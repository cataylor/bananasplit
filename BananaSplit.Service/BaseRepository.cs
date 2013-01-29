using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BananaSplit.Core.Utility;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// 
    public class BaseRepository<TEntity>
        where TEntity : class
    {
        private ObjectContext context;
        private readonly ObjectSet<TEntity> objectSet; /* Represents the current entity */

        /// <summary>
        /// CTOR
        /// </summary>
        /// 
        public BaseRepository()
        {
            this.context = new BananaSplitEntityModels();
            objectSet = context.CreateObjectSet<TEntity>();
        }

        //public BaseRepository(string connectionString)
        //{
        //    context = this.ResolveContext(connectionString);
        //    objectSet = context.CreateObjectSet<TEntity>();
        //}

        
        //private ObjectContext ResolveContext(string connString = null)
        //{
        //    var t = typeof(TObjContext);
        //    ObjectContext ctx = null;

        //    var eConn = this.GetConnectionString(connString);

        //    /*
        //    if (t == typeof(CoreConnection))
        //    {
        //        ctx = new CoreConnection(eConn);
        //    }
        //    else if (t == typeof(LogsConnection))
        //    {
        //        ctx = new LogsConnection(eConn);
        //    }
        //    */

        //    return ctx;
        //}
    

        /*
        private EntityConnection GetConnectionString(string connString = null)
        {
            var t = typeof(TObjContext);
            var connectionString = (String.IsNullOrEmpty(connString)) ? Config.GetConnectionString(t.Name) : connString;
            var cb = new EntityConnectionStringBuilder(connectionString);
            var conn = new EntityConnection(cb.ToString());
            return conn;
        }

        
        protected CoreConnection CoreContext
        {
            get { return context as CoreConnection; }
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public IQueryable<TEntity> GetAll()
        {
            return objectSet;
        }


        public IEnumerable<TEntity> GetAllPaging(IOrderedQueryable<TEntity> orderByQuery, int pageNum = 1, int numOfRecordsPerPage = 25)
        {
            /*
            Example Call to this method:
             * var repo = new WhateverRepository();
             * var orderedQuery = repo.GetAll().OrderBy(u => u.VenueId);
             * repo.GetAllPaging(orderedQuery, 1, 25);
            */
            return orderByQuery.Skip((pageNum - 1) * numOfRecordsPerPage).Take(numOfRecordsPerPage);
        }


        /// <summary>
        /// Finds a record with the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A collection containing the results of the query</returns>
        /// 
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return objectSet.Where<TEntity>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Add(TEntity entity)
        {
            if (entity == null)
            {
                var entityName = typeof(TEntity).Name;
                throw new ArgumentNullException(entityName);
            }
            objectSet.AddObject(entity);
            return (context.SaveChanges() > 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// 
        public Boolean Update(TEntity entity)
        {
            if (entity == null)
            {
                var entityName = typeof(TEntity).Name;
                throw new ArgumentNullException(entityName);
            }

            return (context.SaveChanges() > 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Delete(TEntity entity)
        {
            if (entity == null)
            {
                var entityName = typeof(TEntity).Name;
                throw new ArgumentNullException(entityName);
            }
            objectSet.DeleteObject(entity);
            return (context.SaveChanges() > 0);
        }


        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        /// 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether or not to dispose managed resources</param>
        /// 
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
