using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.Windows.Threading;
using System.Collections;

namespace Germin_ping_multiplo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList Lista;
        DispatcherTimer timer = new DispatcherTimer();
        int i = 0;
        int y = 0;
        string[] indirizzi = new string[8];

        public MainWindow()
        {
            InitializeComponent();
            Lista = new ArrayList();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0,0,2);
            
        }

       
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
           lsb_output.Items.Clear();
           GestioneIndirizzi();
           Stampa();
        }

        private void GestioneIndirizzi()
        {
            try
            {
                for (y = 0; y < indirizzi.Length; y++)
                {
                    if (indirizzi[y] != null)
                    {
                        Ping ping = new Ping();
                        PingReply replys = ping.Send(indirizzi[y]);
                        string millisec = replys.RoundtripTime.ToString();
                        string status = replys.Status.ToString();
                        Indirizzi input = new Indirizzi(indirizzi[y], status, millisec);
                        Lista.Add(input);
                    }
                    else
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Stampa()
        {

            foreach (Indirizzi input in Lista)
            {
                if (input != null)
                {
                    lsb_output.Items.Add($"Indirizzo:{input.Ip} \t Stato:{input.Risposta} \t Tempo:{input.Tempo}ms");
                    
                }
            }
        }

        private void InserimentoIP(string input)
        {
            try
            {
                indirizzi[i] = input;
                i++;
                MessageBox.Show("Operazione avvenuta con successo");
             
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void btn_Inserisci_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            if(txt_IP.Text == "")
            {
                MessageBox.Show(" Inserire un valore");
            }
            else
            {
                InserimentoIP(txt_IP.Text);
                txt_IP.Text = "";
            }
            
        }

        private void btn_Avvia_Click(object sender, RoutedEventArgs e)
        {
            if (indirizzi[0] != null)
            {
                GestioneIndirizzi();
                Stampa();
                timer.Start();
            }
            else
                MessageBox.Show("Inserire un indirizzo");
            
           

        }

        private void btn_stopTimer_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void btn_azzera_Click(object sender, RoutedEventArgs e)
        {
            i = 0;
            Array.Clear(indirizzi, 0, 7);
            Lista.Clear();
            lsb_output.Items.Clear();
            timer.Stop();
        }
    }
}
