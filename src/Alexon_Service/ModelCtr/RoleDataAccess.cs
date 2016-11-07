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
    public class RoleDataAccess : BaseDataAccess
    {
        public RoleDataAccess(string connectionString) : base(connectionString)  
        {
        }

        public Entity getRole()
        {
            Entity entity = new Entity();
            List<Role> listRole = new List<Role>();
            List<DbParameter> parameterList = new List<DbParameter>();
            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_ROLE", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Role role = new Role();
                        role.id = (int)dataReader["ID"];
                        role.role = (string)dataReader["ROLE"];


                        listRole.Add(role);
                    }
                }
            }
            entity.listInfo = listRole.ToArray();

            entity.respCode = "0";
            entity.respContent = "Thành công";
            return entity;
        }
    }
}
