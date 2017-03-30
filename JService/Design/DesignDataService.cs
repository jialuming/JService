using System;
using JService.Model;
using JEntity.WebService;

namespace JService.Design
{
    public class DesignDataService : IDataService
    {
        public bool CheckUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        //public void GetData(Action<DataItem, Exception> callback)
        //{
        //    // Use this to create design time data

        //    var item = new DataItem("Welcome to MVVM Light [design]");
        //    callback(item, null);
        //}
        public void GetMessage(MessageInfo messageResult)
        {
            throw new NotImplementedException();
        }
    }
}