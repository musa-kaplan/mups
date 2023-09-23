using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MusaUtils.Editor
{
    public class CarCreator : EditorWindow
    {
        

        private static GameObject _car;
        private static GameObject _wheel;


        [MenuItem("MU/Creators/Create Car Controller")]
        public static void ShowCarControllerWindow()
        {
            GetWindowWithRect<CarCreator>(new Rect(10, 20, 320, 120));
        }

        private static bool isPortrait;
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 20, 300, 120));
            isPortrait = EditorGUILayout.Toggle("Is Portrait?", isPortrait);
            if (GUILayout.Button("Create New Car"))
            {
                AddComponentEditor();
            }
            GUILayout.EndArea();
        }

        private static void AddComponentEditor()
        {
            BaseElements.AddTag("WheelFront");
            BaseElements.AddTag("WheelRear");
                
            //CAR BASE OBJECT
            _car = new GameObject("NewCar");
            
            ObjectFactory.AddComponent<Templates.CarController.CarController>(_car);
            
            BoxCollider collider = ObjectFactory.AddComponent<BoxCollider>(_car);
            collider.center = new Vector3(0, .5f, 0);
            collider.size = new Vector3(2.5f, 1f, 4f);
            
            ObjectFactory.AddComponent<Rigidbody>(_car).mass = 200f;

            //WHEELS
            AddWheel("WheelFR", "WheelFront", new Vector3(1.75f, 0, 1.5f));
            
            AddWheel("WheelFL", "WheelFront", new Vector3(-1.75f, 0, 1.5f));
            
            AddWheel("WheelRR", "WheelRear", new Vector3(1.75f, 0, -1.5f));
            
            AddWheel("WheelRL", "WheelRear", new Vector3(-1.75f, 0, -1.5f));
            
            _car.transform.position = Vector3.up;

            if (FindObjectOfType<Canvas>() == null)
            { BaseElements.CreateCanvas(isPortrait); }
            
            if (FindObjectOfType<EventSystem>() == null)
            { BaseElements.CreateEventSystem(); }

            if (FindObjectOfType<FloatingJoystick>() == null)
            { BaseElements.CreateJoystick(); }
        }

        private static void AddWheel(string wheelName, string wheelTag, Vector3 wheelPos)
        {
            _wheel = new GameObject(wheelName);
            WheelCollider _wSet = ObjectFactory.AddComponent<WheelCollider>(_wheel);
            JointSpring _spring = new JointSpring();
            _spring.spring = 3500f;
            _spring.damper = 450f;
            _wSet.suspensionSpring = _spring;

            _wheel.transform.SetParent(_car.transform);
            _wheel.tag = wheelTag;
            _wheel.transform.position = wheelPos;
        }
    }
}
