using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {
    public float health;
    public GameObject explosionPrefab;
    bool destructionInProgress;

    public AudioClip explodeSound;

    void Awake()
    {
        if (explosionPrefab == null) explosionPrefab = Resources.Load("defaultExplosion") as GameObject;
    }
    void Start()
    {
        destructionInProgress = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(health <= 0)
        {
            if (!destructionInProgress)
            {
                StartCoroutine(RunDestroy());
            }
        }
	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }

    IEnumerator RunDestroy()
    {
        destructionInProgress = true;
        var explode = Instantiate(explosionPrefab) as GameObject;
        explode.AddComponent<AudioSource>();
        explode.audio.PlayOneShot(explodeSound, 1.0f);
        explode.transform.position = transform.position;
        yield return StartCoroutine(GameTime.WaitForSeconds(0.25f));
        if (GetComponentInChildren<Camera>()) Camera.main.transform.parent = explode.transform;      
        Destroy(gameObject);        
    }

}
