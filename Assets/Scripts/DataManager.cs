using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int difficulty = 2;
    public int highscore;
    public bool isMuted;

    //On awake
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //What to save
    [System.Serializable]
    class SaveData
    {
        public int highscore;
        public bool isMuted;
    }

    //Save the data
    public void Save()
    {
        SaveData data = new SaveData();
        data.highscore = highscore;
        data.isMuted = isMuted;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highscore = data.highscore;
            isMuted = data.isMuted;
        }
    }
}
