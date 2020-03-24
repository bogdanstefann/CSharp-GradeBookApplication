using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {

        public RankedGradeBook(string name) : base(name)
        {
            this.Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var numberOfStudents = this.Students.Count;
            if (numberOfStudents < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            int indexPercentage = (int)Math.Ceiling(numberOfStudents * 0.2);
            var sortedGrades = Students.OrderByDescending(student => student.AverageGrade).ToList();

            if (sortedGrades[indexPercentage - 1].AverageGrade <= averageGrade)
            {
                return 'A';
            }
            else if (sortedGrades[indexPercentage * 2 - 1].AverageGrade <= averageGrade)
            {
                return 'B';
            }
            else if (sortedGrades[indexPercentage * 3 - 1].AverageGrade <= averageGrade)
            {
                return 'C';
            }
            else if (sortedGrades[indexPercentage * 4 - 1].AverageGrade <= averageGrade)
            {
                return 'D';
            }
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (this.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            int numberOfValidStudents = this.Students.Count(x => x.AverageGrade >= 0.0);
            if (numberOfValidStudents < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
