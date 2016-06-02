using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;

namespace VK_API
{
    public partial class FormWork : Form
    {
        int iIndex;

        public FormWork()
        {
            InitializeComponent();
        }

        private void FormWork_Load(object sender, EventArgs e)
        {
            
        }

        private void FormWork_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            buttonSend.Enabled = false;
            MessageBox.Show("Выберите получателя");
            if (textBoxNumberChat.Text != "")
            {
                buttonSend.Enabled = true;
                string message = richTextBoxText.Text;
                string chatId = textBoxNumberChat.Text;

                Program.api.MessageSendInChat(chatId, message);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            iIndex = e.RowIndex;
            
                dataGridView1.Rows[iIndex].Cells[0].Selected = false;

                string UserId = dataGridView1.Rows[iIndex].Cells[0].Value.ToString();
                string message = richTextBoxText.Text;

                Program.api.MessageSend(UserId, message);
                dataGridView1.Rows[iIndex].Cells[0].Selected = true;
                buttonSend.Enabled = true;
                MessageBox.Show("Отправлено");
        }

        private void FormWork_Shown(object sender, EventArgs e)
        {
            VKApiView.FillAccountInfo(Program.api.AccountGetInfo(), this.Controls);

            VKApiView.FillFriendsTable(Program.api.FriendsGet(Program.api.UserId), dataGridView1);
        }
    }
}
