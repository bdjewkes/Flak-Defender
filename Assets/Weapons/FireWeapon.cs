using UnityEngine;
using System.Collections;

public class FireWeapon : MonoBehaviour {
    public GameObject projectilePrefab;
    public float launchForce;
    public float armDistance = 3;
    public bool autoFire;

    public float driftRange = 0;
    public float driftLateral = 0;

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
	    if(autoFire && fireReady)
        {
            Fire();
        }
        else if(Input.GetAxis("Fire1") == 1 && fireReady)
        {
            Fire(); 
        }
        
	}
    void FixedUpdate()
    {
        if (fire)
        {
            LaunchWeapon();
        }
    }
    void Fire()
    {
        fire = true;
        fireReady = false;
        StartCoroutine(ReloadDelay());
    }


    private void LaunchWeapon()
    {
        fire = false;
        GetComponent<AudioSource>().PlayOneShot(projectilePrefab.GetComponent<Projectile>().fireSound, 0.05f);
        var p = Instantiate(projectilePrefab) as GameObject;
        p.transform.position = new Vector3(transform.position.x, transform.position.y, 0) + transform.up * armDistance;
        p.rigidbody.velocity = rigidbody.velocity;
        p.rigidbody.AddRelativeForce(launchForce * transform.up);
        if (p.GetComponent<FlakProjectile>()) p.GetComponent<FlakProjectile>().detonateDistance += Random.Range(-driftRange, driftRange);
        p.rigidbody.AddRelativeForce(Random.Range(-driftLateral, driftLateral) * transform.right);
    }
    IEnumerator ReloadDelay()
    {
        yield return StartCoroutine(GameTime.WaitForSeconds(reloadSpeed));
        fireReady = true;
    }


}
