using System.Timers;
using Actuators;
using UnityEngine;

namespace PhysReps
{

    public class RobotBatteryBehaviour : MonoBehaviour
    {
        private RobotBattery _robotBattery;
        private Renderer _batteryRenderer;
        void Awake()
        {
            var robotActuator = GetComponentInParent<RobotActuator>();
            _robotBattery = new RobotBattery(robotActuator);
            _batteryRenderer = gameObject.GetComponent<Renderer>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _robotBattery.UpdateBatteryPercentage();
            
            var batteryColor = _robotBattery.GetBatteryColor();
            _batteryRenderer.material.color = batteryColor;
        }
    }

    public class RobotBattery
    {   
        
        private float _drainAmountEachTick = 0.3f;
        private float _rechargeAmountEachTick = 0.1f;
        private Color _batteryFullColor = Color.blue;
        private Color _batteryEmptyColor = Color.red;

        private Timer BatteryDepletedTimeoutTimer;
        private IRobotActuator _robotActuator;

        public float BatteryPercentage = 0.0f;
        public bool IsBatteryEmpty => BatteryPercentage < 0.1f;
        public RobotBattery(IRobotActuator robotActuator)
        {
            _robotActuator = robotActuator;
        }

        public void UpdateBatteryPercentage()
        {
            BatteryPercentage = CalculateBatteryPercentage();
        }

        public float CalculateBatteryPercentage()
        {
            // When our battery ran out, start a small recharge timeout 
            if (IsBatteryEmpty && !BatteryDepletedTimeoutTimer.Enabled)
                StartDepletedTimeout();
            
            // Recharge when we are not boosting or when the recharge timer is running
            if ((!_robotActuator.IsBoosting && BatteryPercentage < 1) || BatteryDepletedTimeoutTimer.Enabled)
                return BatteryPercentage + _rechargeAmountEachTick;
            
            // If boosting with enough battery left, drain battery while we can.
            if (_robotActuator.IsBoosting && !IsBatteryEmpty)
                return BatteryPercentage - _drainAmountEachTick;

            return 0;
        }

        public void StartDepletedTimeout()
        {
            if (BatteryDepletedTimeoutTimer.Enabled)
                return;

            BatteryDepletedTimeoutTimer = new Timer(2000)
            {
                Enabled = true
            };
        }
               
        public Color GetBatteryColor()
        {
            return Color.Lerp(_batteryEmptyColor, _batteryFullColor, BatteryPercentage);
        }
    }
}
