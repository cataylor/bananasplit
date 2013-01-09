using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BananaSplit.Data.Models
{
    public class ApiResultCollection<T> where T : class //where C: class
    {
        public Boolean Success { get; set; }

        public String Description { get; set; }

        public virtual List<T> ApiCollection { get; set; }
    }
}
