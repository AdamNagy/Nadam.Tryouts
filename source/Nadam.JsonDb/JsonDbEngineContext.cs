using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using Nadam.Global.JsonDb.DatabaseGraph;
using Nadam.Global.Lib;
using Newtonsoft.Json;

namespace Nadam.Global.JsonDb
{ 
    /// <summary>
    /// Json file based data storage with a context class to access these files.
    /// CRUD operations are supported
    /// </summary>
    public abstract class JsonDbEngineContext
    {
        private FileUtility fileUtility;
        public readonly string RootFolder;
        protected readonly bool Inmemory;
        protected readonly DeferredExecutionPlans ExePlan =
            DeferredExecutionPlans.EagerLoading;

        private readonly DbModelGraph dbGraph;
        private readonly string fileExtension = ".json";
        private Type derivedContextType;

        #region constructors
        protected JsonDbEngineContext(string configName, bool inmemory = true)
        {
            Inmemory = inmemory;
            if ( configName.Contains("path") )
            {
                RootFolder = configName.Split('=')[1];
            }
            else
            {
                RootFolder = ConfigurationManager.AppSettings[configName];
            }
            
            fileUtility = new FileUtility();
            dbGraph = new DbModelGraph();

            derivedContextType = this.GetType();
            BuildDatabaseGraph();
            InitListProperties();

            if (Inmemory)
                LoadAllTable();
        }
        #endregion

        private void InitListProperties()
        {
            foreach (var table in this.GetType().GetProperties().Select(p => p.Name))
            {                
                this.SetValueFor(table, Activator.CreateInstance(this.GetType().GetProperty(table).PropertyType));
            }            
        }

