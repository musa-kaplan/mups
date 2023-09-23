using System;
using Cysharp.Threading.Tasks;
using MusaUtils.RigidBody;
using UnityEngine;

namespace MusaUtils.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class MuCharacter : MonoBehaviour
    {
        [Range(0.01f, 1f)] [SerializeField] private float movementSmoothness = .1f;
        [Range(0.1f, 20f)] [SerializeField] private float rotationSmoothness = 10f;
        [SerializeField] private InputData inputData;
        [SerializeField] private CharacterData characterData;

        #region Local Variables

        private Rigidbody _rigidbody;
        private Vector3 xAxis;
        private Vector3 zAxis;
        private Vector3 currentRot;
        private Vector3 currentCameraRot;
        private Vector3 oldCameraPos;
        private Vector3 currentCameraPos;
        private Camera currentCamera;
        private float currentSpeed;
        private float xAxisRotation;
        private float yAxisRotation;
        private float currentFieldOfView;
        private bool canJump = true;
        
        #endregion

        #region Input Variables

        private bool isForward;
        private bool isBack;
        private bool isRight;
        private bool isLeft;
        private bool isDucked;
        private bool isRunning;
        private bool isJumped;
        
        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Initialize();
        }

        private void FixedUpdate()
        {
            CollectInput();
            Movement();
            Rotation();
        }

        private void CheckDuck()
        {
            currentCameraPos.y = isDucked ? (oldCameraPos.y / 3f) : oldCameraPos.y;
            currentCamera.transform.localPosition = Vector3.Lerp(currentCamera.transform.localPosition, 
                currentCameraPos, .15f);
        }

        private void Rotation()
        {
            currentRot.y += xAxisRotation * rotationSmoothness;
            currentCameraRot.x += yAxisRotation * -rotationSmoothness;

            currentCameraRot.x = Mathf.Clamp(currentCameraRot.x, -characterData.lookRotationAngle,
                characterData.lookRotationAngle);
            
            transform.localRotation = Quaternion.Slerp(transform.localRotation, 
                Quaternion.Euler(currentRot), .1f);
            
            currentCamera.transform.localRotation = Quaternion.Slerp(currentCamera.transform.localRotation, 
                Quaternion.Euler(currentCameraRot), .1f);
            
        }

        private void Movement()
        {
            CameraFieldOfView();
            
            currentSpeed = isDucked ? characterData.duckedMovementSpeed : characterData.movementSpeed;
            currentSpeed = isRunning ? characterData.runningSpeed : currentSpeed;
            currentSpeed = !canJump ? (characterData.duckedMovementSpeed / 2f) : currentSpeed;

            zAxis += transform.forward * (isForward ? currentSpeed : 0);
            zAxis += transform.forward * (isBack ? -currentSpeed : 0);
            xAxis += transform.right * (isRight ? currentSpeed : 0);
            xAxis += transform.right * (isLeft ? -currentSpeed : 0);
            xAxis.y = zAxis.y = characterData.gravity;

            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, (xAxis + zAxis), 
                movementSmoothness);

            xAxis = zAxis = Vector3.zero;
            
            CheckDuck();
            
            if(!canJump) return;
            if (!isJumped) return;
            _rigidbody.AddForce(Vector3.up * characterData.jumpForce, ForceMode.Force);
            CheckCanJump();
        }

        private void CameraFieldOfView()
        {
            currentFieldOfView = isRunning ? characterData.runningFieldOfView : characterData.defaultFieldOfView;
            currentFieldOfView = isDucked ? characterData.duckFieldOfView : currentFieldOfView;
            currentCamera.fieldOfView = Mathf.Lerp(currentCamera.fieldOfView, currentFieldOfView, .1f);
        }

        private async void CheckCanJump()
        {
            canJump = false;
            await UniTask.Delay(TimeSpan.FromSeconds(characterData.jumpDelay));
            canJump = true;
        }

        private void CollectInput()
        {
            isForward = Input.GetKey(inputData.forward);
            isBack = Input.GetKey(inputData.back);
            isRight = Input.GetKey(inputData.right);
            isLeft = Input.GetKey(inputData.left);
            isDucked = Input.GetKey(inputData.duck);
            isJumped = Input.GetKey(inputData.jump);
            isRunning = Input.GetKey(inputData.run);

            xAxisRotation = Input.GetAxis("Mouse X");
            yAxisRotation = Input.GetAxis("Mouse Y");
        }

        private void Initialize()
        {
            gameObject.tag = "Player";
            _rigidbody = GetComponent<Rigidbody>();
            QuickBody.GetRigid(_rigidbody).FreezeRotation(true, false, true);
            
            for (var i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).TryGetComponent(out Camera c)) continue;
                currentCamera = c;
                break;
            }

            if (currentCamera == null)
            {
                var cam = new GameObject("Camera");
                currentCamera = cam.AddComponent<Camera>();
                currentCamera.transform.parent = transform;
                currentCamera.transform.localPosition = transform.localPosition;
            }

            oldCameraPos = currentCamera.transform.localPosition;
        }
    }
}
