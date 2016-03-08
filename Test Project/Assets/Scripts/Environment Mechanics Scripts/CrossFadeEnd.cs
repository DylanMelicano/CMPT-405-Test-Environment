using UnityEngine;
using System.Collections;

public class CrossFadeEnd : MonoBehaviour
{
	public Texture lastMessage;
	Vector2 tillAndOff;

  private Texture  newTexture;
  private Vector2  newOffset;
  private Vector2  newTiling;
  
  public  float  BlendSpeed = 3.0f;
  
  private bool trigger = false;
  private float fader = 0f;
  
  void Start ()
  {
    GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
	tillAndOff = new Vector2 (1f,1f);
  }
  
  void Update ()
  {
	crossFadeTo(lastMessage, tillAndOff, tillAndOff);
    if ( true == trigger )
    {
      fader += Time.deltaTime * BlendSpeed;
      
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
    }
  }
  
  public void crossFadeTo( Texture curTexture, Vector2 offset, Vector2 tiling )
  {
    newOffset = offset;
    newTiling = tiling;
    newTexture = curTexture;
    GetComponent<Renderer>().material.SetTexture( "_Texture2", curTexture );
    GetComponent<Renderer>().material.SetTextureOffset ( "_Texture2", newOffset );
    GetComponent<Renderer>().material.SetTextureScale ( "_Texture2", newTiling );
    trigger = true;
  }
}