using JService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JService.Model
{
    public interface IDataService
    {
        void GetMessage(MessageInfo messageResult);
    }
}
