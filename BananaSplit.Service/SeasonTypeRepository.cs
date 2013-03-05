using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class SeasonTypeRepository : BaseRepository<SeasonType>
    {
        public List<SeasonType> GetAllSeasonTypes()
        {
            return GetAll().ToList();
        }
    }
}
