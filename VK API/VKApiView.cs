using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Windows.Forms;

namespace VK_API
{
    class VKApiView
    {
        public static void FillAccountInfo(XmlDocument result, Control.ControlCollection controls)
        {
            controls["textBoxID"].Text = Program.api.UserId;

            controls["textBoxFirstName"].Text = result["response"]["user"]["first_name"].InnerText;

            controls["textBoxLastName"].Text = result["response"]["user"]["last_name"].InnerText;

            controls["textBoxSex"].Text = (result["response"]["user"]["sex"].InnerText == "2") ? "муж" : "жен";

            if (result["response"]["user"].SelectSingleNode("bdate") == null)
            {
                controls["textBoxBdate"].Text = "Не определено";
            }
            else
            {
                string bdate = result["response"]["user"]["bdate"].InnerText;
                int countPoint = bdate.Split('.').Length - 1;

                controls["textBoxBdate"].Text = countPoint == 1 ? bdate + ".год не определен" : bdate;
            }
        }

        public static void FillFriendsTable(XmlDocument result,DataGridView dgw)
        {
            dgw.Rows.Clear();
            foreach(XmlNode node in result["response"].ChildNodes)
            {
                string userId = node["uid"].InnerText;
                string first_name = node["first_name"].InnerText;
                string last_name = node["last_name"].InnerText;

                string age = "---";
                if(node["bdate"] != null)
                {
                    string bdate = node["bdate"].InnerText;
                    int countPoint = bdate.Split('.').Length - 1;
                    if(countPoint == 2)
                    {
                        age = ((int)(DateTime.Now-Convert.ToDateTime(bdate)).TotalDays/365).ToString();
                    }
                }

                string sex = (node["sex"].InnerText == "2") ? "муж" : "жен";
                string status = (node["online"].InnerText == "0") ? "off" : "on";

                dgw.Rows.Add(userId, last_name, first_name, age, sex, status);
            }
        }
    }
}
