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
    public class BillDataAccess : BaseDataAccess
    {
        public BillDataAccess(string connectionString) : base(connectionString)
        {
        }

        public Entity getBills()
        {
            Entity entity = new Entity();
            List<Bill> listBill = new List<Bill>();
            List<DbParameter> parameterList = new List<DbParameter>();
            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_BILLS", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Bill bill = new Bill();
                        bill.code = (string)dataReader["CODE_BILLS"];
                        bill.code_material = (string)dataReader["CODE_MATERIAL"];
                        bill.code_unit = (string)dataReader["CODE_UNIT"];
                        bill.date_make = (string)dataReader["DATE_MAKE"];
                        bill.receiver = (string)dataReader["RECEIVER"];
                        bill.note = (string)dataReader["NOTE"];
                        bill.quantity = (decimal)dataReader["QUANTITY"];
                        bill.name_material = (string)dataReader["NAME_MATERIAL"];
                        bill.name_unit = (string)dataReader["NAME_UNIT"];
                        
                        listBill.Add(bill);
                    }
                }
            }
            entity.listInfo = listBill.ToArray();

            entity.respCode = "0";
            entity.respContent = "Thành công";
            return entity;
        }

        public Entity addBill(Bill bill)
        {
            Entity entity = new Entity();
            String xmlMaterial = convertXMLMaterial(bill);

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter xmlParams = base.GetParameter("@XML", xmlMaterial);
            DbParameter codeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, null, ParameterDirection.Output);
            DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, null, ParameterDirection.Output);
            parameterList.Add(xmlParams);
            parameterList.Add(codeParams);
            parameterList.Add(descParams);
            try
            {
                base.ExecuteNonQuery("PROC_ADD_BILL", parameterList, CommandType.StoredProcedure);
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

        public String convertXMLMaterial(Bill bill)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(rootNode);

            XmlNode materialNode = xmlDoc.CreateElement("bill");
            rootNode.AppendChild(materialNode);

            XmlNode codeNode = xmlDoc.CreateElement("code");
            codeNode.InnerText = bill.code;
            materialNode.AppendChild(codeNode);

            XmlNode nameNode = xmlDoc.CreateElement("code_material");
            nameNode.InnerText = bill.code_material;
            materialNode.AppendChild(nameNode);

            XmlNode codeMaterailTypeNode = xmlDoc.CreateElement("code_unit");
            codeMaterailTypeNode.InnerText = bill.code_unit;
            materialNode.AppendChild(codeMaterailTypeNode);

            XmlNode unitNode = xmlDoc.CreateElement("date_make");
            unitNode.InnerText = bill.date_make;
            materialNode.AppendChild(unitNode);

            XmlNode receiverNode = xmlDoc.CreateElement("receiver");
            receiverNode.InnerText = bill.receiver;
            materialNode.AppendChild(receiverNode);


            XmlNode quantityNode = xmlDoc.CreateElement("quantity");
            quantityNode.InnerText = bill.quantity.ToString();
            materialNode.AppendChild(quantityNode);

            XmlNode noteNode = xmlDoc.CreateElement("note");
            noteNode.InnerText = bill.note;
            materialNode.AppendChild(noteNode);

           

            rootNode.AppendChild(materialNode);
            xmlDoc.Save(Console.Out);
            String xml = xmlDoc.OuterXml.ToString();
            return xml;
        }
    }
}
