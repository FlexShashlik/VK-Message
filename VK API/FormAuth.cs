using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VK_API
{
    public partial class FormAuth : Form
    {
        string appID = "5470121";

        [Flags]
        private enum VkontakteScopeList
        {
            /// Пользователь разрешил отправлять ему уведомления. 
            notify = 1,
            /// Доступ к друзьям. 
            friends = 2,
            /// Доступ к фотографиям. 
            photos = 4,
            /// Доступ к аудиозаписям. 
            audio = 8,
            /// Доступ к видеозаписям. 
            video = 16,
            /// Доступ к предложениям (устаревшие методы). 
            offers = 32,
            /// Доступ к вопросам (устаревшие методы). 
            questions = 64,
            /// Доступ к wiki-страницам. 
            pages = 128,
            /// Добавление ссылки на приложение в меню слева. 
            link = 256,
            /// Доступ заметкам пользователя. 
            notes = 2048,
            /// (для Standalone-приложений) Доступ к расширенным методам работы с сообщениями. 
            messages = 4096,
            /// Доступ к обычным и расширенным методам работы со стеной. 
            wall = 8192,
            /// Доступ к документам пользователя. 
            docs = 131072

        }

        int appScope = (int)(VkontakteScopeList.audio|
            VkontakteScopeList.docs|
            VkontakteScopeList.friends|
            VkontakteScopeList.link|
            VkontakteScopeList.messages|
            VkontakteScopeList.notes|
            VkontakteScopeList.notify|
            VkontakteScopeList.offers|
            VkontakteScopeList.pages|
            VkontakteScopeList.photos|
            VkontakteScopeList.questions|
            VkontakteScopeList.video|
            VkontakteScopeList.wall);

        string GetAccessToken(string url)
        {
            string accessToken;
            string l = url.Split('#')[1];

            accessToken = l.Split('&')[0].Split('=')[1];

            return accessToken;
        }

        string GetUserID(string url)
        {
            string userID;
            string l = url.Split('#')[1];

            userID = l.Split('=')[3];

            return userID;
        }

        public FormAuth()
        {
            InitializeComponent();
        }

        private void FormAuth_Load(object sender, EventArgs e)
        {
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate(String.Format("http://api.vkontakte.ru/oauth/authorize?client_id={0}&scope={1}&display=popup&response_type=token",appID,appScope));
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textBoxAddres.Text = e.Url.ToString();

            if(textBoxAddres.Text.Contains("access_token") == true)
            {
                MessageBox.Show(GetAccessToken(e.Url.ToString()) + " " + GetUserID(e.Url.ToString()));

                Program.api = new VKapi(GetUserID(e.Url.ToString()),
                    GetAccessToken(e.Url.ToString()));

                this.Hide();

                Form fw = new FormWork();
                fw.Show();
            }
        }
    }
}
