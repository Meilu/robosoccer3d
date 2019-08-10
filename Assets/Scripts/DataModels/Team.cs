using System.Collections.Generic;
using UnityEngine;

namespace DataModels
{
    public class Team
    {
        public string Name { get; set; }
        public Color TeamColor { get; set;  }
        public IList<Robot> Players { get; set; }
        public TeamSide Side { get; set; }

        public int GetPlayerCount()
        {
            if (Players == null)
                return 0;
            
            return Players.Count;
        }
    }
}