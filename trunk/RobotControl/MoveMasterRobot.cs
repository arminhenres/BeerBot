using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RobotControl
{
    public class MoveMasterRobot : IRobot
    {
        #region Fields
        private SerialPort _connection;

        private ManualResetEvent _mre = new ManualResetEvent(false);

        private List<RobotCommand> commands;

        private bool _isInitialized;

        private bool _isBusy;

        private string _returnString;
        #endregion

        #region Constructor
        public MoveMasterRobot()
        {
            _isBusy = false;
            _isInitialized = false;
            commands = new List<RobotCommand>();

        }
        #endregion

        #region Properties
        public List<RobotCommand> CommandsList
        {
            get
            {
                return commands;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return _isInitialized;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDatateReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string read = _connection.ReadExisting();
            if (read.Contains("&H0000\r\n"))
            {
                _returnString = string.Empty;
            }
            else
            {
                _returnString = _returnString + read;

                this._mre.Set();

            }

        }

        /// <summary>
        /// Initializes the Robot
        /// </summary>
        public void Init(string portName, int dataBits, int baudRate, string newLine, Parity parity, StopBits stopBits, bool rtsEnable, Handshake handshake)
        {
            _connection = new SerialPort();
            _connection.PortName = portName;
            _connection.DataBits = dataBits;
            _connection.BaudRate = baudRate;
            _connection.NewLine = "LF";
            _connection.Parity = parity;
            _connection.StopBits = stopBits;
            _connection.RtsEnable = true;
            _connection.Handshake = handshake;

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
        /// Turns by specified angles
        /// </summary>
        /// <param name="j1">angle 1</param>
        /// <param name="j2">angle 2</param>
        /// <param name="j3">angle 3</param>
        /// <param name="j4">angle 4</param>
        /// <param name="j5">angle 5</param>
        /// <param name="instant">whether to execute command instant or not</param>
        public void MoveJoint(decimal j1, decimal j2, decimal j3, decimal j4, decimal j5, int speed, bool instant)
        {
            RobotCommand command = new RobotCommand("MJ",j1.ToString() + ", " + j2.ToString() + ", " + j3.ToString() + ", " + j4.ToString() + ", " + j5.ToString());
            Speed(speed, instant);
            SendMessage(command, instant);
        }

        /// <summary>
        /// Moves Roboter arm to given absolut coordinates
        /// </summary>
        /// <param name="coordinates">Coordinates to move to</param>
        /// <param name="speed">Speed to move</param>
        /// <param name="instant">whether to execute command instant or not</param>
        public void MoveAbsolut(Coordinate coordinates, int speed, bool instant)
        {
            var message = new RobotCommand("MP", coordinates.ToString());
            Speed(speed, instant);
            SendMessage(message, instant);
        }
        /// <summary>
        /// Resets Roboter state
        /// </summary>
        public void Reset()
        {
            SendMessage(new RobotCommand("RS",""), true);
        }

        /// <summary>
        /// Closes Grip of Robot
        /// </summary>
        /// <param name="instant">whether to execute command instant or not</param>
        public void GripClose(bool instant)
        {
            RobotCommand command = new RobotCommand("GC", "");
            SendMessage(command, instant);
        }

        /// <summary>
        /// Opens Grip of Robot
        /// </summary>
        /// <param name="instant">whether to execute command instant or not</param>
        public void GripOpen(bool instant)
        {
            RobotCommand command = new RobotCommand("GO", "");
            SendMessage(command, instant);
        }

        public void Timeout(string millis)
        {
            RobotCommand command = new RobotCommand("TI", millis);
            SendMessage(command, false);
        }
        /// <summary>
        /// Moves to Nest Position
        /// </summary>
        /// <param name="instant">whether to execute command instant or not</param>
        public void Nest(bool instant)
        {
            RobotCommand command = new RobotCommand("NT", "");
            SendMessage(command, instant);
        }

        /// <summary>
        /// Moves to Origin Position
        /// </summary>
        /// <param name="instant">whether to execute command instant or not</param>
        public void Origin(bool instant)
        {
            RobotCommand command = new RobotCommand("OG", "");
            SendMessage(command, instant);
        }

        /// <summary>
        /// Sets the Speed
        /// </summary>
        /// <param name="speed">Speed value to set, must be between 1 and 7</param>
        /// <param name="instant">whether to execute command instant or not</param>
        public void Speed(int speed, bool instant)
        {
            if (speed > 0 && speed < 8)
            {
                RobotCommand command = new RobotCommand("SP", speed.ToString());
                SendMessage(command, instant);
            }
            else
            {
                RobotCommand command = new RobotCommand("SP", "5");
                SendMessage(command, instant);
            }

        }

        /// <summary>
        /// Gets the current coordinates of the robot
        /// </summary>
        /// <returns>Coordinates of the robot</returns>
        public Coordinate Where()
        {

            _connection.Write("ID\r\n");
            Thread.Sleep(100);
            _connection.Write("DR\r\n");
            Thread.Sleep(200);


            _connection.Write("WH\r\n");
            _connection.DiscardInBuffer();
            _mre.Reset();
            _mre.WaitOne();
            Thread.Sleep(100);
            return new Coordinate("", _returnString);

        }

        /// <summary>
        /// Saves all commands in commandsloop to xml file
        /// </summary>
        /// <param name="path">Directory and file name where to save to</param>
        public void SaveFile(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(path, settings);

            
            writer.WriteStartDocument();
            writer.WriteStartElement("Pipapo");
            int i = 0;
            foreach (RobotCommand s in commands)
            {
                writer.WriteStartElement("Befehl");
                writer.WriteAttributeString("Command", s.CommandType);
                writer.WriteAttributeString("Parameters",s.Parameters);
                writer.WriteEndElement();
                i++;
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();

        }

        /// <summary>
        /// Reads commands out of File into commands buffer
        /// </summary>
        /// <param name="path">Path to file</param>
        public void ReadFile(string path)
        {
            var document = XDocument.Load(path);

            var elements = document.Descendants("Befehl");
            commands.Clear();
            foreach (var command in elements)
            {
                commands.Add(new RobotCommand(command.Attribute("Command").Value, command.Attribute("Parameters").Value));
                
            }
        }

        /// <summary>
        /// Sends Message
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(RobotCommand message, bool instant)
        {
            if (instant)
            {
                _connection.Write(message.ToString());
            }
            else
            {
                commands.Add(message);
            }

        }

        /// <summary>
        /// Executes all commands in loop
        /// </summary>
        public void ExecuteCommands()
        {
            _isBusy = true;
            foreach (RobotCommand command in commands)
            {
                SendMessage(command,true);
            }
            _isBusy = false;
        }
        #endregion
               
    }
}
