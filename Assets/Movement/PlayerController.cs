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

    public GameObject mainThrustEffect;
    public GameObject secondaryThrustEffect;

    void Update()
    {
        float heave = Input.GetAxis("Heave");
        if (heave != 0)
        {
            if (heave > 0) mainThrustEffect.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));
            if (heave < 0) mainThrustEffect.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            mainThrustEffect.SetActive(true);
        }
        else mainThrustEffect.SetActive(false);
        float sway = Input.GetAxis("Sway");
        if (sway != 0)
        {
            if (sway > 0) secondaryThrustEffect.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            if (sway < 0) secondaryThrustEffect.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            secondaryThrustEffect.SetActive(true);
        }
        else secondaryThrustEffect.SetActive(false);
    }

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
        if (rigidbody.angularVelocity.magnitude >= maxAngularVelocity)
        {
            rigidbody.AddRelativeForce(angularVelocityRate * -rigidbody.angularVelocity.normalized
                * (1 + Mathf.Pow(rigidbody.angularVelocity.magnitude - maxAngularVelocity, 2)
                ));
        }

        float rotationalAxis = Input.GetAxis("Yaw");
        if (Mathf.Abs(rotationalAxis) > 0)
        {
            if(rigidbody.angularVelocity.magnitude < maxAngularVelocity) rigidbody.AddRelativeTorque(transform.forward * -rotationalAxis * angularVelocityRate);
        }
        //rotation automatically stopped if you're not turning
        else
        {
            var mag = rigidbody.angularVelocity.magnitude;
            rigidbody.AddRelativeTorque(angularVelocityRate * -rigidbody.angularVelocity.normalized);
            if (rigidbody.angularVelocity.magnitude > mag) rigidbody.angularVelocity = new Vector3(0, 0, 0);
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
        var mag = rigidbody.velocity.magnitude;
        rigidbody.velocity -= reverseThrustRate * rigidbody.velocity.normalized;
        if ((rigidbody.velocity + (reverseThrustRate * transform.forward)).magnitude > mag) rigidbody.velocity = new Vector3(0, 0, 0);
    }
}
