using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Threading; 

namespace Test
{
    //***************************************
    //*   Developed by Bezhentcev Nikita    *
    //*               Tiun                  *
    //*            04.10.2016               *
    //*            votak.org                *
    //***************************************

    public partial class FormMain : Form
    {
        String host; // server address
        int port; // port
        IPAddress ipAddr = null;
        IPEndPoint ipEndPoint = null;
        Socket socket = null;
        Thread myThread = null;
        Socket socketServer = null;
        volatile bool threadRunning = true;

        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                // checking the connection
                /*if (socket == null)
                {
                    textBoxServer.AppendText("Socket is not created. Trying to reconnect\n");
                    ConnectToServer();
                }*/
                if (socket.IsConnected() == false)
                {
                    textBoxServer.AppendText("Connection lost. Trying to reconnect\n");
                    ConnectToServer();
                }

                comboBox.Text = System.Text.RegularExpressions.Regex.Replace(comboBox.Text, @"\s+", " ");
                textBoxCommand.Clear();

                // enter the date for commands 'getsmsafter' and 'getcallafter'
                if (comboBox.Text.Contains("getsmsafter") || comboBox.Text.Contains("getcallafter"))
                {
                    FormDate frm = new FormDate();
                    frm.ShowDialog();
                    string temp = frm.l.ToString() + " ";
                    comboBox.Text = comboBox.Text + " ";
                    if (comboBox.Text.Contains("getsmsafter"))
                        comboBox.Text = comboBox.Text.Insert(12, temp);
                    else
                        comboBox.Text = comboBox.Text.Insert(13, temp);
                }

                // a buffer for incoming data
                byte[] bytes = new byte[10000000];

                // send command
                byte[] byData = Encoding.UTF8.GetBytes(comboBox.Text.ToString() + "\r\n");
                socket.Send(byData);
                textBoxCommand.Text = "Sent command: " + comboBox.Text + "\n";

                // get a response from server and parse
                int byteRec = socket.Receive(bytes);
                String receivedJson = Encoding.UTF8.GetString(bytes, 0, byteRec);
                if (comboBox.Text.Contains("sendsms"))
                {
                    JsonSms sms = JsonConvert.DeserializeObject<JsonSms>(receivedJson);
                    textBoxCommand.AppendText("Номер:" + sms.arg.ToString() + "\n");
                    textBoxCommand.AppendText("Текст сообщения: " + sms.arg2.ToString());
                }
                else if (comboBox.Text.Contains("makecall"))
                {
                    JsonCall call = JsonConvert.DeserializeObject<JsonCall>(receivedJson);
                    textBoxServer.AppendText(call.answer.ToString());
                }
                else if (comboBox.Text.Contains("getsmsafter") || comboBox.Text.Contains("getcallafter"))
                {
                    JsonAnswer jsonAnswer = JsonConvert.DeserializeObject<JsonAnswer>(receivedJson);
                    textBoxCommand.AppendText(jsonAnswer.answer.ToString());
                }
                else if (comboBox.Text.Contains("addgroup"))
                {
                    JsonSms jsonGroup = JsonConvert.DeserializeObject<JsonSms>(receivedJson);
                    textBoxCommand.AppendText(jsonGroup.arg.ToString());
                }
                else if (comboBox.Text.Contains("addcontact"))
                {
                    JsonSms jsonContact = JsonConvert.DeserializeObject<JsonSms>(receivedJson);
                    textBoxCommand.AppendText(jsonContact.arg.ToString());
                }
                else
                {
                    JsonAnswer jsonAnswer = JsonConvert.DeserializeObject<JsonAnswer>(receivedJson);
                    textBoxCommand.AppendText(jsonAnswer.answer.ToString());
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                string caption = "Error!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            finally
            {
                comboBox.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // entering server's address and port
            // if you press 'Cancel' button, will be recorded default address and port from const
            FormCreate frm = new FormCreate();
            frm.ShowDialog();
            host = frm.host;
            port = frm.port;
            ConnectToServer();
        }

        public class JsonAnswer
        {
            public String cmd { get; set; }
            public Object answer { get; set; }
            public IList<String> obj { get; set; }
        }

        public class JsonSms
        {
            public Object arg { get; set; }
            public String cmd { get; set; }
            public Object arg2 { get; set; }
        }

        public class JsonCall
        {
            public Object answer { get; set; }
            public String cmd { get; set; }
        }

        public void ConnectToServer()
        {
            try
            {
                ipAddr = IPAddress.Parse(host);
                // set the end point for the socket
                ipEndPoint = new IPEndPoint(ipAddr, port);
                socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // connect socket with end point
                socket.Connect(ipEndPoint);

                // send a codeword
                byte[] bySecret = System.Text.Encoding.ASCII.GetBytes("1234\r\n");
                socket.Send(bySecret);
                textBoxServer.AppendText("Сonnection to the server " + host + ":" + port + " is established\n");
                buttonConnect.Enabled = false;

                // start thread for listening answers from server
                myThread = new Thread(ListenServer);
                myThread.IsBackground = false;
                myThread.Start();
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                string caption = "Error!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                buttonConnect.Enabled = true;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            // similar to Form_Load
            FormCreate frm = new FormCreate();
            frm.ShowDialog();
            host = frm.host;
            port = frm.port;
            ConnectToServer();
        }

        public void ListenServer()
        {
            ipAddr = IPAddress.Parse(host);
            // set the end point for the socket
            ipEndPoint = new IPEndPoint(ipAddr, port);
            socketServer = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            while (threadRunning)
            {
                try
                {
                    // connect socket with end point
                    socketServer.Connect(ipEndPoint);

                    // send a codeword
                    byte[] bySecret = System.Text.Encoding.ASCII.GetBytes("1234\r\n");
                    socketServer.Send(bySecret);

                    // a buffer for incoming data
                    byte[] bytesServer = new byte[10000000];
                    // get a response from server and parse
                    int byteRecServer = socketServer.Receive(bytesServer);
                    String receivedJson = Encoding.UTF8.GetString(bytesServer, 0, byteRecServer);
                    JsonAnswer jsonAnswerServer = JsonConvert.DeserializeObject<JsonAnswer>(receivedJson);
                    if (jsonAnswerServer.cmd.Contains("newsms") || jsonAnswerServer.cmd.Contains("incomingcall") || jsonAnswerServer.cmd.Contains("call") || jsonAnswerServer.cmd.Contains("ping"))
                    {
                        AppendTextBox(jsonAnswerServer.answer.ToString());
                    }
                }
                catch (ThreadAbortException ex)
                {
                    // catch the exception from myThread.Abort() and ignore it
                    throw ex;
                }
                catch (Exception ex)
                {
                    string message = ex.Message.ToString();
                    string caption = "Server error!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                    buttonConnect.Enabled = true;
                }
            }
        }

        // implementation of data output from a thread to textBoxServer
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBoxServer.Text += value;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            // complete thread, close sockets and exit from application
            if (socket.Connected == true)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            if (myThread != null)
            {
                threadRunning = false;
                myThread.Abort();
                socketServer.Shutdown(SocketShutdown.Both);
                socketServer.Close();
            }
            Application.Exit();
        }
    }
}
