using OsztottSzavazasiRendszerBE.Data;

namespace OsztottSzavazasiRendszerBE.Models
{
    public class Form
    {
        public int Id { get; set; }
        public List<string> Questions { get; set; }
        public List<QuestionType> QuestionType { get; set; }
        public List<string> Validations { get; set; }
        public List<string?>? PossibleAnswers { get; set; }
    }
}
