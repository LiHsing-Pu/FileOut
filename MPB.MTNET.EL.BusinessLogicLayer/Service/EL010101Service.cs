using System;
using System.Collections.Generic;
using System.Linq;
using ADOUtility;
using ADOUtility.Interface;
using MPB.MTNET.EL.Parameter.ViewModel;
using MPB.MTNET.EL.Parameter.ResultModel;
using MPB.MTNET.EL.Parameter;
using MPB.MTNET.EL.BusinessLogicLayer.Interface;
using System.Web.Mvc;
using MPB.MTNET.M2.DataAccessLayer.Interface;
//using MPB.MTNET.EL.Parameter.CodeModel;
//using MPB.MTNET.M2.Parameter;
using MPB.MTNET.MICS.MES.MDM;
using MPB.MTNET.Library;

using MPB.MTNET.MICS.MES.Core;
using MPB.MTNET.MICS.MES.SSO;
using System.Configuration;

using System.Net.Http;
using System.Net.Http.Headers;

using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace MPB.MTNET.EL.BusinessLogicLayer.Service
{
    public class EL010101Service : ADOHelper, IEL010101Service
    {
        #region 宣告底層連線

        public ICWBProvider CWBProvider { get; set; }

        #endregion


        public EL010101Service()
        {

        }


        public EL010101ViewModel GetViewCWB01Table(EL010101ViewModel model)
        {
            EL010101ViewModel dataViewModel = new EL010101ViewModel();
            List<EL010101ResultModel> CWB01ResultModel = new List<EL010101ResultModel>();
            dataViewModel.database_action = DataBaseAction.Read;
            Rootobject out1;

            CWBProvider = (ICWBProvider)CreateProvider("MPB.MTNET.M2.DataAccessLayer", "CWBProvider");
            int k = CWBProvider.DeleteData();

            try
            {
                out1 = useHttpWebRequest_Get();

                List<Weatherelement> wea = out1.records.location[0].weatherElement.ToList();

                foreach (Weatherelement a in wea)
                {
                    string elementName = a.elementName;

                    for (int i = 0; i<3; i++)
                    {
                        string parameterName = a.time[i].parameter.parameterName;
                        string parameterValue = a.time[i].parameter.parameterValue;
                        string parameterUnit = a.time[i].parameter.parameterUnit;
                        DateTime sTime = Convert.ToDateTime(a.time[i].startTime);
                        DateTime eTime = Convert.ToDateTime(a.time[i].endTime);

                        EL010101ResultModel ResultModel = new EL010101ResultModel();
                        ResultModel.locationName = "台北市";
                        ResultModel.elementName = elementName;
                        ResultModel.parameterName = parameterName;
                        ResultModel.parameterUnit = parameterUnit;
                        ResultModel.parameterValue = parameterValue;
                        ResultModel.startTime = sTime;
                        ResultModel.endTime = eTime;

                        int r = CWBProvider.CreateNewData(ResultModel);

                        CWB01ResultModel.Add(ResultModel);

                    }
                }



                dataViewModel.resultModel = CWB01ResultModel;
                dataViewModel.result = true;
                dataViewModel.error = false;
                dataViewModel.messager = "成功";
            }
            catch (Exception ex)
            {
                dataViewModel.result = false;
                dataViewModel.error = true;
                dataViewModel.exception = ex;
                dataViewModel.messager = "執行錯誤";
            }
            return dataViewModel;
        }

        public class Rootobject
        {
            public string success { get; set; }
            public Result result { get; set; }
            public Records records { get; set; }
        }

        public class Result
        {
            public string resource_id { get; set; }
            public Field[] fields { get; set; }
        }

        public class Field
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class Records
        {
            public string datasetDescription { get; set; }
            public Location[] location { get; set; }
        }

        public class Location
        {
            public string locationName { get; set; }
            public Weatherelement[] weatherElement { get; set; }
        }

        public class Weatherelement
        {
            public string elementName { get; set; }
            public Time[] time { get; set; }
        }

        public class Time
        {
            public string startTime { get; set; }
            public string endTime { get; set; }
            public Parameter parameter { get; set; }
        }

        public class Parameter
        {
            public string parameterName { get; set; }
            public string parameterValue { get; set; }
            public string parameterUnit { get; set; }
        }


        private Rootobject useHttpWebRequest_Get()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=CWB-16DA2DC6-542A-46C1-9702-0207DE95BC3D&locationName=%E8%87%BA%E5%8C%97%E5%B8%82");
            request.Method = WebRequestMethods.Http.Get;
            //request.ContentType = "application/xml";
            Rootobject a = null;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        var temp = reader.ReadToEnd();
                        a = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(temp);

                    }
                }
                else
                {
                    //this.dataGridView1.DataSource = null;
                }
            }
            return a;

        }

    }
}
