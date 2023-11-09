using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHA_TOOL.Classes
{
    
   class Global_Data
    {
        // Declare a public static variable that can hold your global data
        public static string dashboard_selectedItems { get; set; }

        public static string innerboxqty { get; set; }

        public static string outterboxqty { get; set; }

       public static string statecode { get; set; }

        public static string image_file_ext { get; set; }

        public static string input_file_ext { get; set; }
         public static string input_file_seprator { get; set; }
         public static string state_name { get; set; }
         public static string validation_check { get; set; }
    }
}
