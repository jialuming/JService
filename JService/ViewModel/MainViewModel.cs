using GalaSoft.MvvmLight;
using JEntity.WebService;
using JService.Model;
using JService.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;

namespace JService.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly MessageManager _messageManager;

        #region DependencyProperty
        /// <summary>
        /// The <see cref="SocketList" /> property's name.
        /// </summary>
        public const string SocketListPropertyName = "SocketList";

        private ObservableCollection<KeyValuePair<string, Socket>> _socketList = new ObservableCollection<KeyValuePair<string, Socket>>();

        /// <summary>
        /// Sets and gets the SocketList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<KeyValuePair<string, Socket>> SocketList
        {
            get
            {
                return _socketList;
            }

            set
            {
                if (_socketList == value)
                {
                    return;
                }

                _socketList = value;
                RaisePropertyChanged(SocketListPropertyName);
            }
        }
        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _messageManager = MessageManager.Instence;
            TCPService tcp = new TCPService();
            tcp.StartSocket();
            _messageManager.GetMessage += GetMessage;
            _messageManager.AliveSocketPool.SocketPoolChanged += AliveSocketPool_SocketPoolChanged;
            SocketList = new ObservableCollection<KeyValuePair<string, Socket>>(_messageManager.AliveSocketPool);
        }

        private void AliveSocketPool_SocketPoolChanged()
        {
            SocketList = new ObservableCollection<KeyValuePair<string, Socket>>(_messageManager.AliveSocketPool);
        }

        private void GetMessage(System.Net.Sockets.Socket socket, MessageInfo messageInfo)
        {
            switch (messageInfo.MessageType)
            {
                case MessageType.Alive:
                    break;
                case MessageType.CheckUser:
                    CheckUser(socket, messageInfo);
                    break;
                case MessageType.SendMessage:
                    break;
                case MessageType.GetFriendList:
                    break;
                case MessageType.GetUserInfo:
                    break;
                case MessageType.Request:
                    break;
                default:
                    break;
            }
        }

        private void CheckUser(System.Net.Sockets.Socket socket, MessageInfo messageInfo)
        {
            if (messageInfo.MessageText.Result == 2 && socket.Connected)
            {
                if (_messageManager.AliveSocketPool.ContainsKey(messageInfo.UserID))
                {
                    var oldSocket = _messageManager.AliveSocketPool[messageInfo.UserID];
                    MessageInfo oldMsgInfo = new MessageInfo(null, MessageType.CheckUser, messageInfo.UserID, 0, 0, new MessageText() { Result = 3 });

                    //通知下线
                    _messageManager.AliveSocketPool[messageInfo.UserID] = socket;
                    _messageManager.MessageSend(oldSocket, oldMsgInfo);
                }
                else
                {
                    _messageManager.AliveSocketPool.Add(messageInfo.UserID, socket);
                }
            }
            else
            {
                if (_dataService.CheckUser(messageInfo.UserID, messageInfo.MessageText.P))
                {
                    messageInfo.MessageText = new MessageText();
                    messageInfo.MessageText.Result = 1;
                }
                else
                {
                    messageInfo.MessageText = new MessageText();
                    messageInfo.MessageText.Result = -1;
                }
                MessageManager.Instence.MessageSend(socket, messageInfo);
            }

        }
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}