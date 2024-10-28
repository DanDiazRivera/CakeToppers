using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level")]
public class LevelData : ScriptableObject
{
    public float time;
    public int minScore;
    public int cookie2Score;
    public int cookie3Score;

    public List<Ingredient> ingredients;

    public List<Texture2D> icingShapes;


}
