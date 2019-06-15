using Actuators;
using NSubstitute;
using NUnit.Framework;
using PhysReps;

namespace Tests
{
    [TestFixture]
    public class RobotBatteryTests
    {
        private RobotBattery _robotBattery;
        private IRobotActuator _robotActuator;
        private ITimer _timer;


        [SetUp]
        public void Init()
        {
            // With substitute.for we create a mock for the IRobotActuator interface.
            _robotActuator = Substitute.For<IRobotActuator>();

            // Because we cannot use time inside unit tests, we need to mock the timer class also
            _timer = Substitute.For<ITimer>();

            // Then we can pass this mock to the robotbattery so that it is not depended on the robotactuator and the timer itself.
            _robotBattery = new RobotBattery(_robotActuator, _timer);
        }

        [Test]
        public void CalculateBatteryPercentage_BatteryIsEmptyNoTimer_RechargeTimerStarted()
        {
            // Setup
            _robotBattery.BatteryPercentage = 0.01f;

            // Set the return value of the enable property to 'false' on the timer mock.
            // This way our first if is triggered (battery is empty and timer is not enabled)
            _timer.Enabled.Returns<bool>(false);

            //Act
            var result = _robotBattery.CalculateBatteryPercentage();


            // Now check if the startimeout on the timer has been called with any amount in seconds.
            // We could also hardcode 2 seconds here, than it will check if this function will always be called
            // with 2 seconds, but that's a bit too strict i think. Any number is fine.
            _timer.Received().StartTimeout(Arg.Any<int>());
        }

        [Test]
        public void CalculateBatteryPercentage_IsNotBoostingNotFull_RechargeAmountAdded()
        {
            // Setup
            _robotBattery.BatteryPercentage = 0.12f;
            _robotActuator.IsBoosting.Returns<bool>(false);

            //Act
            var result = _robotBattery.CalculateBatteryPercentage();

            // Assert
            // Hardcode the expected value.
            var expected = 0.22f;
            Assert.AreEqual(result, expected, 0.001);
        }

        [Test]
        public void CalculateBatteryPercentage_BatteryDepletedTimeoutEnabled_RechargeAmountAdded()
        {
            // Setup
            _robotBattery.BatteryPercentage = 0.12f;
            _robotActuator.IsBoosting.Returns<bool>(true);
            _timer.Enabled.Returns<bool>(true);

            //Act
            var result = _robotBattery.CalculateBatteryPercentage();

            // Assert
            var expected = 0.22f;
            Assert.AreEqual(result, expected, 0.001);
        }

        [Test]
        public void CalculateBatteryPercentage_NotEmptyIsBoostingNoTimeout_DrainAmountRemoved()
        {
            // Setup
            _robotBattery.BatteryPercentage = 0.9f;
            _robotActuator.IsBoosting.Returns<bool>(true);
            _timer.Enabled.Returns<bool>(false);

            //Act
            var result = _robotBattery.CalculateBatteryPercentage();

            // Assert
            var expected = 0.6f;
            Assert.AreEqual(result, expected, 0.001);
        }

        [Test]
        public void CalculateBatteryPercentage_BatteryEmpty_ReturnsFalse()
        {
            // Setup
            _robotBattery.BatteryPercentage = 0.01f;

            //Act
            var result = _robotBattery.IsBatteryEmpty;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CalculateBatteryPercentage_BatteryFull_ReturnsFalse()
        {
            // Setup
            _robotBattery.BatteryPercentage = 0.5f;

            //Act
            var result = _robotBattery.IsBatteryFull;

            // Assert
            Assert.IsFalse(result);
        }
    }
}

