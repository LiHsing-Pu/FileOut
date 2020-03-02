using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ADOTemplate.MSSQL;
using ADOUtility.MSSQL;
using ADOUtility.Interface;
using MPB.MTNET.EL.Parameter;
using MPB.MTNET.M2.DataAccessLayer.Interface;
using MPB.MTNET.EL.Parameter.ResultModel;
namespace MPB.MTNET.M2.DataAccessLayer.Provider.MSSQL
{

    public class CWBProvider : SQLDAL, ICWBProvider
    {
        #region 底層連線
        public CWBProvider() : base(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name) { }
        public CWBProvider(SQLDataTransaction tra) : base(tra) { }
        #endregion

        public int DeleteData()
        {
            StringBuilder SbSql = new StringBuilder();
            SQLParameterCollection objParameter = new SQLParameterCollection();

            SbSql.Append("DELETE FROM [cwb01]" + Environment.NewLine);
            SbSql.Append("WHERE 1 = 1" + Environment.NewLine);
            return ExecuteNonQuery(CommandType.Text, SbSql.ToString(), objParameter);

        }

        public int CreateNewData(EL010101ResultModel Item)
        {
            StringBuilder SbSql = new StringBuilder();
            SQLParameterCollection objParameter = new SQLParameterCollection();

            SbSql.Append("INSERT INTO [cwb01]" + Environment.NewLine);
            SbSql.Append("(" + Environment.NewLine);
            SbSql.Append("         locationName" + Environment.NewLine);
            SbSql.Append("        ,elementName" + Environment.NewLine);
            SbSql.Append("        ,parameterName" + Environment.NewLine);
            SbSql.Append("        ,parameterUnit" + Environment.NewLine);
            SbSql.Append("        ,parameterValue" + Environment.NewLine);
            SbSql.Append("        ,startTime" + Environment.NewLine);
            SbSql.Append("        ,endTime" + Environment.NewLine);
            SbSql.Append(")" + Environment.NewLine);
            SbSql.Append("VALUES" + Environment.NewLine);
            SbSql.Append("(" + Environment.NewLine);
            SbSql.Append("         @locationName"); objParameter.Add("@locationName", DbType.String, Item.locationName);
            SbSql.Append("        ,@elementName"); objParameter.Add("@elementName", DbType.String, Item.elementName);
            SbSql.Append("        ,@parameterName"); objParameter.Add("@parameterName", DbType.String, Item.parameterName);
            SbSql.Append("        ,@parameterUnit"); objParameter.Add("@parameterUnit", DbType.String, Item.parameterUnit);
            SbSql.Append("        ,@parameterValue"); objParameter.Add("@parameterValue", DbType.String, Item.parameterValue);
            SbSql.Append("        ,@startTime"); objParameter.Add("@startTime", DbType.DateTime, Item.startTime);
            SbSql.Append("        ,@endTime"); objParameter.Add("@endTime", DbType.DateTime, Item.endTime);
            SbSql.Append(")" + Environment.NewLine);

            return ExecuteNonQuery(CommandType.Text, SbSql.ToString(), objParameter);
        }
    }
}
