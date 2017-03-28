using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using JEntity;

namespace JService.Services
{
    class UDPPool : ISocketPool
    {
        private Dictionary<User, Socket> UdpPool = new Dictionary<User, Socket>();

        public void Add(User user, Socket Socket)
        {
            if (UdpPool == null) UdpPool = new Dictionary<User, Socket>();
            UdpPool.Add(user, Socket);
        }

        public Socket GetSocket(User user)
        {
            return UdpPool[user];
        }

        public void Remove(User user)
        {
            if (UdpPool.ContainsKey(user))
            {
                UdpPool.Remove(user);
            }
        }
    }
}
