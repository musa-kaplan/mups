using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace MusaUtils
{
    public class Cookies
    {
        public static bool QuickChance(int chance = 10)
        {
            return Random.Range(0, chance) == 0;
        }

        public static GameObject QuickFind(string word = "Player")
        {
            return GameObject.FindGameObjectWithTag(word) == null ? GameObject.Find(word) : GameObject.FindGameObjectWithTag(word);
        }

        public static void LevelUp(string prefName = "Level")
        {
            var i = PlayerPrefs.GetInt(prefName);
            i++;
            PlayerPrefs.SetInt(prefName, i);
        }

        public static void QuickScene(string sceneName = "0")
        {
            PlayerPrefs.Save();
            if (sceneName == "0")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        private static float _returnYoyo;
        private static bool _reverseYoyo;
        
        public static float Yoyo(float min, float max, float speed)
        {
            if (_returnYoyo >= max) { _reverseYoyo = true; }
            if (_returnYoyo <= min) { _reverseYoyo = false; }

            _returnYoyo += _reverseYoyo ? -speed * Time.deltaTime : speed * Time.deltaTime;
            return _returnYoyo;
        }
    }

    public class QuickRay
    {
        private static RaycastHit _hit;
        private static Ray _ray;
        private static Vector3 _pos;

        public static Vector3 Point(Camera camera, float height = 1f)
        {
            _ray = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(_ray, out _hit, Mathf.Infinity);
            _pos = _hit.point;
            _pos.y = height;
            return _pos;
        }

        public static RaycastHit Object(Camera camera)
        {
            _ray = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(_ray, out _hit, Mathf.Infinity);
            return _hit;
        }
    }

    public class QuickPatrol
    {
        
    }

    public class QuickInput
    {
        private static Vector2 mousePos;
        private static bool _right;
        
        public static Vector2 GetMouse()
        {
            mousePos = Input.mousePosition;
            return mousePos;
        }
    }
}