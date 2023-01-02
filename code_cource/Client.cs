using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
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
using System.Threading;

namespace programmation_systeme

{
    class Client
    {
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        private int PORT = Values.Instance.Port;

        /*static void Main()
        {
            Console.Title = "Client";
            ConnectToServer();
            RequestLoop();
            Exit();
        }*/

        public void ConnectToServer()
        {
            while (!ClientSocket.Connected)
            {
                try
                {
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException)
                {
                }
            }

            
            Console.WriteLine("Connected!");
        }

        public void RequestLoop()
        {
            while(true)
            {
                ReceiveResponse();
            }
                
            
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        public void Exit()
        {
            // Tell the server we are exiting
            SendString("R-" + Values.Instance.UserName);
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
        
        }

 
        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        public void SendString(string text)
        {

            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        public void ReceiveResponse()
        {
            try
            {


                var buffer = new byte[2048];
                int received = ClientSocket.Receive(buffer, SocketFlags.None);
                if (received == 0) return;
                var data = new byte[received];
                Array.Copy(buffer, data, received);
                string text = Encoding.ASCII.GetString(data);
                string code = text.Substring(0, 1);
                string code2 = text.Substring(0, 2);
                string lastFragment = text.Split('-').Last();
                Console.WriteLine("Received code from server: " + text);

                if (code == "U") // User request
                {
                    if (Values.Instance.UserList.Contains(lastFragment))
                    {

                    }

                    else
                    {
                        Values.Instance.UserList.Add(lastFragment);
                    }
                }

                if (code == "A") // Remove request
                {

                    ValuesSession.Instance.MarkPoint.Add(lastFragment);
                }

                if (code == "N") // Remove request
                {
                    ValuesSession.Instance.Update = true;
                }


                if (code == "I") // final request
                {
                    if (code2 == "I0") // 
                    {
                        Values.Instance.Settings[0] = lastFragment;
                    }

                    if (code2 == "I1") // 
                    {
                        Values.Instance.Settings[1] = lastFragment;
                    }

                    if (code2 == "I2") // 
                    {
                        Values.Instance.Settings[2] = lastFragment;
                    }

                    if (code2 == "I3") // 
                    {
                        Values.Instance.Settings[3] = lastFragment;
                    }

                    if (code2 == "I4") // 
                    {
                        Values.Instance.Settings[4] = lastFragment;
                    }
                }

                if (code == "Q") // Remove request
                {
                    ValuesSession.Instance.CurrentQuestion = lastFragment;
                }

                if (code == "S") // final request
                {

                    ValuesSessionEnd.Instance.FinalList.Add(lastFragment);

                }

                if (code == "R") // final request
                {
                    Exit();
                }
            }
            catch
            {
            }

        }
    }
}