using UnityEngine;
using System.Collections;

public class ChangeTexture : MonoBehaviour
{
    public Renderer rend;
    public Texture texture1;
    public Texture texture2;
    float time = 0.0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        time = time + 1.0f;
        /**float scaleX = Mathf.Cos(Time.time) * 0.5F + 1;
        float scaleY = Mathf.Sin(Time.time) * 0.5F + 1;
        rend.material.mainTextureScale = new Vector2(scaleX, scaleY);**/
       // rend.material.mainTexture = texture1;

        if (time % 20 == 0.0f)
        {
            if (rend.material.mainTexture == texture1)
                rend.material.mainTexture = texture2;
            else
                rend.material.mainTexture = texture1;
        }
    }
}
