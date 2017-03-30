using JEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDBService.DAO
{
    public class DBService : IDBService
    {
        //禁止实例化
        public DBService()
        {

        }
        public static string ConnectionString
        {
            get
            {
                string reval = "Data Source=.;Initial Catalog=JChat;Integrated Security=True"; //这里可以动态根据cookies或session实现多库切换
                return reval;
            }
        }
        public static SqlSugarClient GetInstance()
        {
            var db = new SqlSugarClient(ConnectionString);
            db.IsEnableLogEvent = true;//启用日志事件
            db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par + "\r\n"); };
            return db;
        }

        public bool HasUser(string userName, string password)
        {
            using (var db = GetInstance())
            {
                return GetInstance().Queryable<User>().Any(c => c.UserID == userName && c.Password == password);
            }
        }

        public bool Delete<T>(List<string> pKey) where T : EntityBase, new()
        {
            using (var db = GetInstance())
            {
                return GetInstance().Delete<T, string>(pKey.ToArray());
            }
        }

        public bool Delete<T>(T t) where T : EntityBase, new()
        {
            using (var db = GetInstance())
            {
                return GetInstance().Delete(t);
            }
        }

        public List<T> GetData<T>() where T : EntityBase, new()
        {
            using (var db = GetInstance())
            {
                return GetInstance().Queryable<T>().ToList();
            }
        }
        public object Insert<T>(List<T> list) where T : EntityBase, new()
        {
            using (var db = GetInstance())
            {
                return GetInstance().Insert(list);
            }
        }

        public object Insert<T>(T t) where T : EntityBase, new()
        {
            using (var db = GetInstance())
            {
                return GetInstance().Insert(t);
            }
        }

        public bool Update<T>(List<string> pKey, T t) where T : EntityBase, new()
        {
            using (var db = GetInstance())
            {
                return GetInstance().Update<T, string>(t, pKey.ToArray());
            }
        }

        public bool Update<T>(T t) where T : EntityBase, new()
        {
            using (var db = GetInstance())
            {
                return GetInstance().Update(t);
            }
        }
    }
}
