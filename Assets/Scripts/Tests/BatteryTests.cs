using Actuators;

using NUnit.Framework;
using RobotBattery = PhysReps.RobotBattery;

namespace Tests
{
    [TestFixture]
    public class RobotBatteryTests
    {
        private RobotBattery _robotBattery;
        private IRobotActuator _robotActuator;

        [SetUp]
        public void Init()
        {
            // With substitute.for we create a mock for the IRobotActuator interface.
            
            // Then we can pass this mock to the robotbattery so that it is not depended on the robotactuator itself.
            _robotBattery = new RobotBattery(_robotActuator);
       
        }

        [Test]
        public void GetBatteryPercentage_BatteryIsEmptyNoTimer_RechargeTimerStarted()
        {
            // Setup
            _robotBattery.BatteryPercentage = 0.01f;
            
            //Act
            var result = _robotBattery.CalculateBatteryPercentage();
            
            // Assert
          //  Assert.IsTrue();
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
//   
//        [Test]
//        public void GetBatteryPercentage__ifNotBoostBatteryCharges()
//        {
//            bool Boost = false;
//            float batteryPercentage = 0.5f;
//
//            float outCome = _robotBattery.GetBatteryPercentage(Boost, batteryPercentage);
//            Assert.IsTrue(outCome < batteryPercentage);
//        }
//
//        [Test]
//        public void GetBatteryPercentage__ifBoostButBatteryEmptyNoDrain()
//        {
//            bool Boost = true;
//            float batteryPercentage = 0;
//
//            float outCome = _robotBattery.GetBatteryPercentage(Boost, batteryPercentage);
//            Assert.IsTrue(outCome < batteryPercentage);
//        }
//
//
//        [Test]
//        public void GetBatteryPercentage_SlightlyDrainedPercentage_()
//        {
//            var batteryPercentage = 0.2f;
//            var outCome = _robotBattery.GetBatteryPercentage(true, batteryPercentage);
//            Assert.IsTrue(outCome < batteryPercentage);
//        }

    

    }
}

