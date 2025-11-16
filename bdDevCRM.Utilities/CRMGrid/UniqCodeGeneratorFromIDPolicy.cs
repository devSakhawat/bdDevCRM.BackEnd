//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Azolution.Common.DataService;
//using Azolution.Common.EntityBase;

//namespace Utilities
//{
//    public class UniqCodeGeneratorFromIdPolicy
//    {

//        private const string UPDATE_IDPOLICY_WITH_STNUMBER =
//           "Update IDPolicy set StartNumber = {0}, LastNumber = {1},YearName={2},MonthName={3},DateName={4} where EntityName = '{5}'";

//        private const string UPDATE_IDPOLICY = "Update IDPolicy set LastNumber = {0},YearName={1},MonthName={2},DateName={3} where EntityName = '{4}'";



//        public string GenerateEntityId(string prefix, string suffix, bool useSeperator, CommonConnection connection, string enitityName, int transectionResetType, DateTime vDate = new DateTime())
//        {
//            if (vDate == DateTime.MinValue)
//            {
//                vDate = DateTime.Now;
//            }
//            //transectionResetType = 1 then Yearly, 2 then Monthly, 3 then Daily
//            string idCode = "";
//            var idPolicy = GetIdPolicy(connection, enitityName);
//            if (prefix == "")
//            {
//                prefix = idPolicy.Prefix.Trim();
//            }
//            if (suffix == "")
//            {
//                suffix = idPolicy.Suffix == null ? "" : idPolicy.Suffix.Trim();
//            }
//            if (idPolicy.LastNumber < 0)
//            {
//                idPolicy.LastNumber = 0;
//            }
//            if (transectionResetType == 1)
//            {
//                if (idPolicy.YearName == vDate.Year)
//                {
//                    //idPolicy.LastNumber = 0;
//                }
//                else
//                {
//                    idPolicy.LastNumber = 0;
//                }
//            }
//            else if (transectionResetType == 2)
//            {
//                if (idPolicy.YearName == vDate.Year && idPolicy.MonthName == vDate.Month)
//                {
//                    //idPolicy.LastNumber = 0;
//                }
//                else
//                {
//                    idPolicy.LastNumber = 0;
//                }
//            }
//            else
//            {
//                if (idPolicy.YearName == vDate.Year && idPolicy.MonthName == vDate.Month &&
//                    idPolicy.DateName == vDate.Day)
//                {
//                    //idPolicy.LastNumber = 0;
//                }
//                else
//                {
//                    idPolicy.LastNumber = 0;
//                }
//            }
//            string newNumber = GenerateIDNumber(idPolicy.StartNumber, idPolicy.NumberDigit, idPolicy.LastNumber, enitityName, connection, vDate);
//            if (useSeperator == false)
//            {
//                idCode = prefix + newNumber + suffix;
//            }
//            else
//            {
//                idCode = prefix + "-" + newNumber + "-" + suffix;
//            }

//            return idCode.Trim();
//        }

//        private IDPolicy GetIdPolicy(CommonConnection connection, string enitityName)
//        {
//            string quary = string.Format("Select * from IDPolicy where EntityName = '{0}'", enitityName);

//            return connection.Data<IDPolicy>(quary).FirstOrDefault();

//        }
//        public string GetVirtualId(string prefix, string suffix, bool useSeperator, CommonConnection connection, string enitityName, int transectionResetType, DateTime vDate)
//        {

//            var idPolicy = GetIdPolicy(connection, enitityName);
//            string idCode = "";


//            if (prefix == "")
//            {
//                prefix = idPolicy.Prefix.Trim();
//            }
//            if (suffix == "")
//            {
//                suffix = idPolicy.Suffix == null ? "" : idPolicy.Suffix.Trim();
//            }
//            if (idPolicy.LastNumber < 0)
//            {
//                idPolicy.LastNumber = 0;
//            }
//            if (transectionResetType == 1)
//            {
//                if (idPolicy.YearName == vDate.Year)
//                {
//                    //idPolicy.LastNumber = 0;
//                }
//                else
//                {
//                    idPolicy.LastNumber = 0;
//                }
//            }
//            else if (transectionResetType == 2)
//            {
//                if (idPolicy.YearName == vDate.Year && idPolicy.MonthName == vDate.Month)
//                {
//                    //idPolicy.LastNumber = 0;
//                }
//                else
//                {
//                    idPolicy.LastNumber = 0;
//                }
//            }
//            else
//            {
//                if (idPolicy.YearName == vDate.Year && idPolicy.MonthName == vDate.Month &&
//                    idPolicy.DateName == vDate.Day)
//                {
//                    //idPolicy.LastNumber = 0;
//                }
//                else
//                {
//                    idPolicy.LastNumber = 0;
//                }
//            }
//            string newNumber = GenerateVirtualIDNumber(idPolicy.StartNumber, idPolicy.NumberDigit, idPolicy.LastNumber, enitityName);
//            if (useSeperator == false)
//            {
//                idCode = prefix + newNumber + suffix;
//            }
//            else
//            {
//                idCode = prefix + "-" + newNumber + "-" + suffix;
//            }

