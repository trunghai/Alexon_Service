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
    public class PurchaseOrderDataAccess : BaseDataAccess
    {
        public PurchaseOrderDataAccess(string connectionString) : base(connectionString)
        {
        }

        public Entity getPurchaseOrder()
        {
            Entity entity = new Entity();
            List<PurchaseOrder> listPurchaseOrder = new List<PurchaseOrder>();
            List<DbParameter> parameterList = new List<DbParameter>();
            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_PURCHASEORDER", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        PurchaseOrder purchaseOrder = new PurchaseOrder();
                        purchaseOrder.code = (string)dataReader["CODE_BILLS"];
                        purchaseOrder.code_material = (string)dataReader["CODE_MATERIAL"];
                        purchaseOrder.date_make = (string)dataReader["DATE_MAKE"];
                        purchaseOrder.receiver = (string)dataReader["RECEIVER"];
                        purchaseOrder.note = (string)dataReader["NOTE"];
                        purchaseOrder.quantity = (decimal)dataReader["QUANTITY"];
                        purchaseOrder.name_material = (string)dataReader["NAME_MATERIAL"];


                        listPurchaseOrder.Add(purchaseOrder);
                    }
                }
            }
            entity.listInfo = listPurchaseOrder.ToArray();

            entity.respCode = "0";
            entity.respContent = "Thành công";
            return entity;
        }
        public Entity addPurchase(PurchaseOrder purchase)
        {
            Entity entity = new Entity();
            String xmlMaterial = convertXMLMaterial(purchase);

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter xmlParams = base.GetParameter("@XML", xmlMaterial);
            DbParameter codeParams = base.GetParameterOut("@ECODE", SqlDbType.NVarChar, null, ParameterDirection.Output);
            DbParameter descParams = base.GetParameterOut("@DESC", SqlDbType.NVarChar, null, ParameterDirection.Output);
            parameterList.Add(xmlParams);
            parameterList.Add(codeParams);
            parameterList.Add(descParams);
            try
            {
                base.ExecuteNonQuery("PROC_ADD_PURCHASEORDER", parameterList, CommandType.StoredProcedure);
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


        public String convertXMLMaterial(PurchaseOrder purchase)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(rootNode);

            XmlNode materialNode = xmlDoc.CreateElement("purchase");
            rootNode.AppendChild(materialNode);

            XmlNode codeNode = xmlDoc.CreateElement("code");
            codeNode.InnerText = purchase.code;
            materialNode.AppendChild(codeNode);

            XmlNode nameNode = xmlDoc.CreateElement("code_material");
            nameNode.InnerText = purchase.code_material;
            materialNode.AppendChild(nameNode);       

            XmlNode unitNode = xmlDoc.CreateElement("date_make");
            unitNode.InnerText = purchase.date_make;
            materialNode.AppendChild(unitNode);       

            XmlNode receiverNode = xmlDoc.CreateElement("receiver");
            receiverNode.InnerText = purchase.receiver;
            materialNode.AppendChild(receiverNode);

            XmlNode quantityNode = xmlDoc.CreateElement("quantity");
            quantityNode.InnerText = purchase.quantity.ToString();
            materialNode.AppendChild(quantityNode);

            XmlNode noteNode = xmlDoc.CreateElement("note");
            noteNode.InnerText = purchase.note;
            materialNode.AppendChild(noteNode);



            rootNode.AppendChild(materialNode);
            xmlDoc.Save(Console.Out);
            String xml = xmlDoc.OuterXml.ToString();
            return xml;
        }
    }
}
