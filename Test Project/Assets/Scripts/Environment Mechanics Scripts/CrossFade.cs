using UnityEngine;
using System.Collections;

public class CrossFade : MonoBehaviour
{
  private Texture newTexture;
  private Vector2 newOffset;
  private Vector2 newTiling;
  
  private Vector2 threshTiling; //Brick1 Material
  private Vector2 threshOffset;
  
  private Vector2 smallTiling; //Brick3 Material
  private Vector2 smallOffset;
  
  private Vector2 tinyTiling; //Brick4 Material
  private Vector2 tinyOffset;
  
  private Vector2 roofTiling;
  private Vector2 roofOffset;
  bool changeRoof = false;

  private Vector2 doorFillerTiling;
  private Vector2 doorFillerOffset;
    private Vector2 doorFiller2Tiling;
    private Vector2 doorFiller2Offset;

    public  float BlendSpeed = 3.0f;
  
  private bool trigger = false;
  private float fader = 0f;
  
  // For the many wall objects to be changed
  GameObject[] changingWalls;
  GameObject[] wallThresholds; //Brick1 material
  GameObject[] smallerWalls; //Brick3 Material
  GameObject[] thinWalls; //Brick4 Material
  GameObject[] roofFloors;
  GameObject[] doorFillers;
  GameObject[] doorFillers2;
  public Material nextWallMaterial;
  public Material nextThreshWallMaterial;
  public Material nextSmallWallMaterial;
  public Material nextTinyWallMaterial;
  
  public bool wallsChanged = false;  
    
  void Start ()
  {
	changingWalls = GameObject.FindGameObjectsWithTag("Wall");
	wallThresholds = GameObject.FindGameObjectsWithTag("ThreshWalls");
	smallerWalls = GameObject.FindGameObjectsWithTag("SmallWalls");
	thinWalls = GameObject.FindGameObjectsWithTag("TinyWalls");
	roofFloors = GameObject.FindGameObjectsWithTag("RoofFloor");
	doorFillers = GameObject.FindGameObjectsWithTag("DoorFiller");
    doorFillers2 = GameObject.FindGameObjectsWithTag("DoorFiller2");

    roofTiling = new Vector2 (4f, 4f);
	roofOffset = new Vector2 (0f, 0f);

    doorFillerTiling = new Vector2(0.73f, 0.29f);
    doorFillerOffset = new Vector2(0.77f, 2.265f);

    doorFiller2Tiling = new Vector2(-0.16f, 0.66f);
    doorFiller2Offset = new Vector2(0f, 0f);
	
    foreach (GameObject wall in changingWalls) {
		wall.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
	}
	
	foreach (GameObject thresh in wallThresholds) {
		thresh.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
	}
	
	foreach (GameObject smallWall in smallerWalls) {
		smallWall.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
	}
	
	foreach (GameObject tinyWall in thinWalls) {
		tinyWall.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
	}
	
	foreach (GameObject roof in roofFloors) {
		roof.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
	}

    foreach (GameObject filler in doorFillers)
    {
        filler.GetComponent<Renderer>().material.SetFloat("_Blend", 0f);
    }

    foreach (GameObject filler2 in doorFillers2)
    {
        filler2.GetComponent<Renderer>().material.SetFloat("_Blend", 0f);
    }
   }
  
  void Update ()
  {
    //StartCoroutine("BlendTextures");
	if (wallsChanged == false) {
		blendTextures();
	}
  }
  
