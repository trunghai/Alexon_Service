using Alexon_Service.Helper;
using Alexon_Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Alexon_Service.ModelCtr
{
    public class UnitDataAccess : BaseDataAccess
    {
        public UnitDataAccess(string connectionString) : base(connectionString)  
        {
        }

        public Entity addUnit(String madv, String tendv, String sdt)
        {
            Entity entity = new Entity();
            Unit unit = new Unit();

            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter madvParams = base.GetParameter("@MADONVI", unit.madonvi);
            DbParameter tendvParams = base.GetParameter("@TENDONVI", unit.tendonvi);
            DbParameter sdtParams = base.GetParameter("@SDT", unit.sdt);

            parameterList.Add(madvParams);
            parameterList.Add(tendvParams);
            parameterList.Add(sdtParams);

            base.ExecuteNonQuery("ADD_DONVI", parameterList, CommandType.StoredProcedure);

            unit.madonvi = (string)madvParams.Value;
            unit.tendonvi = (string)tendvParams.Value;
            unit.sdt = (string)sdtParams.Value;

            entity.respCode = "0";
            entity.respContent = "Giao dịch thành công";
            entity.info = unit;
            return entity;
        }

    }
}
