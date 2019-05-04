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

//Logical to make an enum of TeamPosition I think?
public enum TeamPosition
{
    Attacker,
    Defender,
    Midfielder,
    Keeper,
    Lazy
}

public enum RobotPropertyChangeHandlerType
{
    ObjectInsideVision,
    ObjectWithinDistance
}

public enum ObjectMovementSpeed
{
    None,
    Still,
    Slow,
    Medium,
    Fast,
    LightSpeed
}

public enum ObjectMovementDirection
{
    None,
    Forward,
    Backwards,
    Left,
    Right
}

//TO DO: is dit hieronder wat hierboven beter wordt gedaan?
public enum VisionTrigger
{
    ballInsideVision,
    ballInsideShootingRange,
    GoalInsideVision,
    GoalsInsideShootingRange
}
enum Formation
{
    Offensive,
    Defensive,
    Neutral
}