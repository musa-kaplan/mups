using System;
using MusaUtils.RigidBody;
using UnityEngine;

namespace MusaUtils.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class MuMobileCharacter : MonoBehaviour
    {
        [HideInInspector] public bool isPortrait;
        
        [Range(0.01f, 1f)] [SerializeField] private float movementSmoothness = .05f;
        [Range(0.01f, 1f)] [SerializeField] private float rotationSmoothness = .1f;
        [SerializeField] private MobileCharacterData characterData;

        #region Local Variables

        private FloatingJoystick joystick;
        private FloatingJoystick sideJoystick;
        private Rigidbody rigidBody;
        private Vector3 currentPos;
        private Vector3 currentXAxis;
        private Vector3 currentZAxis;
        private Vector3 currentRot;
        private Vector3 currentCameraRot;
        private Camera currentCamera;

        #endregion

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (isPortrait)
            {
                SimpleMovement();
            }
            else
            {
                TwoJoystickMovement();
                Rotation();
            }
        }

        private void Rotation()
        {
            currentRot.y += sideJoystick.Horizontal * rotationSmoothness;

            currentCameraRot.x += -sideJoystick.Vertical * rotationSmoothness;
            currentCameraRot.x = Mathf.Clamp(currentCameraRot.x, -characterData.verticalLookAngle,
                characterData.verticalLookAngle);
            
            currentCamera.transform.localRotation = Quaternion.Slerp(currentCamera.transform.localRotation, 
                Quaternion.Euler(currentCameraRot), .1f);
            
            transform.localRotation = Quaternion.Slerp(transform.localRotation, 
                Quaternion.Euler(currentRot), .1f);
            
        }

        private void SimpleMovement()
        {
            var isStaying = joystick.Direction.magnitude <= .1f;
            currentPos.x += isStaying ? 0 : joystick.Horizontal * characterData.movementSpeed;
            currentPos.z += isStaying ? 0 : joystick.Vertical * characterData.movementSpeed;

            rigidBody.velocity = currentPos.magnitude > 0.1f ? transform.forward * characterData.movementSpeed : Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            currentPos.x = 0;
            currentPos.z = 0;
            //transform.localPosition = currentPos;

            currentRot.x = !isStaying ? joystick.Horizontal : currentRot.x;
            
            currentRot.z = !isStaying ? joystick.Vertical : currentRot.z;
            
            transform.localRotation = Quaternion.Slerp(transform.localRotation, 
                Quaternion.LookRotation(currentRot.normalized), rotationSmoothness);
        }
        
        private void TwoJoystickMovement()
        {
            currentXAxis += transform.right * (joystick.Horizontal * characterData.movementSpeed);
            currentZAxis += transform.forward * (joystick.Vertical * characterData.movementSpeed);
            currentXAxis.y = currentZAxis.y = characterData.gravity;

            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, (currentXAxis + currentZAxis), movementSmoothness);
            currentXAxis = Vector3.zero;
            currentZAxis = Vector3.zero;
        }

        private void Initialize()
        {
            rigidBody = GetComponent<Rigidbody>();
            
            
            if (isPortrait)
            { joystick = FindObjectOfType<FloatingJoystick>(); QuickBody.GetRigid(rigidBody).FreezePosition(false, true, false).FreezeRotation(false, false, false).SetKinematic(false);}
            else
            { joystick = Cookies.QuickFind("LeftJoystick").GetComponent<FloatingJoystick>();
                sideJoystick = Cookies.QuickFind("RightJoystick").GetComponent<FloatingJoystick>(); }
        }
    }
}
