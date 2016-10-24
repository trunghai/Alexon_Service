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
    public class MaterialDataAccess : BaseDataAccess
    {
        public MaterialDataAccess(string connectionString) : base(connectionString)
        {
        }

        public Entity addMaterial(Material material)
        {
            Entity entity = new Entity();
            String xmlMaterial = convertXMLMaterial(material);

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter xmlParams = base.GetParameter("@xml", xmlMaterial);
            parameterList.Add(xmlParams);
            try
            {
                base.ExecuteNonQuery("PROC_ADD_MATERIAL", parameterList, CommandType.StoredProcedure);
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

        public Entity updateMaterial(Material material)
        {
            Entity entity = new Entity();
            String xmlMaterial = convertXMLMaterial(material);

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter xmlParams = base.GetParameter("@xml", xmlMaterial);
            parameterList.Add(xmlParams);
            try
            {
                base.ExecuteNonQuery("PROC_UPDATE_MATERIAL", parameterList, CommandType.StoredProcedure);
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

        public String convertXMLMaterial(Material material)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(rootNode);

            XmlNode materialNode = xmlDoc.CreateElement("material");
            rootNode.AppendChild(materialNode);

            XmlNode codeNode = xmlDoc.CreateElement("code");
            codeNode.InnerText = material.code;
            materialNode.AppendChild(codeNode);

            XmlNode nameNode = xmlDoc.CreateElement("name");
            nameNode.InnerText = material.name;
            materialNode.AppendChild(nameNode);

            XmlNode unitNode = xmlDoc.CreateElement("unit");
            unitNode.InnerText = material.unit;
            materialNode.AppendChild(unitNode);

            XmlNode production_countriesNode = xmlDoc.CreateElement("production_countries");
            production_countriesNode.InnerText = material.production_countries;
            materialNode.AppendChild(production_countriesNode);

            XmlNode code_symbolstNode = xmlDoc.CreateElement("code_symbols");
            code_symbolstNode.InnerText = material.code_symbols;
            materialNode.AppendChild(code_symbolstNode);

            XmlNode numberNode = xmlDoc.CreateElement("number");
            numberNode.InnerText = material.number;
            materialNode.AppendChild(numberNode);

            XmlNode capacityNode = xmlDoc.CreateElement("capacity");
            capacityNode.InnerText = material.capacity;
            materialNode.AppendChild(capacityNode);

            XmlNode in_useNode = xmlDoc.CreateElement("in_use");
            in_useNode.InnerText = material.in_use;
            materialNode.AppendChild(in_useNode);

            XmlNode positionNode = xmlDoc.CreateElement("position");
            positionNode.InnerText = material.position;
            materialNode.AppendChild(positionNode);

            XmlNode moi_dat_deNode = xmlDoc.CreateElement("moi_dat_de");
            moi_dat_deNode.InnerText = material.moi_dat_de;
            materialNode.AppendChild(moi_dat_deNode);

            XmlNode statusNode = xmlDoc.CreateElement("status");
            statusNode.InnerText = material.status;
            materialNode.AppendChild(statusNode);

            XmlNode sourceNode = xmlDoc.CreateElement("source");
            sourceNode.InnerText = material.source;
            materialNode.AppendChild(sourceNode);

            XmlNode quantityNode = xmlDoc.CreateElement("quantity");
            quantityNode.InnerText = material.quantity;
            materialNode.AppendChild(quantityNode);

            rootNode.AppendChild(materialNode);
            xmlDoc.Save(Console.Out);
            String xml = xmlDoc.OuterXml.ToString();
            return xml;
        }
    }
}