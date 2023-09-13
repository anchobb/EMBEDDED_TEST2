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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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
