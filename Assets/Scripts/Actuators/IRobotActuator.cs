using DataModels;

namespace Actuators
{
    public interface IRobotActuator
    {
        RobotActionState ActiveRobotActionState { get; }
        bool IsBoosting { get; }
        void ExecuteRobotAction(RobotActionState robotActionState);
        void MoveRight();
        void MoveLeft();
        void MoveForward();
        void BoostForward();
        void MoveBackward();
        void TurnRight();
        void TurnLeft();
        void KickForward();
    }
}