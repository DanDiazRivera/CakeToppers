using System;
using System.IO;
using UnityEngine;

/*public class SaveSystemExample
{
    
    public class SaveDataExample : SaveData<SaveDataExample>
    {
        protected override string FileName() => "ExampleData";

        public bool data1;


    }

    public static SaveDataExample Data() => SaveDataExample.Get(ref _saveData);
    public static SaveDataExample Data2() => SaveDataExample.Get(ref _saveData2);
    public static void Save()
    {
        Data().Save();
        Data2().Save(); 
    }



    private static SaveDataExample _saveData;
    private static SaveDataExample _saveData2;


}*/

[Serializable]
public abstract class SaveData<T> where T : SaveData<T>
{

    protected virtual string DataPath() => Application.persistentDataPath + "/Saves";
    protected abstract string FileName();

    private string FullFilePath() => this.DataPath() + "/" + this.FileName() + ".json";

    public void Save()
    {
        (this as T).BeforeSave();
        DirectoryCheck();
        using StreamWriter save = File.CreateText(FullFilePath());
        save.WriteLine(JsonUtility.ToJson(this as T, true)); 
    }

    public void Load()
    {
        DirectoryCheck();
        if (!File.Exists(FullFilePath())) Save();
        using StreamReader load = File.OpenText(FullFilePath());
        JsonUtility.FromJsonOverwrite(load.ReadToEnd(), this as T);
        (this as T).AfterLoad();
    }

    protected virtual void BeforeSave() { }
    protected virtual void AfterLoad() { }

    protected void DirectoryCheck() { if (!Directory.Exists(this.DataPath())) Directory.CreateDirectory(this.DataPath()); }

    public static T Get(ref T data)
    {
        if (data == null)
        {
            data = Activator.CreateInstance<T>();
            data.Load();
        }
        return data;
    }
}