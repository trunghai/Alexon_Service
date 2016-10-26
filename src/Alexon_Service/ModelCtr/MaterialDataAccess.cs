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

        public Entity getMaterials(String page, String pageSize, String codeMaterialType)
        {
            Entity entity = new Entity();
            List<Material> listMaterial = new List<Material>();

            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter pageParamter = base.GetParameterOut("@PAGE", SqlDbType.Int, page, ParameterDirection.Input);
            DbParameter pageSizeParamter = base.GetParameterOut("@PAGESIZE", SqlDbType.Int, pageSize, ParameterDirection.Input);
            DbParameter codeParamter = base.GetParameterOut("@CODE_MATERIAL_TYPE", SqlDbType.NVarChar, codeMaterialType, ParameterDirection.Input);

            parameterList.Add(pageParamter);
            parameterList.Add(pageSizeParamter);
            parameterList.Add(codeParamter);

            using (DbDataReader dataReader = base.GetDataReader("PROC_GET_MATERIALS", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Material materail = new Material();
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

            XmlNode codeMaterailTypeNode = xmlDoc.CreateElement("code_material_type");
            codeMaterailTypeNode.InnerText = material.name;
            materialNode.AppendChild(codeMaterailTypeNode);

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
            statusNode.InnerText = material.status.ToString();
            materialNode.AppendChild(statusNode);

            XmlNode noteNode = xmlDoc.CreateElement("note");
            noteNode.InnerText = material.note.ToString();
            materialNode.AppendChild(noteNode);

            XmlNode originalPriceNode = xmlDoc.CreateElement("original_price");
            originalPriceNode.InnerText = material.original_price.ToString();
            materialNode.AppendChild(originalPriceNode);

            XmlNode sourceNode = xmlDoc.CreateElement("source");
            sourceNode.InnerText = material.source;
            materialNode.AppendChild(sourceNode);

            XmlNode quantityNode = xmlDoc.CreateElement("quantity");
            quantityNode.InnerText = material.quantity.ToString();
            materialNode.AppendChild(quantityNode);

            rootNode.AppendChild(materialNode);
            xmlDoc.Save(Console.Out);
            String xml = xmlDoc.OuterXml.ToString();
            return xml;
        }
    }
}