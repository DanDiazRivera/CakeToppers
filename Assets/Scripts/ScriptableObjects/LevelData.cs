using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level"), System.Serializable]
public class LevelData : ScriptableObject
{
    [JsonIgnore] public float time;
    [JsonIgnore] public int minScore;
    [JsonIgnore] public int cookie2Score;
    [JsonIgnore] public int cookie3Score;

    [JsonIgnore] public List<Ingredient> ingredients;

    [JsonIgnore] public List<FruitArrangment> fruitArrangments;
    [JsonIgnore] public List<Texture2D> icingShapes;

    [HideInInspector] public int highScore;

}

public static class _LevelDataListExt
{
    public static int Cookies(this LevelData[] list)
    {
        int result = 0;

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].highScore >= list[i].minScore) result++;
            if (list[i].highScore >= list[i].cookie2Score) result++;
            if (list[i].highScore >= list[i].cookie3Score) result++;
        }

        return result;
    }    
    public static int Cookies(this LevelData data)
    {
        int result = 0;

        if (data.highScore >= data.minScore) result++;
        if (data.highScore >= data.cookie2Score) result++;
        if (data.highScore >= data.cookie3Score) result++;

        return result;
    }
    public static bool MinComplete(this LevelData data) => data.highScore >= data.minScore;
}