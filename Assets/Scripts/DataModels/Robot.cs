namespace DataModels
{
    public class Robot
    {
        public string Name { get; set; }
        public int Number { get; set; }

        //should teamposition be here? where do we want to give the robot the teamposition?
        // Answer: Yes this is a perfect position! we just werent using this class yet. 
        // I have implemented it in the team class now. Robot prefabs will now be created based on the settings of a list of this class
        // Check out team.cs :)
        public TeamPosition TeamPosition { get; set; }

        public float AttackPower { get; set; }
        public float DefensePower { get; set; }
        public float Speed { get; set; }
    }
}