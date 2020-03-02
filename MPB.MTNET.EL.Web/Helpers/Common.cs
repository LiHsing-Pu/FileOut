using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace MPB.MTNET.EL.Web.Helpers
{
    public static class Common
    {
        public static int HtmlPage = 3;

		/// <summary>2018/10/11 L.A. Encryption_key 更名為 EncryptiKoeny (For 弱掃修補用)</summary>
		public static string EncryptiKoeny = "pOCXr3fx";
        public static string Encryption_iv = "n3e0GjsJ";
		
        /*加密解密工具*/
        public class EncryptionTool
        {
            /*將字串加密*/
            /// <summary>
            /// 將字串加密
            /// </summary>
            /// <param name="str">須加密之字串</param>
            /// <returns></returns>
            /// <info>Author: Kevin; Date: 2016/1/25 </info>
            /// <history>
            /// xx.  YYYY/MM/DD   Ver   Author      Comments
            /// ===  ==========  ====  ==========  ==========
            /// 01.  2016/1/25    1.00  Kevin  Created
            /// </history>
            public static string Encryption(string str)
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = Encoding.ASCII.GetBytes(EncryptiKoeny);
                des.IV = Encoding.ASCII.GetBytes(Encryption_iv);
                byte[] s = Encoding.UTF8.GetBytes(str);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                string sec_str = BitConverter.ToString(desencrypt.TransformFinalBlock(s, 0, s.Length)).Replace("-", string.Empty);
                return sec_str;
            }
            /*將字串解密*/
            /// <summary>
            /// 將字串解密
            /// </summary>
            /// <param name="str">須解密之字串</param>
            /// <returns></returns>
            /// <info>Author: Kevin; Date: 2016/1/25 </info>
            /// <history>
            /// xx.  YYYY/MM/DD   Ver   Author      Comments
            /// ===  ==========  ====  ==========  ==========
            /// 01.  2016/1/25    1.00  Kevin  Created
            /// </history>
            public static string Decryption(string str)
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = Encoding.ASCII.GetBytes(EncryptiKoeny);
                des.IV = Encoding.ASCII.GetBytes(Encryption_iv);
                byte[] s = new byte[str.Length / 2];
                int j = 0;
                for (int i = 0; i < str.Length / 2; i++)
                {
                    s[i] = Byte.Parse(str[j].ToString() + str[j + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    j += 2;
                }
                ICryptoTransform desencrypt = des.CreateDecryptor();
                string desec_str = Encoding.UTF8.GetString(desencrypt.TransformFinalBlock(s, 0, s.Length));
                return desec_str;
            }
        }
        //public string getname(Type b, string a)
        //{
        //    //Object theObject = Activator.CreateInstance(b);
        //    //MemberInfo property = b.GetProperty(a);
        //    //DisplayAttribute dd = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
        //    string name = "";
        //    //if (dd != null)
        //    //{
        //    //    name = dd.Name;
        //    //}
        //    return name;
        //}


        /*Xlsx轉檔工具*/
        public class XlsxTransformer
        {
            public byte[] XlsxToOds(string tempFilePath)
            {
                try
                {
                    byte[] result = null;

                    string odtfilepath = tempFilePath.Split('.')[0] + ".ods";
                    string XlsxToOdsTransformerToolPath = System.Web.Configuration.WebConfigurationManager.AppSettings["XlsxToOdsTransformerToolPath"].ToString();

                    string batname = Guid.NewGuid().ToString() + ".bat";
                    //if (!File.Exists(XlsxToOdsTransformerToolPath + batname))
                    //{
                    //    File.Create(XlsxToOdsTransformerToolPath + batname);
                    //}


                    File.WriteAllText(XlsxToOdsTransformerToolPath + batname, XlsxToOdsTransformerToolPath.Split(':')[0] + ":\r\ncd " + XlsxToOdsTransformerToolPath + "CommandLineTool\\\r\n\r\nOdfConverter /I " + tempFilePath + " /O " + odtfilepath + "\r\n", Encoding.Default);

                    if (File.Exists(tempFilePath))
                    {
                        System.Diagnostics.Process proc = null;
                        string targetDir = string.Format(XlsxToOdsTransformerToolPath);//this is where mybatch.bat lies
                        proc = new System.Diagnostics.Process();
                        proc.StartInfo.WorkingDirectory = targetDir;
                        proc.StartInfo.FileName = batname;
                        proc.StartInfo.CreateNoWindow = false;
                        proc.Start();
                        proc.WaitForExit();

                        if (File.Exists(odtfilepath))
                        {
                            //byte[] fileContent = File.ReadAllBytes(odtfilepath);
                            //File.Delete(odtfilepath);
                            //return fileContent;
                            result = File.ReadAllBytes(odtfilepath);
                            File.Delete(odtfilepath);
                            File.Delete(XlsxToOdsTransformerToolPath + batname);
                            File.Delete(tempFilePath);
                            return result;
                        }
                        else
                        {
                            //return new byte[] { }; 
                            return result;
                        }
                    }
                    else
                    {
                        //goto wait;
                        return result;
                    }





                    //if (File.Exists(XlsxToOdsTransformerToolPath + "1.bat"))
                    //{
                    //    File.WriteAllText(XlsxToOdsTransformerToolPath + "1.bat", XlsxToOdsTransformerToolPath.Split(':')[0] + ":\r\ncd " + XlsxToOdsTransformerToolPath + "CommandLineTool\\\r\n\r\nOdfConverter /I " + tempFilePath + " /O " + odtfilepath + "\r\n", Encoding.Default);
                    ////wait:
                    //    if (File.Exists(tempFilePath))
                    //    {

                    //        System.Diagnostics.Process proc = null;
                    //        string targetDir = string.Format(XlsxToOdsTransformerToolPath);//this is where mybatch.bat lies
                    //        proc = new System.Diagnostics.Process();
                    //        proc.StartInfo.WorkingDirectory = targetDir;
                    //        proc.StartInfo.FileName = "1.bat";
                    //        proc.StartInfo.CreateNoWindow = false;
                    //        proc.Start();
                    //        proc.WaitForExit();

                    //        if (File.Exists(odtfilepath))
                    //        {
                    //            //byte[] fileContent = File.ReadAllBytes(odtfilepath);
                    //            //File.Delete(odtfilepath);
                    //            //return fileContent;
                    //            result = File.ReadAllBytes(odtfilepath);
                    //            File.Delete(odtfilepath);
                    //            return result;
                    //        }
                    //        else
                    //        {
                    //            //return new byte[] { }; 
                    //            return result;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //goto wait;
                    //        return result;
                    //    }
                    //}
                    //else
                    //{
                    //    //return new byte[] { }; 
                    //    return result;
                    //}

                    //return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /*Docx轉檔工具*/
        public class DocxTransformer
        {
            public string DocxToDocx(string tempFilePath)
            {
                try
                {
                    //新檔案的路徑
                    string result = "";
                    Application WordApp = new Application();
                    Document doc = WordApp.Documents.Open(tempFilePath);
                    string docxfilepath = tempFilePath.Split('.')[0] + DateTime.Now.ToString("yyyyMMddHHmmss") + ".docx";
                    try
                    {
                        doc.SaveAs2(docxfilepath, WdSaveFormat.wdFormatDocumentDefault);
                        WordApp.Visible = false;
                        WordApp.Quit();

                        if (File.Exists(docxfilepath))
                        {
                            result = docxfilepath;
                            return result;
                        }
                        else
                        {
                            WordApp.Quit();
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        WordApp.Quit();
                        throw ex;
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public byte[] DocxToOdt(string tempFilePath)
            {
                try
                {
                    byte[] result = null;
                    Application WordApp = new Application();
                    Document doc = WordApp.Documents.Open(tempFilePath);
                    //string odtfilepath = tempFilePath.Split('.')[0] + ".odt";
                    string odtfilepath = tempFilePath.Split('.')[0] + DateTime.Now.ToString("yyyyMMddHHmmss") + ".odt";
                    try
                    {
                        doc.SaveAs2(odtfilepath, WdSaveFormat.wdFormatOpenDocumentText);
                        //doc.SaveAs2(odtfilepath, WdSaveFormat.wdFormatDocumentDefault);
                        //WordApp.Visible = true;
                        //doc.SaveAs2(odtfilepath);
                        WordApp.Visible = false;
                        WordApp.Quit();
                        System.Threading.Thread.Sleep(100);
                        if (File.Exists(odtfilepath))
                        {
                            result = File.ReadAllBytes(odtfilepath);
                            File.Delete(odtfilepath);
                            File.Delete(tempFilePath);
                            return result;
                        }
                        else
                        {
                            WordApp.Quit();
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        WordApp.Quit();
                        throw ex;
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}