using System;
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
            _robotBattery = new RobotBattery(robotActuator, new TimerAdapter());
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
        private int _depletionTimeoutIntervalInSeconds = 2;

        public ITimer BatteryDepletedTimeoutTimer;
        private IRobotActuator _robotActuator;

        public float BatteryPercentage = 0.0f;
        public bool IsBatteryEmpty => BatteryPercentage < 0.1f;
        public bool IsBatteryFull => BatteryPercentage >= 1;

        public RobotBattery(IRobotActuator robotActuator, ITimer timer)
        {
            _robotActuator = robotActuator;
            BatteryDepletedTimeoutTimer = timer;
        }

        public void UpdateBatteryPercentage()
        {
            BatteryPercentage = CalculateBatteryPercentage();
        }

        public float CalculateBatteryPercentage()
        {
            // When our battery ran out, start a small recharge timeout 
            if (IsBatteryEmpty && !BatteryDepletedTimeoutTimer.Enabled)
                BatteryDepletedTimeoutTimer.StartTimeout(_depletionTimeoutIntervalInSeconds);
            
            // Recharge when we are not boosting or when the recharge timer is running
            if ((!_robotActuator.IsBoosting && !IsBatteryFull) || BatteryDepletedTimeoutTimer.Enabled)
                return BatteryPercentage + _rechargeAmountEachTick;
            
            // If boosting with enough battery left, drain battery while we can.
            if (_robotActuator.IsBoosting)
                return BatteryPercentage - _drainAmountEachTick;

            return 0;
        }

        public Color GetBatteryColor()
        {
            return Color.Lerp(_batteryEmptyColor, _batteryFullColor, BatteryPercentage);
        }
    }
}
