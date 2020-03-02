using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MPB.MTNET.EL.Web.Helpers
{
    public class Permissions
    {
        /*Forms表單驗證(FormsAuthenticationFun) 詳細敘述如下*/
        /// <summary>
        /// Forms表單驗證
        /// </summary>
        /// <info>Author: Wei; Date: 2015/07/02  </info>
        /// <history>
        /// xx.  YYYY/MM/DD   Ver   Author      Comments
        /// ===  ==========  ====  ==========  ==========
        /// 01.  2015/07/02  1.00    Wei        Create
        /// </history>
        public class FormsAuthenticationFun
        {
            private FormsAuthenticationTicket ticket;
            public HttpCookie Ticket(string users_id, string users_cname)
            {
                string uid = Common.EncryptionTool.Encryption(users_id);
                string ucn = Common.EncryptionTool.Encryption(users_cname);
                ticket = new FormsAuthenticationTicket(1, uid, DateTime.Now, DateTime.Now.AddMinutes(30), false, ucn, FormsAuthentication.FormsCookiePath);
                string encrypt = FormsAuthentication.Encrypt(ticket);
                return new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
            }
            public void Logout()
            {
                FormsAuthentication.SignOut();
            }
        }

    }
}