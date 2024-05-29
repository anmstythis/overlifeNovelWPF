using Newtonsoft.Json;

namespace JsonLibrary
{
    public class JsonSave
    {
        public static void jsonSerialize<T>(List<T> textfile, string pathjson) //сериализация
        {
            if (!File.Exists(pathjson))
            {
                File.Create(pathjson).Close();
            }
            string txtjson = JsonConvert.SerializeObject(textfile, Formatting.Indented);
            File.WriteAllText(pathjson, txtjson);
        }
        public static T jsonDeserialize<T>(string pathjson) //десериализация
        {
            if (File.Exists(pathjson))
            {
                var txtjson = File.ReadAllText(pathjson);
                T result = JsonConvert.DeserializeObject<T>(txtjson);
                return result;
            }
            else
            {
                return default(T);
            }
        }

        public static void appendObject<T>(T new_json, string pathjson) //добавление игрока
        {
            var forAppend = jsonDeserialize<List<T>>(pathjson);
            forAppend.Add(new_json);
            jsonSerialize(forAppend, pathjson);
        }

        public static void updateObject<T>(int ind, T jsonfile, string pathjson) //обновление данных о существующем игроке
        {
            int index;
            var forUpdate = JsonSave.jsonDeserialize<List<T>>(pathjson);
            if (ind == -1)
            {
                index = forUpdate.Count - 1;
            }
            else
            {
                index = ind;
            }

            forUpdate[index] = jsonfile;
            jsonSerialize(forUpdate, pathjson);
        }

        public static void removeObject<T>(string pathjson, int index) //удаление игрока
        {
            if (File.Exists(pathjson))
            {
                var forDelete = jsonDeserialize<List<T>>(pathjson);
                forDelete.RemoveAt(index);
                jsonSerialize(forDelete, pathjson);
            }
        }
    }
    
}
