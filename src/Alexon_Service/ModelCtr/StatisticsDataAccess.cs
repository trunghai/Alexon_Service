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
    public class StatisticsDataAccess : BaseDataAccess
    {
        public StatisticsDataAccess(string connectionString) : base(connectionString)
        {

        }

        public Entity getAllMaterial(String codeunit, String startDate, String endDate )
        {
            Entity entity = new Entity();
            List<Material> listMaterial = new List<Material>();

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter codeParamter = base.GetParameterOut("@CODE_UNIT", SqlDbType.NVarChar, codeunit, ParameterDirection.Input);
            DbParameter startParamter = base.GetParameterOut("@STARTDATE", SqlDbType.DateTime, startDate, ParameterDirection.Input);
            DbParameter endParamter = base.GetParameterOut("@ENDDATE", SqlDbType.DateTime, endDate, ParameterDirection.Input);

            parameterList.Add(codeParamter);
            parameterList.Add(startParamter);
            parameterList.Add(endParamter);

            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_ALL_MATERIAL", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Material materail = new Material();
                        materail.id = (int)dataReader["ID"];
                        materail.code = (string)dataReader["CODE"];
                        materail.name = (string)dataReader["NAME"];
                        materail.code_material_type = (string)dataReader["CODE_MATERIAL_TYPE"];
                        materail.unit = (string)dataReader["UNIT"];
                        materail.production_countries = (string)dataReader["PRODUCTION_COUNTRIES"];
                        materail.code_symbols = (string)dataReader["CODE_SYMBOLS"];
                        materail.number = (string)dataReader["NUMBER"];
                        materail.capacity = (string)dataReader["CAPACITY"].ToString();
                        materail.in_use = (string)dataReader["IN_USE"];
                        materail.position = (string)dataReader["POSITION"];
                        materail.moi_dat_de = (string)dataReader["MOI_DAT_DE"].ToString();
                        materail.status = (Int16)dataReader["STATUS"];
                        materail.source = (string)dataReader["SOURCE"];
                        materail.quantity = (Decimal)dataReader["QUANTITY"];
                        materail.note = (string)dataReader["NOTE"].ToString();
                        materail.original_price = (Decimal)dataReader["ORIGINAL_PRICE"];

                        listMaterial.Add(materail);
                    }
                }
            }
            entity.listInfo = listMaterial.ToArray();

            entity.respCode = "0";
            entity.respContent = "Thành công";
            return entity;
        }
    }
}
