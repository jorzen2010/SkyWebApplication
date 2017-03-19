using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Web;

namespace Common
{
    public class ImageTools
    {
        //{"x":30.003846153846148,"y":16.715384615384625,"height":300.8,"width":300.8,"rotate":0}
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public int rotate { get; set; }

        public int ToInt(double doubleValue)
        {
            return Convert.ToInt32(doubleValue);
        }

      
    }
}
