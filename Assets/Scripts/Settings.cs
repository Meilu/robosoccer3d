public class Settings
{
    public const string SoccerBallObjectName = "soccerball";
    public const string RobotFieldOfViewObjectName = "robotFieldOfView";
    public const string RobotMovementStatusObjectName = "robotMovementSensor";
    public const string OtherRobotsTagName = "robot";
    public const string AwayGoalLine = "AwayGoalLine";
    public const string HomeGoalLine = "HomeGoalLine";
    
    public int NormalHalfDuration { get; set; } 
    public int ExtraHalfDuration { get; set; }
}