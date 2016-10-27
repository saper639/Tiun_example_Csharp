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
    public partial class FormCreate : Form
    {
        const String default_host = "192.168.2.106"; // адрес сервера по умолчанию
        const int default_port = 1234; // порт по умолчанию
        public String host; 
        public int port; 
        public FormCreate()
        {
            InitializeComponent();
        }

        private void buttonCreateOK_Click(object sender, EventArgs e)
        {
            host = textBox1.Text;
            port = int.Parse(textBox2.Text);
            this.Close();
        }

        private void buttonCreateCancel_Click(object sender, EventArgs e)
        {
            host = default_host;
            port = default_port;
            this.Close();
        }
    }
}
