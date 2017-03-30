using JEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JService.Services
{
    public class SocketPool : ISocketPool
    {
        public delegate void SocketPoolHandler();
        public event SocketPoolHandler SocketPoolChanged;
        private Dictionary<string, Socket> TcpPool = new Dictionary<string, Socket>();
        public SocketPool()
        {
        }
        public Socket this[string key]
        {
            get { return TcpPool[key]; }
            set { TcpPool[key] = value; SocketPoolChanged(); }
        }

        public int Count
        {
            get { return TcpPool.Count; }
        }

        public ICollection<string> Keys
        {
            get { return TcpPool.Keys; }
        }
        public ICollection<Socket> Values
        {
            get { return TcpPool.Values; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(KeyValuePair<string, Socket> item)
        {
            TcpPool.Add(item.Key, item.Value);
            SocketPoolChanged();
        }

        public void Add(string user, Socket Socket)
        {
            if (TcpPool == null) TcpPool = new Dictionary<string, Socket>();
            TcpPool.Add(user, Socket);
            SocketPoolChanged();
        }

        public void Clear()
        {
            TcpPool.Clear();
            SocketPoolChanged();
        }

        public bool Contains(KeyValuePair<string, Socket> item)
        {
            return TcpPool.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return TcpPool.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Socket>[] array, int arrayIndex)
        {
        }

        public IEnumerator<KeyValuePair<string, Socket>> GetEnumerator()
        {
            return TcpPool.GetEnumerator(); ;
        }

        public bool Remove(string key)
        {
            if (TcpPool.Remove(key))
            {
                SocketPoolChanged();
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<string, Socket> item)
        {
            if (TcpPool[item.Key].Equals(item.Value))
            {
                SocketPoolChanged();
                if (TcpPool.Remove(item.Key))
                {
                    SocketPoolChanged();
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValue(string key, out Socket value)
        {
            return TcpPool.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

    }
}
