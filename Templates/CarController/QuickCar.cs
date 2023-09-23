using UnityEngine;

namespace MusaUtils.Templates.CarController
{
    public class QuickCar : Templates.CarController.CarController
    {
        private static float _torque;
        private static float _brake;
        private static float _steer;


        public static void Gas(float maxSpeed = 200f, float gasAmount = 1f)
        {
            _brake = 0;
            _torque += gasAmount;

            _torque = Mathf.Clamp(_torque, 0, maxSpeed);
            
            SetWheels();
        }

        public static void Break(float breakAmount = 5f)
        {
            _torque = 0;
            _brake += breakAmount;
            
            _brake = Mathf.Clamp(_brake, 0, 5000f);
            
            SetWheels();
        }

        public static float Steer(float _directionAngle = 30f)
        {
            _steer = _joystick.Horizontal * _directionAngle;
            return _steer;
        }

        private static void SetWheels()
        {
            foreach (var w in _wheels)
            {
                w.motorTorque = _torque;
                w.brakeTorque = _brake;
            }

            _wheels[0].steerAngle = Steer();
            _wheels[1].steerAngle = Steer();
        }

        private static float SetFree(float gasAmount = 1f)
        {
            _brake = 0;
            if (_torque > 0)
            {
                _torque -= gasAmount;
            }
            
            return _torque;
        }
    }
}
