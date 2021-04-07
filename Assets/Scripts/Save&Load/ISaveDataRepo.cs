public interface ISaveDataRepo
{
    void SaveData(int value, string key);
    void SaveData(float value, string key);
    void SaveData(string value, string key);

    int LoadInt(string key);
    float LoadFloat(string key);
    string LoadString(string key);
}
