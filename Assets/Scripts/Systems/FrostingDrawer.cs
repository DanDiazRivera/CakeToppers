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
            Ray screenRay = camera.ViewportPointToRay(Input.Position / new Vector2(Screen.width, Screen.height));
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
                this.pos = pos;
                currentTexture.SetPixel((int)pos.x, (int)pos.y, Color.white);
                currentTexture.Apply();
            }
            //castHits.First(i => i.collider.gameObject == frosting)
        }
    }
    private RaycastHit[] castHits;
    public Vector2 pos;
}
