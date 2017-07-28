using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongQuiz
{
    public class Quiz
    {
        public string Name { get; set; }
        public List<Song> Songs { get; set; }
        public Quiz()
        {
            Songs = new List<Song>();
        }
    }
}
