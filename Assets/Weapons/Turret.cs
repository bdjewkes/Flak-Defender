using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
    GameObject flakPrefab;
    void Start()
    {
        flakPrefab = GetComponent<FireWeapon>().projectilePrefab;
    }

    Vector3 GetMaximumDriftVector(bool right, Vector3 origin)
    {
        Vector3 direction = transform.right;
        if (!right) direction = transform.right * -1;
        var launcher = GetComponent<FireWeapon>();
        var flak = launcher.projectilePrefab.GetComponent<FlakProjectile>();
        Vector3 vector = origin + (transform.up * (launcher.launchForce * flak.rigidbody.mass)
            + (direction * (launcher.driftLateral * flak.rigidbody.mass))).normalized
            *(flak.detonateDistance + launcher.driftRange); //max range;
        return vector;
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var launcher = GetComponent<FireWeapon>();
        Gizmos.DrawWireSphere(transform.position, launcher.armDistance);
        Gizmos.color = Color.red;
        var flak = launcher.projectilePrefab.GetComponent<FlakProjectile>();
        float adjustedDetonateDistance = launcher.armDistance + flak.detonateDistance;
        Vector3 armedOrigin = transform.position + (transform.up * launcher.armDistance);
        Gizmos.DrawWireSphere(transform.position, adjustedDetonateDistance + launcher.driftRange);
        Gizmos.DrawWireSphere(transform.position, adjustedDetonateDistance - launcher.driftRange);
        Gizmos.color = Color.yellow;
   //     Gizmos.DrawLine(
        Gizmos.DrawLine(armedOrigin, GetMaximumDriftVector(true, armedOrigin));
        Gizmos.DrawLine(armedOrigin, GetMaximumDriftVector(false, armedOrigin));
    }
}
