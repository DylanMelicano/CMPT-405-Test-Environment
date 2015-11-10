using UnityEngine;
using System.Collections;

public class rotateMe : MonoBehaviour {

    public int speed;
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
           // temp.y -= speed;

        }
        else
        {
            temp.y += speed;
            //temp.x += speed;
            //temp.y += speed;
        }
        transform.rotation = Quaternion.Euler(temp.x, temp.y, temp.z);
    }
}
