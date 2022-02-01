using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Util
{
    public static class Utils
    {
        public static Double convertTimeToNum(Double number)
        {

            String[] time = number.ToString().Split(".");
            String hours = time[0];
            String minutes;
            try
            {
                minutes = time[1];
            }
            catch(IndexOutOfRangeException e)
            {
                minutes = "0";
            }
            
            if (minutes == null)
            {
                minutes = "00";
            }
            if (minutes.Length == 1)
            {
                minutes += "0";
            }
            Double m = Math.Round((Double.Parse(minutes) / 60), 2);
            m *= 100;
            minutes = Convert.ToInt32(m).ToString();
            if (minutes.Length < 2)
            {
                minutes = "0" + minutes;
            }
            return Double.Parse(hours + "." + minutes);

        }
    }
}
