using UnityEngine;
using System.Collections;

public class mazeGuardianMovement : MonoBehaviour
{

    void Start()
    {
        iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPath("guardian"), "time", 12, "looptype", "loop", "easetype", "linear"));
    }
}

