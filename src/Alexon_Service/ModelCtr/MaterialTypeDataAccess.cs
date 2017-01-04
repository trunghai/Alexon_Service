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
    public class MaterialTypeDataAccess : BaseDataAccess
    {

        public MaterialTypeDataAccess(string connectionString) : base(connectionString)  
        {
        }

        public Entity getMaterialType()
        {
            Entity entity = new Entity();
            List<MaterialType> listMaterialType = new List<MaterialType>();
            List<DbParameter> parameterList = new List<DbParameter>();

            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_MATERIAL_TYPE", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        MaterialType materail = new MaterialType();
                        materail.id = (int)dataReader["ID"];
                        materail.code = (string)dataReader["CODE"];
                        materail.name = (string)dataReader["NAME"];
                       
                        listMaterialType.Add(materail);
                    }
                }
            }
            entity.listInfo = listMaterialType.ToArray();

            entity.respCode = "0";
            entity.respContent = "Thành công";
            return entity;
        }

        public Entity addMaterialType(String code, String name)
        {
            Entity entity = new Entity();

            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter codeParams = base.GetParameter("@CODE", code);
            DbParameter nameParams = base.GetParameter("@NAME", name);
            DbParameter ecodeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, null, ParameterDirection.Output);
            DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, null, ParameterDirection.Output);

            parameterList.Add(codeParams);
            parameterList.Add(nameParams);
            parameterList.Add(ecodeParams);
            parameterList.Add(descParams);

            try
            {
                base.ExecuteNonQuery("PROC_ADD_MATERIAL_TYPE", parameterList, CommandType.StoredProcedure);
                entity.respCode = (string)ecodeParams.Value;
                entity.respContent = (string)descParams.Value;
            }
            catch(Exception e)
            {
                entity.respCode = "10";
                entity.respContent = "Thêm mới không thành công";
            }

            
            return entity;
        }

        public Entity updateMaterialType(String code, String name)
        {
            Entity entity = new Entity();
            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter codeParams = base.GetParameter("@CODE", code);
            DbParameter nameParams = base.GetParameter("@NAME", name);
            parameterList.Add(codeParams);
            parameterList.Add(nameParams);

            try
            {
                base.ExecuteNonQuery("PROC_ADD_MATERIAL_TYPE", parameterList, CommandType.StoredProcedure);
                entity.respCode = "0";
                entity.respContent = "Thêm mới thành công";
            }
            catch (Exception e)
            {
                entity.respCode = "10";
                entity.respContent = "Thêm mới không thành công";
            }

            return entity;
        }
    }
}
