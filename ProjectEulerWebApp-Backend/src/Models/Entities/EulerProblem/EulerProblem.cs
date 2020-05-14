using System;

namespace ProjectEulerWebApp.Models.Entities.EulerProblem
{
    public class EulerProblem
    {
        public EulerProblem(int id,
                            string title,
                            string description,
                            DateTime? solveDate = null,
                            string solution = null,
                            DateTime? publishDate = null,
                            int? difficulty = null)
        {
            Id = id;
            Title = title;
            Description = description;
            IsSolved = solveDate.HasValue;
            SolveDate = solveDate;
            Solution = solution;
            PublishDate = publishDate;
            Difficulty = difficulty;
        }

        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public bool IsSolved { get; }
        public DateTime? SolveDate { get; }
        public string Solution { get; }
        public DateTime? PublishDate { get; }
        public int? Difficulty { get; }

        public override string ToString()
        {
            return "Id: " + Id + "\t" +
                   "Title: " + Title + "\t" +
                   "Description: " + Description + "\t" +
                   "IsSolved: " + IsSolved + "\t" +
                   "SolveDate: " + SolveDate + "\t" +
                   "Solution: " + Solution + "\t" +
                   "PublishDate: " + PublishDate + "\t" +
                   "Difficulty: " + Difficulty;
        }
    }
}