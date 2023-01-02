using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programmation_systeme
{
    internal class Values
    {
        public List<string> ScoreList { get; set; } = new List<string>();
        public List<string> UserList { get; set; } = new List<string>();
        public List<string> Settings { get; set; } = new List<string>();
        public bool Status { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string File { get; set; }
        public string Folder { get; set; }
        public Values() { }
        public static readonly Values Instance = new Values();

    }
}
