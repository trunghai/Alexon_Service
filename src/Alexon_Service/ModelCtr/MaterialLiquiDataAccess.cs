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
    public class MaterialLiquiDataAccess : BaseDataAccess
    {
        public MaterialLiquiDataAccess(string connectionString) : base(connectionString)
        {
        }

        public Entity getMaterialLiqui(String page, String pageSize, String codeMaterialType, String keySearch)
        {
            Entity entity = new Entity();
            List<Material> listMaterial = new List<Material>();

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter pageParamter = base.GetParameterOut("@PAGE", SqlDbType.Int, page, ParameterDirection.Input);
            DbParameter pageSizeParamter = base.GetParameterOut("@PAGESIZE", SqlDbType.Int, pageSize, ParameterDirection.Input);
            DbParameter codeParamter = base.GetParameterOut("@CODE_MATERIAL_TYPE", SqlDbType.NVarChar, codeMaterialType, ParameterDirection.Input);
            DbParameter keySearchParamter = base.GetParameterOut("@KEYSEARCH", SqlDbType.NVarChar, keySearch, ParameterDirection.Input);

            parameterList.Add(pageParamter);
            parameterList.Add(pageSizeParamter);
            parameterList.Add(codeParamter);
            parameterList.Add(keySearchParamter);

            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_MATERIAL_LIQUI", parameterList, CommandType.StoredProcedure))
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

        public Entity materialLiqui(String[] ids)
        {
            Entity entity = new Entity();

            for(var i = 0; i < ids.Length; i++)
            {
                List<DbParameter> parameterList = new List<DbParameter>();
                DbParameter xmlParams = base.GetParameter("@ID", ids[i]);
                DbParameter codeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, null, ParameterDirection.Output);
                DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, null, ParameterDirection.Output);
                parameterList.Add(xmlParams);
                parameterList.Add(codeParams);
                parameterList.Add(descParams);
                try
                {
                    base.ExecuteNonQuery("PROC_UPDATE_MATERIAL_LIQUI", parameterList, CommandType.StoredProcedure);
                    entity.respCode = (string)codeParams.Value;
                    entity.respContent = (string)descParams.Value;
                }
                catch (Exception e)
                {
                    entity.respCode = "10";
                    entity.respContent = "Thêm mới không thành công";
                    break;
                }
            }

            return entity;
        }
    }
}
