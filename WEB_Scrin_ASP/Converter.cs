using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_Scrin_ASP
{
    public static class Converter
    {
        public static Int32 IfNullThenZero(this object value)
        {
            if (value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }
    }
}