using JDBService.DAO;
using JDBService.DAO.Login;
using JService.Services;
using System;

namespace JService.Model
{
    public class DataService : IDataService
    {
        DBService db = new DBService();
        public bool CheckUser(string userName, string password)
        {
            return db.HasUser(userName, password);
        }

        public void GetMessage(JEntity.WebService.MessageInfo messageResult)
        {
            throw new NotImplementedException();
        }
    }
}