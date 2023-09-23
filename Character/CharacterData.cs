using UnityEngine;

namespace MusaUtils.Character
{
    [CreateAssetMenu(menuName = "MU/Character/PC/CharacterData", fileName = "NewCharacterData")]
    public class CharacterData : ScriptableObject
    {
        [Header("Movement")]
        public float movementSpeed = 11f;
        public float duckedMovementSpeed = 10f;
        public float runningSpeed = 13f;
        
        [Header("Jump Settings")]
        public float jumpForce = 900f;
        public float jumpDelay = .75f;
        
        public float lookRotationAngle = 30f;
        
        [Header("Optionals")]
        public float gravity = -3f;

        [Header("Camera Settings")]
        public float defaultFieldOfView = 60f;
        public float runningFieldOfView = 75f;
        public float duckFieldOfView = 55f;
    }
}
