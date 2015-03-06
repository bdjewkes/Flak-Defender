using UnityEngine;
using System.Collections;

public class ExplosionCleanup : MonoBehaviour {
    public float timer = 3;
        
    IEnumerator Start()
    {
        var startT = GameTime.time;
        while(true)
        {
            if (GameTime.time - startT > timer) break;
            yield return null;
        }
        Destroy(gameObject);
    }
}
