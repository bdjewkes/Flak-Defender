using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour {
    public float spawnTimer;
    //right now just the position on the x axis enemy should spawn; in the future, will be the distance from baseship an enemy spawns
    public float radius;
    public GameObject target;
    public GameObject enemyPrefab;
    public GameObject minimapDisplay;
    void Awake()
    {
        //Draw the minimap layer
        float theta = 0;
        var l = Instantiate(minimapDisplay) as GameObject;
        LineRenderer lR = l.GetComponent<LineRenderer>();
        int i = 0;
        while(true)
        {   
            if (theta > 2 * Mathf.PI) break;
            Vector3 pt = GetPosOnCircumference(theta);
            theta += 0.1f;;
            lR.SetVertexCount(i + 1);
            lR.SetPosition(i, pt);
            i++;
        }
    }

    IEnumerator Start()
    {
        while(true)
        {
            yield return StartCoroutine(GameTime.WaitForSeconds(spawnTimer));
            SpawnShip();
        }
    }

    void SpawnShip()
    {
        float theta = (Mathf.PI * 2) * (Mathf.Round(Random.Range(0, 100))/100);
        var e = Instantiate(enemyPrefab) as GameObject;
        e.transform.position = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), transform.position.z);
        e.GetComponent<MoveTowards>().target = target;
    }


    Vector3 GetPosOnCircumference(float theta)
    {
        return new Vector3(radius * Mathf.Cos(theta), 
            radius * Mathf.Sin(theta), 
            0);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        float theta = 0;
        while(true)
        {
            if(theta >= 2* Mathf.PI) break;
            Vector3 pt1 = GetPosOnCircumference(theta);
            theta += 0.1f;
            Vector3 pt2 = GetPosOnCircumference(theta);
            Gizmos.DrawLine(pt1, pt2);
        }
    }
}
