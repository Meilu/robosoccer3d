using System.Collections;
using System.Collections.Generic;
using DataModels;
using System.Linq;
using Actuators;
using UnityEngine;
using NUnit.Framework;
using UnityEditor;
using RobotBattery = PhysReps.RobotBattery;
using RobotActionStates;

namespace Tests
{
    [TestFixture]
    public class RobotBatteryTests
    {
        private RobotBattery _robotBattery;
        private RobotActuator _robotActuator;

        [SetUp]
        public void Init()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/robot.prefab");
            var _robot = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));

            _robotActuator = _robot.GetComponentInParent<RobotActuator>();
            _robotBattery = new RobotBattery(_robotActuator);
       
        }

       //// [Test]
       // public void ShowBatteryPercentage_FullBatteryColor()
       // {

       //     Color batteryColor = Color.red;

       //     float BatteryPercentage = 1;

       //    // _robotBattery.ShowBatteryPercentage(_robotBattery);
       //    // Assert.IsTrue(batteryObject.GetComponent<Renderer>().material.color == batteryColor);
       // }

       // [Test]
       // public void BatteryDrain_ReturnsLowerFloat()
       // {
       //     float batteryPercentage = 0.5f;

       //     float outCome = _robotBattery.BatteryDrain(batteryPercentage);
       //     Assert.IsTrue(outCome < batteryPercentage);
       // }

       // [Test]
       // public void BatteryDrain_cantDrainMore()
       // {
       //     float batteryPercentage = 0;

       //     float outCome = _robotBattery.BatteryDrain(batteryPercentage);
       //     Assert.IsTrue(outCome == batteryPercentage);
       // }

       // [Test]
       // public void BatteryCharge__ReturnsHigherFloat()
       // {
       //     float batteryPercentage = 0.5f;

       //     float outCome = _robotBattery.BatteryCharge(batteryPercentage);
       //     Assert.IsTrue(outCome < batteryPercentage);
       // }

       // [Test]
       // public void BatteryCharge__cantChargeMore()
       // {
       //     float batteryPercentage = 1f;

       //     float outCome = _robotBattery.BatteryCharge(batteryPercentage);
       //     Assert.IsTrue(outCome == batteryPercentage);
       // }

   
        [Test]
        public void GetBatteryPercentage__ifNotBoostBatteryCharges()
        {
            bool Boost = false;
            float batteryPercentage = 0.5f;

            float outCome = _robotBattery.GetBatteryPercentage(Boost, batteryPercentage);
            Assert.IsTrue(outCome < batteryPercentage);
        }

        [Test]
        public void GetBatteryPercentage__ifBoostButBatteryEmptyNoDrain()
        {
            bool Boost = true;
            float batteryPercentage = 0;

            float outCome = _robotBattery.GetBatteryPercentage(Boost, batteryPercentage);
            Assert.IsTrue(outCome < batteryPercentage);
        }
        
       // [Test]
       // public void GetBatteryColor_ReturnBatteryEmptyColor()
       // {
       //     Color BatteryFullColor = Color.blue;
       //     Color BatteryEmptyColor = Color.red;

       //     float batteryPercentage = 0;

       //     Assert.IsTrue(_robotBattery.GetBatteryColor(batteryPercentage) == Color.red);
       // }
        
       // [Test]
       // public void GetBatteryColor_ReturnBatteryFullColor()
       // {
       //     Color BatteryFullColor = Color.blue;
       //     Color BatteryEmptyColor = Color.red;

       //     float batteryPercentage = 1;

       //     Assert.IsFalse(_robotBattery.GetBatteryColor(batteryPercentage) == Color.red);
       // }

        [Test]
        public void GetBatteryPercentage_SlightlyDrainedPercentage_()
        {
            var batteryPercentage = 0.2f;
            var outCome = _robotBattery.GetBatteryPercentage(true, batteryPercentage);
            Assert.IsTrue(outCome < batteryPercentage);
        }


        [Test]
        public void RobotIsBoosting_returnsTrue()
        {
            _robotActuator.activeRobotActionState = new RobotActionState(RobotArmAction.DoNothing,
                                                                        RobotLegAction.DoNothing,
                                                                        RobotMotorAction.BoostForward,
                                                                        RobotWheelAction.DoNothing,
                                                                        0);
            Assert.IsTrue(_robotBattery.CheckIfRobotIsBoosting());
        }

        [Test]
        public void RobotIsNotBoosting_returnsfalse()
        {
            _robotActuator.activeRobotActionState = new RobotActionState(RobotArmAction.DoNothing,
                                                                        RobotLegAction.DoNothing,
                                                                        RobotMotorAction.DoNothing,
                                                                        RobotWheelAction.DoNothing,
                                                                        0);
            Assert.IsFalse(_robotBattery.CheckIfRobotIsBoosting());
        }

    }
}

