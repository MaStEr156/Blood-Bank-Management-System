namespace Hospital_system
{
    partial class PendingCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.PRequestID = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.PStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(18)))), ((int)(((byte)(4)))));
            this.label1.Location = new System.Drawing.Point(36, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 32);
            this.label1.TabIndex = 51;
            this.label1.Text = "Request ID:";
            // 
            // PRequestID
            // 
            this.PRequestID.BackColor = System.Drawing.Color.Transparent;
            this.PRequestID.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PRequestID.Location = new System.Drawing.Point(221, 58);
            this.PRequestID.Name = "PRequestID";
            this.PRequestID.Size = new System.Drawing.Size(99, 25);
            this.PRequestID.TabIndex = 50;
            this.PRequestID.Text = "Placeholder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(18)))), ((int)(((byte)(4)))));
            this.label2.Location = new System.Drawing.Point(36, 145);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 32);
            this.label2.TabIndex = 53;
            this.label2.Text = "Status:";
            // 
            // PStatus
            // 
            this.PStatus.BackColor = System.Drawing.Color.Transparent;
            this.PStatus.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PStatus.Location = new System.Drawing.Point(221, 152);
            this.PStatus.Name = "PStatus";
            this.PStatus.Size = new System.Drawing.Size(99, 25);
            this.PStatus.TabIndex = 52;
            this.PStatus.Text = "Placeholder";
            // 
            // PendingCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(243)))), ((int)(((byte)(219)))));
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PRequestID);
            this.Name = "PendingCard";
            this.Size = new System.Drawing.Size(423, 246);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2HtmlLabel PRequestID;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2HtmlLabel PStatus;
    }
}
