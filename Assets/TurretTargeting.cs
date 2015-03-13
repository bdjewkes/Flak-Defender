using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretTargeting : MonoBehaviour {
    public List<GameObject> turrets;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Fire2") > 0)
        {
            foreach(var t in turrets)
            {
                var turret = t.GetComponent<Turret>();
                if(turret.target == null) turret.target = gameObject;
            }
        }
	}
}
