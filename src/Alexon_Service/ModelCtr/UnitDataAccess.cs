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
    public class UnitDataAccess : BaseDataAccess
    {
        public UnitDataAccess(string connectionString) : base(connectionString)  
        {
        }

        public Entity getUnit()
        {
            Entity entity = new Entity();
            List<Unit> listUnit = new List<Unit>();
            List<DbParameter> parameterList = new List<DbParameter>();
            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_UNITS", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Unit unit = new Unit();
                        unit.id = (int)dataReader["ID"];
                        unit.code = (string)dataReader["CODE"];
                        unit.name = (string)dataReader["NAME"];
                        unit.phone = (string)dataReader["PHONE"].ToString();


                        listUnit.Add(unit);
                    }
                }
            }
            entity.listInfo = listUnit.ToArray();

            entity.respCode = "0";
            entity.respContent = "Thành công";
            return entity;
        }

        public Entity addUnit(Unit unit)
        {
            Entity entity = new Entity();

            String xml = convertXMLUnit(unit);

            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter xmlParams = base.GetParameter("@xml", xml);
            DbParameter ecodeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, ParameterDirection.Output);
            DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, ParameterDirection.Output);

            parameterList.Add(xmlParams);
            parameterList.Add(ecodeParams);
            parameterList.Add(descParams);
            

            base.ExecuteNonQuery("PROC_ADD_UNIT", parameterList, CommandType.StoredProcedure);

            //unit.code = (string)madvParams.Value;
            //unit.tendonvi = (string)tendvParams.Value;
            //unit.sdt = (string)sdtParams.Value;

            entity.respCode = (string)ecodeParams.Value;
            entity.respContent = (string)descParams.Value;
            entity.info = unit;
            return entity;
        }

        public Entity updateUnit(int id, Unit unit)
        {
            Entity entity = new Entity();

            String xml = convertXMLUnit(unit);

            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter xmlParams = base.GetParameter("@xml", xml);
            DbParameter idParams = base.GetParameter("@ID", id);
            DbParameter ecodeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, ParameterDirection.Output);
            DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, ParameterDirection.Output);

            parameterList.Add(xmlParams);
            parameterList.Add(idParams);
            parameterList.Add(ecodeParams);
            parameterList.Add(descParams);


            base.ExecuteNonQuery("PROC_UPDATE_UNIT", parameterList, CommandType.StoredProcedure);

            //unit.code = (string)madvParams.Value;
            //unit.tendonvi = (string)tendvParams.Value;
            //unit.sdt = (string)sdtParams.Value;

            entity.respCode = (string)ecodeParams.Value;
            entity.respContent = (string)descParams.Value;
            entity.info = unit;
            return entity;
        }

        public Entity deleteUnit(int id)
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
                base.ExecuteNonQuery("PROC_DELETE_UNIT", parameterList, CommandType.StoredProcedure);
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

        public String convertXMLUnit(Unit unit)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(rootNode);

            XmlNode unitNode = xmlDoc.CreateElement("units");
            rootNode.AppendChild(unitNode);

            XmlNode codeNode = xmlDoc.CreateElement("code");
            codeNode.InnerText = unit.code;
            unitNode.AppendChild(codeNode);

            XmlNode nameNode = xmlDoc.CreateElement("name");
            nameNode.InnerText = unit.name;
            unitNode.AppendChild(nameNode);

            XmlNode phoneNode = xmlDoc.CreateElement("phone");
            phoneNode.InnerText = unit.phone;
            unitNode.AppendChild(phoneNode);

            rootNode.AppendChild(unitNode);
            xmlDoc.Save(Console.Out);
            String xml = xmlDoc.OuterXml.ToString();
            return xml;
        }
    }
}
