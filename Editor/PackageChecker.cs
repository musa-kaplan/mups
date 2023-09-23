using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace MusaUtils.Editor
{
    public class PackageChecker : UnityEditor.Editor
    {
        private static AddRequest _request;
    
        [MenuItem("MU/PackageCheck")]
        static void AddRequiredPackages()
        {
            // Add a package to the project
            if (!IsPackageInstalled("com.unity.nuget.newtonsoft-json"))
            {
                _request = Client.Add("com.unity.nuget.newtonsoft-json");
                EditorApplication.update += Progress;
            }
            
            // Add a package to the project
            if (!IsPackageInstalled("com.unity.cinemachine"))
            {
                _request = Client.Add("com.unity.cinemachine");
                EditorApplication.update += Progress;
            }
        }
        
        private static bool IsPackageInstalled(string packageId)
        {
            if ( !File.Exists("Packages/manifest.json") )
                return false;
 
            string jsonText = File.ReadAllText("Packages/manifest.json");
            return jsonText.Contains( packageId );
        }
    
        static void Progress()
        {
            Debug.Log("Installing...");
            if (_request.IsCompleted)
            {
                if (_request.Status == StatusCode.Success)
                    Debug.Log("Installed: " + _request.Result.packageId);
                else if (_request.Status >= StatusCode.Failure)
                    Debug.Log(_request.Error.message);
    
                EditorApplication.update -= Progress;
            }
        }
    }
}
