using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class FormDate : Form
    {
        public long l;
        public FormDate()
        {
            InitializeComponent();
        }

        private void buttonDateCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDateOk_Click(object sender, EventArgs e)
        {
            string s = null;
            DateTime date;
            s = dateTimePicker1.Value.ToString();
            date = DateTime.Parse(dateTimePicker1.Value.ToString());
            DateTime centuryBegin = new DateTime(1970, 1, 1);
            l = (date.Ticks - centuryBegin.Ticks) / 10000;
            this.Close();
        }
    }
}
