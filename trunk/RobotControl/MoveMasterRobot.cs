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

        private bool _isInitialized;

        private bool _isBusy;
        #endregion

        #region Constructor
        public MoveMasterRobot()
        {
            _isBusy = false;
            _isInitialized = false;
            _connection = new SerialPort();
            _connection.PortName = "COM1";
            _connection.DataBits = 7;
            _connection.BaudRate = 9600;
            _connection.NewLine = "LF";
            _connection.Parity = Parity.Even;
            _connection.StopBits = StopBits.Two;
            _connection.RtsEnable = true;
            _connection.Handshake = Handshake.RequestToSend;
            
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
            commands = new List<string>();
            _connection.DataReceived += new SerialDataReceivedEventHandler(this.OnDatateReceived);
            if (!_connection.IsOpen)
            {
                _connection.Open();
            }
            Nest(true);
            _isInitialized = true;
        }

        /// <summary>
        /// Deinitializes the Robot
        /// </summary>
        public void Deinit()
        {
            commands = null;
            Nest(true);
            _connection.DataReceived -= new SerialDataReceivedEventHandler(this.OnDatateReceived);
            if (_connection.IsOpen)
            {
                _connection.Close();
            }
            _isInitialized = false;
        }

        /// <summary>
        /// Moves to specified position
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        /// <param name="l1">l1 coordinate</param>
        /// <param name="l2">l2 coordinate</param>
        public void MoveToPosition(double x, double y, double z, double l1, double l2, bool instant)
        {
            SendMessage("MP " + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ", " + l1.ToString() + ", " + l2.ToString(), instant);
        }

        /// <summary>
        /// Turns by specified angles
        /// </summary>
        /// <param name="j1">angle 1</param>
        /// <param name="j2">angle 2</param>
        /// <param name="j3">angle 3</param>
        /// <param name="j4">angle 4</param>
        /// <param name="j5">angle 5</param>
        public void MoveJoint(decimal j1, decimal j2, decimal j3, decimal j4, decimal j5, int speed, bool instant)
        {
            string message = "MJ " + j1.ToString() + ", " + j2.ToString() + ", " + j3.ToString() + ", " + j4.ToString() + ", " + j5.ToString();
            Speed(speed, instant);
            SendMessage(message, instant);
            
            
        }

        /// <summary>
        /// Resets Roboter state
        /// </summary>
        public void Reset()
        {
            SendMessage("RS", true);
        }

        /// <summary>
        /// Moves to Nest Position
        /// </summary>
        public void Nest(bool instant)
        {
            SendMessage("NT", instant);
        }

        /// <summary>
        /// Moves to Origin Position
        /// </summary>
        public void Origin(bool instant)
        {
            SendMessage("OG", instant);
        }

        /// <summary>
        /// Sets the Speed
        /// </summary>
        /// <param name="speed">Speed value to set, must be between 1 and 7</param>
        public void Speed(int speed, bool instant)
        {
            if(speed > 0 && speed < 8)
            {
                SendMessage("SP " + speed.ToString(), instant);
            }
            else
            {
                SendMessage("SP 5", instant);
            }
            
        }
        public void SaveFile(string path)
        {
            XmlWriter writer = XmlWriter.Create(path);
            writer.WriteStartDocument();
            writer.WriteStartElement("Pipapo");
            int i = 0;
            foreach(string s in commands)
            {
                writer.WriteElementString("Befehl_"+i.ToString(), s);
                i++;
            }
            
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
           
        }

        
        /// <summary>
        /// Sends Message
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message, bool instant)
        {
            if(instant)
            {
                _connection.Write(message + "\r\n");
            }
            else
            {
                commands.Add(message + "\r\n");                
            }
           
        }

        public void ExecuteCommands()
        {
            _isBusy = true;
            foreach(string command in commands)
            {
                _connection.Write(command);
            }
            _isBusy = false;
        }
        #endregion
    }
}
