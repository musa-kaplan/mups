using UnityEngine;

namespace MusaUtils.Templates.CarController
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 200f;

        public static WheelCollider[] _wheels;
        public static FloatingJoystick _joystick;
        
        private void Start()
        {
            _wheels = GetComponentsInChildren<WheelCollider>();
            _joystick = FindObjectOfType<FloatingJoystick>();
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                QuickCar.Gas(_maxSpeed);
                QuickCar.Steer();
            }
            else
            {
                QuickCar.Break();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                //TODO FINISHED EVENTS
            }
        }
    }
}
