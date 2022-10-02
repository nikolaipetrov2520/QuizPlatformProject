using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication.InputModels
{
    public class QuestionInputModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public IEnumerable<PossibleAnswerInputModel> PossibleAnswer { get; set; }
    }
}
