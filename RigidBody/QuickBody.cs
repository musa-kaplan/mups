using UnityEngine;

namespace MusaUtils.RigidBody
{
    public class QuickBody
    {
        private static QuickBody _instance;
        private Rigidbody _currentBody;

        private QuickBody()
        {
            
        }
        
        public static QuickBody GetRigid(Rigidbody rb)
        {
            if(_instance == null)
                _instance = new QuickBody();

            _instance._currentBody = rb;
            return _instance;
        }
        
        
        public QuickBody FreezeAll()
        {
            _currentBody.constraints = RigidbodyConstraints.FreezeAll;
            return this;
        }
        
        public QuickBody FreezeNone()
        {
            _currentBody.constraints = RigidbodyConstraints.None;
            return this;
        }
        
        public QuickBody FreezePosition(bool x = true, bool y = true, bool z = true)
        {
            if (x)
            {
                _currentBody.constraints = _currentBody.constraints | RigidbodyConstraints.FreezePositionX;
            }

            if (y)
            {
                _currentBody.constraints = _currentBody.constraints | RigidbodyConstraints.FreezePositionY;
            }

            if (z)
            {
                _currentBody.constraints = _currentBody.constraints | RigidbodyConstraints.FreezePositionZ;
            }
            return this;
        }
        
        public QuickBody FreezeRotation(bool x = true, bool y = true, bool z = true)
        {
            if (x)
            {
                _currentBody.constraints = _currentBody.constraints | RigidbodyConstraints.FreezeRotationX;
            }

            if (y)
            {
                _currentBody.constraints = _currentBody.constraints | RigidbodyConstraints.FreezeRotationY;
            }

            if (z)
            {
                _currentBody.constraints = _currentBody.constraints | RigidbodyConstraints.FreezeRotationZ;
            }
            return this;
        }
        
        public QuickBody SetKinematic(bool isKine = false)
        {
            _currentBody.isKinematic = isKine;
            return this;
        }

        public QuickBody ResetBody()
        {
            _currentBody.velocity = Vector3.zero;
            _currentBody.angularVelocity = Vector3.zero;
            FreezeAll();
            _currentBody.isKinematic = true;
            FreezeNone();
            _currentBody.isKinematic = false;
            return this;
        }

        public QuickBody GoForward(Vector3 _velocity, float speed = 10f)
        {
            _currentBody.velocity = _velocity * speed;
            return this;
        }
    }
}