namespace IMS
{
    partial class ChangeStatus
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
            this.cmbusernames = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnactive = new System.Windows.Forms.RadioButton();
            this.rbtnblock = new System.Windows.Forms.RadioButton();
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "UserName:";
            // 
            // cmbusernames
            // 
            this.cmbusernames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbusernames.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbusernames.FormattingEnabled = true;
            this.cmbusernames.Location = new System.Drawing.Point(29, 72);
            this.cmbusernames.Name = "cmbusernames";
            this.cmbusernames.Size = new System.Drawing.Size(301, 37);
            this.cmbusernames.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select Status:";
            // 
            // rbtnactive
            // 
            this.rbtnactive.AutoSize = true;
            this.rbtnactive.Font = new System.Drawing.Font("Verdana", 11F);
            this.rbtnactive.Location = new System.Drawing.Point(29, 169);
            this.rbtnactive.Name = "rbtnactive";
            this.rbtnactive.Size = new System.Drawing.Size(104, 30);
            this.rbtnactive.TabIndex = 5;
            this.rbtnactive.TabStop = true;
            this.rbtnactive.Text = "Active";
            this.rbtnactive.UseVisualStyleBackColor = true;
            // 
            // rbtnblock
            // 
            this.rbtnblock.AutoSize = true;
            this.rbtnblock.Font = new System.Drawing.Font("Verdana", 11F);
            this.rbtnblock.Location = new System.Drawing.Point(235, 169);
            this.rbtnblock.Name = "rbtnblock";
            this.rbtnblock.Size = new System.Drawing.Size(95, 30);
            this.rbtnblock.TabIndex = 6;
            this.rbtnblock.TabStop = true;
            this.rbtnblock.Text = "Block";
            this.rbtnblock.UseVisualStyleBackColor = true;
            // 
            // btnChangeStatus
            // 
            this.btnChangeStatus.BackColor = System.Drawing.Color.DarkMagenta;
            this.btnChangeStatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeStatus.ForeColor = System.Drawing.Color.White;
            this.btnChangeStatus.Location = new System.Drawing.Point(29, 214);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(151, 60);
            this.btnChangeStatus.TabIndex = 10;
            this.btnChangeStatus.Text = "Change";
            this.btnChangeStatus.UseVisualStyleBackColor = false;
            this.btnChangeStatus.Click += new System.EventHandler(this.BtnChangeStatus_Click);
            // 
            // ChangeStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 297);
            this.Controls.Add(this.btnChangeStatus);
            this.Controls.Add(this.rbtnblock);
            this.Controls.Add(this.rbtnactive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbusernames);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ChangeStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeStatus";
            this.Load += new System.EventHandler(this.ChangeStatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbusernames;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnactive;
        private System.Windows.Forms.RadioButton rbtnblock;
        private System.Windows.Forms.Button btnChangeStatus;
    }
}