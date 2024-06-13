using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using WMPLib;

namespace SleepStation //Para la proxima version mostrar la cancion que se esta reproduciendo actualmente
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer wmsound = new WMPLib.WindowsMediaPlayer();

        
        string[] archivos;
        private int reloj=1000;
        private decimal minutos = 0;
        int mp3 = 0;
        string choose;
        string realg;

        public Form1()
        {
            InitializeComponent();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
                    

            //object escogido = Path.GetFileName(listBox1.SelectedItem.ToString()); //Me la saque aqui           
           // materialLabel3.Text = escogido.ToString();
               
            //wmsound.URL = listBox1.SelectedItem.ToString();
            //wmsound.controls.play();
            //MessageBox.Show(archivos.Length.ToString());
            //MessageBox.Show(mp3.ToString());
            /*WMPLib.IWMPPlaylist playlist = wmsound.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;*/
            try
            {
                choose = Path.GetFullPath(listBox1.SelectedItem.ToString()); //Es necesaria la ruta entera, no vale con un Path.GetFilename
                realg = Convert.ToString(choose);
            }catch(Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message.ToString()+ "You selected a song??", "SleepStation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            /*if (realg.Length > 0) //Compruebo si ha seleccionado alguna cancion para reproducirla antes de continuar con la playlist
            {
                
                    //MessageBox.Show(choose);
                    media = wmsound.newMedia(realg);
                    playlist.appendItem(media);
                    wmsound.currentPlaylist = playlist;
                    wmsound.controls.play();
                
                
            }
            */
            //Ahora tiramos con la playlist
            WMPLib.IWMPPlaylist playlist2 = wmsound.playlistCollection.newPlaylist("myplaylist2");
            WMPLib.IWMPMedia media2;
            try
            {
                string first = realg; //Aqui va la primera en caso de que haya escogido una (solucion final)
                media2 = wmsound.newMedia(first);
                playlist2.appendItem(media2);

                foreach (string name in archivos)
                {



                    //wmsound.URL = name;
                    //wmsound.controls.play();

                    string extension = Path.GetExtension(name); //De nuevo filtramos solo los .mp3
                    if (extension == ".mp3")
                    {
                        //MessageBox.Show(name);
                        //Y añadimos cancion a cancion del directorio escogido
                        media2 = wmsound.newMedia(name);
                        playlist2.appendItem(media2);
                        //var playlist = wmsound.newPlaylist("My playlist", "");
                        //playlist.appendItem(wmsound.newMedia(name));
                        //wmsound.currentPlaylist = playlist;
                        //wmsound.controls.play();
                    }




                }
                //Arrancamos la lista
                wmsound.currentPlaylist = playlist2;
                wmsound.controls.play();
                
                timer1.Enabled = true;
                timer1.Start();
                timer3.Enabled = true;
                timer3.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR TO LOAD AND PLAY THE TRACK : " + ex.Message.ToString() + "PRESS THE HELP BUTTON IF YOU NEED IT", "SleepStation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
                
           
          


            //MessageBox.Show(wmsound.currentMedia.name);

          

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            reloj--; //el contador de apagado
            if (reloj <= 0)
            {
                Process.Start("shutdown", "/s /t 0"); //Tira de system para arrancar 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            try
            {


                if (textBox1.Text.Length == 0)
                {
                    MessageBox.Show("SET A VALID TIME!", "SleepStation" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (timer1.Enabled == true)
                {
                    MessageBox.Show("STOP THE COUNTER FIRST", "SleepStation" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    reloj = Convert.ToInt32(textBox1.Text);
                    minutos = reloj / 60;
                    materialLabel2.Text = minutos.ToString() + " " + "min to sleep"; //Muchas conversiones?¿
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "SleepStation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            wmsound.controls.stop();
            timer1.Stop();
            timer1.Enabled = false;
            timer3.Enabled = false;
            
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            try
            {
                var carpetas = new FolderBrowserDialog();
                DialogResult result = carpetas.ShowDialog();
                archivos = Directory.GetFiles(carpetas.SelectedPath);
                foreach (string name in archivos)
                {
                    string extension = Path.GetExtension(name);
                    if (extension == ".mp3")
                    {
                        listBox1.Items.Add(name);
                        mp3++;
                    }

                }
                //listBox1.Sorted = true;
            }catch(Exception ex)
            {
                MessageBox.Show("Error al tratar la ruta : "+ex.Message.ToString(),"SleepStation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("- Set the time on SECONDS\n" +
                "- Time to off appears in MINUTES\n"+"- Only files with .mp3 extension will appear in the check moment\n"+"\n" + "-By sad0g--January 2016","Sleep Station HELP",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void materialLabel4_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //materialLabel4.Text = wmsound.controls.currentItem.duration.ToString();
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {
            //Hacer progressbar
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            minutos--;
            materialLabel2.Text = minutos.ToString() + " " + "min to sleep"; //Pa la proxima dejar escoger min o sec a mostrar

        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
