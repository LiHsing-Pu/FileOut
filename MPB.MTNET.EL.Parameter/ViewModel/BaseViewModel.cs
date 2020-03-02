using System;


namespace MPB.MTNET.EL.Parameter.ViewModel
{
    public class BaseViewModel
    {
        public Exception exception { set; get; }
        public bool? result { set; get; }
        public bool? error { set; get; }
        public string messager { get; set; }
        public string query_parm { get; set; }
    
        public DataBaseAction database_action { get; set; }
    }
    public enum DataBaseAction
    {
        Create,
        Read,
        Update,
        Delete
    }
}
