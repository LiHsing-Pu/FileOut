using Microsoft.Security.Application;
using System;
using System.Reflection;

namespace MPB.MTNET.EL.Web.Helpers
{
    public class XSSHelper
    {
        public XSSHelper()
        {
        }
        public static string EncoderText(string Input)
        {
			string Ouput = string.Empty;
			//篩選掉html隱碼
			Ouput = Encoder.HtmlEncode(Input);
			//html -> string
			Ouput = System.Web.HttpUtility.HtmlDecode(Ouput);
			return Ouput;
		}
        public static T EncoderClass<T>(T Model)
        {
            Type obj = Model.GetType();
            PropertyInfo[] propertyInfos = obj.GetProperties();

            foreach (var item in propertyInfos)
            {
                if (item == null) continue;
				if (item.GetValue(Model)==null) continue;
				if (item.GetValue(Model).GetType() == null) continue;
				if (item.GetValue(Model).GetType().Name == "String")
                {
                    string Text = (string)item.GetValue(Model);
                    Text = EncoderText(Text);
                    item.SetValue(Model, Text, null);
                }
            }
            return Model;
        }
    }
}