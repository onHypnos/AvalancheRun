using UnityEngine;

public class SaveDataRepo : ISaveDataRepo
{
    public void SaveData(int value, string key)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public void SaveData(float value, string key)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public void SaveData(string value, string key)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public int LoadInt(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            Debug.Log("Data has not been loaded");
            return default;
        }
    }
    public float LoadFloat(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            Debug.Log("Data has not been loaded");
            return default;
        }
    }
    public string LoadString(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            Debug.Log("Data has not been loaded");
            return default;
        }
    }

    public void DeleteSave(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
        else
        {
            Debug.Log("Key soes not exist");
        }
    }
    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}