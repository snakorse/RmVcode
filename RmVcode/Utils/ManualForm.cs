using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RmVcode
{
    public partial class ManualForm : Form
    {
        private string result = null;

        private ManualForm()
        {
            InitializeComponent();
        }

        public ManualForm(byte[] img):this()
        {
            using (var s = new MemoryStream(img))
            {
                this.picImage.Image = Image.FromStream(s);
            }
            
            this.btnOK.Click += btnOK_Click;
            this.FormClosing += (s, e) =>
            {
                if (this.picImage.Image != null)
                {
                    this.picImage.Image.Dispose();
                }
            };
        }

        public string Result
        {
            get { return this.result; }
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbResult.Text))
                return;

            this.result = tbResult.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.Equals(Keys.Enter))
            {
                btnOK_Click(null,null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
