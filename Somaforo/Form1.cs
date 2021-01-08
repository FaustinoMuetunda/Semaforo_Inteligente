using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Reflection;
using Microsoft.SqlServer.Server;

namespace ISW_project
{
    public partial class Form1 : Form
    {
        //
        //---------------------------------------------------------------------------------
        //

        // VARIÁVEIS_______________________________________________________________________

        // Relógio ---
        public DateTime start;
        public DateTime end;

        // Checks ---
        public Boolean initCheck = false;
        public Boolean timeCheck = false;
        public Boolean checkUp = false;
        public Boolean boxCheck = true;
        public Boolean aguaCheck = false;

        // Button Checks ---
        public Boolean mBtCheck = false;
        public Boolean lBtCheck = false;
        public Boolean tapeteCheck = false;

        // Timer
        public int general_timer = 0;
        public int aqua_timer = 0;


        // PLACA___________________________________________________________________________
        public static class K8055
        {
            [DllImport("K8055D.dll")]
            public static extern int OpenDevice(int CardAddress);
            [DllImport("K8055D.dll")]
            public static extern int CloseDevice();
            [DllImport("K8055D.dll")]
            public static extern int SetDigitalChannel(int Channel);
            [DllImport("K8055D.dll")]
            public static extern int ClearDigitalChannel(int Channel);
        }

        // INTERFACE_______________________________________________________________________

        // Form ---
        public Form1()
        {
            InitializeComponent();
        }

        public void vazio()
        {
            vazio1.Visible = false;
            vazio2.Visible = false;
            virado1.Visible = false;
            virado11.Visible = false;
        }

        public void peao()
        {


            vazio();
            Redstop.Visible = true;
            Redtop1.Visible = true;
            Yellowtop1.Visible = false;
            YellStop.Visible = false;
            GreeStop.Visible = false;
            Grenstop1.Visible = false;

            Red2.Visible = false;
            Red22.Visible = false;
            Yel2.Visible = false;
            Yell22.Visible = false;
            Gre2.Visible = true;
            Gree22.Visible = true;


            G.Visible = true;
            R.Visible = false;
            timer1.Interval = 6000;
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            timer1.Enabled = true;
            lbdia.Text = DateTime.Now.ToString("dd/MM/yy");
            groupBox1.Hide();
            groupBox2.Hide();

        }

        // "INICIAR" ---
        private void button1_Click(object sender, EventArgs e)
        {
            // START
            K8055.OpenDevice(0);
            initCheck = true;
            checkUp = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //STOP
            K8055.CloseDevice();

            initCheck = false;
        }

       //Definição da data e hora atual
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbhora.Text = DateTime.Now.ToString("HH:mm:ss");
            DateTime thisDay = DateTime.Now;
            timer1.Interval = 1000;
            String horacompleta = thisDay.ToString("HH:mm:ss");

            string[] words = horacompleta.Split(':');
            int hora = 0;
            for (int i = 0; i < 1; i++)

            {
                hora = Convert.ToInt32(words[0]);
            }
            //base para o controlo da hora no funcionamento normal
            if (radioButton3.Checked == true)
            {
                

                if (hora >= 0 && hora <= 5 || hora >= 23)
                    //Ativa os intermitentes automaticamente,
                    //quando a hora estiver neste intervalo. Caso contrario, executar na normalidade e automaticamente
                {
                    

                    intermitente();
                    label14.Text = Convert.ToString("Noturno: " + hora);

                }
                else
                {
                    normal();
                    label14.Text = Convert.ToString("Horario normal: " + hora);

                }
            }


            if (radioButton5.Checked == true)
            {
                intermitente();
                label14.Text = Convert.ToString("Noturno: " + hora);
            }

            if (radioButton6.Checked == true)
            {
                if (textBox3.Text == " ")
                {
                    normal();
                    label15.Text = Convert.ToString("PIN req");
                }
                else if (textBox3.Text == "123")
                {
                    intermitente();
                    label15.Text = Convert.ToString("Sucess");
                }
                else
                {
                    normal();
                    label15.Text = Convert.ToString("Error");

                }


            }


