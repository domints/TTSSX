namespace TTSSXTest
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
            this.gbStopService = new System.Windows.Forms.GroupBox();
            this.lbStopService = new System.Windows.Forms.ListBox();
            this.btAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAutoComplete = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbPassagesOld = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPAssagesSID = new System.Windows.Forms.TextBox();
            this.btLoadPassages = new System.Windows.Forms.Button();
            this.lbPassagesAct = new System.Windows.Forms.ListBox();
            this.gbStopService.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbStopService
            // 
            this.gbStopService.Controls.Add(this.lbStopService);
            this.gbStopService.Controls.Add(this.btAll);
            this.gbStopService.Controls.Add(this.label1);
            this.gbStopService.Controls.Add(this.tbAutoComplete);
            this.gbStopService.Location = new System.Drawing.Point(12, 12);
            this.gbStopService.Name = "gbStopService";
            this.gbStopService.Size = new System.Drawing.Size(253, 269);
            this.gbStopService.TabIndex = 0;
            this.gbStopService.TabStop = false;
            this.gbStopService.Text = "StopService";
            // 
            // lbStopService
            // 
            this.lbStopService.FormattingEnabled = true;
            this.lbStopService.Location = new System.Drawing.Point(6, 45);
            this.lbStopService.Name = "lbStopService";
            this.lbStopService.Size = new System.Drawing.Size(241, 212);
            this.lbStopService.TabIndex = 3;
            // 
            // btAll
            // 
            this.btAll.Location = new System.Drawing.Point(196, 17);
            this.btAll.Name = "btAll";
            this.btAll.Size = new System.Drawing.Size(51, 23);
            this.btAll.TabIndex = 2;
            this.btAll.Text = "All";
            this.btAll.UseVisualStyleBackColor = true;
            this.btAll.Click += new System.EventHandler(this.btAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "AC";
            // 
            // tbAutoComplete
            // 
            this.tbAutoComplete.Location = new System.Drawing.Point(33, 19);
            this.tbAutoComplete.Name = "tbAutoComplete";
            this.tbAutoComplete.Size = new System.Drawing.Size(157, 20);
            this.tbAutoComplete.TabIndex = 0;
            this.tbAutoComplete.TextChanged += new System.EventHandler(this.tbAutoComplete_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbPassagesAct);
            this.groupBox1.Controls.Add(this.btLoadPassages);
            this.groupBox1.Controls.Add(this.tbPAssagesSID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbPassagesOld);
            this.groupBox1.Location = new System.Drawing.Point(12, 364);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 259);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PassageService";
            // 
            // lbPassagesOld
            // 
            this.lbPassagesOld.FormattingEnabled = true;
            this.lbPassagesOld.Location = new System.Drawing.Point(6, 46);
            this.lbPassagesOld.Name = "lbPassagesOld";
            this.lbPassagesOld.Size = new System.Drawing.Size(520, 69);
            this.lbPassagesOld.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "stopid";
            // 
            // tbPAssagesSID
            // 
            this.tbPAssagesSID.Location = new System.Drawing.Point(47, 19);
            this.tbPAssagesSID.Name = "tbPAssagesSID";
            this.tbPAssagesSID.Size = new System.Drawing.Size(206, 20);
            this.tbPAssagesSID.TabIndex = 2;
            // 
            // btLoadPassages
            // 
            this.btLoadPassages.Location = new System.Drawing.Point(259, 17);
            this.btLoadPassages.Name = "btLoadPassages";
            this.btLoadPassages.Size = new System.Drawing.Size(75, 23);
            this.btLoadPassages.TabIndex = 3;
            this.btLoadPassages.Text = "Go";
            this.btLoadPassages.UseVisualStyleBackColor = true;
            this.btLoadPassages.Click += new System.EventHandler(this.btLoadPassages_Click);
            // 
            // lbPassagesAct
            // 
            this.lbPassagesAct.FormattingEnabled = true;
            this.lbPassagesAct.Location = new System.Drawing.Point(6, 121);
            this.lbPassagesAct.Name = "lbPassagesAct";
            this.lbPassagesAct.Size = new System.Drawing.Size(520, 134);
            this.lbPassagesAct.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 635);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbStopService);
            this.Name = "Form1";
            this.Text = "Form1";
            this.gbStopService.ResumeLayout(false);
            this.gbStopService.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbStopService;
        private System.Windows.Forms.Button btAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAutoComplete;
        private System.Windows.Forms.ListBox lbStopService;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbPassagesAct;
        private System.Windows.Forms.Button btLoadPassages;
        private System.Windows.Forms.TextBox tbPAssagesSID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbPassagesOld;
    }
}

