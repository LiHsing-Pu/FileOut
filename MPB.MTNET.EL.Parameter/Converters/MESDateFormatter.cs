using Newtonsoft.Json.Converters;

namespace MPB.MTNET.EL.Parameter.Converters
{
    public class MESDateFormatter:IsoDateTimeConverter
    {
        const string DateFormat = "yyyy/MM/dd";
        public MESDateFormatter()
        {
            base.DateTimeFormat = DateFormat;
        }
    }

    public class MESDateTimeFormatter: IsoDateTimeConverter
    {
        const string DateFormat = "yyyy/MM/dd HH:mm:ss";
        public MESDateTimeFormatter()
        {
            base.DateTimeFormat = DateFormat;
        }
    }
}
