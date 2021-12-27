using UnityEngine;

namespace MusaUtils.Character
{
    [CreateAssetMenu(menuName = "InputData", fileName = "NewInputData")]
    public class InputData : ScriptableObject
    {
        [Header("Movement")]
        public KeyCode forward = KeyCode.W;
        public KeyCode back = KeyCode.S;
        public KeyCode right = KeyCode.D;
        public KeyCode left = KeyCode.A;
        
        [Header("Optional")]
        public KeyCode jump = KeyCode.Space;
        public KeyCode duck = KeyCode.LeftControl;
        public KeyCode run = KeyCode.LeftShift;
    }
}
