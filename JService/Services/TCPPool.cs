using JEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JService.Services
{
    class TCPPool : ISocketPool
    {
        private Dictionary<User, Socket> TcpPool = new Dictionary<User, Socket>();

        public void Add(User user, Socket Socket)
        {
            if (TcpPool == null) TcpPool = new Dictionary<User, Socket>();
            TcpPool.Add(user, Socket);
        }

        public Socket GetSocket(User user)
        {
            return TcpPool[user];
        }

        public void Remove(User user)
        {
            if (TcpPool.ContainsKey(user))
            {
                TcpPool.Remove(user);
            }
        }
    }
}
