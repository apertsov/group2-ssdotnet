﻿namespace ChatClient
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.Messages = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.users = new System.Windows.Forms.ListBox();
            this.Private = new System.Windows.Forms.RadioButton();
            this.toAll = new System.Windows.Forms.RadioButton();
            this.Message = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.loginPanel.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.loginPanel.Location = new System.Drawing.Point(2, 3);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(359, 58);
            this.loginPanel.TabIndex = 18;
            // 
            // freeEntre
            // 
            this.freeEntre.AutoSize = true;
            this.freeEntre.Location = new System.Drawing.Point(263, 10);
            this.freeEntre.Name = "freeEntre";
            this.freeEntre.Size = new System.Drawing.Size(86, 17);
            this.freeEntre.TabIndex = 24;
            this.freeEntre.Text = "вільний вхід";
            this.freeEntre.UseVisualStyleBackColor = true;
            this.freeEntre.CheckedChanged += new System.EventHandler(this.freeEntre_CheckedChanged);
            // 
            // Register
            // 
            this.Register.Location = new System.Drawing.Point(197, 33);
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
            this.label4.Location = new System.Drawing.Point(3, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Логін";
            // 
            // userPassword
            // 
            this.userPassword.Location = new System.Drawing.Point(49, 33);
            this.userPassword.Name = "userPassword";
            this.userPassword.PasswordChar = '*';
            this.userPassword.Size = new System.Drawing.Size(142, 20);
            this.userPassword.TabIndex = 20;
            this.userPassword.UseWaitCursor = true;
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(49, 7);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(142, 20);
            this.userName.TabIndex = 19;
            // 
            // LogOn
            // 
            this.LogOn.Location = new System.Drawing.Point(197, 7);
            this.LogOn.Name = "LogOn";
            this.LogOn.Size = new System.Drawing.Size(60, 20);
            this.LogOn.TabIndex = 18;
            this.LogOn.Text = "Увійти";
            this.LogOn.UseVisualStyleBackColor = true;
            this.LogOn.Click += new System.EventHandler(this.LogOn_Click);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.splitContainer1);
            this.panel.Controls.Add(this.Private);
            this.panel.Controls.Add(this.toAll);
            this.panel.Controls.Add(this.Message);
            this.panel.Controls.Add(this.Send);
            this.panel.Location = new System.Drawing.Point(2, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(359, 264);
            this.panel.TabIndex = 19;
            this.panel.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.Messages);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.users);
            this.splitContainer1.Size = new System.Drawing.Size(353, 209);
            this.splitContainer1.SplitterDistance = 183;
            this.splitContainer1.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Повідомлення";
            // 
            // Messages
            // 
            this.Messages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Messages.FormattingEnabled = true;
            this.Messages.Location = new System.Drawing.Point(3, 16);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(177, 186);
            this.Messages.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "користувачі в чаті";
            // 
            // users
            // 
            this.users.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.users.FormattingEnabled = true;
            this.users.Location = new System.Drawing.Point(3, 16);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(160, 186);
            this.users.TabIndex = 22;
            this.users.SelectedIndexChanged += new System.EventHandler(this.users_SelectedIndexChanged);
            // 
            // Private
            // 
            this.Private.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Private.AutoSize = true;
            this.Private.Location = new System.Drawing.Point(59, 244);
            this.Private.Name = "Private";
            this.Private.Size = new System.Drawing.Size(72, 17);
            this.Private.TabIndex = 23;
            this.Private.Text = "приватно";
            this.Private.UseVisualStyleBackColor = true;
            // 
            // toAll
            // 
            this.toAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toAll.AutoSize = true;
            this.toAll.Checked = true;
            this.toAll.Location = new System.Drawing.Point(6, 244);
            this.toAll.Name = "toAll";
            this.toAll.Size = new System.Drawing.Size(47, 17);
            this.toAll.TabIndex = 22;
            this.toAll.TabStop = true;
            this.toAll.Text = "всім";
            this.toAll.UseVisualStyleBackColor = true;
            // 
            // Message
            // 
            this.Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Message.Location = new System.Drawing.Point(6, 218);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(275, 20);
            this.Message.TabIndex = 18;
            // 
            // Send
            // 
            this.Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Send.Location = new System.Drawing.Point(285, 218);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(68, 20);
            this.Send.TabIndex = 17;
            this.Send.Text = "Надіслати";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(367, 272);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.loginPanel);
            this.MinimumSize = new System.Drawing.Size(0, 104);
            this.Name = "Form1";
            this.Text = "Чат";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Messages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox users;
    }
}

