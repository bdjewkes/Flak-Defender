using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {
    public float health;
	
	// Update is called once per frame
	void Update () {
	    if(health <= 0)
        {
            Destroy(gameObject);
        }
	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }

}
