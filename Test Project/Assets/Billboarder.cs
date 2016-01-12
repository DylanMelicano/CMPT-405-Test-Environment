using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.localEulerAngles = new Vector3(90, player.transform.localEulerAngles.y - 180, 0);
    }
}
