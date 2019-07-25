using System;
using PhysReps;

namespace DataModels
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
    }
}