using System.Linq;
using UnityEngine;

public class FrostingDrawer : MonoBehaviour
{
    public GameObject frosting;
    public Transform cameraT;
    public new Camera camera;

    private Texture2D currentTexture;


    private void Awake()
    {
        camera = cameraT.GetComponent<Camera>();
        currentTexture = new(64, 64);
        Color[] pixels = Enumerable.Repeat(Color.black, Screen.width * Screen.height).ToArray();
        currentTexture.SetPixels(pixels);
        currentTexture.Apply();
        frosting.GetComponent<Renderer>().material.SetTexture("_DisplaceTex", currentTexture);
    }
    private void Update()
    {
        if (Input.Press.IsPressed())
        {
            Vector2 PositionScaled = Input.Position / new Vector2(Screen.width, Screen.height);
            if (!(PositionScaled.x >= 0 && PositionScaled.x <= 1 && PositionScaled.y >= 0 && PositionScaled.y <= 1)) return;
            Ray screenRay = camera.ViewportPointToRay(PositionScaled);
            castHits = Physics.RaycastAll(screenRay, 1000f);
            int i = -1;
            for (; i < castHits.Length - 1; i++)
            {
                if (castHits[i + 1].transform.gameObject == frosting)
                {
                    i++;
                    break;
                }
            }
            if (castHits.Length > 0 && i != -1)
            {
                Vector2 pos = castHits[i].textureCoord * new Vector2(currentTexture.width, currentTexture.height);
                DrawPixel(new Vector3Int((int)(pos.x - 0.5f), (int)(pos.y - 0.5f)));
            }
        }
    }
    private RaycastHit[] castHits;

    readonly Color[] colors = { Color.white, Color.white, Color.white, Color.white };

    private void DrawPixel(Vector3Int pos)
    {
        currentTexture.SetPixels(pos.x, pos.y, 2, 2, colors);
        currentTexture.Apply();
    }

}
