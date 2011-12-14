using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using ChatCore;
using System.IO;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private IChatService chat = null;

        private string target;

        public Form1()
        {
            InitializeComponent();
        }

        public void EntreUser( string name )
        {
            users.Items.Add(name);
        }

        public void LeaveUser(string name)
        {
            users.Items.Remove(name);
        }

        public void Receive(string name, string msg)
        {
            string text = msg.Replace("|", "(" + name + ")" );
            Messages.Items.Add( text );
        }

        public void ReceivePrivate(string name, string msg)
        {
            string text = msg.Replace("|", "[" + name + "]");
            Messages.Items.Add(text);
        }

        private void SendMsg()
        {
            string msg = DateTime.Now.ToLongTimeString().ToString() + " | " + Message.Text;
            if(toAll.Checked)
                chat.Send( msg );
            else
                chat.SendPrivate( target, msg );
        }

        private void Entre()
        {
            // Створення об'єкту що відповідатиме за зворотній звязок
            try
            {
                InstanceContext context = new InstanceContext(new ChatCallbackHandler(this));
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                DuplexChannelFactory<IChatService> factory = new DuplexChannelFactory<IChatService>(context, binding);
                Uri adress = new Uri(new StreamReader("uri.txt").ReadLine());
                EndpointAddress endpoint = new EndpointAddress(adress.ToString());

                chat = factory.CreateChannel(endpoint);
                string name = userName.Text;
                List<string> userInChat = null;
                userInChat = chat.Join(name, freeEntre.Checked ? null : userPassword.Text);

                if (userInChat == null)
                {
                    MessageBox.Show(freeEntre.Checked ? "Логін уже зайнятий" : "Не вірний логін або пароль");
                    return;
                }

                foreach (string item in userInChat) users.Items.Add(item);
            }
            catch (Exception e)
            {
                MessageBox.Show( "Проблема при звернені до сервера" );
                chat = null;
                Close();
            }
            target = userName.Text;
            Private.Text = "собі";

            loginPanel.Hide();
            panel.Show();
            Text = userName.Text;

            this.Height += 300;
        }


        private void LogOn_Click(object sender, EventArgs e)
        {
            Entre();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(chat!=null) chat.Leave();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            SendMsg();
        }

        private void users_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (users.SelectedIndex < 0)
            {
                target = userName.Text;
                Private.Text = "собі";
            }
            else
            {
                target = target = users.Items[users.SelectedIndex] as string;
                Private.Text = "приватно: " + target;
            }
        }

        private void freeEntre_CheckedChanged(object sender, EventArgs e)
        {
            userPassword.Enabled = !freeEntre.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Height = loginPanel.Height;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void надіслатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMsg();
        }

        private void зберегтиІсторіюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Зберігти історію в ...";
            sfd.Filter = "Текстові файли (*.txt)|*.txt | Всі файли|*.*";
            if( sfd.ShowDialog() == DialogResult.Cancel)
                return;

            StreamWriter sw = File.CreateText( sfd.FileName );
            foreach (string line in Messages.Items)
                sw.WriteLine(line);
            sw.Close();
        }

        private void вийтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if( fd.ShowDialog() == DialogResult.Cancel)
                return;
            users.Font = Messages.Font = fd.Font;
        }
    }
    
    [
        CallbackBehaviorAttribute
        (
            IncludeExceptionDetailInFaults = true,
            UseSynchronizationContext = true,
            ValidateMustUnderstand = true
        )
    ]
    public class ChatCallbackHandler : IChatCallback
    {
        private Form1 form;
        public ChatCallbackHandler(Form1 f)
        { form = f; }

        public void Receive(string name, string msg)
        { form.Receive(name, msg); }

        public void ReceivePrivate(string name, string msg)
        { form.ReceivePrivate(name, msg); }

        public void UserEnter(string name)
        { form.EntreUser(name); }

        public void UserLeave(string name)
        { form.LeaveUser(name); }
    }
}
