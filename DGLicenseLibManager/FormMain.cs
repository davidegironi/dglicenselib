#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Windows.Forms;

namespace DG.LicenseLib.Manager
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // show startup form
            ShowForm(this, typeof(FormLicenseManager));
        }

        /// <summary>
        /// Show about
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Do you want to close this application?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }

        /// <summary>
        /// Show a Form
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form ShowForm(object s, Type t, params object[] args)
        {
            bool loadme = true;
            Form f = null;
            foreach (Form fa in (s as Form).MdiChildren)
            {
                if (fa.GetType() == t)
                {
                    f = fa;
                    loadme = false;
                }
            }

            if (loadme)
            {
                if (args != null)
                {
                    f = (Form)Activator.CreateInstance(t, args);
                }
                f.MdiParent = (s as Form);
                f.WindowState = FormWindowState.Minimized;
                f.Show();
                f.WindowState = FormWindowState.Maximized;
            }
            else
            {
                f.Show();
                f.WindowState = FormWindowState.Maximized;
            }
            return f;
        }

        /// <summary>
        /// Display license manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void licenseManagerToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowForm(this, typeof(FormLicenseManager));
        }
    }
}
