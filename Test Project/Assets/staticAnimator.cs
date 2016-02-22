using UnityEngine;
using System.Collections;

public class staticAnimator : MonoBehaviour {

    int lineFrequency = 5;
    float updateFrequency = 20.0f;

    private Texture2D tex;
    private Color32[] pixels;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material; //
        tex = new Texture2D(255, 255);
        Color32 c32 = new Color32(0, 0, 0, 0);
        pixels = tex.GetPixels32();
        for (var i = 0; i < pixels.Length; i++)
        {
            pixels[i] = c32;
        }
        tex.SetPixels32(pixels);
        tex.Apply();
        mat.mainTexture = tex;

        InvokeRepeating("UpdateStatic", 0.0f, 1.0f / updateFrequency);
    }

    void UpdateStatic()
    {
        for (var i = 0; i < pixels.Length; i++)
        {
            if ((((i / tex.width) % lineFrequency) == 0))
                pixels[i].a = (byte)Random.Range(85, 101);
            else
                pixels[i].a = (byte)Random.Range(0, 25);
        }

        tex.SetPixels32(pixels);
        tex.Apply();
        mat.mainTexture = tex;
    }
}
