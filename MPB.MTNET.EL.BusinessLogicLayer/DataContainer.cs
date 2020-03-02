using MPB.MTNET.Library;
using MPB.MTNET.MICS.MES.SSO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPB.MTNET.EL.BusinessLogicLayer
{
    public class DataContainer
    {

        private static List<QueryAccRes> accList { get; set; }
        private static DateTime? date { get; set; }
        private static int SSOCacheTimeout = int.Parse(ConfigurationManager.AppSettings["SSOCacheTimeout"].ToString());

        public static List<DeptDataRes> deptList = new List<DeptDataRes>();
        public static string GetAccCname(string AccCd)
        {
            string result = string.Empty;
            try
            {
                CheckTimeExpired();

                QueryAccRes acc = accList.FirstOrDefault(o => o.AccCd == AccCd);

                if (acc != null)
                {
                    result = acc.AccCname;
                }
                else
                {
                    result = "系統管理者";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                result = "系統管理者";
            }

            return result;
        }
        public static string GetDeptCName(string DeptCd)
        {
            string result = string.Empty;

            DeptDataRes dept = deptList.FirstOrDefault(o => o.DeptCd == DeptCd);

            if (dept != null)
            {
                result = dept.DeptCname;
            }
            else
            {
                result = "航港局";
            }
            return result;
        }


        private static void CheckTimeExpired()
        {
            if (date == null)
            {
                date = DateTime.Now;
            }
            else
            {
                if (date.Value.AddMinutes(SSOCacheTimeout) > DateTime.Now)
                {
                    Post();
                }
            }
        }

        private static void Post()
        {
            QueryAccListRes query = SSOService.POST<QueryAccListReq, QueryAccListRes>(SSOAPISysEnum.QueryAccList, new QueryAccListReq()
            {

            });
            if (query != null)
            {
                accList = query.QueryAccResList.ToList();
            }
        }
    }
}
