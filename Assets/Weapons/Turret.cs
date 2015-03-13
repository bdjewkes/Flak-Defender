using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
    public GameObject arcLeft;
    public GameObject arcRight;
    
    /// <summary>
    /// Segment of arc that is outside the turret's firing arc
    /// </summary>
    [SerializeField]
    float minNoFireTheta;
    public float minNoFireRad
    {
        get { return Mathf.Deg2Rad * minNoFireTheta; }
        set { minNoFireTheta = value; }
    }
    [SerializeField]
    float maxNoFireTheta;
    public float maxNoFireRad
    {
        get { return Mathf.Deg2Rad * maxNoFireTheta; }
        set { maxNoFireTheta = value; }
    }

    
    GameObject flakPrefab;
    public GameObject target;

    bool rotating;

    void Start()
    {
        rotating = false;
        flakPrefab = GetComponent<FireWeapon>().projectilePrefab;
    }

    void Update()
    {
        if(target != null && !rotating)
        {
            StartCoroutine(RunRotateToTarget());   
        }

        var lLeft = arcLeft.GetComponent<LineRenderer>();
        var lRight = arcRight.GetComponent<LineRenderer>();
        lLeft.SetPosition(0, transform.position);
        lLeft.SetPosition(1, GetMaximumDriftVector(false, transform.position));
        lRight.SetPosition(0, transform.position);
        lRight.SetPosition(1, GetMaximumDriftVector(true, transform.position));
    }

    IEnumerator RunRotateToTarget()
    {
        rotating = true;
        float percentRot = 0;
        Quaternion startRot = transform.rotation;
        while(true)
        {
            if (percentRot >= 1) break;
            float angle = (Mathf.Atan2(target.transform.position.y - transform.position.y,
                target.transform.position.x - transform.position.x)*180 / Mathf.PI) - 90;
            if (angle < 0) angle += 360;
            Debug.Log(angle);
            if (angle > minNoFireTheta && angle < maxNoFireTheta) break;
            transform.rotation = Quaternion.Slerp(startRot, Quaternion.Euler(0,0,angle), percentRot);
            percentRot += Time.deltaTime * 0.8f;
            yield return null;
        }
        target = null;
        rotating = false;
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
        Gizmos.DrawLine(transform.position, transform.position + 
            new Vector3(launcher.armDistance * -Mathf.Sin(minNoFireRad), 
                        launcher.armDistance * Mathf.Cos(minNoFireRad), 0));
        Gizmos.DrawLine(transform.position, transform.position + 
            new Vector3(launcher.armDistance * -Mathf.Sin(maxNoFireRad), 
                        launcher.armDistance * Mathf.Cos(maxNoFireRad), 0));
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
