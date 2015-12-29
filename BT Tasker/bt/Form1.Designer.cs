namespace bt
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.idle = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.lblCash = new System.Windows.Forms.Label();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.wait = new System.Windows.Forms.Timer(this.components);
            this.updateTime = new System.Windows.Forms.Timer(this.components);
            this.lblStats = new System.Windows.Forms.Label();
            this.updateStats = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtGang = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRob = new System.Windows.Forms.CheckBox();
            this.cbRace = new System.Windows.Forms.CheckBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.lblIvnalidLogin = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPass = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnInfo = new System.Windows.Forms.Button();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.cbRus = new System.Windows.Forms.CheckBox();
            this.pbCaptcha = new System.Windows.Forms.PictureBox();
            this.lblcaptcha = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cbTF = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbTransfer = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.txtTransPin = new System.Windows.Forms.TextBox();
            this.txtTrans = new System.Windows.Forms.TextBox();
            this.txtTransName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnBank = new System.Windows.Forms.Button();
            this.cbCaptcha = new System.Windows.Forms.CheckBox();
            this.bgwServerCom = new System.ComponentModel.BackgroundWorker();
            this.gbLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.gbTransfer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 24);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(1038, 754);
            this.webBrowser1.TabIndex = 20;
            this.webBrowser1.Url = new System.Uri("http://biltjuv.se", System.UriKind.Absolute);
            this.webBrowser1.Visible = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowser1_PreviewKeyDown);
            // 
            // idle
            // 
            this.idle.Interval = 1000;
            this.idle.Tick += new System.EventHandler(this.idle_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Lime;
            this.lblStatus.Location = new System.Drawing.Point(174, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(121, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Nästa     00:00";
            this.lblStatus.MouseLeave += new System.EventHandler(this.lblStatus_MouseLeave);
            this.lblStatus.MouseHover += new System.EventHandler(this.lblStatus_MouseHover);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Black;
            this.btnPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPause.BackgroundImage")));
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPause.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.ForeColor = System.Drawing.Color.White;
            this.btnPause.Location = new System.Drawing.Point(89, 2);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(50, 20);
            this.btnPause.TabIndex = 8;
            this.btnPause.Text = "ıı";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCash.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCash.ForeColor = System.Drawing.Color.Lime;
            this.lblCash.Location = new System.Drawing.Point(761, 4);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(35, 14);
            this.lblCash.TabIndex = 9;
            this.lblCash.Text = "Cash";
            this.lblCash.Click += new System.EventHandler(this.lblCash_Click);
            // 
            // txtCash
            // 
            this.txtCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCash.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCash.ForeColor = System.Drawing.Color.White;
            this.txtCash.Location = new System.Drawing.Point(802, 2);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(79, 20);
            this.txtCash.TabIndex = 4;
            this.txtCash.Text = "600000000";
            this.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCash_KeyPress);
            // 
            // wait
            // 
            this.wait.Interval = 1000;
            this.wait.Tick += new System.EventHandler(this.wait_Tick);
            // 
            // updateTime
            // 
            this.updateTime.Enabled = true;
            this.updateTime.Interval = 10;
            this.updateTime.Tick += new System.EventHandler(this.updateTime_Tick);
            // 
            // lblStats
            // 
            this.lblStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStats.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStats.ForeColor = System.Drawing.Color.Lime;
            this.lblStats.Location = new System.Drawing.Point(320, 4);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(189, 17);
            this.lblStats.TabIndex = 11;
            this.lblStats.Text = "Levels/h      0";
            this.lblStats.MouseLeave += new System.EventHandler(this.lblStats_MouseLeave);
            this.lblStats.MouseHover += new System.EventHandler(this.lblStats_MouseHover);
            // 
            // updateStats
            // 
            this.updateStats.Interval = 60000;
            this.updateStats.Tick += new System.EventHandler(this.updateStats_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(687, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "Gäng";
            // 
            // txtGang
            // 
            this.txtGang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtGang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGang.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGang.ForeColor = System.Drawing.Color.White;
            this.txtGang.Location = new System.Drawing.Point(728, 2);
            this.txtGang.Name = "txtGang";
            this.txtGang.Size = new System.Drawing.Size(31, 20);
            this.txtGang.TabIndex = 3;
            this.txtGang.Text = "15";
            this.txtGang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGang.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGang_KeyPress);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial Black", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(89, 253);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(926, 136);
            this.label5.TabIndex = 22;
            this.label5.Text = "ACCESS DENIED";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // cbRob
            // 
            this.cbRob.AutoSize = true;
            this.cbRob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbRob.Enabled = false;
            this.cbRob.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRob.ForeColor = System.Drawing.Color.Lime;
            this.cbRob.Location = new System.Drawing.Point(570, 3);
            this.cbRob.Name = "cbRob";
            this.cbRob.Size = new System.Drawing.Size(54, 18);
            this.cbRob.TabIndex = 24;
            this.cbRob.Text = "Råna";
            this.cbRob.UseVisualStyleBackColor = false;
            this.cbRob.CheckedChanged += new System.EventHandler(this.cbRob_CheckedChanged);
            // 
            // cbRace
            // 
            this.cbRace.AutoSize = true;
            this.cbRace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbRace.Enabled = false;
            this.cbRace.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRace.ForeColor = System.Drawing.Color.Lime;
            this.cbRace.Location = new System.Drawing.Point(513, 3);
            this.cbRace.Name = "cbRace";
            this.cbRace.Size = new System.Drawing.Size(61, 18);
            this.cbRace.TabIndex = 25;
            this.cbRace.Text = "Rejsa";
            this.cbRace.UseVisualStyleBackColor = false;
            this.cbRace.CheckedChanged += new System.EventHandler(this.cbRace_CheckedChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Black;
            this.btnUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdate.BackgroundImage")));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(972, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(50, 20);
            this.btnUpdate.TabIndex = 27;
            this.btnUpdate.Text = "Upd.";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // gbLogin
            // 
            this.gbLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbLogin.Controls.Add(this.lblIvnalidLogin);
            this.gbLogin.Controls.Add(this.pictureBox1);
            this.gbLogin.Controls.Add(this.txtPass);
            this.gbLogin.Controls.Add(this.label2);
            this.gbLogin.Controls.Add(this.txtUser);
            this.gbLogin.Controls.Add(this.label1);
            this.gbLogin.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLogin.ForeColor = System.Drawing.Color.Lime;
            this.gbLogin.Location = new System.Drawing.Point(438, 278);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Size = new System.Drawing.Size(200, 111);
            this.gbLogin.TabIndex = 28;
            this.gbLogin.TabStop = false;
            this.gbLogin.Text = "Inloggning";
            // 
            // lblIvnalidLogin
            // 
            this.lblIvnalidLogin.AutoSize = true;
            this.lblIvnalidLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblIvnalidLogin.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIvnalidLogin.ForeColor = System.Drawing.Color.Red;
            this.lblIvnalidLogin.Location = new System.Drawing.Point(12, 86);
            this.lblIvnalidLogin.Name = "lblIvnalidLogin";
            this.lblIvnalidLogin.Size = new System.Drawing.Size(166, 12);
            this.lblIvnalidLogin.TabIndex = 13;
            this.lblIvnalidLogin.Text = "Fel användarnamn eller lösenord";
            this.lblIvnalidLogin.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(160, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 23);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.ForeColor = System.Drawing.Color.White;
            this.txtPass.Location = new System.Drawing.Point(55, 57);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '•';
            this.txtPass.Size = new System.Drawing.Size(100, 21);
            this.txtPass.TabIndex = 10;
            this.txtPass.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Lösen";
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.White;
            this.txtUser.Location = new System.Drawing.Point(55, 33);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 21);
            this.txtUser.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Namn";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(972, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(50, 20);
            this.progressBar1.TabIndex = 29;
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.Black;
            this.btnInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInfo.BackgroundImage")));
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInfo.ForeColor = System.Drawing.Color.White;
            this.btnInfo.Location = new System.Drawing.Point(918, 2);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(50, 20);
            this.btnInfo.TabIndex = 33;
            this.btnInfo.Text = "Info";
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.txtLog);
            this.gbLog.Controls.Add(this.cbLog);
            this.gbLog.Location = new System.Drawing.Point(15, 32);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(206, 701);
            this.gbLog.TabIndex = 34;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Händelselogg";
            this.gbLog.Visible = false;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(7, 44);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(193, 651);
            this.txtLog.TabIndex = 1;
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Location = new System.Drawing.Point(7, 20);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(65, 17);
            this.cbLog.TabIndex = 0;
            this.cbLog.Text = "Aktivera";
            this.cbLog.UseVisualStyleBackColor = true;
            // 
            // cbRus
            // 
            this.cbRus.AutoSize = true;
            this.cbRus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbRus.Enabled = false;
            this.cbRus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRus.ForeColor = System.Drawing.Color.Lime;
            this.cbRus.Location = new System.Drawing.Point(468, 3);
            this.cbRus.Name = "cbRus";
            this.cbRus.Size = new System.Drawing.Size(47, 18);
            this.cbRus.TabIndex = 35;
            this.cbRus.Text = "RUS";
            this.cbRus.UseVisualStyleBackColor = false;
            // 
            // pbCaptcha
            // 
            this.pbCaptcha.Location = new System.Drawing.Point(238, 34);
            this.pbCaptcha.Name = "pbCaptcha";
            this.pbCaptcha.Size = new System.Drawing.Size(200, 50);
            this.pbCaptcha.TabIndex = 36;
            this.pbCaptcha.TabStop = false;
            this.pbCaptcha.Visible = false;
            // 
            // lblcaptcha
            // 
            this.lblcaptcha.AutoSize = true;
            this.lblcaptcha.Location = new System.Drawing.Point(238, 90);
            this.lblcaptcha.Name = "lblcaptcha";
            this.lblcaptcha.Size = new System.Drawing.Size(46, 13);
            this.lblcaptcha.TabIndex = 37;
            this.lblcaptcha.Text = "captcha";
            this.lblcaptcha.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(145, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(767, 24);
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbTF
            // 
            this.cbTF.AutoSize = true;
            this.cbTF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbTF.ForeColor = System.Drawing.Color.Lime;
            this.cbTF.Location = new System.Drawing.Point(627, 4);
            this.cbTF.Name = "cbTF";
            this.cbTF.Size = new System.Drawing.Size(44, 17);
            this.cbTF.TabIndex = 39;
            this.cbTF.Text = "T/F";
            this.cbTF.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Lime;
            this.label6.Location = new System.Drawing.Point(0, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "  ";
            this.label6.MouseEnter += new System.EventHandler(this.label6_MouseEnter);
            this.label6.MouseLeave += new System.EventHandler(this.label6_MouseLeave);
            // 
            // gbTransfer
            // 
            this.gbTransfer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbTransfer.Controls.Add(this.pictureBox3);
            this.gbTransfer.Controls.Add(this.txtTransPin);
            this.gbTransfer.Controls.Add(this.txtTrans);
            this.gbTransfer.Controls.Add(this.txtTransName);
            this.gbTransfer.Controls.Add(this.label9);
            this.gbTransfer.Controls.Add(this.label8);
            this.gbTransfer.Controls.Add(this.label7);
            this.gbTransfer.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTransfer.ForeColor = System.Drawing.Color.Lime;
            this.gbTransfer.Location = new System.Drawing.Point(715, 23);
            this.gbTransfer.Name = "gbTransfer";
            this.gbTransfer.Size = new System.Drawing.Size(174, 104);
            this.gbTransfer.TabIndex = 41;
            this.gbTransfer.TabStop = false;
            this.gbTransfer.Text = "Överför";
            this.gbTransfer.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(131, 73);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 23);
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // txtTransPin
            // 
            this.txtTransPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTransPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransPin.ForeColor = System.Drawing.Color.White;
            this.txtTransPin.Location = new System.Drawing.Point(57, 73);
            this.txtTransPin.MaxLength = 4;
            this.txtTransPin.Name = "txtTransPin";
            this.txtTransPin.PasswordChar = '•';
            this.txtTransPin.Size = new System.Drawing.Size(63, 20);
            this.txtTransPin.TabIndex = 5;
            this.txtTransPin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTransPin_KeyUp);
            // 
            // txtTrans
            // 
            this.txtTrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTrans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTrans.ForeColor = System.Drawing.Color.White;
            this.txtTrans.Location = new System.Drawing.Point(57, 50);
            this.txtTrans.MaxLength = 13;
            this.txtTrans.Name = "txtTrans";
            this.txtTrans.Size = new System.Drawing.Size(100, 20);
            this.txtTrans.TabIndex = 4;
            // 
            // txtTransName
            // 
            this.txtTransName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTransName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransName.ForeColor = System.Drawing.Color.White;
            this.txtTransName.Location = new System.Drawing.Point(57, 25);
            this.txtTransName.Name = "txtTransName";
            this.txtTransName.Size = new System.Drawing.Size(100, 20);
            this.txtTransName.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = "Pin";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 1;
            this.label8.Text = "Belopp";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "Namn";
            // 
            // btnBank
            // 
            this.btnBank.BackColor = System.Drawing.Color.Black;
            this.btnBank.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBank.BackgroundImage")));
            this.btnBank.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBank.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBank.ForeColor = System.Drawing.Color.White;
            this.btnBank.Location = new System.Drawing.Point(33, 2);
            this.btnBank.Name = "btnBank";
            this.btnBank.Size = new System.Drawing.Size(50, 20);
            this.btnBank.TabIndex = 42;
            this.btnBank.Text = "Bank";
            this.btnBank.UseVisualStyleBackColor = false;
            this.btnBank.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbCaptcha
            // 
            this.cbCaptcha.AutoSize = true;
            this.cbCaptcha.BackColor = System.Drawing.Color.Transparent;
            this.cbCaptcha.Enabled = false;
            this.cbCaptcha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cbCaptcha.Location = new System.Drawing.Point(918, 28);
            this.cbCaptcha.Name = "cbCaptcha";
            this.cbCaptcha.Size = new System.Drawing.Size(66, 17);
            this.cbCaptcha.TabIndex = 43;
            this.cbCaptcha.Text = "Captcha";
            this.cbCaptcha.UseVisualStyleBackColor = false;
            this.cbCaptcha.Visible = false;
            // 
            // bgwServerCom
            // 
            this.bgwServerCom.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwServerCom_DoWork);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1038, 779);
            this.Controls.Add(this.cbCaptcha);
            this.Controls.Add(this.btnBank);
            this.Controls.Add(this.gbTransfer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbTF);
            this.Controls.Add(this.cbRob);
            this.Controls.Add(this.cbRace);
            this.Controls.Add(this.cbRus);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.lblcaptcha);
            this.Controls.Add(this.pbCaptcha);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.gbLogin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtGang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCash);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "BTT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.gbTransfer.ResumeLayout(false);
            this.gbTransfer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Timer idle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Timer wait;
        private System.Windows.Forms.Timer updateTime;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Timer updateStats;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGang;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbRob;
        private System.Windows.Forms.CheckBox cbRace;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MaskedTextBox txtPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIvnalidLogin;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.CheckBox cbRus;
        private System.Windows.Forms.PictureBox pbCaptcha;
        private System.Windows.Forms.Label lblcaptcha;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox cbTF;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbTransfer;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox txtTransPin;
        private System.Windows.Forms.TextBox txtTrans;
        private System.Windows.Forms.TextBox txtTransName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBank;
        private System.Windows.Forms.CheckBox cbCaptcha;
        private System.ComponentModel.BackgroundWorker bgwServerCom;
    }
}

