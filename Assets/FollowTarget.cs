using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
    public GameObject followTarget;

    void Update()
    {
        transform.position = new Vector3(followTarget.transform.position.x,
            followTarget.transform.position.y,
            transform.position.z);
    }
	
}
