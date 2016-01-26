using UnityEngine;
using System.Collections;

public class rotateMe : MonoBehaviour {

    public float speed;
    public bool invert;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 temp = gameObject.GetComponent<Transform>().localRotation.eulerAngles;
        if (invert)
        {
            temp.y -= speed;
			//temp.x -= speed;
            //temp.z -= speed;

        }
        else
        {
            temp.y += speed;
            //temp.x += speed;
            //temp.z += speed;
        }
        transform.rotation = Quaternion.Euler(temp.x, temp.y, temp.z);
    }
}
