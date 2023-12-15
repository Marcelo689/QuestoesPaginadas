namespace BancoProject
{
    public class ExamAnswerSheet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
    }
}
