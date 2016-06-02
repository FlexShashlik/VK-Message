using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Collections.Specialized;

namespace VK_API
{
    class VKapi
    {
        private string _userId;
        private string _accessToken;

        public VKapi(string userId, string accessToken)
        {
            _userId = userId;
            _accessToken = accessToken;
        }

        public string UserId
        {
            get { return _userId; }
        }

        private XmlDocument ExecuteCommand(string command, NameValueCollection parameters)
        {
            XmlDocument result = new XmlDocument();

            string strParam = String.Empty;

            strParam = String.Join
                (
                "&",
                from item in parameters.AllKeys select item + "=" + parameters[item]
                );

            result.Load(String.Format("https://api.vkontakte.ru/method/{0}.xml?access_token={1}&{2}",command,_accessToken,strParam));

            return result;
        }

        public XmlDocument AccountGetInfo()
        {
            XmlDocument result;
            string command = "users.get";

            NameValueCollection parametr = new NameValueCollection();
            parametr["user_ids"] = _userId;
            parametr["fields"] = "first_name,last_name,bdate,sex";

            result = ExecuteCommand(command, parametr);

            return result;
        }

        public XmlDocument FriendsGet(string userId)
        {
            XmlDocument result;
            string command = "friends.get";
            NameValueCollection parametrs = new NameValueCollection();

            parametrs["user_id"] = userId;
            parametrs["fields"] = "first_name,last_name,bdate,sex,online";
            result = ExecuteCommand(command, parametrs);

            return result;
        }

        public XmlDocument MessageSend(string userId, string message)
        {
            XmlDocument result;
            string command = "messages.send";
            NameValueCollection parametrs = new NameValueCollection();
            
            parametrs["user_id"] = userId;
            parametrs["message"] = message;
            result = ExecuteCommand(command, parametrs);
            return result;
        }

        public XmlDocument MessageSendInChat(string chatId, string message)
        {
            XmlDocument result;
            string command = "messages.send";
            NameValueCollection parametrs = new NameValueCollection();

            parametrs["chat_id"] = chatId;
            parametrs["message"] = message;
            result = ExecuteCommand(command, parametrs);
            return result;
        }
    }
}
