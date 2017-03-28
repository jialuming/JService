using System;
using JService.Model;

namespace JService.Design
{
    public class DesignDataService : IDataService
    {
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