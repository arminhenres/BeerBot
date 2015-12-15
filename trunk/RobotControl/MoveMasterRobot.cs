using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RobotControl
{
    public class MoveMasterRobot
    {
        #region Fields
        private SerialPort _connection;

        private List<string> commands;
        #endregion

        #region Constructor
        public MoveMasterRobot()
        {
            _connection = new SerialPort();
            _connection.PortName = "COM1";
            _connection.DataBits = 7;
            _connection.BaudRate = 9600;
            _connection.NewLine = "LF";
            _connection.Parity = Parity.Even;
            _connection.StopBits = StopBits.Two;
            _connection.RtsEnable = true;
            _connection.Handshake = Handshake.RequestToSend;
            commands = new List<string>();
        }
        #endregion

        #region Methods
        private void OnDatateReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes the Robot
        /// </summary>
        public void Init()
        {
            _connection.DataReceived += new SerialDataReceivedEventHandler(this.OnDatateReceived);
            if (!_connection.IsOpen)
            {
                _connection.Open();
            }
            Nest();
        }

        /// <summary>
        /// Deinitializes the Robot
        /// </summary>
        public void Deinit()
        {
            _connection.DataReceived -= new SerialDataReceivedEventHandler(this.OnDatateReceived);
            if (_connection.IsOpen)
            {
                _connection.Close();
            }
            Nest();
        }

        /// <summary>
        /// Moves to specified position
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        /// <param name="l1">l1 coordinate</param>
        /// <param name="l2">l2 coordinate</param>
        public void MoveToPosition(double x, double y, double z, double l1, double l2)
        {
            SendMessage("MP " + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ", " + l1.ToString() + ", " + l2.ToString());
        }

        /// <summary>
        /// Turns by specified angles
        /// </summary>
        /// <param name="j1">angle 1</param>
        /// <param name="j2">angle 2</param>
        /// <param name="j3">angle 3</param>
        /// <param name="j4">angle 4</param>
        /// <param name="j5">angle 5</param>
        public void MoveJoint(decimal j1, decimal j2, decimal j3, decimal j4, decimal j5, int speed)
        {
            Speed(speed);
            SendMessage("MJ " + j1.ToString() + ", " + j2.ToString() + ", " + j3.ToString() + ", " + j4.ToString() + ", " + j5.ToString());
        }

        /// <summary>
        /// Resets Roboter state
        /// </summary>
        public void Reset()
        {
            SendMessage("RS");
        }

        /// <summary>
        /// Moves to Nest Position
        /// </summary>
        public void Nest()
        {
            SendMessage("NT");
        }

        /// <summary>
        /// Moves to Origin Position
        /// </summary>
        public void Origin()
        {
            SendMessage("OG");
        }

        /// <summary>
        /// Sets the Speed
        /// </summary>
        /// <param name="speed">Speed value to set, must be between 1 and 7</param>
        public void Speed(int speed)
        {
            if(speed > 0 && speed < 8)
            {
                SendMessage("SP " + speed.ToString());
            }
            else
            {
                SendMessage("SP 5");
            }
            
        }
        public void SaveFile(string path)
        {
            XmlWriter writer = XmlWriter.Create(path);
            writer.WriteStartDocument();
            foreach(string s in commands)
            {
                
            }
           
        }

        
        /// <summary>
        /// Sends Message
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            _connection.Write(message + "\r\n"); 
        }

        private void ExecuteCommands()
        {
            foreach(string command in commands)
            {
                SendMessage(command);
            }
        }
        #endregion
    }
}
