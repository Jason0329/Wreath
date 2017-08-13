using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.X0_Widget.Widget.StatisticPrice.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.StatisticPrice.Service
{
    public class ImportExportManagement
    {

        public int Import_CSV(Stream stream
            , IRepository<StatisticPriceMonthly> ImportDataRepository) 
        {
            string[] property_name=null;
            string ImportFailID="";
            int count = 0;

      
            using (var reader = new StreamReader(stream,Encoding.Default))
            {
                if (reader.Peek() != 0)
                {
                    property_name = reader.ReadLine().Split(',');
                }
                else
                {
                    return 0;
                }

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (String.IsNullOrWhiteSpace(line))
                        continue;
                    string[] tmp = line.Split(',');

                    StatisticPriceMonthly ImportedData = new StatisticPriceMonthly();



                    for (int i = 0; i < property_name.Length; i++)
                    {
                        PropertyInfo property = ImportedData.GetType().GetProperty(property_name[i]);

                        if (tmp[i] != null && tmp[i].CompareTo("") != 0)
                        {
                            if (property.PropertyType.Name.CompareTo(typeof(Nullable<>).Name) != 0)
                                property.SetValue(ImportedData, Convert.ChangeType(tmp[i], property.PropertyType), null);
                            else
                            {
                                try
                                {
                                    property.SetValue(ImportedData, int.Parse(tmp[i]), null);
                                }
                                catch (Exception e)
                                {
                                    property.SetValue(ImportedData, 0, null);
                                }
                            }
                        }
                        else
                        {
                            if (property.PropertyType.Name.CompareTo(typeof(Nullable<>).Name) != 0)
                                property.SetValue(ImportedData, Convert.ChangeType(tmp[i], property.PropertyType), null);
                            else
                            {
                                try
                                {
                                    property.SetValue(ImportedData, int.Parse("0"), null);
                                }
                                catch (Exception e)
                                {
                                    property.SetValue(ImportedData, 0, null);
                                }
                            }
                        }
                    }

                    ImportedData.CreateTime = DateTime.Now;
                    ImportedData.UpdateTime = DateTime.Now;
                    ImportDataRepository.Insert(ImportedData);
                    count++;
                }
            }


            if (ImportFailID.CompareTo("") != 0)
                throw new Exception(ImportFailID + " import failed");

            return count;
        }

        public string Export_CSV(List<Product> product_list)
        {
            string[] DataTypeName = new string[] { "Id", "Name", "Price", "OldPrice","Published","AdminComment","ShortDescription","FullDescription"};
            string ExportData = "";
            int ProductPropertyCount = 6;

            for (int i = 0; i < DataTypeName.Length; i++)
            {
                ExportData += DataTypeName[i] + ",";
            }

            ExportData += "\n";

            for (int i = 0; i < product_list.Count; i++)
            {
                for (int j = 0; j < ProductPropertyCount; j++)
                {
                    PropertyInfo DataProperty = product_list[i].GetType().GetProperty(DataTypeName[j]);
                    ExportData += DataProperty.GetValue(product_list[i]) + "," ;
                }

                string CategoriesString = "";
                foreach (var Categories in product_list[i].ProductCategories)
                {
                    CategoriesString += Categories.Category.Name+";";
                }

                ExportData += CategoriesString + ",";

                foreach (var _attribute in product_list[i].ProductAttributeMappings)
                {
                     
                }

                ExportData += "\n";
            }

            return "\uFEFF"+ExportData;
        }




     

       
        
    }
}
