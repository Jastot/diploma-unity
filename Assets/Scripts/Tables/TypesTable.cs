using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;
using SQLite4Unity3d;

namespace Diploma.Tables
{
    public class TypesTable : IDataBase
    {
        public List<Types> GetAllData<Types>(SQLiteConnection connection)
        {
            List<Types> typesList = new List<Types>();
            typesList = connection.Table<Types>().ToList();
            
            return typesList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            return connection.Table<Types>().FirstOrDefault(x => x.Type_Id == id);
        }
        
        public ITable GetRecordByName(SQLiteConnection connection, string name)
        {
            return null;
        }

        public void AddNewRecord(SQLiteConnection connection, string[] typeParams)
        {
            if (typeParams[0] == null || typeParams[0]=="")
                typeParams[0] = "-1";
            var newType = new Types()
            {
                TypeS = typeParams[0]
            };
            connection.Insert(newType);
        }

        public void UpdateRecordById(SQLiteConnection connection, int id, string[] paramsToChange)
        {
            
        }

        public void DeleteLastRecord(SQLiteConnection connection, int id)
        {
            connection.Delete<Types>(id);
        }
    }


    public class Types : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int Type_Id { get; set; }
        public string TypeS { get; set; }
    }
}