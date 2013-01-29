using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BananaSplit.Helpers
{
    public class ImageDimensions
    {
        public ImageDimensions(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public Int32 Width { get; private set; }
        public Int32 Height { get; private set; }
    }
}