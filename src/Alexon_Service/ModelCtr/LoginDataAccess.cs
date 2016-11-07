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
    public class LoginDataAccess : BaseDataAccess
    {
        public LoginDataAccess(string connectionString) : base(connectionString)  
        {
        }

        public Entity login(String username, String password)
        {
            Entity entity = new Entity();

            User user = new User();
            user.session = Guid.NewGuid().ToString();
            user.username = username;



            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter userParams = base.GetParameter("@USER", user.username);
            DbParameter passParams = base.GetParameter("@PASS", password);
            DbParameter sessionParams = base.GetParameter("@SESSIONID", user.session);

            DbParameter fullnameParamter = base.GetParameterOut("@FULL_NAME", SqlDbType.NVarChar, "", ParameterDirection.Output);
            DbParameter emailParamter = base.GetParameterOut("@EMAIL", SqlDbType.NVarChar, "", ParameterDirection.Output);
            DbParameter addressParamter = base.GetParameterOut("@ADDRESS", SqlDbType.NVarChar, "", ParameterDirection.Output);

            parameterList.Add(userParams);
            parameterList.Add(passParams);
            parameterList.Add(sessionParams);

            parameterList.Add(fullnameParamter);
            parameterList.Add(emailParamter);
            parameterList.Add(addressParamter);


            base.ExecuteNonQuery("PROC_LOGIN", parameterList, CommandType.StoredProcedure);

            user.fullname = (string)fullnameParamter.Value;
            user.email = (string)emailParamter.Value;
            user.address = (string)addressParamter.Value;

            entity.respCode = "0";
            entity.respContent = "Giao dịch thành công";
            entity.info = user;
            return entity;
        }
    }
}
