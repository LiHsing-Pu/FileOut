using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Web;

namespace MPB.MTNET.EL.Web.Helpers
{
    public class HeapInspectionHelper
    {
        public HeapInspectionHelper()
        {
        }
        public static string getPwdSecurity(string vstrPassword)
        {
            SecureString result = new SecureString();
            foreach (char c in string.Format(vstrPassword, vstrPassword))
            {
                result.AppendChar(c);
            }
            return SecureStringToString(result);
        }
        private static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}