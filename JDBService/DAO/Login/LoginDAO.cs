using JEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDBService.DAO.Login
{
    public class LoginDAO
    {
        SqlSugarClient db = DBService.GetInstance();
        public User QueryLogin(User user)
        {
            return db.Queryable<User>().Single(c => c.UserID == user.UserID);
        }

    }
}
