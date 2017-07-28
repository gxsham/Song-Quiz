using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongQuiz
{
    public class Song
    {
        public byte[] RightAnswer { get; set; }
        public int Interval { get; set; }
        public List<string> Answers { get; set; }
        public Song()
        {
            Answers = new List<string>();
        }
    }
}
