using UnityEngine;
using System.Collections;

public class FireWeapon : MonoBehaviour {
    public GameObject projectilePrefab;
    public float launchForce;
    public bool autoFire;


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
        var p = Instantiate(projectilePrefab) as GameObject;
        p.transform.position = transform.position + transform.up * 3;
        p.rigidbody.velocity = rigidbody.velocity;
        p.rigidbody.AddRelativeForce(launchForce * transform.up);
    }
    IEnumerator ReloadDelay()
    {
        yield return StartCoroutine(GameTime.WaitForSeconds(reloadSpeed));
        fireReady = true;
    }


}