        // <SaveChanges>
        public virtual void SaveChanges()
        {
            var props = this.GetType().GetProperties();
            // TODO: delete this part and implement elswhere
            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //var tables = new Dictionary<string, IEnumerable<object>>();

            //foreach (var table in _dbData)
            //{
            //    var tableName = _dbGraph.FindByNodeId(table.Key).Value;
            //    if (!TableExistInRoot(tableName))
            //    {
            //        FileUtility.CreateFile(RootFolder, tableName, fileExtension);
            //    }
            //    var tableRows = table.Value;
            //    tables.Add(tableName, tableRows);
            //}
            // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //var it = _dbGraph.DependecyIteration();
            //it.Reset();
            //while (it.MoveNext())
            //{
            //    //it.Current.Value
            //    foreach (var tableRow in (IEnumerable<Object>)this.GetValueFor(it.Current.TableName))
            //    {
            //        if (it.Current.HaveDependency)
            //        {
            //            foreach (var foreignKey in propsWithForeignKeyAttr)
            //            {
            //                var foreignKeyNavigationPropValue = tableRow.GetValueFor(foreignKey.Name);
            //                if (foreignKeyNavigationPropValue != null)
            //                {
            //                    object[] attrs = foreignKey.GetCustomAttributes(true);
            //                    var foreignKeyAttr = attrs[0] as ForeignKeyAttribute;
            //                    var foreignKeyPropertyName = foreignKeyAttr.Name;

            //                    tableRow.SetValueFor(foreignKeyPropertyName,
            //                        foreignKeyNavigationPropValue.GetValueFor("Id"));
            //                }
            //            }
            //        }
            //    }
            //}

            // get the table type, and determine if it has any foreign key defined (have any navigation prop)
            //foreach (var data in tables.Where(p => p.Value != null && p.Value.Any()).ToList())
            //{
            //    var tableRowEntity = data.Value?.First();
            //    if (tableRowEntity == null)
            //        continue;

            //    var propsWithForeignKeyAttr = tableRowEntity
            //        .GetType()
            //        .GetProperties()
            //        .Where(prop => Attribute.IsDefined(prop, typeof(ForeignKeyAttribute)));                

            //    var haveForeignKey = propsWithForeignKeyAttr.Any();

            //    // here we enumerate the tables
            //    foreach (var tableRow in data.Value)
            //    {
            //        // Here we set ForeignKeys for records from the navigation property
            //        if (haveForeignKey)
            //        {
            //            foreach (var foreignKey in propsWithForeignKeyAttr)
            //            {
            //                var foreignKeyNavigationPropValue = tableRow.GetValueFor(foreignKey.Name);
            //                if (foreignKeyNavigationPropValue != null)
            //                {
            //                    object[] attrs = foreignKey.GetCustomAttributes(true);
            //                    var foreignKeyAttr = attrs[0] as ForeignKeyAttribute;
            //                    var foreignKeyPropertyName = foreignKeyAttr.Name;

            //                    tableRow.SetValueFor(foreignKeyPropertyName,
            //                        foreignKeyNavigationPropValue.GetValueFor("Id"));
            //                }
            //            }
            //        }
            //    }
            //}

            // now we can write the data to files
            // TODO: extract to method
            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>            
            //foreach (var table in tables)
            //{
            //    table.Value.SetIdsFor();
            //    SaveTable(table.Key, table.Value.MakeVirtualPropertiesNull());
            //}

            //var table = dbGraph.DependecyIteration();
            //table.Reset();
            //while (table.MoveNext())
            //{
            //    ((IEnumerable<Object>)this.GetValueFor(table.Current.TableName)).SetIds();
            //    SaveTable(table.Current.TableName, (IEnumerable<Object>)this.GetValueFor(table.Current.TableName));
            //}
            // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        }

        private void BuildDatabaseGraph()
        {
            // TODO: extract to extension method (getTables)
            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            var inmemoryDbTableStructure = new List<PropertyInfo>(derivedContextType.GetProperties()
                                                                           .Where(p => p.PropertyType.Name.Contains("IList") || p.PropertyType.Name.Contains("List") || p.PropertyType.Name.Contains("JsonDbTable"))
                                                                           .ToList());
            // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            foreach (var tableProperty in inmemoryDbTableStructure)
            {
                var tableName = tableProperty.Name;

                // TODO: extract to extension method (getPropsWithForeignKeyAttr)
                // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> 
                var propsWithForeignKeyAttr = tableProperty.PropertyType
                                                        .GetGenericArguments()[0]
                                                        .GetProperties()
                                                        .Where(prop => Attribute.IsDefined(prop, typeof(ForeignKeyAttribute)))
                                                        .Select(q => q.Name.PluralizeString())
                                                        .ToList();
                // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                dbGraph.AddTable(tableName, propsWithForeignKeyAttr);
            }
        }

        private void SaveTable(string tableName, IEnumerable<object> table)
        {
            var tableData = JsonConvert.SerializeObject(
                table,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            fileUtility.WriteDataToFileAsJson(RootFolder, tableName, ".json", tableData);
        }
        // </SaveChanges>

        // <Db_engine_helper_functions>
        private bool TableExistInRoot(string table) => SavedDbTables.Contains(table);

        private IEnumerable<string> SavedDbTables => Directory.GetFiles(RootFolder);

        private void LoadAllTable()
        {
            //InitListProperties();
            //var table = dbGraph.DependecyIteration();
            //table.Reset();
            //while (table.MoveNext())
            //{
            //    var tableType = ((IEnumerable<object>)this.GetValueFor(table.Current.TableName)).InnerType();   // Get the type of the table
            //    MethodInfo method = typeof(JsonDbEngineContext)     // Get the method from this instance which reads data
            //        .GetMethod("GetTableData", BindingFlags.NonPublic | BindingFlags.Instance);
            //    method = method.MakeGenericMethod(tableType);       // Set the generic of it
            //    var args = new Object[1];                           // Create args objech[] for the method
            //    args[0] = table.Current.TableName;                  // Set args[0] for the "GetTableData<T>(string table)" method
            //    var dbTable = method.Invoke(this, args);            // Invoke the method
            //    if( dbTable != null )
            //        this.SetValueFor(table.Current.TableName, dbTable); // Set the derived class table property with the data
            //}
        }

        protected IList<T> GetTableData<T>(string table)
        {
            var tableData = new List<T>();
            try
            {
                string jsonStr;
                using (var fs = new FileStream(RootFolder + "\\" + table + fileExtension,
                                FileMode.Open,
                                FileAccess.Read))
                {
                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                }
                tableData = JsonConvert.DeserializeObject<List<T>>(jsonStr);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            return tableData;
        }
        // </Db_engine_helper_functions>
    }
}
