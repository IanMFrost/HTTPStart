using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPStart
{
    class Service
    {

        private TcpClient _connectionSocket;
        private string _url;
        public Service(TcpClient connection)
        {
            _connectionSocket = connection;
            _url = @"C:\Program Files\hehe";
        }

        public void doIt()
        {
            Stream ns = _connectionSocket.GetStream();
            //Stream ns = new NetworkStream(connectionSocket);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string[] mlist = message.Split(' ');


            if (mlist.ElementAt(0) == "GET")
            {
                string uri = mlist.ElementAt(1);
                //Console.WriteLine(mlist[1]);     // Bruges til at tjekke om splitteren viser plads nummer 2 i array
            }

            string answer0 = "HTTP/1.1 200 OK\r\n Content - Type: text / html\r\n Connection: close\r\n";

            do
            {
                Console.WriteLine("Client: " + message);
                sw.WriteLine(answer0);
                message = sr.ReadLine();
                FileStream fs = new FileStream(_url, FileMode.Open, FileAccess.Read);
                fs.CopyTo(sw.BaseStream);




            } while (message != null && message != "STOP");
            Console.WriteLine("Server stopped");


            ns.Close();
            _connectionSocket.Close();
        }
    }
}
