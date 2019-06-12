using Actuators;
using UnityEngine;

namespace PhysReps
{

    public class RobotBatteryBehaviour : MonoBehaviour
    {
        private RobotBattery _robotBattery;

        // Start is called before the first frame update
        void Awake()
        {
            // heb je maar 1 keer nodig
            var robotActuator = GetComponentInParent<RobotActuator>();
            _robotBattery = new RobotBattery(robotActuator);
        }

        // Update is called once per frame
        void Update()
        {
            _robotBattery.ShowBatteryPercentage(gameObject);
        }
    }

    public class RobotBattery
    {   
        private float BatteryPercentage = 1.0f;
        private float _drain = 0.3f;
        private float _recharge = 0.1f;

        public RobotActuator _RobotActuator;
        public Color BatteryColor;

        public RobotBattery(RobotActuator robotActuator)
        {
            _RobotActuator = robotActuator;
        }

        public void ShowBatteryPercentage(GameObject batteryObject)
        {
            // Get new percentage
            BatteryPercentage = GetBatteryPercentage(CheckIfRobotIsBoosting(), BatteryPercentage);

            // Get color based on the new percentage
            BatteryColor = GetBatteryColor(BatteryPercentage);

            // Update the renderer with the new color 
            SetBatteryColor(batteryObject);
        }

        public float GetBatteryPercentage(bool Boost, float currentBatteryPercentage)
        {
            if (Boost && BatteryPercentage > 0.01)
                return currentBatteryPercentage - this._drain * Time.deltaTime;
            if (BatteryPercentage < 1)
                return currentBatteryPercentage + this._recharge * Time.deltaTime;
            else return 0;
        }

        //TO DO: event gooien
        public bool CheckIfBatteryIsEmpty()
        {
            return BatteryPercentage < 0.01;
        }

        public Color GetBatteryColor(float currentBatteryPercentage)
        {
            Color BatteryFullColor = Color.blue;
            Color BatteryEmptyColor = Color.red;

            return  Color.Lerp(BatteryEmptyColor, BatteryFullColor, currentBatteryPercentage);
        }

        public bool CheckIfRobotIsBoosting()
        {
            if (_RobotActuator.activeRobotActionState == null)
                return false;

            return _RobotActuator.activeRobotActionState.MotorAction == RobotMotorAction.BoostForward;
        }

        public void SetBatteryColor(GameObject batteryObject)
        {
            batteryObject.GetComponent<Renderer>().material.color = BatteryColor;
        }
    }
}
