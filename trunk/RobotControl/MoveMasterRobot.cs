using Core;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotControl
{
    public class MoveMasterRobot
    {
        private IBus _connection;

        public MoveMasterRobot()
        {
            _connection = new SerialBus() { PortName = "COM1", BaudRate = 9600, DataBits = 8, Parity = Parity.Even, StopBits = StopBits.Two, DTR = true };

        }

        public void Init()
        {
            if (!_connection.ConnectionActive)
            {
                _connection.Init();
            }
        }

        public void Deinit()
        {
            if (_connection.ConnectionActive)
            {
                _connection.Deinit();
            }
        }

        public void SendMessage(MoveMasterMessage message)
        {
            
        }

        public MoveMasterMessage BuildMessage()
        {

        }
    }
}
