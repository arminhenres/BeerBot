using RobotControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GUI
{
    public class ViewModel : NotifyPropertyChanged
    {
        private MoveMasterRobot _robot;
        public ViewModel()
        {
            _robot = new MoveMasterRobot();
            SaveSettings = new ActionCommand(SaveSettingsCommand);
            ReadSettings();
        }

        private void SaveSettingsCommand()
        {
            SaveSettingss(Port, Baud, ParityBit, Handshakee, Bits, StopBit);
        }

        private void SaveSettingss(string port, int baud, Parity parity, Handshake handshake, int dataBits, StopBits stopBits)
        {
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, NewLineOnAttributes = true };
            XmlWriter writer = XmlWriter.Create("Settings.xml", settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Settings");

            writer.WriteElementString("Port", port);
            writer.WriteElementString("Baudrate", baud.ToString());
            writer.WriteElementString("Parity", parity.ToString());
            writer.WriteElementString("Handshake", handshake.ToString());
            writer.WriteElementString("Data_Bits", dataBits.ToString());
            writer.WriteElementString("Stop_Bits", stopBits.ToString());

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }
        private void ReadSettings()
        {
            if (File.Exists("Settings.xml"))
            {
                XmlReader reader = XmlReader.Create("Settings.xml");
                Dictionary<string, string> settings = new Dictionary<string, string>();

                reader.ReadToFollowing("Port");
                var port = reader.ReadElementContentAsString();
                Port = port;

                reader.ReadToFollowing("Baudrate");
                var baud = reader.ReadElementContentAsString();
                Convert.ToInt32(baud);

                reader.ReadToFollowing("Parity");
                var parity = reader.ReadElementContentAsString();
                ParityBit = (Parity)System.Enum.Parse(typeof(Parity), parity);

                reader.ReadToFollowing("Handshake");
                var handshake = reader.ReadElementContentAsString();
                Handshakee = (Handshake)System.Enum.Parse(typeof(Handshake), handshake);

                reader.ReadToFollowing("Data_Bits");
                var databits = reader.ReadElementContentAsString();
                Bits = Convert.ToInt32(databits);

                reader.ReadToFollowing("Stop_Bits");
                var stopbits = reader.ReadElementContentAsString();
                StopBit = (StopBits)System.Enum.Parse(typeof(StopBits), stopbits);
                reader.Close();
            }
            else
            {
                SaveSettingss("", 300, Parity.None, Handshake.None, 5, StopBits.None);
                ReadSettings();
            }

        }

        private bool _isInitialized;
        public bool IsInitialized
        {
            get
            {
                return _isInitialized;
            }
            set
            {
                if (!value)
                {
                    _robot.Deinit();
                }
                else
                {
                    _robot.Init();
                }
                _isInitialized = value;
            }
        }

        public ObservableCollection<string> Ports
        {
            get
            {
                var ports = SerialPort.GetPortNames();
                ObservableCollection<string> portList = new ObservableCollection<string>();
                foreach (string port in ports)
                {
                    portList.Add(port);
                }
                return portList;
            }
        }

        string _port;
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                OnPropertyChanged("Port");
            }
        }
        private int _bits;
        public int Bits
        {
            get
            {
                return _bits;
            }
            set
            {
                _bits = value;
            }
        }

        private int _baud;
        public int Baud
        {
            get
            {
                return _baud;
            }
            set
            {
                _baud = value;
            }
        }

        private Parity _parity;
        public Parity ParityBit
        {
            get
            {
                return _parity;
            }
            set
            {
                _parity = value;
                OnPropertyChanged("ParityBit");
            }
        }

        private Handshake _handshake;
        public Handshake Handshakee
        {
            get
            {
                return _handshake;
            }
            set
            {
                _handshake = value;
                OnPropertyChanged("Handshakee");
            }
        }

        private StopBits _stopBits;
        public StopBits StopBit
        {
            get
            {
                return _stopBits;
            }
            set
            {
                _stopBits = value;
                OnPropertyChanged("StopBit");
            }
        }

        public ActionCommand SaveSettings
        {
            get;
            set;
        }
    }
}
