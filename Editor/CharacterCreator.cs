using System;
using MusaUtils.Character;
using MusaUtils.RigidBody;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MusaUtils.Editor
{
    public class CharacterCreator : EditorWindow
    {
        private static GameObject go;
        private static GUIStyle buttonStyle;
        private static GUIStyle toggleStyle;
        private static bool isCamera;
        private static bool isPortrait;

        
        private bool LeftButton(string title)
        {
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.margin = new RectOffset(0, 0, buttonStyle.margin.top, buttonStyle.margin.bottom);
            
            var clicked = false;
            var rect = GUILayoutUtility.GetRect(30f, 50f);
            GUI.BeginGroup(rect);
            if(GUI.Button(new Rect(0, 0, rect.width + buttonStyle.border.right, rect.height), title, buttonStyle)) clicked = true;
            GUI.EndGroup();
            return clicked;
        }
 
        private bool MidButton(string title)
        {
            var clicked = false;
            var rect = GUILayoutUtility.GetRect(30f, 50f);
            GUI.BeginGroup(rect);
            if (GUI.Button(new Rect(-buttonStyle.border.left, 0, rect.width + buttonStyle.border.left + buttonStyle.border.right, rect.height), title, buttonStyle)) clicked = true;
            GUI.EndGroup();
            return clicked;
        }
 
        private bool RightButton(string title)
        {
            var clicked = false;
            var rect = GUILayoutUtility.GetRect(30f, 50f);
            GUI.BeginGroup(rect);
            if (GUI.Button(new Rect(-buttonStyle.border.left, 0, rect.width + buttonStyle.border.left, rect.height), title, buttonStyle)) clicked = true;
            GUI.EndGroup();
            return clicked;
        }

        [MenuItem("MU/Creators/CreateCharacterController")]
        private static void ShowWindow()
        {
            GetWindowWithRect<CharacterCreator>(new Rect(0, 0, 320, 230));
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(Rect.MinMaxRect(10,20,300,175));
            GUILayout.Label ("Select Your Character To Add Controller \nOr Select Nothing To Create Controller", EditorStyles.boldLabel);
            if (LeftButton("PC"))
            {
                if (Selection.activeObject == null)
                {
                    go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                }
                else
                {
                    go = Selection.activeGameObject;
                    if (go.TryGetComponent(out MuCharacter c)) { return; }
                }
                go.AddComponent<MuCharacter>();
                
                QuickBody.GetRigid(go.GetComponent<Rigidbody>()).FreezeRotation(true, false);
                if (isCamera) { InstantiateCamera(); }
            }
            
            if (LeftButton("Mobile"))
            {
                if (!FindObjectOfType<Canvas>()) { BaseElements.CreateCanvas(isPortrait); }
                if (!FindObjectOfType<EventSystem>()) { BaseElements.CreateEventSystem(); }

                if (!FindObjectOfType<FloatingJoystick>())
                { if (isPortrait) { BaseElements.CreateJoystick(); }
                    else { BaseElements.CreateTwoJoysticks(); } }

                if (Selection.activeObject == null)
                {
                    go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                }
                else
                {
                    go = Selection.activeGameObject;
                    if (go.TryGetComponent(out MuMobileCharacter c)) { return; }
                }
                go.AddComponent<MuMobileCharacter>().isPortrait = isPortrait;
                
                QuickBody.GetRigid(go.GetComponent<Rigidbody>()).FreezeRotation(true, false);
                if (isCamera) { InstantiateCamera(); }
            }
            
            GUILayout.EndArea();
            GUILayout.BeginArea(Rect.MinMaxRect(10,170,300,230));
            isCamera = EditorGUILayout.Toggle("Create an FPS Camera?", isCamera, EditorStyles.toggle);
            GUILayout.EndArea();
            GUILayout.BeginArea(Rect.MinMaxRect(10,190,300,230));
            isPortrait = EditorGUILayout.Toggle("Is Portrait?", isPortrait, EditorStyles.toggle);
            GUILayout.EndArea();
        }

        private static void InstantiateCamera()
        {
            var cam = new GameObject("Camera");
            cam.AddComponent<Camera>();
            cam.transform.parent = go.transform;
            cam.transform.position = go.transform.position;
        }
    }
}
