using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Busca_minas
{
    public partial class Form2 : Form
    {

        private int cronometro; //variable para el conometro
        int[] mt = new int[82];// matriz de estado
        Random Randoms = new Random();
        int[] mn = new int[15];// posiciones de las minas

        public Form2()
        {
            InitializeComponent();
            cronometro= 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Creación de la matriz
            int bandera = 0;
            int iteracion = 55;
            int numeroma = 1;
            

            while (bandera <= 8)
            {
                int sumax = 1;
                

                for (int x = 1; x <= 9; x++)
                {
                    Button miboton = new Button();
                    miboton.Location = new Point(sumax, iteracion);
                    miboton.Size = new Size(40, 40);
                    miboton.Name = x + ",";
                    miboton.TabIndex = numeroma;
                    miboton.Click += new EventHandler(miBoton_Click);
                    this.Controls.Add(miboton);
                    sumax = sumax + 40;
                    numeroma = numeroma + 1;
                }
                iteracion = iteracion + 40;//al terminar el ciclo for se le sumara 40 para que baje
                //a la otra linea y como la vandera sigue verdadera repetira el proceso
                
                bandera = bandera + 1;


                minas();

                foreach (System.Windows.Forms.Control ctrl in this.Controls) ctrl.Text = (Convert.ToString(mt[ctrl.TabIndex]));
            }

            Random rnd = new Random();

        }

        private void miBoton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }

        int recorrido, posición, temporal;// posición recorrido a, posición u, temporal t

        private void minas()
        {
            for (int minita = 0; minita < 81; minita++) mt[minita] = minita;

            for (posición = 80; posición > 0; posición--)
            {
                recorrido = Randoms.Next(posición);
                temporal = mt[recorrido];
                mt[recorrido] = mt[posición];
                mt[posición] = temporal;
            }

            for (int minita = 0; minita < 15; minita++)
                mn[minita] = mt[minita]; 
            for (int minita = 0; minita < 81; minita++)
                mt[minita] = 0; 
            for (int minita = 0; minita < 15; minita++) 
                mt[mn[minita]] = 5;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            cronometro++;
            label1.Text = cronometro.ToString(); //cronometro
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
