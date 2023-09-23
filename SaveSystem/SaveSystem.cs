using UnityEngine;

namespace MusaUtils.SaveSystem
{
    public class SaveSystem<T>
    {
        private static SaveSystem<T> instance;

        public static SaveSystem<T> Init()
        {
            return instance ??= new SaveSystem<T>();
        }

        public SaveSystem<T> Save(T saveClass, string saveName)
        {
            var data = JsonUtility.ToJson(saveClass);
            var path = Application.persistentDataPath + "/JsonSaves/" + saveName + ".json";
            System.IO.File.WriteAllText(path, data);
            return this;
        }

        public T Load(string saveName)
        {
            var path = Application.persistentDataPath + "/JsonSaves/" + saveName + ".json";
            var data = System.IO.File.ReadAllText(path);
            
            return JsonUtility.FromJson<T>(data);
        }
    }
}
