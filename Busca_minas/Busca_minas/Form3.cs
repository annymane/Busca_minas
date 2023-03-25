﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Busca_minas
{
    public partial class Form3 : Form
    {
        private int cronometro; //variable para el conometro
        int[] matriz_e = new int[145];// matriz de estado 
        Random Randoms = new Random();
        int[] minas_pos = new int[30];// posiciones de las minas mn 
        int[] calcular_area_x = new int[8] { -1, 0, 1, 1, 1, 0, -1, -1 };// calcular el area donde se encuentra la bomba
        int[] calcular_area_y = new int[8] { -1, -1, -1, 0, 1, 1, 1, 0 };// y

        public Form3()
        {
            InitializeComponent();
            cronometro = 0;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //Creación de la matriz
            int iteracion = 55;
            int numeroma = 1;

            for (int x = 1; x <= 12; x++) 
            {
                int sumax = 1;
                for (int j = 1; j <= 12; j++) 
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
                    miboton.MouseDown += new MouseEventHandler(Form3_MouseDown);
                }

                iteracion = iteracion + 40;
            }
            minas();
            area();

            Random rnd = new Random();

        }

        private void miBoton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            int t = ActiveControl.TabIndex;
            if (matriz_e[t] != 9)
                ActiveControl.Text = Convert.ToString(matriz_e[t]);
            else
                foreach (System.Windows.Forms.Control ctrl in this.Controls)
                {
                    int ta = ctrl.TabIndex;
                    if (matriz_e[ta] == 9)
                    {
                        ctrl.ForeColor = Color.Red;
                        ctrl.Text = "💣";
                        timer1.Enabled = false;
                        SoundPlayer Player = new SoundPlayer();
                        Player.SoundLocation = "C:/Users/patri/OneDrive/Desktop/Busca_minas/Busca_minas/Busca_minas/musica/Bomba_1.wav";
                        Player.Play();
                    }
                }

        }

        private void minas()
        {
            int recorrido, posición, temporal;// posición recorrido a, posición u, temporal t

            for (int minita = 0; minita < 145; minita++) matriz_e[minita] = minita; 

            for (posición = 143; posición > 0; posición--) 
            {
                recorrido = Randoms.Next(posición);
                temporal = matriz_e[recorrido];
                matriz_e[recorrido] = matriz_e[posición];
                matriz_e[posición] = temporal;
            }

            for (int minita = 0; minita < 30; minita++) minas_pos[minita] = matriz_e[minita]; 

            for (int minita = 0; minita < 145; minita++) matriz_e[minita] = 0; 

            for (int minita = 0; minita < 30; minita++) matriz_e[minas_pos[minita]] = 9; 
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            cronometro++;
            label1.Text = cronometro.ToString(); //cronometro
        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void area()
        {
            int minas_encontradas, filas, columnas_pos; // minas enncontradas en el area mv, filas f, columnas_pos c
            int minas_x, minas_y, minas_posicion; // minas_x vx, minas_y vy, minas_posicion pv

            for (int t = 0; t < 145; t++) 
            {
                if (matriz_e[t] != 9)
                {
                    minas_encontradas = 0;
                    filas = t / 12;
                    columnas_pos = t % 12;

                    for (int v = 0; v < 8; v++) 
                    {

                        minas_x = filas + calcular_area_x[v];
                        minas_y = columnas_pos + calcular_area_y[v];
                        minas_posicion = 12 * minas_x + minas_y;
                        if (minas_x > -1 && minas_x < 12 && minas_y > 0 && minas_y < 13 && matriz_e[minas_posicion] == 9) minas_encontradas++;
                    }
                    matriz_e[t] = minas_encontradas;
                }


            }
        }


        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button currentButton = (Button)sender;
                currentButton.Text = "🚩";
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            cronometro++;
            label1.Text = cronometro.ToString(); 

        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 ventana = new Form3(); 
            ventana.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control ctrl in this.Controls) ctrl.Text = (Convert.ToString(matriz_e[ctrl.TabIndex]));
        }
    }
}






