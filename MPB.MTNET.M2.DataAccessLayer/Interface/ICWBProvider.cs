using MPB.MTNET.EL.Parameter;
using System.Collections.Generic;
using MPB.MTNET.EL.Parameter.ResultModel;

namespace MPB.MTNET.M2.DataAccessLayer.Interface
{

    public interface ICWBProvider
    {

        int DeleteData();

        int CreateNewData(EL010101ResultModel Item);

    }
}
