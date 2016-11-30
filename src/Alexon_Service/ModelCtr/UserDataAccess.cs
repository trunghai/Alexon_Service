using Alexon_Service.Helper;
using Alexon_Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Alexon_Service.ModelCtr
{
    public class UserDataAccess : BaseDataAccess
    {
        public UserDataAccess(string connectionString) : base(connectionString)  
        {
        }

        public Entity getUsers()
        {
            Entity entity = new Entity();     
            List<User> listUser = new List<User>();      
            List<DbParameter> parameterList = new List<DbParameter>();

            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_USERS", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        User user = new User();
                        user.id = (int)dataReader["ID"];
                        user.username = (string)dataReader["IDUSER"].ToString();
                        user.fullname = (string)dataReader["FULLNAME"].ToString();
                        user.address = (string)dataReader["ADDRESS"].ToString();
                        user.email = (string)dataReader["EMAIL"].ToString();
                        user.salution = (string)dataReader["SALUTION"].ToString();
                        user.phone = (string)dataReader["PHONENUMBER"].ToString();
                        user.role = (string)dataReader["ROLE"].ToString();
                        user.roleId = (int)dataReader["ROLE_ID"];

                        listUser.Add(user);
                    }
                }
            }
            entity.listInfo = listUser.ToArray();

            entity.respCode = "0";
            entity.respContent = "Thành công";
            return entity;
        }

        public Entity addUser(User user)
        {
            Entity entity = new Entity();
         
            String xmlUser = convertXMLUser(user);

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter xmlParams = base.GetParameter("@xml", xmlUser);
            DbParameter codeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, null, ParameterDirection.Output);
            DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, null, ParameterDirection.Output);
            parameterList.Add(xmlParams);
            parameterList.Add(codeParams);
            parameterList.Add(descParams);
            try
            {
                base.ExecuteNonQuery("PROC_ADD_USER", parameterList, CommandType.StoredProcedure);
                entity.respCode = (string)codeParams.Value;
                entity.respContent = (string)descParams.Value;
            }
            catch (Exception e)
            {
                entity.respCode = "10";
                entity.respContent = "Thêm mới không thành công";
            }

            return entity;
        }

        public Entity updateUser(User user)
        {
            Entity entity = new Entity();     
            String xmlUser = convertXMLUser(user);

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter xmlParams = base.GetParameter("@xml", xmlUser);
            DbParameter codeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, null, ParameterDirection.Output);
            DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, null, ParameterDirection.Output);
            parameterList.Add(xmlParams);
            parameterList.Add(codeParams);
            parameterList.Add(descParams);
            try
            {
                base.ExecuteNonQuery("PROC_UPDATE_USER", parameterList, CommandType.StoredProcedure);
                entity.respCode = (string)codeParams.Value;
                entity.respContent = (string)descParams.Value;
            }
            catch (Exception e)
            {
                entity.respCode = "10";
                entity.respContent = "Cập nhật không thành công";
            }

            return entity;
        }

        public Entity deleteUser(int id)
        {
            Entity entity = new Entity();
            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter idParams = base.GetParameterOut("@ID", SqlDbType.Int, id, ParameterDirection.Input);
            DbParameter codeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, null, ParameterDirection.Output);
            DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, null, ParameterDirection.Output);
            parameterList.Add(idParams);
            parameterList.Add(codeParams);
            parameterList.Add(descParams);
            try
            {
                base.ExecuteNonQuery("PROC_DELETE_USER", parameterList, CommandType.StoredProcedure);
                entity.respCode = (string)codeParams.Value;
                entity.respContent = (string)descParams.Value;
            }
            catch (Exception e)
            {
                entity.respCode = "10";
                entity.respContent = "Xóa không thành công";
            }
            return entity;
        }

        public String convertXMLUser(User user)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(rootNode);

            XmlNode userNode = xmlDoc.CreateElement("user");
            rootNode.AppendChild(userNode);

            XmlNode idNode = xmlDoc.CreateElement("id");
            idNode.InnerText = user.id.ToString();
            userNode.AppendChild(idNode);

            XmlNode usernameNode = xmlDoc.CreateElement("username");
            usernameNode.InnerText = user.username;
            userNode.AppendChild(usernameNode);

            XmlNode fullnameNode = xmlDoc.CreateElement("fullname");
            fullnameNode.InnerText = user.fullname;
            userNode.AppendChild(fullnameNode);

            XmlNode emailNode = xmlDoc.CreateElement("email");
            emailNode.InnerText = user.email;
            userNode.AppendChild(emailNode);

            XmlNode addressNode = xmlDoc.CreateElement("address");
            addressNode.InnerText = user.address;
            userNode.AppendChild(addressNode);

            XmlNode phoneNode = xmlDoc.CreateElement("phone");
            phoneNode.InnerText = user.phone;
            userNode.AppendChild(phoneNode);

            XmlNode roleIdNode = xmlDoc.CreateElement("roleId");
            roleIdNode.InnerText = user.roleId.ToString();
            userNode.AppendChild(roleIdNode);

            XmlNode passwordNode = xmlDoc.CreateElement("password");
            passwordNode.InnerText = user.password;
            userNode.AppendChild(passwordNode);

            XmlNode salutionNode = xmlDoc.CreateElement("salution");
            salutionNode.InnerText = user.salution;
            userNode.AppendChild(salutionNode);

            rootNode.AppendChild(userNode);
            xmlDoc.Save(Console.Out);
            String xml = xmlDoc.OuterXml.ToString();
            return xml;
        }
    }
}
