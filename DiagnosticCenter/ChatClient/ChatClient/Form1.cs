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

namespace ChatClient
{


    public partial class Form1 : Form
    {
        private IChatService chat;
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
            Messages.Items.Add( "(" + name + "): " + msg );
        }

        public void ReceivePrivate(string name, string msg)
        {
            Messages.Items.Add("[" + name + "]: " + msg);
        }


        private void Entre(bool forRegister)
        {
            //Создаем объект который отвечает за обратную связь  
            InstanceContext context = new InstanceContext(new ChatCallbackHandler(this));
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            DuplexChannelFactory<IChatService> factory = new DuplexChannelFactory<IChatService>(context, binding);
            Uri adress = new Uri("net.tcp://localhost:20010/ChatService");
            EndpointAddress endpoint = new EndpointAddress(adress.ToString());
            //Связь с сервером не устанавливается до тех пор, пока не будет вызван метод Join  
            chat = factory.CreateChannel(endpoint);
            string name = userName.Text;
            List<string> userInChat = null;
            userInChat = forRegister ?
                chat.Register(name, userPassword.Text)
            :
                chat.Join(name, freeEntre.Checked ? null : userPassword.Text);

            if (userInChat == null)
            {
                MessageBox.Show( freeEntre.Checked ? "Логін уже зайнятий" : "Не вірний логін або пароль" );
                return;
            }

            foreach (string item in userInChat)
            {
                users.Items.Add(item);
            }

            target = userName.Text;
            Private.Text = "собі";

            loginPanel.Hide();
            panel.Show();
        }


        private void LogOn_Click(object sender, EventArgs e)
        {
            Entre(false);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(chat!=null) chat.Leave();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if(toAll.Checked)
                chat.Send(Message.Text);
            else
                chat.SendPrivate( target, Message.Text );
        }

        private void PrivateSend_Click(object sender, EventArgs e)
        {

            
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

        private void Register_Click(object sender, EventArgs e)
        {
            Entre( true );
        }

        private void freeEntre_CheckedChanged(object sender, EventArgs e)
        {
            userPassword.Enabled = !freeEntre.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
