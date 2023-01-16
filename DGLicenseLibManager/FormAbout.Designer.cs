
namespace DG.LicenseLib.Manager
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.label_applicationname = new System.Windows.Forms.Label();
            this.label_applicationversion = new System.Windows.Forms.Label();
            this.label_copyright = new System.Windows.Forms.Label();
            this.button_close = new System.Windows.Forms.Button();
            this.panel_top = new System.Windows.Forms.Panel();
            this.panel_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_applicationname
            // 
            this.label_applicationname.AutoSize = true;
            this.label_applicationname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_applicationname.Location = new System.Drawing.Point(24, 19);
            this.label_applicationname.Name = "label_applicationname";
            this.label_applicationname.Size = new System.Drawing.Size(149, 20);
            this.label_applicationname.TabIndex = 0;
            this.label_applicationname.Text = "Application Name";
            // 
            // label_applicationversion
            // 
            this.label_applicationversion.AutoSize = true;
            this.label_applicationversion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_applicationversion.Location = new System.Drawing.Point(25, 42);
            this.label_applicationversion.Name = "label_applicationversion";
            this.label_applicationversion.Size = new System.Drawing.Size(97, 13);
            this.label_applicationversion.TabIndex = 1;
            this.label_applicationversion.Text = "Application Version";
            // 
            // label_copyright
            // 
            this.label_copyright.AutoSize = true;
            this.label_copyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_copyright.Location = new System.Drawing.Point(25, 102);
            this.label_copyright.Name = "label_copyright";
            this.label_copyright.Size = new System.Drawing.Size(45, 12);
            this.label_copyright.TabIndex = 2;
            this.label_copyright.Text = "Copyright";
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Location = new System.Drawing.Point(258, 79);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 3;
            this.button_close.Text = "OK";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // panel_top
            // 
            this.panel_top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_top.Controls.Add(this.label_applicationname);
            this.panel_top.Controls.Add(this.label_applicationversion);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(345, 70);
            this.panel_top.TabIndex = 4;
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 120);
            this.Controls.Add(this.panel_top);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.label_copyright);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(345, 120);
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_applicationname;
        private System.Windows.Forms.Label label_applicationversion;
        private System.Windows.Forms.Label label_copyright;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Panel panel_top;
    }
}