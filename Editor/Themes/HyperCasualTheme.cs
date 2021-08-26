using System;
using MusaUtils.Templates.HyperCasual;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = System.Object;

namespace MusaUtils.Editor.Themes
{
    public class HyperCasualTheme : EditorWindow
    {
        [MenuItem("MU/Templates/Hyper Casual Template")]
        public static void ShowWindow()
        {
            GetWindowWithRect<ThemeWindow>(new Rect(0, 0, 300, 300), false, "Theme Settings");
        }
    }

    public class ThemeWindow : EditorWindow
    {
        private Rect _gameManagerRect;
        private Rect _sfxManagerRect;
        private Rect _uiManagerRect;
        private GUIStyle _gmStyle;
        private GUIStyle _sfxStyle;
        private GUIStyle _uiStyle;
        private GUIStyle _haveOneStyle;

        private GameObject _object;
        private Texture2D tex;

        private void OnEnable()
        {
            _gameManagerRect = new Rect(25, 10, 250, 50);
            _sfxManagerRect = new Rect(25, 70, 250, 50);
            _uiManagerRect = new Rect(25, 130, 250, 50);

            _gmStyle = SetStyles(new Color(.4f, .3f, .3f), new Color(1f, .8f, .8f));
            _sfxStyle = SetStyles(new Color(.3f, .4f, .4f), new Color(.8f, 1f, .8f));
            _uiStyle = SetStyles(new Color(.4f, .3f, .6f), new Color(.8f, .7f, 1f));
            _haveOneStyle = SetStyles(new Color(.4f, .2f, .2f), new Color(1f, .4f, .4f));
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(_gameManagerRect);
            GUILayout.Label("GAME MANAGER", _gmStyle);
            if (FindObjectOfType<GameManager>() == null)
            {
                SetGameManagerButton();
            }
            else
            {
                GUILayout.Label("YOU ALREADY HAVE ONE...", _haveOneStyle);
            }
            GUILayout.EndArea();
            
            GUILayout.Space(10);
            
            GUILayout.BeginArea(_sfxManagerRect);
            GUILayout.Label("SFX MANAGER", _sfxStyle);
            if (FindObjectOfType<SoundManager>() == null)
            {
                SetSFXManagerButton();
            }
            else
            {
                GUILayout.Label("YOU ALREADY HAVE ONE...", _haveOneStyle);
            }
            GUILayout.EndArea();
            
            GUILayout.Space(10);
            
            GUILayout.BeginArea(_uiManagerRect);
            GUILayout.Label("UI MANAGER & CANVAS", _uiStyle);
            if (FindObjectOfType<UiManager>() == null)
            {
                SetUiButton();
            }
            else
            {
                GUILayout.Label("YOU ALREADY HAVE ONE...", _haveOneStyle);
            }
            GUILayout.EndArea();
        }

        private void SetUiButton()
        {
            if (GUILayout.Button("Add", GUILayout.Height(30)))
            {
                _object = new GameObject("EventSystem");
                ObjectFactory.AddComponent<EventSystem>(_object);
                ObjectFactory.AddComponent<StandaloneInputModule>(_object);

                _object = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/MusaUtils/Templates/HyperCasual/Prefabs/UI.prefab");
                PrefabUtility.InstantiatePrefab(_object);
            }
        }
        
        private void SetSFXManagerButton()
        {
            if (GUILayout.Button("Add", GUILayout.Height(30)))
            {
                _object = new GameObject("SFX Manager");
                ObjectFactory.AddComponent<SoundManager>(_object);
            }
        }
        
        private void SetGameManagerButton()
        {
            if (GUILayout.Button("Add", GUILayout.Height(30)))
            {
                _object = new GameObject("GameManager");
                ObjectFactory.AddComponent<GameManager>(_object);
            }
        }

        private GUIStyle SetStyles(Color c1, Color c2)
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