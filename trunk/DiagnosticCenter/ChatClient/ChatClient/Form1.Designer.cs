namespace ChatClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.loginPanel = new System.Windows.Forms.Panel();
            this.freeEntre = new System.Windows.Forms.CheckBox();
            this.Register = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userPassword = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.LogOn = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.Private = new System.Windows.Forms.RadioButton();
            this.toAll = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Messages = new System.Windows.Forms.ListBox();
            this.Message = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.users = new System.Windows.Forms.ListBox();
            this.loginPanel.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Chat";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // loginPanel
            // 
            this.loginPanel.Controls.Add(this.freeEntre);
            this.loginPanel.Controls.Add(this.Register);
            this.loginPanel.Controls.Add(this.label4);
            this.loginPanel.Controls.Add(this.label3);
            this.loginPanel.Controls.Add(this.userPassword);
            this.loginPanel.Controls.Add(this.userName);
            this.loginPanel.Controls.Add(this.LogOn);
            this.loginPanel.Location = new System.Drawing.Point(6, 12);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(359, 66);
            this.loginPanel.TabIndex = 18;
            // 
            // freeEntre
            // 
            this.freeEntre.AutoSize = true;
            this.freeEntre.Location = new System.Drawing.Point(270, 17);
            this.freeEntre.Name = "freeEntre";
            this.freeEntre.Size = new System.Drawing.Size(86, 17);
            this.freeEntre.TabIndex = 24;
            this.freeEntre.Text = "вільний вхід";
            this.freeEntre.UseVisualStyleBackColor = true;
            this.freeEntre.CheckedChanged += new System.EventHandler(this.freeEntre_CheckedChanged);
            // 
            // Register
            // 
            this.Register.Location = new System.Drawing.Point(204, 40);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(109, 20);
            this.Register.TabIndex = 23;
            this.Register.Text = "Зареєструватися";
            this.Register.UseVisualStyleBackColor = true;
            this.Register.Click += new System.EventHandler(this.Register_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Логін";
            // 
            // userPassword
            // 
            this.userPassword.Location = new System.Drawing.Point(56, 40);
            this.userPassword.Name = "userPassword";
            this.userPassword.Size = new System.Drawing.Size(142, 20);
            this.userPassword.TabIndex = 20;
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(56, 14);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(142, 20);
            this.userName.TabIndex = 19;
            // 
            // LogOn
            // 
            this.LogOn.Location = new System.Drawing.Point(204, 14);
            this.LogOn.Name = "LogOn";
            this.LogOn.Size = new System.Drawing.Size(60, 20);
            this.LogOn.TabIndex = 18;
            this.LogOn.Text = "Увійти";
            this.LogOn.UseVisualStyleBackColor = true;
            this.LogOn.Click += new System.EventHandler(this.LogOn_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.Private);
            this.panel.Controls.Add(this.toAll);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.Messages);
            this.panel.Controls.Add(this.Message);
            this.panel.Controls.Add(this.Send);
            this.panel.Controls.Add(this.users);
            this.panel.Location = new System.Drawing.Point(6, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(359, 216);
            this.panel.TabIndex = 19;
            this.panel.Visible = false;
            // 
            // Private
            // 
            this.Private.AutoSize = true;
            this.Private.Location = new System.Drawing.Point(257, 186);
            this.Private.Name = "Private";
            this.Private.Size = new System.Drawing.Size(72, 17);
            this.Private.TabIndex = 23;
            this.Private.Text = "приватно";
            this.Private.UseVisualStyleBackColor = true;
            // 
            // toAll
            // 
            this.toAll.AutoSize = true;
            this.toAll.Checked = true;
            this.toAll.Location = new System.Drawing.Point(204, 186);
            this.toAll.Name = "toAll";
            this.toAll.Size = new System.Drawing.Size(47, 17);
            this.toAll.TabIndex = 22;
            this.toAll.TabStop = true;
            this.toAll.Text = "всім";
            this.toAll.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "користувачі в чаті";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Повідомлення";
            // 
            // Messages
            // 
            this.Messages.FormattingEnabled = true;
            this.Messages.Location = new System.Drawing.Point(5, 26);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(193, 147);
            this.Messages.TabIndex = 19;
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(5, 186);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(119, 20);
            this.Message.TabIndex = 18;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(130, 186);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(68, 20);
            this.Send.TabIndex = 17;
            this.Send.Text = "Надіслати";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // users
            // 
            this.users.FormattingEnabled = true;
            this.users.Location = new System.Drawing.Point(204, 26);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(109, 147);
            this.users.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 233);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.loginPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Чат";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.CheckBox freeEntre;
        private System.Windows.Forms.Button Register;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userPassword;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Button LogOn;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.RadioButton Private;
        private System.Windows.Forms.RadioButton toAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Messages;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.ListBox users;
    }
}

