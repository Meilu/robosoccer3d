using System.Linq;
using System.Numerics;
using NUnit.Framework;
using Sensors;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Tests
{
    [TestFixture]
    public class RobotVisionSensorTests
    {
        private RobotVisionSensor _robotVisionSensor;
        
        [SetUp]
        public void Init()
        {
            _robotVisionSensor = new RobotVisionSensor();
        }
        
        [Test]
        public void CreateObjectOfInterestForGameObjects_SomeGameObjects_ReturnsList()
        {
            // Setup
            var gameObjects = new GameObject[]
            {
                new GameObject("go1"),
                new GameObject("go2")
            };
            
            // Act
            var list = _robotVisionSensor.CreateObjectOfInterestForGameObjects(gameObjects).ToArray();
            
            // Assert
            Assert.IsTrue(gameObjects.Length == list.Length);
        }
        
        [Test]
        public void IsObjectWithinDistance_ObjectWithinDistance_ReturnsTrue()
        {
            // Setup
            var objectPosition = new Vector3(2.0f, 2.5f, 5f);
            var robotPosition = new Vector3(2.0f, 2.5f, 4.5f);

            // Act
            var result = _robotVisionSensor.IsObjectWithinDistance(objectPosition, robotPosition, 1.0f);
            
            // Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void IsObjectWithinDistance_ObjectWithinDistance_ReturnsFalse()
        {
            // Setup
            var objectPosition = new Vector3(2.0f, 2.5f, 5f);
            var robotPosition = new Vector3(2.0f, 2.5f, 10f);

            // Act
            var result = _robotVisionSensor.IsObjectWithinDistance(objectPosition, robotPosition, 1.0f);
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void IsObjectInsideVisionAngle_ObjectInsideAngle_ReturnsTrue()
        {
            // Setup
            var objectPosition = new Vector3(2.0f, 2.5f, 0f);
            var robotPosition = new Vector3(1f, 2.5f, 0f);

            // Act
            var result = _robotVisionSensor.IsObjectInsideVisionAngle(objectPosition, robotPosition, Vector3.forward, 100);
            
            // Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void IsObjectInsideVisionAngle_ObjectBehind_ReturnsFalse()
        {
            // Setup
            var objectPosition = new Vector3(2.77f, 2.5f, 0f);;
            var robotPosition = new Vector3(1.97f, 2.5f, 0f);
            var forward = new Vector3(-1.0f, 0.0f, 0.0f);
            
            // Act
            var result = _robotVisionSensor.IsObjectInsideVisionAngle(objectPosition, robotPosition, forward, 100);
            
            // Assert
            Assert.IsFalse(result);
        }
    }
}