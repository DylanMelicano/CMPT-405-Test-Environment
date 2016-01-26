using UnityEngine;
using System.Collections;

public class RotateTexture : MonoBehaviour {

	public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.1f;
    public bool main = true;
    public bool bump = true;
	 
	// Use this for initialization
	void Start () {
	
	}
     
     void Update () 
     {
        float offsetX = scrollSpeedX * Time.time;
        float offsetY = scrollSpeedY * Time.time;
         if(main)
         {
             GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
         }
         if(bump)
         {
             GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(offsetX, offsetY));
         }
     }
}