            void normal()
            {

                timer1.Interval = 2000;
                if (Redstop.Visible == true)
                {
                    //Configuração do semáforo de automóveis com sensor de presença (os
                    //semáforos são iguais, um sensor por automóve)
                    //as cores, são refenciadas para os contextos
                    //para este caso, a função e os leds, é controlada em função do led vermelho do somafro dos automoveis

                    Redstop.Visible = false;
                    GreeStop.Visible = true;
                    YellStop.Visible = false;
                    K8055.SetDigitalChannel(6);
                    K8055.ClearDigitalChannel(5);
                    K8055.ClearDigitalChannel(4);
                    G.Visible = false;
                    R.Visible = true;
                    K8055.SetDigitalChannel(8);
                    K8055.ClearDigitalChannel(7);
                   
                    Redtop1.Visible = false;
                    Grenstop1.Visible = true;
                    Yellowtop1.Visible = false;

                    vazio();

                    //
                    Yel2.Visible = false;
                    Red2.Visible = true;
                    Gre2.Visible = false;
                    Yell22.Visible = false;
                    Red22.Visible = true;
                    Gree22.Visible = false;
                    K8055.SetDigitalChannel(2);
                    K8055.ClearDigitalChannel(1);
                    K8055.ClearDigitalChannel(3);


                }
                else if (GreeStop.Visible == true)
                {

                    Redstop.Visible = false;
                    GreeStop.Visible = false;
                    YellStop.Visible = true;

                    G.Visible = false;
                    R.Visible = true;

                    Redtop1.Visible = false;
                    Grenstop1.Visible = false;
                    Yellowtop1.Visible = true;

                    K8055.SetDigitalChannel(4);
                    K8055.ClearDigitalChannel(5);
                    K8055.ClearDigitalChannel(6);
                    vazio();

                    //
                    Yel2.Visible = true;
                    Red2.Visible = false;
                    Gre2.Visible = false;
                    Yell22.Visible = true;
                    Red22.Visible = false;
                    Gree22.Visible = false;
                    K8055.SetDigitalChannel(1);
                    K8055.ClearDigitalChannel(2);
                    K8055.ClearDigitalChannel(3);

                }
                else if (YellStop.Visible == true)
                {

                    Redstop.Visible = true;
                    GreeStop.Visible = false;
                    YellStop.Visible = false;
                    //timer1.Interval = 3000;
                    Redtop1.Visible = true;
                    Grenstop1.Visible = false;
                    Yellowtop1.Visible = false;

                    K8055.SetDigitalChannel(5);
                    K8055.ClearDigitalChannel(4);
                    K8055.ClearDigitalChannel(6);

                    //Semáforo de desvio
                    G.Visible = true;
                    R.Visible = false;
                    K8055.SetDigitalChannel(7);
                    K8055.ClearDigitalChannel(8);
                  

                    vazio();
                    //semaforo de peão
                    Yel2.Visible = false;
                    Red2.Visible = false;
                    Gre2.Visible = true;
                    Yell22.Visible = false;
                    Red22.Visible = false;
                    Gree22.Visible = true;

                    K8055.SetDigitalChannel(3);
                    K8055.ClearDigitalChannel(1);
                    K8055.ClearDigitalChannel(2);

                }
            }

            void intermitente()
            {
                label14.Text = Convert.ToString("Noturno: " + hora);
                if (vazio1.Visible == true)
                {
                    vazio();
                    coresfalsas();
                    Yell22.Visible = true;
                    Yel2.Visible = true;
                    Yellowtop1.Visible = true;
                    YellStop.Visible = true;
                    K8055.SetDigitalChannel(4);
                    K8055.SetDigitalChannel(1);
                    K8055.ClearDigitalChannel(2);
                    K8055.ClearDigitalChannel(3);
                    K8055.ClearDigitalChannel(5);
                    K8055.ClearDigitalChannel(6);

                }
                else if (Yell22.Visible == true)
                {
                    coresfalsas();
                    vazio1.Visible = true;
                    vazio2.Visible = true;
                    virado1.Visible = true;
                    virado11.Visible = true;

                    Yell22.Visible = false;
                    Yel2.Visible = false;
                    Yellowtop1.Visible = false;
                    YellStop.Visible = false;

                   
                }
            }