  public void crossFadeTo( Texture curTexture, Vector2 offset, Vector2 tiling, Vector2 smOffset, Vector2 smTiling, Vector2 tnOffset, Vector2 tnTiling, Vector2 tshOffset, Vector2 tshTiling )
  {
    newOffset = offset;
    newTiling = tiling;
	
	smallOffset = smOffset;
    smallTiling = smTiling;
	
	tinyOffset = tnOffset;
    tinyTiling = tnTiling;
	
	threshOffset = tshOffset;
    threshTiling = tshTiling;
	
    newTexture = curTexture;
	
	if (wallsChanged == false) {
		foreach (GameObject wall in changingWalls) {
			wall.GetComponent<Renderer>().material.SetTexture( "_Texture2", curTexture );
			wall.GetComponent<Renderer>().material.SetTextureOffset ( "_Texture2", newOffset );
			wall.GetComponent<Renderer>().material.SetTextureScale ( "_Texture2", newTiling );
		}
		
		foreach (GameObject smallWall in smallerWalls) {
			smallWall.GetComponent<Renderer>().material.SetTexture( "_Texture2", curTexture );
			smallWall.GetComponent<Renderer>().material.SetTextureOffset ( "_Texture2", smallOffset );
			smallWall.GetComponent<Renderer>().material.SetTextureScale ( "_Texture2", smallTiling );
		}
		
		foreach (GameObject tinyWall in thinWalls) {
			tinyWall.GetComponent<Renderer>().material.SetTexture( "_Texture2", curTexture );
			tinyWall.GetComponent<Renderer>().material.SetTextureOffset ( "_Texture2", tinyOffset );
			tinyWall.GetComponent<Renderer>().material.SetTextureScale ( "_Texture2", tinyTiling );
		}
		
		foreach (GameObject thresh in wallThresholds) {
			thresh.GetComponent<Renderer>().material.SetTexture( "_Texture2", curTexture );
			thresh.GetComponent<Renderer>().material.SetTextureOffset ( "_Texture2", threshOffset );
			thresh.GetComponent<Renderer>().material.SetTextureScale ( "_Texture2", threshTiling );
		}

            foreach (GameObject filler in doorFillers)
            {
                filler.GetComponent<Renderer>().material.SetTexture("_Texture2", curTexture);
                filler.GetComponent<Renderer>().material.SetTextureOffset("_Texture2", doorFillerOffset);
                filler.GetComponent<Renderer>().material.SetTextureScale("_Texture2", doorFillerTiling);
            }

            foreach (GameObject filler2 in doorFillers2)
            {
                filler2.GetComponent<Renderer>().material.SetTexture("_Texture2", curTexture);
                filler2.GetComponent<Renderer>().material.SetTextureOffset("_Texture2", doorFiller2Offset);
                filler2.GetComponent<Renderer>().material.SetTextureScale("_Texture2", doorFiller2Tiling);

            }

            if (changeRoof == true) {
			foreach (GameObject roof in roofFloors) {
				roof.GetComponent<Renderer>().material.SetTexture( "_Texture2", curTexture );
				roof.GetComponent<Renderer>().material.SetTextureOffset ( "_Texture2", roofOffset );
				roof.GetComponent<Renderer>().material.SetTextureScale ( "_Texture2", roofTiling );
			}
		}
	}
    trigger = true;
  }
  
