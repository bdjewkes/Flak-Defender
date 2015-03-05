using UnityEngine;
using System.Collections;

public class FireWeapon : MonoBehaviour {
    public GameObject projectilePrefab;
    public float launchForce;

    public float reloadSpeed;
    bool fire;
    bool fireReady;

	// Use this for initialization
	void Start () {
        fire = false;
        fireReady = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetAxis("Fire1") == 1 && fireReady)
        {
            fire = true;
            fireReady = false;
        }
	}
    void FixedUpdate()
    {
        if (fire)
        {
            LaunchWeapon();
            fire = false;
        }
    }

    void LaunchWeapon()
    {
        var p = Instantiate(projectilePrefab) as GameObject;
        p.transform.position = transform.position + transform.up * 2;
        p.rigidbody.velocity = rigidbody.velocity;
        p.rigidbody.AddRelativeForce(launchForce * transform.up);
        StartCoroutine(ReloadDelay());
    }
    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(reloadSpeed);
        fireReady = true;
    }


}
