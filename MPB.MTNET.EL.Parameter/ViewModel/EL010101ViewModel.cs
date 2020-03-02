using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MPB.MTNET.EL.Parameter.ResultModel;
using MPB.MTNET.EL.Parameter.SearchModel;
using System.Linq;

namespace MPB.MTNET.EL.Parameter.ViewModel
{
    public class EL010101ViewModel : BaseViewModel
    {
        public EL010101SearchModel searchModel { get; set; }
        public List<EL010101ResultModel> resultModel { get; set; }

    }

}
