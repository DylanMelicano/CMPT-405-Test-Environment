using UnityEngine;
using System.Collections;

public class CrossFade : MonoBehaviour
{
  private Texture    newTexture;
  private Vector2    newOffset;
  private Vector2    newTiling;
  
  public  float    BlendSpeed = 3.0f;
  
  private bool    trigger = false;
  private float    fader = 0f;
  
  void Start ()
  {
    GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
  }
  
  void Update ()
  {
    //StartCoroutine("BlendTextures");
	BlendTextures();
  }
  
  public void CrossFadeTo( Texture curTexture, Vector2 offset, Vector2 tiling )
  {
    newOffset = offset;
    newTiling = tiling;
    newTexture = curTexture;
    GetComponent<Renderer>().material.SetTexture( "_Texture2", curTexture );
    GetComponent<Renderer>().material.SetTextureOffset ( "_Texture2", newOffset );
    GetComponent<Renderer>().material.SetTextureScale ( "_Texture2", newTiling );
    trigger = true;
  }
  
  public void BlendTextures () {
	if ( true == trigger )
    {
      fader += Time.deltaTime * BlendSpeed;
      //yield return new WaitForSeconds(10f);
      GetComponent<Renderer>().material.SetFloat( "_Blend", fader );
      
      if ( fader >= 1.0f )
      {
        trigger = false;
        fader = 0f;
        
        GetComponent<Renderer>().material.SetTexture ("_MainTex", newTexture );
        GetComponent<Renderer>().material.SetTextureOffset ( "_MainTex", newOffset );
        GetComponent<Renderer>().material.SetTextureScale ( "_MainTex", newTiling );
        GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
      }
	  //trigger = false;
    } 	  
  }
}
