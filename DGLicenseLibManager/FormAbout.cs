#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Reflection;
using System.Windows.Forms;

namespace DG.LicenseLib.Manager
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAbout_Load(object sender, System.EventArgs e)
        {
            label_applicationname.Text = Application.ProductName;
            label_applicationversion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            label_copyright.Text = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute), false)).Copyright;
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
