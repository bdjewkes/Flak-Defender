using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour {
    public float angularVelocityRate;
    public float maxAngularVelocity;
    public float forwardThrustRate;
    public float reverseThrustRate;
    public float maxVelocity;
    public float maxPivotOffset;
    public float strafeRate;
    
    float velocity = 0;
    

    void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxis("Break")) > 0) ApplyBreak();
        else
        {
            ApplyHeave();
            ApplyStrafe();
            //limit the maximum velocity;
            if (rigidbody.velocity.magnitude > transform.up.magnitude * maxVelocity) rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        }
        ApplyRotation();
    }

    

    void ApplyRotation()
    {
        //if you spin out, first rotation under control:
        if (rigidbody.angularVelocity.magnitude > maxAngularVelocity) rigidbody.angularVelocity -= (angularVelocityRate * rigidbody.angularVelocity.normalized);
        else
        {
            float rotationalAxis = Input.GetAxis("Yaw");
            if (Mathf.Abs(rotationalAxis) > 0)
            {
                rigidbody.angularVelocity += new Vector3(0, 0, -rotationalAxis * angularVelocityRate);
            }
            //rotation automatically stopped if you're not turning
            else
            {
                var mag = rigidbody.angularVelocity.magnitude;
                //break turning at twice the normal rate to make it feel a little tighter?
                rigidbody.angularVelocity -= angularVelocityRate * 2 * rigidbody.angularVelocity.normalized;
                if (rigidbody.angularVelocity.magnitude > mag) rigidbody.angularVelocity = new Vector3(0, 0, 0);
            }
        }
        
    }
    void ApplyHeave()
    {
        float thrustAxis = Input.GetAxis("Heave");
        var thrustDir = transform.up * thrustAxis;
        if (thrustAxis != 0)
        {
            float thrustRate;
            //allow for different forward/reverse thrust rates
            if (thrustAxis > 0) thrustRate = forwardThrustRate;
            else thrustRate = reverseThrustRate;
            rigidbody.velocity += thrustRate * thrustDir;
            
        }
    }
    void ApplyStrafe()
    {
        float strafeAxis = Input.GetAxis("Sway");
        var strafeDir = transform.right * strafeAxis;
        if (strafeAxis != 0)
        {
            rigidbody.velocity += strafeRate * strafeDir;
        }
    }

    void ApplyBreak()
    {
        float breakAxis = Input.GetAxis("Break");
        var mag = rigidbody.velocity.magnitude;
        rigidbody.velocity -= reverseThrustRate * rigidbody.velocity.normalized;
        if ((rigidbody.velocity + (reverseThrustRate * transform.forward)).magnitude > mag) rigidbody.velocity = new Vector3(0, 0, 0);
    }
}
