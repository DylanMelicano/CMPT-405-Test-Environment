using UnityEngine;
using System.Collections;

public class CrossFade : MonoBehaviour
{
  private Texture newTexture;
  private Vector2 newOffset;
  private Vector2 newTiling;
  
  public  float BlendSpeed = 3.0f;
  
  private bool trigger = false;
  private float fader = 0f;
  
  // For the many wall objects to be changed
  GameObject[] changingWalls;
  public Material nextWallMaterial;
  /**public Material secondWall;
  public Material thirdWall;
  public Material fourthWall;**/
  
  public bool wallsChanged = false;  
    
  void Start ()
  {
	changingWalls = GameObject.FindGameObjectsWithTag("Wall");
    foreach (GameObject wall in changingWalls) {
		wall.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
	}
  }
  
  void Update ()
  {
    //StartCoroutine("BlendTextures");
	if (wallsChanged == false) {
		blendTextures();
	}
  }
  
  public void crossFadeTo( Texture curTexture, Vector2 offset, Vector2 tiling )
  {
    newOffset = offset;
    newTiling = tiling;
    newTexture = curTexture;
	if (wallsChanged == false) {
		foreach (GameObject wall in changingWalls) {
			wall.GetComponent<Renderer>().material.SetTexture( "_Texture2", curTexture );
			wall.GetComponent<Renderer>().material.SetTextureOffset ( "_Texture2", newOffset );
			wall.GetComponent<Renderer>().material.SetTextureScale ( "_Texture2", newTiling );
		}
	}
    trigger = true;
  }
  
  public void blendTextures () {
	if ( true == trigger )
    {
		fader += Time.deltaTime * BlendSpeed;
		//yield return new WaitForSeconds(10f);
		foreach (GameObject wall in changingWalls) {
			wall.GetComponent<Renderer>().material.SetFloat( "_Blend", fader );
		}    
      
		if ( fader >= 1.0f )
		{
			trigger = false;
			fader = 0f;
			
			foreach (GameObject wall in changingWalls) {			
				wall.GetComponent<Renderer>().material.SetTexture ("_MainTex", newTexture );
				wall.GetComponent<Renderer>().material.SetTextureOffset ( "_MainTex", newOffset );
				wall.GetComponent<Renderer>().material.SetTextureScale ( "_MainTex", newTiling );
				wall.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
				
				if ( wall.GetComponent<Renderer>().material.mainTexture == newTexture) {
					//Debug.Log ("The wall's current texture is the new texture. Now will change to second material");
					wall.GetComponent<Renderer>().material = nextWallMaterial;
				//confirmed you can check textures using this
				//We can use this to thus switch materials (each with the set of textures) when the blending has finished
				
				//having this trigger false in the bottom also controls how much blends happen
				//Each frame
				//Gradual appearance of the second texture
				}
			}
			wallsChanged = true;
		}
	  trigger = false;
    } 	  
  }
  
  //Set the material that you want to change into after blending is finished
  public void setNewMaterial (Material nextMaterial) {
	  nextWallMaterial = nextMaterial;
  }
  
  //Reset the flag to allow wall changing again
  public void resetWallChanging () {
	  wallsChanged = false;
  }
  
  //Check to see if all walls have changed or not
  public bool hasWallsChanged () {
	  return wallsChanged;
  }
}
