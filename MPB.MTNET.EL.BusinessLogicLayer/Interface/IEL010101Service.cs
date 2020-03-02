using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MPB.MTNET.EL.Parameter.ViewModel;

namespace MPB.MTNET.EL.BusinessLogicLayer.Interface
{
    public interface IEL010101Service
    {

        EL010101ViewModel GetViewCWB01Table(EL010101ViewModel model);

 
    }
}
