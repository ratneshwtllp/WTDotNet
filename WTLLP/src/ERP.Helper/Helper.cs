using System;

namespace ERP.Helper
{
    public class Helper
    {
        public static string Create_Session()
        {
            string FinYR = "";
            if (DateTime.Now.Date.Month < 4)
                FinYR = Convert.ToString(Convert.ToInt16(DateTime.Now.Date.Year) - 1) + "-" + DateTime.Now.Date.Year.ToString().Substring(2, 2); //Convert.ToString(DateTime.Now.Date.Year);
            else
                FinYR = Convert.ToString(DateTime.Now.Date.Year) + "-" + (Convert.ToInt16(DateTime.Now.Date.Year) + 1).ToString().Substring(2, 2); //Convert.ToString(Convert.ToInt16(DateTime.Now.Date.Year) + 1);
            return FinYR;
        }

        public static string Create_Sort_Session()
        {
            string FinYR = "";
            if (DateTime.Now.Date.Month < 4)
                FinYR = (Convert.ToInt16(DateTime.Now.Date.Year) - 1).ToString().Substring(2, 2) + "-" + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            else
                FinYR = DateTime.Now.Date.Year.ToString().Substring(2, 2) + "-" + (Convert.ToInt16(DateTime.Now.Date.Year) + 1).ToString().Substring(2, 2);
            return FinYR;
        }

        public static string Create_Session_FromDate(DateTime dt)
        {
            string FinYR = "";
            if (dt.Month < 4)
                FinYR = Convert.ToString(Convert.ToInt16(dt.Year) - 1) + "-" + dt.Year.ToString().Substring(2, 2);
            else
                FinYR = Convert.ToString(dt.Year) + "-" + (Convert.ToInt16(dt.Year) + 1).ToString().Substring(2, 2);
            return FinYR;
        }

        public static string Create_Sort_Session_FromDate(DateTime dt)
        {
            string FinYR = "";
            if (dt.Month < 4)
                FinYR = (Convert.ToInt16(dt.Year) - 1).ToString().Substring(2, 2) + "-" + dt.Year.ToString().Substring(2, 2);
            else
                FinYR = dt.Year.ToString().Substring(2, 2) + "-" + (Convert.ToInt16(dt.Year) + 1).ToString().Substring(2, 2);
            return FinYR;
        }
    }
}
