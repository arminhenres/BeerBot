using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Description of SerialBus-Class
    /// </summary>
    public class SerialBus : IBus
    {
        #region Fields
        /// <summary>
        /// Connection to serial port
        /// </summary>
        private SerialPort _serialPort;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SerialBus"/> class.
        /// </summary>
        public SerialBus()
        {
            _serialPort = new SerialPort();
        }
        #endregion

        #region Events
        /// <summary>
        /// Event to be thrown when a message is received
        /// </summary>
        public event EventHandler MessageReceived;
        #endregion

        #region Properties
        public bool ConnectionActive
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the BaudRate
        /// </summary>
        public int BaudRate
        {
            get
            {
                return _serialPort.BaudRate;
            }

            set
            {
                if (value < 460800 && value > 0)
                {
                    _serialPort.BaudRate = value;
                }
                else
                {
                    _serialPort.BaudRate = 500;
                }
            }
        }

        /// <summary>
        /// Gets or sets the port name
        /// </summary>
        public string PortName
        {
            get
            {
                return _serialPort.PortName;
            }

            set
            {
                _serialPort.PortName = value;
            }
        }

        /// <summary>
        /// Sets the Parity
        /// </summary>
        public Parity Parity
        {
            set
            {
                _serialPort.Parity = value;
            }
        }

        /// <summary>
        /// Gets or sets the standard length of data bits per byte.
        /// </summary>
        public int DataBits
        {
            get
            {
                return _serialPort.DataBits;
            }

            set
            {
                _serialPort.DataBits = value;
            }
        }

        /// <summary>
        /// Sets the StopBits
        /// </summary>
        public StopBits StopBits
        {
            set
            {
                _serialPort.StopBits = value;
            }
        }

        /// <summary>
        /// Sets the Handshake
        /// </summary>
        public Handshake Handshake
        {
            set
            {
                _serialPort.Handshake = value;
            }
        }

        public bool DTR
        {
            set
            {
                _serialPort.DtrEnable = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tries to initialize
        /// </summary>
        /// <returns>state whether initialization was successful</returns>
        public bool Init()
        {
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(On_MessageReceived);

            if(!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }

            return true;
        }

        private void On_MessageReceived(object sender, SerialDataReceivedEventArgs e)
        {
            MessageReceived(sender, e);
        }

        /// <summary>
        /// Tries to de-initialize
        /// </summary>
        /// <returns>state whether de-initialization was successful</returns>
        public bool Deinit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends message
        /// </summary>
        /// <param name="message">message to send</param>
        public void SendMessage(IMessage message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Builds message out of given data
        /// </summary>
        /// <param name="data">data to build message out</param>
        /// <returns>the ready-to-send message</returns>
        public IMessage BuildMessage(string data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads stream
        /// </summary>
        public void Read()
        {
        }
        #endregion
    }
}
