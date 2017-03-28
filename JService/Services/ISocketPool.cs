using JEntity;
using System.Net.Sockets;

namespace JService.Services
{
    public interface ISocketPool
    {
        void Add(User user, Socket Socket);
        void Remove(User user);
        Socket GetSocket(User user);
    }
}
