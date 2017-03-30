using JEntity;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

namespace JService.Services
{
    public interface ISocketPool : IDictionary<string, Socket>, IEnumerable<KeyValuePair<string, Socket>>
    {
    }
}
