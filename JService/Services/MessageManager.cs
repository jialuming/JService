using JDBService.DAO;
using JEntity.WebService;
using JService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace JService.Services
{
    public class MessageManager
    {
        public delegate void GetMessageHandler(Socket socket, MessageInfo messageInfo);
        public event GetMessageHandler GetMessage;
        private static MessageManager _instence;

        public static MessageManager Instence
        {
            get { return _instence ?? (_instence = new MessageManager()); }
        }

        private SocketPool _UDPSocketPool;

        public SocketPool UDPSocketPool
        {
            get { return _UDPSocketPool ?? (_UDPSocketPool = new SocketPool()); }
        }
        private SocketPool _aliveSocketPool;

        public SocketPool AliveSocketPool
        {
            get { return _aliveSocketPool ?? (_aliveSocketPool = new SocketPool()); }
        }

        public MessageManager()
        {
        }

        public void MessageAnalysis(Socket socket, byte[] bytes, int length)
        {
            var message = Encoding.Unicode.GetString(bytes, 0, length);
            MessageInfo Result = Deserialize<MessageInfo>(message);
            GetMessage?.Invoke(socket, Result);
        }
        // Json->Object
        public T Deserialize<T>(string json)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //执行反序列化
            T obj = jsonSerializer.Deserialize<T>(json);
            return obj;
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public void MessageSend(Socket socket, MessageInfo messageInfo)
        {
            string s = Serialize(messageInfo);
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            socket.Send(bytes, SocketFlags.None);
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public void MessageSend(MessageInfo messageInfo)
        {
            //string s = Serialize(messageInfo);
            //byte[] bytes = Encoding.Unicode.GetBytes(s);
            //socket.Send(bytes, SocketFlags.None);
        }
        /// <summary>
        /// 回应
        /// </summary>
        /// <param name="socket"></param>
        public void Response(Socket socket)
        {

        }

        public string Serialize(object obj)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(obj);
        }
    }
}
