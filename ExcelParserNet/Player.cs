using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticPlugin
{
    public class Player
    {
        public int Id { get; set; } 

        public string SteamId { get; set; }
        public string NickName { get; set; }

        public string Role { get; set; }
        
        public int? Ip { get; set; }

        public int? Time { get; set; }

        public DateTime LastDataTime { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
