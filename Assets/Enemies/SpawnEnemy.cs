using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour {
    public float spawnTimer;
    //right now just the position on the x axis enemy should spawn; in the future, will be the distance from baseship an enemy spawns
    public float radius;
    public GameObject target;


    public GameObject enemyPrefab;

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
        var e = Instantiate(enemyPrefab) as GameObject;
        float x = Random.Range(-radius, radius);
        e.transform.position = new Vector3(x, transform.position.y, transform.position.z);
        e.GetComponent<MoveTowards>().target = target;
    }



}
