namespace Test
{
    partial class FormDate
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
            this.buttonDateOk = new System.Windows.Forms.Button();
            this.buttonDateCancel = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonDateOk
            // 
            this.buttonDateOk.Location = new System.Drawing.Point(33, 83);
            this.buttonDateOk.Name = "buttonDateOk";
            this.buttonDateOk.Size = new System.Drawing.Size(75, 23);
            this.buttonDateOk.TabIndex = 0;
            this.buttonDateOk.Text = "ОК";
            this.buttonDateOk.UseVisualStyleBackColor = true;
            this.buttonDateOk.Click += new System.EventHandler(this.buttonDateOk_Click);
            // 
            // buttonDateCancel
            // 
            this.buttonDateCancel.Location = new System.Drawing.Point(131, 83);
            this.buttonDateCancel.Name = "buttonDateCancel";
            this.buttonDateCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonDateCancel.TabIndex = 0;
            this.buttonDateCancel.Text = "Cancel";
            this.buttonDateCancel.UseVisualStyleBackColor = true;
            this.buttonDateCancel.Click += new System.EventHandler(this.buttonDateCancel_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(22, 39);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(78, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select date:";
            // 
            // FormDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 129);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.buttonDateCancel);
            this.Controls.Add(this.buttonDateOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Date";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDateOk;
        private System.Windows.Forms.Button buttonDateCancel;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
    }
}