//            return idCode.Trim();

//        }

//        private string GenerateIDNumber(int startNumber, int digit, int lastNumber, string entityName, CommonConnection connection, DateTime vDate)
//        {
//            string num = "";
//            var length = (lastNumber + 1).ToString().Length;
//            var diff = digit - length;
//            if (diff < 0)
//            {
//                try
//                {
//                    num = "000001";
//                    startNumber = startNumber + 1;
//                    lastNumber = 1;

//                    string quary = string.Format(UPDATE_IDPOLICY_WITH_STNUMBER, startNumber, lastNumber, vDate.Year, vDate.Month, vDate.Day, entityName);
//                    connection.ExecuteNonQuery(quary);
//                }
//                catch
//                {
//                    connection.RollBack();
//                }
//            }
//            else
//            {
//                for (int i = 0; i < diff; i++)
//                {
//                    num += "0";
//                }
//                num = num + (lastNumber + 1);
//                try
//                {
//                    lastNumber = lastNumber + 1;
//                    string quary = string.Format(UPDATE_IDPOLICY, lastNumber, vDate.Year, vDate.Month, vDate.Day, entityName);
//                    connection.ExecuteNonQuery(quary);
//                }
//                catch
//                {
//                    connection.RollBack();
//                }


//            }
//            return num;

//        }

//        private static string GenerateVirtualIDNumber(int startNumber, int digit, int lastNumber, string entityName)
//        {
//            string num = "";
//            var length = (lastNumber + 1).ToString().Length;
//            var diff = digit - length;
//            if (diff < 0)
//            {

//                num = "000001";
//                startNumber = startNumber + 1;
//                lastNumber = 1;

//            }
//            else
//            {
//                for (int i = 0; i < diff; i++)
//                {
//                    num += "0";
//                }
//                num = num + (lastNumber + 1);

//                lastNumber = lastNumber + 1;




//            }
//            return num;

//        }

//        public static string GetVoucherNo(int transectionType, DateTime vDate, bool isVirtual)
//        {

//            // var idPolicy = new UniqCodeGeneratorFromIdPolicy();
//            // CommonConnection connection = new CommonConnection();




//            string voucherNo = "";
//            string voucherType = "";
//            string prifix = "";
//            try
//            {


//                switch (transectionType)
//                {
//                    case 1:
//                        voucherType = "VoucherBP";
//                        prifix = "BP";
//                        break;
//                    case 2:
//                        voucherType = "VoucherCP";
//                        prifix = "CP";
//                        break;
//                    case 3:
//                        voucherType = "VoucherBR";
//                        prifix = "BR";
//                        break;
//                    case 4:
//                        voucherType = "VoucherCR";
//                        prifix = "CR";
//                        break;
//                    case 5:
//                        voucherType = "VoucherJV";
//                        prifix = "JV";
//                        break;
//                    case 6:
//                        voucherType = "VoucherLP";
//                        prifix = "LP";
//                        break;
//                }

//                int lastNumber = GetVoucherLastNo(prifix, vDate);
//                string newNumber = GenerateVirtualIDNumber(1, 6, lastNumber, "");
//              //  voucherNo = newNumber;

//                //   voucherNo = GenerateVirtualIDNumber(1, 6, lastNumber, "");
//                // voucherNo = isVirtual ? idPolicy.GetVirtualId("", "", false, connection, voucherType, 2, vDate) : idPolicy.GenerateEntityId("", "", false, connection, voucherType, 2, vDate);
//                voucherNo = prifix + "-" + vDate.ToString("yyyyMM") + "-" + newNumber;
//            }
//            catch (Exception)
//            {

//                voucherNo = "";
//            }
//            finally
//            {

//                //  connection.Close();
//            }
//            return voucherNo;

//        }

//        private static int GetVoucherLastNo(string prifix, DateTime vDate)
//        {
//            string sql = string.Format(@"Select  isnull(MAX(Number),0)Number from (
//            Select VoucharNo, substring(VoucharNo,1,2)Prefix,
//            substring(VoucharNo,4,6)MonthYear,
//            Convert(int,substring(VoucharNo,11,6))Number ,
//            substring(VoucharNo,11,6)IncNumber,
//            convert(date,substring(VoucharNo,4,4)+'-'+substring(VoucharNo,8,2)+'-01')vDate
//            from VoucharMaster) T where vDate='{1}'
//            and Prefix='{0}' ", prifix, vDate.ToString("yyyy-MM-01"));
//            var conn = new CommonConnection();
//            var lNo = conn.GetScaler(sql);
//            conn.Close();
//            return lNo;

//        }
//    }
//}
