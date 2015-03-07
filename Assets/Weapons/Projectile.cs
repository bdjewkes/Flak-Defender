using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float lifespan;
    public float damage;
    public GameObject explosionEffect;

    public AudioClip fireSound;
    public AudioClip hitSound;

	IEnumerator Start () {
        float startTime = GameTime.time;
        while(true)
        {
            if (lifespan < (GameTime.time - startTime)) Destroy(gameObject);
            yield return null;
        }
	}
	void OnCollisionEnter(Collision col)
    {
        Debug.Log("collided with " + col.gameObject.name);
        var explode = Instantiate(explosionEffect) as GameObject;
        explode.transform.position = transform.position;
        explode.audio.PlayOneShot(hitSound, 0.1f);
        col.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
