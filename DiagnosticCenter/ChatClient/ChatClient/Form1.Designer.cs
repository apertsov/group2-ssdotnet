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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.надіслатиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зберегтиІсторіюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.вийтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginPanel.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.loginPanel.Controls.Add(this.label4);
            this.loginPanel.Controls.Add(this.label3);
            this.loginPanel.Controls.Add(this.userPassword);
            this.loginPanel.Controls.Add(this.userName);
            this.loginPanel.Controls.Add(this.LogOn);
            this.loginPanel.Location = new System.Drawing.Point(3, 3);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(287, 58);
            this.loginPanel.TabIndex = 18;
            // 
            // freeEntre
            // 
            this.freeEntre.AutoSize = true;
            this.freeEntre.Location = new System.Drawing.Point(197, 33);
            this.freeEntre.Name = "freeEntre";
            this.freeEntre.Size = new System.Drawing.Size(86, 17);
            this.freeEntre.TabIndex = 24;
            this.freeEntre.Text = "вільний вхід";
            this.freeEntre.UseVisualStyleBackColor = true;
            this.freeEntre.CheckedChanged += new System.EventHandler(this.freeEntre_CheckedChanged);
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
            this.LogOn.Size = new System.Drawing.Size(84, 20);
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
            this.panel.Controls.Add(this.menuStrip1);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(287, 383);
            this.panel.TabIndex = 20;
            this.panel.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 27);
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
            this.splitContainer1.Size = new System.Drawing.Size(281, 304);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 3);
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
            this.Messages.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Messages.FormattingEnabled = true;
            this.Messages.ItemHeight = 15;
            this.Messages.Location = new System.Drawing.Point(8, 16);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(139, 274);
            this.Messages.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 3);
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
            this.users.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.users.FormattingEnabled = true;
            this.users.ItemHeight = 15;
            this.users.Location = new System.Drawing.Point(8, 16);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(112, 274);
            this.users.TabIndex = 22;
            this.users.SelectedIndexChanged += new System.EventHandler(this.users_SelectedIndexChanged);
            // 
            // Private
            // 
            this.Private.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Private.AutoSize = true;
            this.Private.Location = new System.Drawing.Point(59, 363);
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
            this.toAll.Location = new System.Drawing.Point(6, 363);
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
            this.Message.Location = new System.Drawing.Point(6, 337);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(203, 20);
            this.Message.TabIndex = 18;
            // 
            // Send
            // 
            this.Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Send.Location = new System.Drawing.Point(213, 337);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(68, 20);
            this.Send.TabIndex = 17;
            this.Send.Text = "Надіслати";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(287, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.надіслатиToolStripMenuItem,
            this.зберегтиІсторіюToolStripMenuItem,
            this.toolStripMenuItem2,
            this.вийтиToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(38, 20);
            this.toolStripMenuItem1.Text = "Чат";
            // 
            // надіслатиToolStripMenuItem
            // 
            this.надіслатиToolStripMenuItem.Name = "надіслатиToolStripMenuItem";
            this.надіслатиToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.надіслатиToolStripMenuItem.Text = "Надіслати";
            this.надіслатиToolStripMenuItem.Click += new System.EventHandler(this.надіслатиToolStripMenuItem_Click);
            // 
            // зберегтиІсторіюToolStripMenuItem
            // 
            this.зберегтиІсторіюToolStripMenuItem.Name = "зберегтиІсторіюToolStripMenuItem";
            this.зберегтиІсторіюToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.зберегтиІсторіюToolStripMenuItem.Text = "Зберегти історію";
            this.зберегтиІсторіюToolStripMenuItem.Click += new System.EventHandler(this.зберегтиІсторіюToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem2.Text = "Шрифт";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // вийтиToolStripMenuItem
            // 
            this.вийтиToolStripMenuItem.Name = "вийтиToolStripMenuItem";
            this.вийтиToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.вийтиToolStripMenuItem.Text = "Вийти";
            this.вийтиToolStripMenuItem.Click += new System.EventHandler(this.вийтиToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(292, 389);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.loginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.CheckBox freeEntre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userPassword;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Button LogOn;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Messages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox users;
        private System.Windows.Forms.RadioButton Private;
        private System.Windows.Forms.RadioButton toAll;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem надіслатиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зберегтиІсторіюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вийтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}

