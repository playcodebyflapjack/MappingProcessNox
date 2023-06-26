
namespace MappingProcessNox
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxProcess = new System.Windows.Forms.ComboBox();
            this.buttonReadProcess = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.findFileAdb = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.textBoxChangeTitle = new System.Windows.Forms.TextBox();
            this.buttonChangeTitle = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Process";
            // 
            // comboBoxProcess
            // 
            this.comboBoxProcess.FormattingEnabled = true;
            this.comboBoxProcess.Location = new System.Drawing.Point(15, 34);
            this.comboBoxProcess.Name = "comboBoxProcess";
            this.comboBoxProcess.Size = new System.Drawing.Size(160, 21);
            this.comboBoxProcess.TabIndex = 1;
            this.comboBoxProcess.SelectionChangeCommitted += new System.EventHandler(this.comboBoxProcess_SelectionChangeCommitted);
            // 
            // buttonReadProcess
            // 
            this.buttonReadProcess.Location = new System.Drawing.Point(190, 34);
            this.buttonReadProcess.Name = "buttonReadProcess";
            this.buttonReadProcess.Size = new System.Drawing.Size(75, 23);
            this.buttonReadProcess.TabIndex = 2;
            this.buttonReadProcess.Text = "Read";
            this.buttonReadProcess.UseVisualStyleBackColor = true;
            this.buttonReadProcess.Click += new System.EventHandler(this.buttonReadProcess_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Change Title";
            // 
            // findFileAdb
            // 
            this.findFileAdb.FileName = "File adb.exe";
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.Enabled = false;
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(15, 89);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(160, 21);
            this.comboBoxDevices.TabIndex = 5;
            this.comboBoxDevices.SelectionChangeCommitted += new System.EventHandler(this.comboBoxDevices_SelectionChangeCommitted);
            // 
            // textBoxChangeTitle
            // 
            this.textBoxChangeTitle.Location = new System.Drawing.Point(15, 145);
            this.textBoxChangeTitle.Name = "textBoxChangeTitle";
            this.textBoxChangeTitle.Size = new System.Drawing.Size(160, 20);
            this.textBoxChangeTitle.TabIndex = 7;
            // 
            // buttonChangeTitle
            // 
            this.buttonChangeTitle.Location = new System.Drawing.Point(190, 143);
            this.buttonChangeTitle.Name = "buttonChangeTitle";
            this.buttonChangeTitle.Size = new System.Drawing.Size(75, 23);
            this.buttonChangeTitle.TabIndex = 8;
            this.buttonChangeTitle.Text = "Change\r\n";
            this.buttonChangeTitle.UseVisualStyleBackColor = true;
            this.buttonChangeTitle.Click += new System.EventHandler(this.buttonChangeTitle_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 66);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(73, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Path ADB";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 203);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonChangeTitle);
            this.Controls.Add(this.textBoxChangeTitle);
            this.Controls.Add(this.comboBoxDevices);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonReadProcess);
            this.Controls.Add(this.comboBoxProcess);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxProcess;
        private System.Windows.Forms.Button buttonReadProcess;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog findFileAdb;
        private System.Windows.Forms.ComboBox comboBoxDevices;
        private System.Windows.Forms.TextBox textBoxChangeTitle;
        private System.Windows.Forms.Button buttonChangeTitle;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

