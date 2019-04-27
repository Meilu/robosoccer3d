using System;

public enum BoundarySide
{
    TopLeftEndline,
    BottomLeftEndline,
    TopRightEndline,
    BottomRightEndline,
    TopLeftSideline,
    TopRightSideLine,
    BottomLeftSideline,
    BottomRightSideline,
    LeftGoalLine,
    RightGoalLine
}

public enum EventType
{
    BallOut,
    HomeGoal,
    AwayGoal,
    MatchFinished
}

//TO DO: to do something with this?
public enum VisionTrigger
{
    ballInsideVision, 
    ballInsideShootingRange, 
    GoalInsideVision,
    GoalsInsideShootingRange
}

public enum RobotMotorAction
{
    None,
    MoveForward,
    MoveBackward,
    DoNothing
}

public enum RobotWheelAction
{
    None,
    TurnRight,
    TurnLeft,
    DoNothing
}

public enum RobotLegAction
{
    None,
    KickForward,
    DoNothing
}

public enum RobotArmAction
{
    None,
    MoveRightArmInward,
    MoveRightArmOutward,
    MoveLeftArmInward,
    MoveLeftArmOutward,
    DoNothing
}

public enum TeamSide
{
    Home,
    Away
}

public enum RobotPropertyChangeHandlerType
{
    ObjectInsideVision,
    ObjectWithinDistance
}