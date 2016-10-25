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

        public Entity addMaterialType(String code, String name)
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
