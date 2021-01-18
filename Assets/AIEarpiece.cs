using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEarpiece : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
    }

    public void GoForwards()
    {
        Transform target = gameObject.transform;
        target.position += 4.0f * target.forward;
        SendMessageUpwards( "SetTarget", target );
    }

    public void TurnLeft()
    {
        Transform target = gameObject.transform;
        target.Rotate( Vector3.up, -15.0f );
        target.position += target.forward;
        SendMessageUpwards( "SetTarget", target );
    }

    public void TurnRight()
    {
        Transform target = gameObject.transform;
        target.Rotate( Vector3.up, 15.0f );
        target.position += target.forward;
        SendMessageUpwards( "SetTarget", target );
    }
}
