using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Nadam.Lib.DatabaseGraphs;

namespace Nadam.Lib.JsonDb
{
    public abstract class JsonDbEngineContext
    {
        public FileUtility FileUtility { get; set; }

        public readonly string RootFolder;
        protected readonly bool Inmemory = true;

        private readonly DatabaseGraph _dbGraph;
        private Dictionary<int, IEnumerable<object>> _dbData;

        private IEnumerable<PropertyInfo> _inmemoryDbTableStructure;

        protected readonly DeferredExecutionPlans ExePlan =
            DeferredExecutionPlans.LazyLoading;   // other execution plan will be implemented later

        #region <constructors>
        protected JsonDbEngineContext(string configName)
        {
            RootFolder = ConfigurationManager.AppSettings[configName];
            FileUtility = new FileUtility();
            _dbGraph = new DatabaseGraph();

            var dbContext = this.GetType();
            BuildDatabaseGraph(dbContext);
        }

        protected JsonDbEngineContext(string configName, bool inmemory) : this(configName)
        {
            Inmemory = inmemory;
        }
        #endregion </constructors>

        // <SaveChanges>
        public void SaveChanges(object derived)
        {
            MapDbGraphToData(ref derived);
            // geting the derived class properties which are the db tables and puting them into a dictionary with their name
            Type myType = derived.GetType();
            var tables = new Dictionary<string, IEnumerable<object>>();

            //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            //foreach (var table in props.Where(p => p.PropertyType.Name.Contains("IList")))    // TODO define better selection insead of '.Contains("IEnumerable")'
            //foreach (var table in _inmemoryDbTableStructure)
            foreach (var table in _dbData)
            {
                var tableName = _dbGraph.FindByNodeId(table.Key).Value;
                if (!TableExistInRoot(tableName))
                {
                    FileUtility.CreateFile(RootFolder, tableName, ".json");
                }
                var tableRows = table.Value;
                tables.Add(tableName, tableRows);
            }

            // get the table type, and determine if it has any foreign key defined (have any navigation prop)
            foreach (var data in tables.Where(p => p.Value != null && p.Value.Any()).ToList())
            {
                var tableRowEntity = data.Value?.First();
                if (tableRowEntity == null)
                    continue;

                var propsWithForeignKeyAttr = tableRowEntity
                    .GetType()
                    .GetProperties()
                    .Where(prop => Attribute.IsDefined(prop, typeof(ForeignKeyAttribute)));

                //dbGraph.AddTable(data.Key, propsWithForeignKeyAttr.Select(p => p.Name));

                var haveForeignKey = propsWithForeignKeyAttr.Any();

                // here we enumerate the tables
                foreach (var tableRow in data.Value)
                {
                    // Here we set ForeignKeys for records from the navigation property
                    if (haveForeignKey)
                    {
                        foreach (var foreignKey in propsWithForeignKeyAttr)
                        {
                            var foreignKeyNavigationPropValue = tableRow.GetValueFor(foreignKey.Name);
                            if (foreignKeyNavigationPropValue != null)
                            {
                                object[] attrs = foreignKey.GetCustomAttributes(true);
                                var foreignKeyAttr = attrs[0] as ForeignKeyAttribute;
                                var foreignKeyPropertyName = foreignKeyAttr.Name;

                                tableRow.SetValueFor(foreignKeyPropertyName,
                                    foreignKeyNavigationPropValue.GetValueFor("Id"));
                            }
                        }
                    }
                }
            }

            // now we can write the data to files
            foreach (var table in tables)
            {
                table.Value.SetIdsFor();
                SaveTable(table.Key, table.Value.MakeVirtualPropertiesNull());
            }
        }

        public virtual void SaveChanges()
        {
            SaveChanges(this);
        }

        protected void BuildDatabaseGraph(object dbContext)
        {
            Type myType = this.GetType();
            BuildDatabaseGraph(myType);
        }

        protected void BuildDatabaseGraph(Type dbContextType)
        {

            _inmemoryDbTableStructure = new List<PropertyInfo>(dbContextType.GetProperties()
                                                                           .Where(p => p.PropertyType.Name.Contains("IList"))
                                                                           .ToList());

            foreach (var tableProperty in _inmemoryDbTableStructure)
            {
                var tableName = tableProperty.Name;

                var propsWithForeignKeyAttr = tableProperty.PropertyType.GetGenericArguments()[0]
                    .GetProperties()
                    .Where(prop => Attribute.IsDefined(prop, typeof(ForeignKeyAttribute)))
                    .Select(q => q.Name.PluralizeString())
                    .ToList();

                _dbGraph.AddTable(tableName, propsWithForeignKeyAttr);
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

            FileUtility.WriteDataToFileAsJson(RootFolder, tableName, ".json", tableData);
        }

        private void MapDbGraphToData(ref object dbContext)
        {
            _dbData = new Dictionary<int, IEnumerable<object>>();
            foreach (var tableNode in _dbGraph)
            {
                var tableData = (IEnumerable<object>)dbContext.GetValueFor(tableNode.Value);
                _dbData.Add(tableNode.NodeId, tableData);
            }
        }
        // </SaveChanges>

        // <Db_engine_helper_functions>
        private bool TableExistInRoot(string table)
        {
            return SavedDbTables.Contains(table);
        }

        private IEnumerable<string> SavedDbTables => Directory.GetFiles(RootFolder);

        protected IList<T> GetTable<T>(string table)
        {
            try
            {
                string jsonStr;
                using (var fs = new FileStream(RootFolder + "\\" + table + ".json",
                    FileMode.Open,
                    FileAccess.Read))
                {
                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                }
                return JsonConvert.DeserializeObject<List<T>>(jsonStr);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
        // </Db_engine_helper_functions>
    }
}
