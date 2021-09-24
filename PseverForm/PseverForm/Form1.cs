using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PseverForm
{
    public partial class Form2 : Form
    {
        Thread thread = null;
        List<Socket> clientList = new List<Socket>();
        public Form2()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 소켓으로 읽어오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public byte[] ImageToByteArray(System.Drawing.Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ServerOpen();
        }
    
        private void ServerOpen()
        {
            Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 9999);

            sListener.Bind(ipEndPoint);
            sListener.Listen(20);

            Console.WriteLine("클라이언트 연결을 대기합니다.");

           
                    Socket sClient = sListener.Accept();
                    IPEndPoint ip = (IPEndPoint)sClient.RemoteEndPoint;

                    Console.WriteLine("주소 {0}에서 접속", ip.Address);

                    clientList.Add(sClient);

          

        
             Byte[] _data = new byte[400040];
                sClient.Receive(_data);
            int iLength = BitConverter.ToInt32(_data, 0);


            Byte[] _data3 = new byte[iLength];
                 //Byte[] _data4 = new byte[iLength1];
                 sClient.Receive(_data3);


            this.pictureBox1.Image = byteArrayToImage(_data3);
          




        }

        /// <summary>
        /// 바이트를 image로
        /// https://www.codeproject.com/Articles/15460/C-Image-to-Byte-Array-and-Byte-Array-to-Image-Conv
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fileName;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "C:/Users/eodud/OneDrive/바탕 화면/내 폴더";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "JPEG File(*.jpg)|*.jpg |Bitmap                    File(*.bmp)|*.bmp |PNG File(*.png)|*.png";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                pictureBox1.Image.Save(fileName);
            }
        }

       

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog dia = new OpenFileDialog();
        //    dia.Multiselect = false;
        //    dia.Filter = "jpg files|*.jpg";

        //    if (dia.ShowDialog() == DialogResult.OK)
        //    {
        //        this.pictureBox1.ImageLocation = dia.FileName;
        //    }
        //}
    }
}