using ActivacionProfetica.Module.DatabaseUpdate;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Linq;
using System.Text;

namespace ActivacionProfetica.Module.SharedKernel
{
    public abstract class BaseData
    {
        public Updater Updater { get; set; }

        public BaseData(Updater updater)
        {
            Updater = updater;
            Execute();
            SaveChanges();
        }

        public DataTable CSVRead(string csvName)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resources = assembly.GetManifestResourceNames();
            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("." + csvName));
            DataTable csvData = new DataTable();
            using (TextFieldParser csvReader = new TextFieldParser(assembly.GetManifestResourceStream(resourceName), Encoding.GetEncoding("iso-8859-1"), false))
            {
                csvReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                csvReader.SetDelimiters(";");
                csvReader.HasFieldsEnclosedInQuotes = false;

                string[] columns = csvReader.ReadFields();
                foreach (string column in columns)
                {
                    DataColumn csvColumn = new DataColumn(column);
                    csvColumn.AllowDBNull = true;
                    csvData.Columns.Add(csvColumn);
                }

                while (!csvReader.EndOfData)
                {
                    string[] rowData = csvReader.ReadFields();
                    for (int i = 0; i < rowData.Length; i++)
                    {
                        if (rowData[i] == "")
                        {
                            rowData[i] = null;
                        }
                    }
                    csvData.Rows.Add(rowData);
                }
            }
            return csvData;
        }

        public abstract void Execute();

        public ObjectType GetObjectByKey<ObjectType>(object key)
        {
            return Updater.ObjectSpace.GetObjectByKey<ObjectType>(key);
        }
        public ObjectType FindObject<ObjectType>(CriteriaOperator criteria)
        {
            return Updater.ObjectSpace.FindObject<ObjectType>(criteria);
        }

        public ObjectType CreateObject<ObjectType>()
        {
            return Updater.ObjectSpace.CreateObject<ObjectType>();
        }
        public XPQuery<T> Query<T>()
        {
            return Updater.Session.Query<T>();
        }

        public void SaveChanges()
        {
            Updater.ObjectSpace.CommitChanges();
        }
    }
}
