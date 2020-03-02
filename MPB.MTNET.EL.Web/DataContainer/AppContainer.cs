using MPB.MTNET.MICS.MES.SSO;
using System;
using System.Collections.Generic;

namespace MPB.MTNET.EL.Web.DataContainer
{
    public class AppContainer
    {
        public static Dictionary<string, List<AccAppRes>> usersApp = new Dictionary<string, List<AccAppRes>>();
        public AppContainer()
        {

        }
        public static void Register(string accId, List<AccAppRes> funList)
        {
            if (!usersApp.ContainsKey(accId))
            {
                usersApp.Add(accId, funList);
            }
            else
            {
                //throw new ArgumentNullException();
            }
        }

        public static void Logoff(string accId)
        {

            if (!usersApp.ContainsKey(accId))
            {
                //throw new ArgumentNullException();
            }
            else
            {
                usersApp.Remove(accId);
            }
        }
    }
}