using System;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace SerialPortLogger
{
    class Program
    {
        
        static DateTime currentTime;
        static string data = "";
        static StreamWriter writer;

        static readonly object lockObj = new object();

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: SerialPortLogger.exe <COM_PORT> <BAUD_RATE> <FILEPATH>");
            }
            else {
                if (File.Exists(args[2])) 
                {
                    Console.WriteLine("The file " + args[2] + " already exists. It will be overwritten.");
                    Console.WriteLine("You may use <ctrl+c> to abort.");
                }
                Console.WriteLine("Hit <enter> to start logging.");
                Console.WriteLine("Hit <enter> again to stop logging and exit the application cleanly.");
                Console.ReadLine();
                writer = new StreamWriter(args[2]);
                SerialPort port = new SerialPort(args[0], int.Parse(args[1]));
                port.DataReceived += port_DataReceived;
                port.Open();
                Console.ReadLine();
                // this will usually be empty, but clearing any possible remaining buffers (both in the serial port, and also due to the lack of a \\n character in received data.
                data += port.ReadExisting();
                if (data.Length > 0) 
                {
                    string logData = "Remaining buffer:" + data;
                    Console.Write(logData);
                    writer.Write(logData);
                }
                port.Close();
                writer.Close();
            }
        }

        static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int index;
            lock (lockObj)
            {
                currentTime = DateTime.Now;
                data += ((SerialPort)sender).ReadExisting();
                while ((index = data.IndexOf('\n')) != -1)
                {
                    string logData = currentTime.ToString("yyyy-MM-ddTHH:mm:ss.fff") + "|" + data.Substring(0,index+1);
                    Console.Write(logData);
                    writer.Write(logData);
                    data = data.Substring(index+1);
                }
            }
        }
    }
}
