using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BananaSplit.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class ImagePaths
    {
        public ImagePaths(string webAddress, string sqlAddress, string filePath)
        {
            this.WebAddress = webAddress;
            this.SqlAddress = sqlAddress;
            this.FilePath = filePath;
        }

        public String WebAddress
        {
            get;
            private set;
        }

        public String SqlAddress
        {
            get;
            private set;
        }

        public String FilePath
        {
            get;
            private set;
        }
    }
}