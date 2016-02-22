using UnityEngine;
using System.Collections;

public class staticAnimator : MonoBehaviour {

    int lineFrequency = 1;
    float updateFrequency = 20.0f;

    private Texture2D tex;
    private Color32[] pixels;
    private Material mat;
    public bool staticOn = false;
    public int proximity;

    MeshRenderer playerRenderer;

    void Start()
    {
        playerRenderer = gameObject.GetComponentInParent<MeshRenderer>();

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

        if (staticOn == true)//check if static is needed
        {
            if(proximity > 220)//check to make sure static value does not exceed the maximum
            {
                proximity = 220;
            }
            else if(proximity < 0)//check if proximity is less than 0
            {
                proximity = 0;
            }

            for (var i = 0; i < pixels.Length; i++)
            {
                if ((((i / tex.width) % lineFrequency) == 0))
                    pixels[i].a = (byte)Random.Range(proximity, proximity + 25);
                else
                    pixels[i].a = (byte)Random.Range(0, proximity/6);
            }

            tex.SetPixels32(pixels);
            tex.Apply();
            mat.mainTexture = tex;
        }
        if(staticOn == true)
        {
            playerRenderer.enabled = true;
        }
        else
        {
            playerRenderer.enabled = true;
        }
    }
}
