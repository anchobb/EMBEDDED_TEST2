using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//url연결을 위해 using 추가
using System.Net.Http;
using System.Net;
using System.Threading;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //-----------------------------------
        //백그라운드 메시지 수신 스레드 참조변수
        private BackgroundWorker recvLedWorker;
        private BackgroundWorker recvTmpWorker;
        private BackgroundWorker recvLightWorker;
        private BackgroundWorker recvDistanceWorker;


        private void conn_btn_Click(object sender, EventArgs e)
        {
            String port = this.comboBox1.Items[this.comboBox1.SelectedIndex].ToString();
            Console.WriteLine("PORT : " + port);

            //요청할 url경로 잡기
            try { 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/arduino/connection/" + port);
            request.Method = "GET";
            request.ContentType = "application/json";
            //request.Timeout = 30 * 1000;

            //연결
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            if(response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("RESPONSE CODE : " + response.StatusCode);
                //수신 스레드 객체 생성 & 스레드 등록
                    //LED
                    recvLedWorker = new BackgroundWorker();
                    recvLedWorker.DoWork += recvLedInfo;

                    //TMP
                    recvTmpWorker = new BackgroundWorker();
                    recvTmpWorker.DoWork += recvTmpInfo;

                    //LIGHT
                    recvLightWorker = new BackgroundWorker();
                    recvLightWorker.DoWork += recvLightInfo;

                    //DISTANCE
                    recvDistanceWorker = new BackgroundWorker();
                    recvDistanceWorker.DoWork += recvDistanceInfo;

                //수신 스레드 실행
                    recvLedWorker.RunWorkerAsync();
                    recvTmpWorker.RunWorkerAsync();
                    recvLightWorker.RunWorkerAsync();
                    recvDistanceWorker.RunWorkerAsync();


                }
            else
            {
                Console.WriteLine("RESPONSE FAILED.. : " + response.StatusCode);
            }
            }catch(Exception ex)
            {
                Console.WriteLine("Ex : " + ex);
            }
        }


        private void recvLedInfo(object sender, DoWorkEventArgs e)
        {
            while (!recvLedWorker.CancellationPending)
            {
                //요청할 url경로 잡기
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/arduino/message/led");
                request.Method = "GET";
                request.ContentType = "application/json";
                //request.Timeout = 30 * 1000;

                //연결
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //기본스트림
                Stream stream = response.GetResponseStream(); //using에 System,IO 추가함.
                //보조스트림
                StreamReader reader = new StreamReader(stream);
                //Invoke(new Action(()=>{}))
                Invoke(new Action(() => {
                    this.led_txt.Text = reader.ReadToEnd();
                }));

                Thread.Sleep(1000);
            }
            
        }

        private void recvTmpInfo(object sender, DoWorkEventArgs e)
        {
            while (!recvTmpWorker.CancellationPending)
            {
                //요청할 url경로 잡기
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/arduino/message/tmp");
                request.Method = "GET";
                request.ContentType = "application/json";
                //request.Timeout = 30 * 1000;

                //연결
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //기본스트림
                Stream stream = response.GetResponseStream(); //using에 System,IO 추가함.
                //보조스트림
                StreamReader reader = new StreamReader(stream);
                //Invoke(new Action(()=>{}))
                Invoke(new Action(() => {
                    this.tmp_txt.Text = reader.ReadToEnd();
                }));

                Thread.Sleep(1000);
            }

        }

        private void recvLightInfo(object sender, DoWorkEventArgs e)
        {
            while (!recvLightWorker.CancellationPending)
            {
                //요청할 url경로 잡기
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/arduino/message/light");
                request.Method = "GET";
                request.ContentType = "application/json";
                //request.Timeout = 30 * 1000;

                //연결
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //기본스트림
                Stream stream = response.GetResponseStream(); //using에 System,IO 추가함.
                //보조스트림
                StreamReader reader = new StreamReader(stream);
                //Invoke(new Action(()=>{}))
                Invoke(new Action(() => {
                    this.light_txt.Text = reader.ReadToEnd();
                }));

                Thread.Sleep(1000);
            }

        }

        private void recvDistanceInfo(object sender, DoWorkEventArgs e)
        {
            while (!recvDistanceWorker.CancellationPending)
            {
                //요청할 url경로 잡기
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/arduino/message/distance");
                request.Method = "GET";
                request.ContentType = "application/json";
                //request.Timeout = 30 * 1000;

                //연결
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //기본스트림
                Stream stream = response.GetResponseStream(); //using에 System,IO 추가함.
                //보조스트림
                StreamReader reader = new StreamReader(stream);
                //Invoke(new Action(()=>{}))
                Invoke(new Action(() => {
                    this.distance_txt.Text = reader.ReadToEnd();
                }));

                Thread.Sleep(1000);
            }

        }

        private void led_on_btn_Click(object sender, EventArgs e)
        {
            //요청할 url경로 잡기
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/arduino/led/1");
            request.Method = "GET";
            request.ContentType = "application/json";
            //request.Timeout = 30 * 1000;

            //연결
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        private void led_off_btn_Click(object sender, EventArgs e)
        {
            //요청할 url경로 잡기
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:8080/arduino/led/0");
            request.Method = "GET";
            request.ContentType = "application/json";
            //request.Timeout = 30 * 1000;

            //연결
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
    }
}
