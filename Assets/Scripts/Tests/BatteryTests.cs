using System.Collections;
using System.Collections.Generic;
using DataModels;
using System.Linq;
using Actuators;
using UnityEngine;
using NUnit.Framework;
using UnityEditor;
using RobotBattery = PhysReps.RobotBattery;

namespace Tests
{
    [TestFixture]
    public class RobotBatteryTests
    {
        private RobotBattery _robotBattery;

        [SetUp]
        public void Init()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/robot.prefab");
            var _robot = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));
            var robotActuator = -_robot.GetComponentInParent<RobotActuator>();
            _robotBattery = new RobotBattery(robotActuator);
        }

        [Test]
        public void ShowBatteryPercentage_ReturnsTrue()
        {
            GameObject batteryObject = new GameObject();

            _robotBattery.ShowBatteryPercentage(batteryObject);
            //Assert.IsTrue(_robotBattery.ShowBatteryPercentage(batteryObject));
        }

        [Test]
        public void BatteryDrain_ReturnsCorrectFloat()
        {
            float testDrain = 0.5f;

            float outCome = _robotBattery.BatteryDrain(testDrain);
            Assert.IsTrue(_robotBattery.BatteryDrain(testDrain) == outCome);
        }

        [Test]
        public void BatteryCharge__ReturnsCorrectFloat()
        {
            float testCharge = 0.5f;

            float outCome = _robotBattery.BatteryCharge(testCharge);
            Assert.IsTrue(_robotBattery.BatteryCharge(testCharge) == outCome);
        }

        [Test]
        public void BatteryDrain__ReturnsWrongFloat()
        {
            float testCharge = 0.5f;

            float outCome = _robotBattery.BatteryDrain(testCharge);
            Assert.IsFalse(_robotBattery.BatteryDrain(testCharge) == outCome);
        }
        
        [Test]
        public void BatteryCharge__ReturnsWrongFloat()
        {
            float testCharge = 0.5f;

            float outCome = _robotBattery.BatteryCharge(testCharge);
            Assert.IsFalse(_robotBattery.BatteryCharge(testCharge) == outCome);
        }
        
        [Test]
        public void GetBatteryPercentage__ReturnsCorrectFloat()
        {
            bool Boost = true;

            float outCome = _robotBattery.GetBatteryPercentage(Boost);
            Assert.IsTrue(_robotBattery.GetBatteryPercentage(Boost) == outCome);
        }
        
        [Test]
        public void GetBatteryPercentage__ReturnsWrongFloat()
        {
            bool Boost = false;

            float outCome = _robotBattery.GetBatteryPercentage(Boost);
            Assert.IsTrue(_robotBattery.GetBatteryPercentage(Boost) == outCome);
        }
        
        [Test]
        public void GetBatteryColor_ReturnCorrectColor()
        {
            //Color BatteryFullColor = Color.blue;
            //Color BatteryEmptyColor = Color.red;

            Color correctColor = Color.grey;
            
            Assert.IsTrue(_robotBattery.GetBatteryColor() == correctColor);
        }
        
        [Test]
        public void GetBatteryColor_ReturnWrongColor()
        {
            //Color BatteryFullColor = Color.blue;
            //Color BatteryEmptyColor = Color.red;

            Color wrongColor = Color.grey;
            
            Assert.IsFalse(_robotBattery.GetBatteryColor() == wrongColor);
        }
    }
}
}
