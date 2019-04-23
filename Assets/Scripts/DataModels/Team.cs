using System.Collections.Generic;

namespace DataModels
{
    public class Team
    {
        public string Name { get; set; }
        public IList<Robot> Players { get; set; }
        public Stadium Stadium { get; set; }
    }
}