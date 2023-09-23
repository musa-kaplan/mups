using UnityEngine;

namespace MusaUtils.Character
{
    [CreateAssetMenu(fileName = "NewMobileCharacterData", menuName = "MU/Character/Mobile/CharacterData")]
    public class MobileCharacterData : ScriptableObject
    {
        public float movementSpeed;
        public float rotationSensitivity;

        [Header("Optional")] 
        public float gravity = -6f;
        public float verticalLookAngle = 30f;
    }
}
