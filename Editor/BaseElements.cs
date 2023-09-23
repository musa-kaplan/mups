using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MusaUtils.Editor
{
    public class BaseElements : MonoBehaviour
    {
        #region TAG ADDING

        private static SerializedObject tagManager = 
            new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        
        private static SerializedProperty tagsProp = tagManager.FindProperty("tags");
        
        public static void AddTag(string newT)
        {
            bool found = false;
            for (int i = 0; i < tagsProp.arraySize; i++)
            {
                SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
                if (t.stringValue.Equals(newT))
                {
                    found = true;
                }
            }

            if (!found)
            {
                tagsProp.InsertArrayElementAtIndex(0);
                SerializedProperty newTag = tagsProp.GetArrayElementAtIndex(0);
                newTag.stringValue = newT;   
                tagManager.ApplyModifiedProperties();
            }
        }

        #endregion

        public static void CreateJoystick()
        {
            Object _joystick = AssetDatabase.LoadAssetAtPath("Assets/MusaUtils/ExtentionAssets/Joystick/Prefabs/Floating.prefab", typeof(GameObject));
            PrefabUtility.InstantiatePrefab(
                _joystick, SceneManager.GetActiveScene()
            );
            FindObjectOfType<FloatingJoystick>().transform.SetParent(FindObjectOfType<Canvas>().transform);
        }
        
        public static void CreateTwoJoysticks()
        {
            AddTag("LeftJoystick");
            AddTag("RightJoystick");
            
            Object _joystick = AssetDatabase.LoadAssetAtPath("Assets/MusaUtils/ExtentionAssets/Joystick/Prefabs/FloatingLeft.prefab", typeof(GameObject));
            Object joy = PrefabUtility.InstantiatePrefab(
                _joystick, SceneManager.GetActiveScene()
            );
            
            GameObject currentJoystick = joy as GameObject;
            currentJoystick.transform.SetParent(FindObjectOfType<Canvas>().transform);
            currentJoystick.gameObject.tag = "LeftJoystick";
            
            _joystick = AssetDatabase.LoadAssetAtPath("Assets/MusaUtils/ExtentionAssets/Joystick/Prefabs/FloatingRight.prefab", typeof(GameObject));
            joy = PrefabUtility.InstantiatePrefab(
                _joystick, SceneManager.GetActiveScene()
            );
            currentJoystick = joy as GameObject;
            currentJoystick.transform.SetParent(FindObjectOfType<Canvas>().transform);
            currentJoystick.gameObject.tag = "RightJoystick";
        }
        
        public static void CreateCanvas(bool isPortrait = true)
        {
            GameObject _canvas = new GameObject("Canvas");
            ObjectFactory.AddComponent<Canvas>(_canvas).renderMode = RenderMode.ScreenSpaceOverlay;
            ObjectFactory.AddComponent<GraphicRaycaster>(_canvas);
            var canvasScaler = ObjectFactory.AddComponent<CanvasScaler>(_canvas);
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(isPortrait ? 1080 : 1920, isPortrait ? 1920 : 1080);
        }

        public static void CreateEventSystem()
        {
            GameObject _eventSystem = new GameObject("EventSystem");
            ObjectFactory.AddComponent<EventSystem>(_eventSystem);
            ObjectFactory.AddComponent<StandaloneInputModule>(_eventSystem);
        }
        
        private static Texture2D tex;
        public static GUIStyle SetLabelStyle(Color c1, Color c2)
        {
            var style = new GUIStyle();
            
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 15;
            
            style.normal.textColor = c1;
            
            tex = new Texture2D(1, 10, TextureFormat.RGBA32, false);
            tex.SetPixel(0, 0, c2);
            tex.Apply();

            style.normal.background = tex;

            return style;
        }
    }
}
