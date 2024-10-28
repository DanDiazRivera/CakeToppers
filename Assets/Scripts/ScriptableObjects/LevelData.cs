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

    [JsonIgnore] public List<Texture2D> icingShapes;

    public int highScore;

}
