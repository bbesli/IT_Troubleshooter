﻿
namespace TroubleshooterUI
{
    partial class SipId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SipId));
            this.btnSetSipId = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSIPIdBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSetSipId
            // 
            this.btnSetSipId.Location = new System.Drawing.Point(230, 29);
            this.btnSetSipId.Name = "btnSetSipId";
            this.btnSetSipId.Size = new System.Drawing.Size(75, 23);
            this.btnSetSipId.TabIndex = 0;
            this.btnSetSipId.Text = "Ayarla";
            this.btnSetSipId.UseVisualStyleBackColor = true;
            this.btnSetSipId.Click += new System.EventHandler(this.btnSetSipId_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SIP ID Giriniz";
            // 
            // txtSIPIdBox
            // 
            this.txtSIPIdBox.Location = new System.Drawing.Point(112, 31);
            this.txtSIPIdBox.Name = "txtSIPIdBox";
            this.txtSIPIdBox.Size = new System.Drawing.Size(100, 20);
            this.txtSIPIdBox.TabIndex = 2;
            this.txtSIPIdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSIPIdBox_KeyDown);
            // 
            // SipId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 89);
            this.Controls.Add(this.txtSIPIdBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSetSipId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SipId";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SipId";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetSipId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSIPIdBox;
    }
}