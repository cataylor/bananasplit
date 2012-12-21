using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class StateRepository : BaseRepository<State>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public State GetById(int id)
        {
            return GetAll().SingleOrDefault(s => s.StateId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public List<State> GetAllStates()
        {
            return GetAll().ToList();
        }
    }
}
