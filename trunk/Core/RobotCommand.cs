using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core
{
    public class RobotCommand
    {

        public RobotCommand(string commandType, string parameters)
        {
            CommandType = commandType;
            Parameters = parameters;
        }

        public string CommandType
        {
            get;
            set;
        }

        public string Parameters
        {
            get;
            set;
        }

        public override string ToString()
        {
            string returnString = CommandType + " " + Parameters + "\r\n";
            return returnString;
        }
    }
}
