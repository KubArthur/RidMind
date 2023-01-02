using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Collections;
using System.Linq;
using System.Threading;

namespace programmation_systeme
{
    class Server
    {
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private int PORT = Values.Instance.Port;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        
        /*static void Main()
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();
        }*/

        public void SetupServer()
        {
            Console.WriteLine(PORT);
            Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(20);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        public void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        public void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        public void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected!");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            string code = text.Substring(0, 1);
            string lastFragment = text.Split('-').Last();
            Console.WriteLine("Received code from client: " + text);

            if (code == "U") // User request
            {
                if (Values.Instance.UserList.Contains(lastFragment))
                {
                    byte[] datal = Encoding.ASCII.GetBytes("R-"); //to get currenturn form client
                    current.Send(datal);
                }

                if (lastFragment != "UI" && lastFragment != "U" && lastFragment != "")
                {
                    Values.Instance.UserList.Add(lastFragment);
                }
                
                for (int i = 0; i < Values.Instance.UserList.Count; i++)
                {
                    byte[] data = Encoding.ASCII.GetBytes("U-" + Values.Instance.UserList[i]);
                    current.Send(data);
                    Thread.Sleep(10);
                }
            }

            if (code == "P") // Remove request
            {
                ValuesSession.Instance.UserResponse.Add(lastFragment);
            }

            if (code == "A") // Remove request
            {
                if (ValuesSession.Instance.Update == true)
                {
                    for (int i = 0; i < ValuesSession.Instance.MarkPoint.Count(); i++)
                    {
                        byte[] data = Encoding.ASCII.GetBytes("A-" + ValuesSession.Instance.MarkPoint[i]); //to get currenturn form client
                        current.Send(data);
                    }

                    byte[] datal = Encoding.ASCII.GetBytes("N-"); //to get currenturn form client
                    current.Send(datal);
                }
            }

            if (code == "Q") // Remove request
            {
                
                byte[] data = Encoding.ASCII.GetBytes("Q-" + ValuesSession.Instance.QuestionList[ValuesSession.Instance.CurrentTurn]); //to get currenturn form client
                current.Send(data);
            }

            if (code == "R") // Remove request
            {
                Values.Instance.UserList.Remove(lastFragment);
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                Console.WriteLine("Client disconnected");
                return;
                
            }


            if (code == "I") // final request
            {
                for (int i = 0; i < Values.Instance.Settings.Count(); i++)
                {
                    byte[] data = Encoding.ASCII.GetBytes("I" + i + "-" + Values.Instance.Settings[i]);
                    current.Send(data);
                    Thread.Sleep(10);
                }
            }

            if (code == "S") // final request
            {
                for (int i = 0; i < Values.Instance.ScoreList.Count(); i++)
                {
                    byte[] data = Encoding.ASCII.GetBytes("S-" + Values.Instance.ScoreList[i]);
                    current.Send(data);
                    Thread.Sleep(10);
                }
            }


            /*else if (text.ToLower() == "exit") // Client wants to exit gracefully
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                Console.WriteLine("Client disconnected");
                return;
            }
            else
            {
                Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                Console.WriteLine("Warning Sent");
            }*/

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
    }
}