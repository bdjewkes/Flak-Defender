using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float lifespan;
    public float damage;
	IEnumerator Start () {
        float startTime = Time.time;
        Debug.Log("Start");
        while(true)
        {
            if (lifespan < (Time.time - startTime)) Destroy(gameObject);
            yield return null;
        }
	}
	void OnCollisionEnter(Collision col)
    {
        col.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);

    }
}
