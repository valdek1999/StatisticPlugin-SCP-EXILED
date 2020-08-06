using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StatisticPlugin
{
    public class Character
    {
        public int Id { get; set; }
        public int? Expirience { get; set; }

        public int? Lvl { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

    }
}
