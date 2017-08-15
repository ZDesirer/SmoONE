﻿using System;
using Smobiler.Core;
namespace SmoONE.UI.TemplateControlName
{
    partial class frmRBFootbarLayout : Smobiler.Core.Controls.MobileForm
    {
        #region "SmobilerForm generated code "

        public frmRBFootbarLayout()
            : base()
        {
            //This call is required by the SmobilerForm.
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
        }

        //SmobilerForm overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


        //NOTE: The following procedure is required by the SmobilerForm
        //It can be modified using the SmobilerForm.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.Checkall = new Smobiler.Core.Controls.CheckBox();
            this.lblCheckall = new Smobiler.Core.Controls.Label();
            this.Label3 = new Smobiler.Core.Controls.Label();
            this.lblAmount = new Smobiler.Core.Controls.Label();
            this.btnSave = new Smobiler.Core.Controls.Button();
            // 
            // Checkall
            // 
            this.Checkall.Border = new Smobiler.Core.Controls.Border(1);
            this.Checkall.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Checkall.Checked = false;
            this.Checkall.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(146)))), ((int)(((byte)(223)))));
            this.Checkall.Location = new System.Drawing.Point(10, 10);
            this.Checkall.Name = "Checkall";
            this.Checkall.Size = new System.Drawing.Size(15, 15);
            this.Checkall.TabIndex = 2;
            this.Checkall.UnCheckedBackColor = System.Drawing.Color.White;
            this.Checkall.ZIndex = 2;
            // 
            // lblCheckall
            // 
            this.lblCheckall.FontSize = 12F;
            this.lblCheckall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblCheckall.Location = new System.Drawing.Point(42, 0);
            this.lblCheckall.Name = "lblCheckall";
            this.lblCheckall.Size = new System.Drawing.Size(33, 35);
            this.lblCheckall.TabIndex = 3;
            this.lblCheckall.Text = "全选";
            this.lblCheckall.ZIndex = 3;
            // 
            // Label3
            // 
            this.Label3.Bold = true;
            this.Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Label3.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.Label3.Location = new System.Drawing.Point(75, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(85, 35);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "总计：";
            this.Label3.ZIndex = 4;
            // 
            // lblAmount
            // 
            this.lblAmount.Bold = true;
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(146)))), ((int)(((byte)(223)))));
            this.lblAmount.Location = new System.Drawing.Point(160, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Padding = new Smobiler.Core.Controls.Padding(2F, 0F, 0F, 0F);
            this.lblAmount.Size = new System.Drawing.Size(140, 35);
            this.lblAmount.TabIndex = 5;
            this.lblAmount.Text = "￥0";
            this.lblAmount.ZIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(146)))), ((int)(((byte)(223)))));
            this.btnSave.BorderRadius = 2;
            this.btnSave.FontSize = 17F;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(10, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(280, 35);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "提交";
            // 
            // frmRBFootbarLayout
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.Checkall,
            this.lblCheckall,
            this.Label3,
            this.lblAmount,
            this.btnSave});
            this.Size = new System.Drawing.Size(300, 80);
            this.Name = "frmRBFootbarLayout";

        }
        #endregion

        internal Smobiler.Core.Controls.CheckBox Checkall;
        internal Smobiler.Core.Controls.Label lblCheckall;
        internal Smobiler.Core.Controls.Label Label3;
        internal Smobiler.Core.Controls.Label lblAmount;
        private Smobiler.Core.Controls.Button btnSave;
    }
}