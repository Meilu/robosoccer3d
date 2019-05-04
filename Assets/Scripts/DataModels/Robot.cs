namespace DataModels
{
    public class Robot
    {
        public string Name { get; set; }
        public int Number { get; set; }

        //should teamposition be here? where do we want to give the robot the teamposition?
        public TeamPosition TeamPosition { get; set; }

        public float AttackPower { get; set; }
        public float DefensePower { get; set; }
        public float Speed { get; set; }
    }
}