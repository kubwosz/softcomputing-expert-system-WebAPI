using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcomputing_expert_system.Model
{
    public class Question
    {
        public int questionID { get; set; }
        //public string Text { get; set; }
        //public Answer Answers { get; set; }
        public int[] answers { get; set; }
    }
}
