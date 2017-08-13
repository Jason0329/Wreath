using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductBatchUpdate.Service
{
    public class ImportExportManagement
    {

        public int Import_CSV<T>(Stream stream
            , IRepository<T> ImportDataRepository) where T : Nop.Core.BaseEntity, new()
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
                   
                    T ImportedData = ImportDataRepository.GetById(Convert.ChangeType(tmp[0],typeof(int)));

                    if (ImportedData == null)
                    {
                        ImportFailID += tmp[0] + ",";
                        continue;
                    }


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

                    ImportDataRepository.Update(ImportedData);
                    count++;
                }
            }


            if (ImportFailID.CompareTo("") != 0)
                throw new Exception(ImportFailID + " import failed");

            return count;
        }

        //public int ImportCsv(Stream stream, IRepository<Product> _ProductRepository)
        //{
        //    string[] property_name = null;
        //    string ImportFailID = "";
        //    int count = 0;
        //    bool IsUpdate = true;
        //    Product _product;
        //    List<Product> ProductList = _ProductRepository.Table.ToList();

        //    using (var reader = new StreamReader(stream, Encoding.Default))
        //    {
        //        #region Check has format type

        //        if (reader.Peek() != 0)
        //        {
        //            property_name = reader.ReadLine().Split(',');
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //        #endregion

        //        while (!reader.EndOfStream)
        //        {
        //            string line = reader.ReadLine();
        //            if (String.IsNullOrWhiteSpace(line))
        //                continue;
        //            string[] tmp = line.Split(',');

                   
        //            _product = (Product)ProductList.Select(m => m.Id = (int)Convert.ChangeType(tmp[0], typeof(int)));

        //            if (_product == null)
        //            {
        //                _product = (Product)ProductList.Select(m => m.Name.Split('(')[0] = tmp[0]);
        //            }
                   


        //            T ImportedData = ImportDataRepository.GetById(Convert.ChangeType(tmp[0], typeof(int)));

        //            if (ImportedData == null)
        //            {
        //                ImportFailID += tmp[0] + ",";
        //                continue;
        //            }


        //            for (int i = 0; i < property_name.Length; i++)
        //            {
        //                PropertyInfo property = ImportedData.GetType().GetProperty(property_name[i]);

        //                if (tmp[i] != null && tmp[i].CompareTo("") != 0)
        //                {
        //                    if (property.PropertyType.Name.CompareTo(typeof(Nullable<>).Name) != 0)
        //                        property.SetValue(ImportedData, Convert.ChangeType(tmp[i], property.PropertyType), null);
        //                    else
        //                    {
        //                        try
        //                        {
        //                            property.SetValue(ImportedData, int.Parse(tmp[i]), null);
        //                        }
        //                        catch (Exception e)
        //                        {
        //                            property.SetValue(ImportedData, 0, null);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (property.PropertyType.Name.CompareTo(typeof(Nullable<>).Name) != 0)
        //                        property.SetValue(ImportedData, Convert.ChangeType(tmp[i], property.PropertyType), null);
        //                    else
        //                    {
        //                        try
        //                        {
        //                            property.SetValue(ImportedData, int.Parse("0"), null);
        //                        }
        //                        catch (Exception e)
        //                        {
        //                            property.SetValue(ImportedData, 0, null);
        //                        }
        //                    }
        //                }
        //            }

        //            ImportDataRepository.Update(ImportedData);
        //            count++;
        //        }
        //    }
        //}

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




        public int Import_CSV_ProductSpecifications(Stream stream
            , IRepository<Product> ImportDataRepository, ref List<SpecificationAttribute> SpecificationAttributeList
            , ref List<SpecificationAttributeOption> SpecificationAttributeOptionList)
        {
            string[] property_name = null;
            string ImportFailID = "";
            int count = 0;
            bool IsCustomerText = false;



            using (var reader = new StreamReader(stream))
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



                    Product ImportedData = ImportDataRepository.GetById(Convert.ChangeType(tmp[0], typeof(int)));

                    if (ImportedData == null)
                    {
                        ImportFailID += tmp[0] + ",";
                        continue;
                    }


                    for (int i = 0; i < property_name.Length; i++)
                    {
                        IsCustomerText = false;
                        SpecificationAttribute _specificationAttribute = SpecificationAttributeList.Where(x => x.Name == property_name[i]).FirstOrDefault();
                        SpecificationAttributeOption _specificationAttributeOption = null;

                        if (_specificationAttribute == null) continue;

                        _specificationAttributeOption = SpecificationAttributeOptionList.Where(x => x.SpecificationAttributeId == _specificationAttribute.Id).Where(x => x.Name.Contains(tmp[i])).FirstOrDefault();

                        if (_specificationAttributeOption == null)
                        {
                            _specificationAttributeOption = SpecificationAttributeOptionList.Where(x => x.SpecificationAttributeId == _specificationAttribute.Id).Where(x => x.Name.Contains(property_name[i])).FirstOrDefault();
                            IsCustomerText = true;
                        }
                        if (_specificationAttributeOption == null) continue;


                        ProductSpecificationAttribute _ProductSpecificationAttribute = null;

                        if (_specificationAttribute.Name.Contains("動力型式") || _specificationAttribute.Name.Contains("能源效率等級")
                            || _specificationAttribute.Name.Contains("車身型式"))
                            _ProductSpecificationAttribute = SpecificationClassify(IsCustomerText, _specificationAttributeOption.Id, tmp[i], true);
                        else
                            _ProductSpecificationAttribute = SpecificationClassify(IsCustomerText, _specificationAttributeOption.Id, tmp[i]);

                        int CheckProductSpecificationAttributes = ImportedData.ProductSpecificationAttributes.Count;//check the specification that is all add in product

                        if (CheckProductSpecificationAttributes >= 11)
                            continue;

                        ImportedData.ProductSpecificationAttributes.Add(_ProductSpecificationAttribute);

                    }


                    ImportDataRepository.Update(ImportedData);
                    count++;


                }
            }


            if (ImportFailID.CompareTo("") != 0)
                throw new Exception(ImportFailID + " import failed");

            return count;
        }

        ProductSpecificationAttribute SpecificationClassify(bool IsCustomerText ,
            int SpecificationAttributeOptionID, string CustomerValue = "", bool AllowFilter = false)
        {
            ProductSpecificationAttribute _Specification = new ProductSpecificationAttribute();

            _Specification.AllowFiltering = AllowFilter;
            _Specification.ShowOnProductPage = true;
            _Specification.DisplayOrder = 0;
            _Specification.SpecificationAttributeOptionId = SpecificationAttributeOptionID;

            if (IsCustomerText)
            {
                _Specification.AttributeTypeId = 10;
                _Specification.CustomValue = CustomerValue;
            }
            else
            {
                
                _Specification.AttributeTypeId = 0;
            }


            return _Specification;
        }


        public int Import_BenzCSV_Attribute(Stream stream, IRepository<Product> ProductRepository 
            ,IRepository<ProductAttribute> productAttribute)
        {
            string[] property_name = null;
            string ImportFailID = "";
            int count = 0;

            using (var reader = new StreamReader(stream))
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

                    Product _Product = ProductRepository.GetById(Convert.ChangeType(tmp[0], typeof(int)));

                    foreach (var _attribute in _Product.ProductAttributeMappings)
                    {
                        if (_attribute.TextPrompt.Contains("選擇顏色"))
                        {
                            foreach (var _attrValue in _attribute.ProductAttributeValues)
                            {
                                if (_attrValue.Name.Contains("銀粉漆"))
                                {
                                    _attrValue.PriceAdjustment = decimal.Parse(tmp[1]);
                                }
                            }
                        }
                    }

                    ProductRepository.Update(_Product);
                    count++;
                }
            }

            if (ImportFailID.CompareTo("") != 0)
                throw new Exception(ImportFailID + " import failed");

            return count;
        }

     

       
        
    }
}
