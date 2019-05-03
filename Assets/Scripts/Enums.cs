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

public enum RobotMotorAction
{
    None,
    MoveForward,
    MoveBackward,
    BoostForward,
    DoNothing,
    MoveLeft,
    MoveRight
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

public enum TeamPosition
{
    Attacker,
    Defender,
    Keeper
}

public enum RobotPropertyChangeHandlerType
{
    ObjectInsideVision,
    ObjectWithinDistance
}

//TO DO: is dit hieronder wat hierboven beter wordt gedaan?
public enum VisionTrigger
{
    ballInsideVision,
    ballInsideShootingRange,
    GoalInsideVision,
    GoalsInsideShootingRange
}