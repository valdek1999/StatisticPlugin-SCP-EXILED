using Exiled.API.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticPlugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public string FullPath { get; set; } = @"C:\Users\HardFoxy\AppData\Roaming\EXILED\StatPlayers\stat.txt";

    }
}
