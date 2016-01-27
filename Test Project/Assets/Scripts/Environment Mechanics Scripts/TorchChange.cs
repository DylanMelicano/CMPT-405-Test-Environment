using UnityEngine;
using System.Collections;

public class TorchChange : MonoBehaviour {

    public Light torch;
    bool upgraded = false;

	// Use this for initialization
	void Start () {
        torch = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && upgraded == false) {
            torch.range = 40;
            //torch.intensity = 8f;
            upgraded = true;
        } else if (Input.GetButtonDown("Jump") && upgraded == true) {
            torch.range = 20;
           // torch.intensity = 4.2f;
            upgraded = false;
        }
	
	}
}
