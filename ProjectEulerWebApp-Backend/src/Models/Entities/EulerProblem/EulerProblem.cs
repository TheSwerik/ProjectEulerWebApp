using System;
using System.Linq;

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
                            int? difficulty = null,
                            long[]? times = null,
                            bool eulerPlus = false)
        {
            Id = id;
            Title = title;
            Description = description;
            PublishDate = publishDate;
            Difficulty = difficulty;
            IsSolved = solveDate.HasValue;
            SolveDate = solveDate;
            Solution = solution;
            Times = times;
            EulerPlus = eulerPlus;
        }

        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? Difficulty { get; set; }
        public bool IsSolved { get; set; }
        public DateTime? SolveDate { get; set; }
        public string Solution { get; set; }
        public long[]? Times { get; set; }
        public bool EulerPlus { get; set; }

        public override string ToString()
        {
            return "{ " +
                   "Id: " + Id + "; " +
                   "Title: " + Title + "; " +
                   "PublishDate: " + PublishDate + "; " +
                   "Difficulty: " + Difficulty + "; " +
                   // "Description: " + Description + "\t" +
                   "IsSolved: " + IsSolved + "; " +
                   "SolveDate: " + SolveDate + "; " +
                   "Solution: " + Solution + "; " +
                   "Times: " + string.Join("ms ", Times ?? Array.Empty<long>()) + "; " +
                   "ProjectEuler+: " + EulerPlus +
                   " }";
        }
    }
}