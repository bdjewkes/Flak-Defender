using UnityEngine;
using System.Collections;

public class FlakProjectile : Projectile
{
    
    public float detonateDistance;
    public float damageRadius;
    

    IEnumerator Start()
    {
        Vector3 origin = transform.position;
        while(true)
        {
            if((origin - transform.position).magnitude > detonateDistance)
            {
                DetonateFlak();
                Destroy(gameObject);
            }
            yield return null;
        }
    }
    void DetonateFlak()
    {
        var explode = Instantiate(explosionEffect) as GameObject;
        if (explode.GetComponent<AudioSource>() == null) explode.AddComponent<AudioSource>();
        explode.transform.position = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(explode.transform.position, damageRadius);
        foreach(var hit in hitColliders)
        {
            hit.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
        explode.audio.PlayOneShot(hitSound, 0.3f);
    }
    new void OnCollisionEnter(Collision col)
    {
        DetonateFlak();
        base.OnCollisionEnter(col);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, damageRadius);
    }
}
