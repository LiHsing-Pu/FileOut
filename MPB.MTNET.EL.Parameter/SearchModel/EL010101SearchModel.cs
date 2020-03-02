using MPB.MTNET.EL.Parameter.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPB.MTNET.EL.Parameter.SearchModel
{
    public class EL010101SearchModel
    {
        [StringLength(5)]
        [Display(Name = "資料格式")]
        public string stringtype { get; set; }


    }
}

