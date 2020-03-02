using MPB.MTNET.MICS.MES.SSO;
using System;
using System.Collections.Generic;

namespace MPB.MTNET.EL.Web.DataContainer
{
    public class FunContainer
    {
        public static Dictionary<string, List<AccFunRes>> usersFun = new Dictionary<string, List<AccFunRes>>();
        public FunContainer()
        {

        }
        public static void Register(string accId, List<AccFunRes> funList)
        {
            if (!usersFun.ContainsKey(accId))
            {
                usersFun.Add(accId, funList);
            }
            else
            {
                //throw new ArgumentNullException();
            }
        }

        public static void Logoff(string accId)
        {

            if (!usersFun.ContainsKey(accId))
            {
                //throw new ArgumentNullException();
            }
            else
            {
                usersFun.Remove(accId);
            }
        }
    }
}