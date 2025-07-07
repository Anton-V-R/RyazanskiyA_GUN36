using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;

using Palmmedia.ReportGenerator.Core.Common;

using UnityEngine;

namespace DefaultNamespace
{
    public class PositionSaver : MonoBehaviour
    {
        [SerializeField]
        public struct Data
        {
            public Vector3 Position;
            public float Time;
        }


        [field: SerializeField]
        public TextAsset JsonData { get; private set; } // Свойство появится в инспекторе

        [field: SerializeField, HideInInspector]
        public List<Data> Records { get; private set; } // Скрыто в инспекторе, но сериализуется


        private void Awake()
        {
            //todo comment: Что будет, если в теле этого условия не сделать выход из метода?
            // Продолжится выполнение и выдаст ошибку Если _json не назначен, отключаем объект и выходим, чтобы избежать ошибок
            if(JsonData == null)
            {
                gameObject.SetActive(false);
                Debug.LogError("Please, create TextAsset and add in field _json");
                return;
            }

            JsonUtility.FromJsonOverwrite(JsonData.text, this);
            //todo comment: Для чего нужна эта проверка (что она позволяет избежать)?
            // Ошибок если при отсутствии записей.
            if(Records == null)
            {
                Records = new List<Data>(10);
            }
            else{
                // Автоматически загрузится при старте, если _json назначен
                Records = GetComponent<PositionSaver>().Records;
            }

            // System.Text.Json(для Unity 2021.2 +)
            //try
            //{
            //    Records = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(_json.text);
            //}
            //catch(JsonException ex)
            //{
            //    Debug.LogError($"JSON parsing error: {ex.Message}");
            //    Records = new List<Data>(10);
            //}
        }

        private void OnDrawGizmos()
        {
            //todo comment: Зачем нужны эти проверки (что они позволляют избежать)?
            // Пропускаем отрисовку, если нет записей
            if(Records == null || Records.Count == 0)
                return;
            var data = Records;
            var prev = data[0].Position;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(prev, 0.3f);
            //todo comment: Почему итерация начинается не с нулевого элемента?
            // Потому что 0 взяли чуть ранее
            for(int i = 1; i < data.Count; i++)
            {
                var curr = data[i].Position;
                Gizmos.DrawWireSphere(curr, 0.3f);
                Gizmos.DrawLine(prev, curr);
                prev = curr;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Create File")]
        private void CreateFile()
        {
            //todo comment: Что происходит в этой строке?
            // Создаем файл для сохранения данных пути
            var stream = File.Create(Path.Combine(Application.dataPath, "Path.txt"));
            //todo comment: Подумайте для чего нужна эта строка? (а потом проверьте догадку, закомментировав) 
            // Освобождение ресурсов. Закрытие потока чтения данных и снятие блокировки с файла
            stream.Dispose();
            UnityEditor.AssetDatabase.Refresh();
            //В Unity можно искать объекты по их типу, для этого используется префикс "t:"
            //После нахождения, Юнити возвращает массив гуидов (которые в мета-файлах задаются, например)
            var guids = UnityEditor.AssetDatabase.FindAssets("t:TextAsset");
            foreach(var guid in guids)
            {
                //Этой командой можно получить путь к ассету через его гуид
                var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                //Этой командой можно загрузить сам ассет
                var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                //todo comment: Для чего нужны эти проверки?
                // Ищем TextAsset с именем "Path"
                if(asset != null && asset.name == "Path")
                {
                    JsonData = asset;
                    UnityEditor.EditorUtility.SetDirty(this);
                    UnityEditor.AssetDatabase.SaveAssets();
                    UnityEditor.AssetDatabase.Refresh();
                    //todo comment: Почему мы здесь выходим, а не продолжаем итерироваться?
                    // Выходим после нахождения нужного файла
                    return;
                }
            }
        }

        private void OnDestroy()
        {
            //todo logic...
            SaveData();
        }

        public void SaveData()
        {
            // Сохраняем данные в JSON при уничтожении объекта
            if(JsonData != null && Records != null)
            {
                string json = JsonUtility.ToJson(Records, true);

                // System.Text.Json (для Unity 2021.2+)
                //string json = System.Text.Json.JsonSerializer.Serialize(
                //Records,
                //new JsonSerializerOptions { WriteIndented = true }
                //);
                File.WriteAllText(Path.Combine(Application.dataPath, "Path.txt"), json);
                UnityEditor.AssetDatabase.Refresh();
            }
        }
#endif
    }
}