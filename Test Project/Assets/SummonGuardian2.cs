using UnityEngine;
using System.Collections;

public class SummonGuardian2 : MonoBehaviour {

    public GameObject guardianSecond;
    bool startSummon = false;
    bool summoned = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (startSummon == true)
        {
            if (summoned == false)
            {
                guardianSecond.SetActive(true);
                summoned = true;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startSummon = true;
        }
    }
}
