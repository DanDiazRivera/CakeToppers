using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ing_FrostingDraw : Ing_Frosting, IPaintIngredient
{
    public Texture2D texture;

    private void Awake()
    {
        texture = new(32, 32, TextureFormat.R16, true);

        Color[] pixels = Enumerable.Repeat(Color.black, Screen.width * Screen.height).ToArray();
        texture.SetPixels(pixels);
        texture.Apply();

        transform.Find("FrostingDisc").GetComponent<Renderer>().material.SetTexture("_DisplaceTex", texture);
    }

    public void SetTexture(Texture2D texture)
    {
        this.texture = texture;
        transform.Find("FrostingDisc").GetComponent<Renderer>().material.SetTexture("_DisplaceTex", this.texture);
    }

    public void DrawPixel(Vector2 pos)
    {
        Vector2Int posI = new Vector2Int(
            (int)((pos.x * texture.width) - 0.5f),
            (int)((pos.y * texture.height) - 0.5f));
        posI.x = Mathf.Clamp(posI.x, 0, texture.width - 1);
        posI.y = Mathf.Clamp(posI.y, 0, texture.width - 1);

        //tex.SetPixels(posI.x, posI.y, 2, 2, colors);
        texture.SetPixel(posI.x, posI.y, Color.white);
        texture.SetPixel(posI.x + 1, posI.y, Color.white);
        texture.SetPixel(posI.x, posI.y + 1, Color.white);
        texture.SetPixel(posI.x + 1, posI.y + 1, Color.white);
        texture.Apply();
    }
}
