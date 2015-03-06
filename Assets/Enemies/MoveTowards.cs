using UnityEngine;
using System.Collections;

public class MoveTowards : MonoBehaviour {
    public GameObject target;
    public float moveToRange;
    public float speed;


    void Start()
    {
        StartCoroutine(RunMoveTowards());
    }
    
        
    IEnumerator RunMoveTowards()
    {
        Vector3 startPos = transform.position;
        float totalDistance = (target.transform.position - transform.position).magnitude;
        Debug.Log((target.transform.position - transform.position).magnitude);
        float distanceTraveled = 0;
        while(true)
        {
            if (distanceTraveled >= totalDistance - moveToRange) break;
            float percentComplete = (distanceTraveled / totalDistance);
            transform.position = Vector3.Lerp(startPos, target.transform.position, percentComplete);
            float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, 
                target.transform.position.x - transform.position.x) * 180 / Mathf.PI;
            transform.rotation = Quaternion.Euler(0, 0, angle-90);
            distanceTraveled += speed * Time.deltaTime;
            yield return null;
        }
    }

}
