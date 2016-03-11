using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IRobot
    {
        void Init(string portName, int dataBits, int baudRate, string newLine, Parity parity, StopBits stopBits, bool rtsEnable, Handshake handshake);

        void Deinit();

        void MoveJoint(decimal j1, decimal j2, decimal j3, decimal j4, decimal j5, int speed, bool instant);

        void MoveAbsolut(Coordinate coordinates, int speed, bool instant);

        void Reset();

        Coordinate Where();

        void SaveFile(string path);

        void ExecuteCommands();

        void GripClose(bool instant);

        void GripOpen(bool instant);

        void Nest(bool instant);

        void Origin(bool instant);

        void Speed(int speed, bool instant);

        void ReadFile(string path);

        List<RobotCommand> CommandsList
        {
            get;
        }
    }
}
