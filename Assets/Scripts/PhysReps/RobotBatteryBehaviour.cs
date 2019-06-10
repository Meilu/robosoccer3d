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
            BatteryPercentage = GetBatteryPercentage(CheckIfRobotIsBoosting());

            // Get color based on the new percentage
            BatteryColor = GetBatteryColor();

            // Update the renderer with the new color 
            SetBatteryColor(batteryObject);
        }

        public float BatteryDrain(float currentBatteryPercentage)
        {
            return currentBatteryPercentage - this._drain * Time.deltaTime;
        }

        public float BatteryCharge(float currentBatteryPercentage)
        {
            return currentBatteryPercentage + this._recharge * Time.deltaTime;
        }

        public float GetBatteryPercentage(bool Boost)
        {
            if (Boost && BatteryPercentage > 0.01)
                return  BatteryDrain(BatteryPercentage);
            if (BatteryPercentage < 1)
                return BatteryCharge(BatteryPercentage);

            return 0;
        }

        //TO DO: event gooien
        public bool CheckIfBatteryIsEmpty()
        {
            return BatteryPercentage < 0.01;
        }

        public Color GetBatteryColor()
        {
            Color BatteryFullColor = Color.blue;
            Color BatteryEmptyColor = Color.red;

            return  Color.Lerp(BatteryEmptyColor, BatteryFullColor, BatteryPercentage);
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
