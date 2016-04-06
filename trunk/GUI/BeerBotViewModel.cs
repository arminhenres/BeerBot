using Core;
using RobotControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class BeerBotViewModel
    {
        IRobot _robot;
        public BeerBotViewModel()
        {

        }

        public BeerBotViewModel(IRobot robot)
        {
            _robot = robot;
        }
    }
}
