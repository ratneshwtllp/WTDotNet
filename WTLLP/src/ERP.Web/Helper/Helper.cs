using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ERP.Web
{
    public class Helper
    {
        public static SelectList ConvertObjectToSelectList(string list)
        {
            List<SelectListItem> masterList = new List<SelectListItem>();
            if (string.IsNullOrEmpty(list) || list.Contains("Error"))
            {
                return new SelectList(masterList, "Value", "Text", "id");
            }            
            dynamic result = JsonConvert.DeserializeObject(list, typeof(object));
            foreach (var item in result)
            {
                masterList.Add(new SelectListItem { Text = ((JValue)((((JObject)item)).Last.First)).Value.ToString(), Value = ((JValue)((((JObject)item)).First.First)).Value.ToString(), Selected = true });
            }
            return new SelectList(masterList, "Value", "Text", "id"); 
        }

        public static SelectList ConvertObjectToSelectList_WithText(string list)
        {
            List<SelectListItem> masterList = new List<SelectListItem>();
            if (string.IsNullOrEmpty(list) || list.Contains("Error"))
            {
                return new SelectList(masterList, "Value", "Text", "id");
            }
            dynamic result = JsonConvert.DeserializeObject(list, typeof(object));
            foreach (var item in result)
            {
                masterList.Add(new SelectListItem { Text = ((JValue)((((JObject)item)).Last.First)).Value.ToString(), Value = ((JValue)((((JObject)item)).Last.First)).Value.ToString(), Selected = true });
            }
            return new SelectList(masterList, "Value", "Text", "id"); 
        }

        public static SelectList FillDropDownList_Empty()
        {            
            List<SelectListItem> MasterList = new List<SelectListItem>();
            return new SelectList(MasterList, "Value", "Text", "id");  
        }

        public static SelectList FillDropDownList_VelcroType()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Loop", Value = "Loop", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Hook", Value = "Hook", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id"); 
        }

        public static SelectList FillDropDownList_NickleFree()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Yes", Value = "Yes", Selected = true });
            MasterList.Add(new SelectListItem { Text = "No", Value = "No", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_Order_Sample()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Order", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Sample", Value = "2", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_Invoice_Packing()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Invoice", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Packing List", Value = "2", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 10000000) > 0)
            {
                words += ConvertNumbertoWords(number / 10000000) + " CRORE ";
                number %= 10000000;
            }
            if ((number / 100000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKHES ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                //if (words != "") words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };
                if (number < 20)
                {
                    words += unitsMap[number];
                }
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public static string ConvertDoubletoWords(string value)
        {
            string result = "";
            if (value.Contains("."))
            {
                string[] arr = value.Split('.');
                result = ConvertNumbertoWords(Convert.ToInt64(arr[0]));
                if(Convert.ToInt64(arr[1]) > 1)
                    result += " AND " + ConvertNumbertoWords(Convert.ToInt64(arr[1])) + "PAISE ";               
            }
            else
            {
                result = ConvertNumbertoWords(Convert.ToInt64(value));
            }
            return result;
        }

        public static SelectList FillDropDownList_TimeAMPM()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "AM", Value = "AM", Selected = true });
            MasterList.Add(new SelectListItem { Text = "PM", Value = "PM", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_CancelActive()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "All", Value = "2", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Active Only", Value = "0", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Cancel", Value = "1", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_DeliveryStatus()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Complete", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "In Complete", Value = "1", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

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

        public static SelectList FillDropDownList_StockType()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Damage", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Increase", Value = "2", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Decrease", Value = "3", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_YesNo()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Yes", Value = "Yes", Selected = true });
            MasterList.Add(new SelectListItem { Text = "No", Value = "No", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_YesNoWithValue()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Yes", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "No", Value = "0", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_FOBCIF()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "FOB", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "CIF", Value = "2", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_YesNoWithID()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Yes", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "No", Value = "0", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_ReqFor()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Sample", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Production", Value = "2", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Other", Value = "3", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }
        public static SelectList FillDropDownList_RangeWhole()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Range", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Whole Salary", Value = "2", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_StockFilter()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "All", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "> 0", Value = "2", Selected = true });
            MasterList.Add(new SelectListItem { Text = "= 0", Value = "3", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_WorkPlanStatus()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "All", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Pending", Value = "2", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Complete", Value = "3", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_POBalaceType() 
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "All", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Full Balance Only", Value = "2", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Partial Balance Only", Value = "3", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_InHouse_Outdoor()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "InHouse", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Outdoor", Value = "2", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_SupplierAddress()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "First Address", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Second Address", Value = "2", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_LUT()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "LUT", Value = "LUT", Selected = true });
            MasterList.Add(new SelectListItem { Text = "GST", Value = "GST", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_GRNType()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            //MasterList.Add(new SelectListItem { Text = "All", Value = "0", Selected = true });
            MasterList.Add(new SelectListItem { Text = "PO", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "JW", Value = "2", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Manual", Value = "3", Selected = true });

            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_ContractorPaymentOrderBy()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            //MasterList.Add(new SelectListItem { Text = "All", Value = "0", Selected = true });
            //MasterList.Add(new SelectListItem { Text = "Buyer", Value = "1", Selected = true });
            //MasterList.Add(new SelectListItem { Text = "OrderNo", Value = "2", Selected = true });
            //MasterList.Add(new SelectListItem { Text = "Contractor", Value = "3", Selected = true });

            //MasterList.Add(new SelectListItem { Text = "ProcessNo", Value = "4", Selected = true });
            MasterList.Add(new SelectListItem { Text = "ProcessDate", Value = "5", Selected = true });
            //MasterList.Add(new SelectListItem { Text = "Recv No.", Value = "6", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Recv Date", Value = "7", Selected = true });

            return new SelectList(MasterList, "Value", "Text", "id");
        }
        public static SelectList FillDropDownList_ActiveDeactiveWithValue()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "Active", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Deactive", Value = "2", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }
        public static SelectList FillDropDownList_MaterialForList()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "For Each Size", Value = "0", Selected = true });
            MasterList.Add(new SelectListItem { Text = "For Master Size(Size Run)", Value = "1", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }

        public static SelectList FillDropDownList_ComboType()
        {
            List<SelectListItem> MasterList = new List<SelectListItem>();
            MasterList.Add(new SelectListItem { Text = "New", Value = "1", Selected = true });
            MasterList.Add(new SelectListItem { Text = "Exiting", Value = "2", Selected = true });
            return new SelectList(MasterList, "Value", "Text", "id");
        }
    }
}