            void coresfalsas()
            {
                Red22.Visible = false;
                Red2.Visible = false;
                Redstop.Visible = false;
                Redtop1.Visible = false;

                Gree22.Visible = false;
                Gre2.Visible = false;
                GreeStop.Visible = false;
                Grenstop1.Visible = false;

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            peao();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            peao();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lbhora.Text = DateTime.Now.ToString("HH:mm:ss");
            DateTime thisDay = DateTime.Now;
            timer2.Interval = 1000;
            // Definir a hora que sera comparada para o sinal

            String horacompleta = thisDay.ToString("HH:mm:ss");

            string[] words = horacompleta.Split(':');
            int hora = 0;
            for (int i = 0; i < 1; i++)

            {
                hora = Convert.ToInt32(words[0]);
            }


            // label1.Text = Convert.ToString("Noturno: "+ hora);
            if (hora >= 0 && hora <= 5 || hora >= 23)
            {

                intermitente();

            }
            else
            {
                timer2.Interval = 2000;
                if (Redstop.Visible == true)
                {
                    Redstop.Visible = false;
                    GreeStop.Visible = true;
                    YellStop.Visible = false;

                    G.Visible = false;
                    R.Visible = true;

                    // timer1.Interval = 6000;
                    Redtop1.Visible = false;
                    Grenstop1.Visible = true;
                    Yellowtop1.Visible = false;

                    vazio();

                    //
                    Yel2.Visible = false;
                    Red2.Visible = true;
                    Gre2.Visible = false;
                    Yell22.Visible = false;
                    Red22.Visible = true;
                    Gree22.Visible = false;


                }
                else if (GreeStop.Visible == true)
                {

                    Redstop.Visible = false;
                    GreeStop.Visible = false;
                    YellStop.Visible = true;

                    G.Visible = false;
                    R.Visible = true;

                    Redtop1.Visible = false;
                    Grenstop1.Visible = false;
                    Yellowtop1.Visible = true;


                    vazio();

                    //
                    Yel2.Visible = true;
                    Red2.Visible = false;
                    Gre2.Visible = false;
                    Yell22.Visible = true;
                    Red22.Visible = false;
                    Gree22.Visible = false;

                }
                else if (YellStop.Visible == true)
                {

                    Redstop.Visible = true;
                    GreeStop.Visible = false;
                    YellStop.Visible = false;
                    //timer1.Interval = 3000;
                    Redtop1.Visible = true;
                    Grenstop1.Visible = false;
                    Yellowtop1.Visible = false;

                    G.Visible = true;
                    R.Visible = false;


                    vazio();

                    //
                    //
                    Yel2.Visible = false;
                    Red2.Visible = false;
                    Gre2.Visible = true;
                    Yell22.Visible = false;
                    Red22.Visible = false;
                    Gree22.Visible = true;

                }



                // A1.Hide();
                //A11.Hide();
            }

            void intermitente()
            {
                label14.Text = Convert.ToString("Noturno: " + hora);
                if (vazio1.Visible == true)
                {
                    Yell22.Visible = true;
                    Yel2.Visible = true;
                    Yellowtop1.Visible = true;
                    YellStop.Visible = true;


                    vazio();

                    coresfalsas();

                }
                else if (Yell22.Visible == true)
                {

                    vazio1.Visible = true;
                    vazio2.Visible = true;
                    virado1.Visible = true;
                    virado11.Visible = true;

                    Yell22.Visible = false;
                    Yel2.Visible = false;
                    Yellowtop1.Visible = false;
                    YellStop.Visible = false;

                    coresfalsas();
                }
            }

            void coresfalsas()
            {
                Red22.Visible = false;
                Red2.Visible = false;
                Redstop.Visible = false;
                Redtop1.Visible = false;

                Gree22.Visible = false;
                Gre2.Visible = false;
                GreeStop.Visible = false;
                Grenstop1.Visible = false;

            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Show();
            groupBox2.Show();
            timer1.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            lBtCheck = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        

    }
}
