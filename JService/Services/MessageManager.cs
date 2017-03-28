using JDBService.DAO;
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
        private static MessageManager _instence;

        public static MessageManager Instence
        {
            get
            {
                return _instence ?? (_instence = new MessageManager());
            }
        }
        private Socket _mainTcp;

        public Socket MainTcp
        {
            get { return _mainTcp; }
            set { _mainTcp = value; }
        }

        private Socket _mainUdp;

        public Socket MainUdp
        {
            get { return _mainUdp; }
            set { _mainUdp = value; }
        }

        public MessageInfo MessageAnalysis(byte[] bytes, int length)
        {

            var message = Encoding.Unicode.GetString(bytes, 0, length);
            MessageInfo Result = Deserialize<MessageInfo>(message);
            return Result;
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
        public void MessageSend(Socket socket, byte[] bytes)
        {
            socket.Send(bytes, SocketFlags.None);
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public MessageSendState MessageSend(byte[] bytes)
        {
            //socket.Send(bytes, SocketFlags.None);
            return MessageSendState.Failed;
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public MessageSendState MessageSend(string userName, string password)
        {
            if (MainTcp == null)
            {
                _mainTcp = new TCPService().StartSocket();
            }
            if (MainTcp != null && MainTcp.Connected)
            {
                byte[] bytes = Encoding.Unicode.GetBytes("1." + userName + "|" + password);
                MainTcp.Send(bytes);
                return MessageSendState.Succeed;
            }
            return MessageSendState.Failed;
        }
        /// <summary>
        /// 回应
        /// </summary>
        /// <param name="socket"></param>
        public void Response(Socket socket)
        {

        }

    }
}
