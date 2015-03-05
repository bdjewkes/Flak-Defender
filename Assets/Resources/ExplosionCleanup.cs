using UnityEngine;
using System.Collections;

public class ExplosionCleanup : MonoBehaviour {
    public float timer = 3;
        
    IEnumerator Start()
    {
        var startT = Time.time;
        while(true)
        {
            if (Time.time - startT > timer) break;
            yield return null;
        }
        Destroy(gameObject);
    }
}
