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
        int[] matriz_e = new int[82];// matriz de estado
        Random Randoms = new Random();
        int[] minas_pos = new int[15];// posiciones de las minas
        int[] calcular_area_x = new int[8] { -1, 0, 1, 1, 1, 0, -1, -1 };// calcular el area donde se encuentra la bomba
        int[] calcular_area_y =new int[8] { -1, -1, -1, 0, 1, 1, 1, 0 };// y

        public Form2()
        {
            InitializeComponent();
            cronometro = 0;
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
                area();

                foreach (System.Windows.Forms.Control ctrl in this.Controls) ctrl.Text = (Convert.ToString(matriz_e[ctrl.TabIndex]));
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
            for (int minita = 0; minita < 81; minita++) matriz_e[minita] = minita;

            for (posición = 80; posición > 0; posición--)
            {
                recorrido = Randoms.Next(posición);
                temporal = matriz_e[recorrido];
                matriz_e[recorrido] = matriz_e[posición];
                matriz_e[posición] = temporal;
            }

            for (int minita = 0; minita < 15; minita++)
                minas_pos[minita] = matriz_e[minita];
            for (int minita = 0; minita < 81; minita++)
                matriz_e[minita] = 0;
            for (int minita = 0; minita < 15; minita++)
                matriz_e[minas_pos[minita]] = 9;
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

        private void area()
        {
            int minas_encontradas, filas, columnas_pos; // minas enncontradas en el area mv, filas f, columnas_pos c
            int minas_x, minas_y, minas_posicion; // minas_x vx, minas_y vy, minas_posicion pv

            for (int t = 0; t < 81; t++)
            {
                if (matriz_e[t] != 9)
                {
                    minas_encontradas = 0; 
                    filas = t / 9; 
                    columnas_pos = t % 9;

                    for (int v = 0; v < 8; v++)
                    {

                        minas_x = filas + calcular_area_x[v];
                        minas_y = columnas_pos + calcular_area_y[v];
                        minas_posicion= 9 * minas_x + minas_y;
                        if (minas_posicion > -1 && minas_posicion < 81 && matriz_e[minas_posicion] == 9) minas_encontradas++;
                    }
                    matriz_e[t] = minas_encontradas;
                }
                
                
            }
        }
    }

}
