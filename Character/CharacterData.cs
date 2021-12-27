using UnityEngine;

namespace MusaUtils.Character
{
    [CreateAssetMenu(menuName = "CharacterData", fileName = "NewCharacterData")]
    public class CharacterData : ScriptableObject
    {
        [Header("Movement")]
        public float movementSpeed = 12f;
        public float duckedMovementSpeed = 9f;
        public float runningSpeed = 15f;
        
        [Header("Jump Settings")]
        public float jumpForce = 1000f;
        public float jumpDelay = .75f;
        
        public float lookRotationAngle = 30f;
        
        [Header("Optionals")]
        public float gravity = -6f;

        [Header("Camera Settings")]
        public float defaultFieldOfView = 60f;
        public float runningFieldOfView = 75f;
        public float duckFieldOfView = 55f;
    }
}
