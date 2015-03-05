using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float lifespan;
    public float damage;
    public GameObject explosionEffect;

	IEnumerator Start () {
        float startTime = Time.time;
        while(true)
        {
            if (lifespan < (Time.time - startTime)) Destroy(gameObject);
            yield return null;
        }
	}
	void OnCollisionEnter(Collision col)
    {
        Debug.Log("collided with " + col.gameObject.name);
        var explode = Instantiate(explosionEffect) as GameObject;
        explode.transform.position = transform.position;
        col.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
