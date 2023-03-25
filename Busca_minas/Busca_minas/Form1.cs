using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Busca_minas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 ventana = new Form2(); // esto es para abrir otra ventana
            ventana.Show();
            Form1 cierre = new Form1();
            cierre.Close();

            // this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 ventana = new Form3(); // Esto es para abrir otra ventana
            ventana.Show();
            Form1 cierre = new Form1();
            cierre.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 ventana = new Form4(); // Esto es para abrir otra ventana
            ventana.Show();
        }
    }
}