  public void blendTextures () {
	if ( true == trigger )
    {
		fader += Time.deltaTime * BlendSpeed;
		//yield return new WaitForSeconds(10f);
		
		//normal walls
		foreach (GameObject wall in changingWalls) {
			wall.GetComponent<Renderer>().material.SetFloat( "_Blend", fader );
		}
		
		foreach (GameObject smallWall in smallerWalls) {
			smallWall.GetComponent<Renderer>().material.SetFloat( "_Blend", fader );
		}
	
		foreach (GameObject tinyWall in thinWalls) {
			tinyWall.GetComponent<Renderer>().material.SetFloat( "_Blend", fader );
		}
		
		foreach (GameObject thresh in wallThresholds) {
			thresh.GetComponent<Renderer>().material.SetFloat( "_Blend", fader );
		}

        foreach (GameObject filler in doorFillers)
        {
            filler.GetComponent<Renderer>().material.SetFloat("_Blend", fader );
        }

        foreach (GameObject filler2 in doorFillers2)
        {
            filler2.GetComponent<Renderer>().material.SetFloat("_Blend", fader );
        }

        if (changeRoof == true) {
			foreach (GameObject roof in roofFloors) {
				roof.GetComponent<Renderer>().material.SetFloat( "_Blend", fader );
			}
		}
      
		if ( fader >= 1.0f )
		{
			trigger = false;
			fader = 0f;
			
			//normal walls (brick2)
			foreach (GameObject wall in changingWalls) {			
				wall.GetComponent<Renderer>().material.SetTexture ("_MainTex", newTexture );
				wall.GetComponent<Renderer>().material.SetTextureOffset ( "_MainTex", newOffset );
				wall.GetComponent<Renderer>().material.SetTextureScale ( "_MainTex", newTiling );
				wall.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
				
				if ( wall.GetComponent<Renderer>().material.mainTexture == newTexture) {
					//Debug.Log ("The wall's current texture is the new texture. Now will change to second material");
					wall.GetComponent<Renderer>().material = nextWallMaterial;
				}
			}
			
			//Smaller walls (brick3)
			foreach (GameObject smallWall in smallerWalls) {			
				smallWall.GetComponent<Renderer>().material.SetTexture ("_MainTex", newTexture );
				smallWall.GetComponent<Renderer>().material.SetTextureOffset ( "_MainTex", smallOffset );
				smallWall.GetComponent<Renderer>().material.SetTextureScale ( "_MainTex", smallTiling );
				smallWall.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
				
				if ( smallWall.GetComponent<Renderer>().material.mainTexture == newTexture) {
					//Debug.Log ("The wall's current texture is the new texture. Now will change to second material");
					smallWall.GetComponent<Renderer>().material = nextSmallWallMaterial;
				}
			}
			
			//tiny walls (brick4)
			foreach (GameObject tinyWall in thinWalls) {			
				tinyWall.GetComponent<Renderer>().material.SetTexture ("_MainTex", newTexture );
				tinyWall.GetComponent<Renderer>().material.SetTextureOffset ( "_MainTex", tinyOffset );
				tinyWall.GetComponent<Renderer>().material.SetTextureScale ( "_MainTex", tinyTiling );
				tinyWall.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
				
				if ( tinyWall.GetComponent<Renderer>().material.mainTexture == newTexture) {
					//Debug.Log ("The wall's current texture is the new texture. Now will change to second material");
					tinyWall.GetComponent<Renderer>().material = nextTinyWallMaterial;
				}
			}
			
			//Wall thresholds
			foreach (GameObject thresh in wallThresholds) {			
				thresh.GetComponent<Renderer>().material.SetTexture ("_MainTex", newTexture );
				thresh.GetComponent<Renderer>().material.SetTextureOffset ( "_MainTex", threshOffset );
				thresh.GetComponent<Renderer>().material.SetTextureScale ( "_MainTex", threshTiling );
				thresh.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
				
				if ( thresh.GetComponent<Renderer>().material.mainTexture == newTexture) {
					//Debug.Log ("The wall's current texture is the new texture. Now will change to second material");
					thresh.GetComponent<Renderer>().material = nextThreshWallMaterial;
				}
			}

                //Door fillers
                foreach (GameObject filler in doorFillers)
                {
                    filler.GetComponent<Renderer>().material.SetTexture("_MainTex", newTexture);
                    filler.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", doorFillerOffset);
                    filler.GetComponent<Renderer>().material.SetTextureScale("_MainTex", doorFillerTiling);
                    filler.GetComponent<Renderer>().material.SetFloat("_Blend", 0f);
                    if (newTexture.name != "brick1B4")
                    {
                        filler.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    }
                }

                foreach (GameObject filler2 in doorFillers2)
                {
                    filler2.GetComponent<Renderer>().material.SetTexture("_MainTex", newTexture);
                    filler2.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", doorFiller2Offset);
                    filler2.GetComponent<Renderer>().material.SetTextureScale("_MainTex", doorFiller2Tiling);
                    filler2.GetComponent<Renderer>().material.SetFloat("_Blend", 0f);
                    if (newTexture.name != "brick1B4")
                    {
                        filler2.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    }
                }

                //Roof and floor changing
                if (changeRoof == true) {
				foreach (GameObject roof in roofFloors) {
					roof.GetComponent<Renderer>().material.SetTexture ("_MainTex", newTexture );
					roof.GetComponent<Renderer>().material.SetTextureOffset ( "_MainTex", roofOffset );
					roof.GetComponent<Renderer>().material.SetTextureScale ( "_MainTex",roofTiling );
					roof.GetComponent<Renderer>().material.SetFloat( "_Blend", 0f );
				}
			}
			changeRoof = false;
			wallsChanged = true;
		}
	  trigger = false;
    } 	  
  }
  
  //Set the material that you want to change into after blending is finished
  public void setNewMaterial (Material nextMaterial, Material nextSmallWallMat, Material nextTinyWallMat, Material nextThreshMat) {
	  nextWallMaterial = nextMaterial;
	  nextSmallWallMaterial = nextSmallWallMat;
	  nextTinyWallMaterial = nextTinyWallMat;
	  nextThreshWallMaterial = nextThreshMat;
  }
  
  //Reset the flag to allow wall changing again
  public void resetWallChanging () {
	  wallsChanged = false;
  }
  
  //Check to see if all walls have changed or not
  public bool hasWallsChanged () {
	  return wallsChanged;
  }
  
  //Toggle to start changing the roof material
  public void startChangeRoof () {
	  changeRoof = true;
  }
}
