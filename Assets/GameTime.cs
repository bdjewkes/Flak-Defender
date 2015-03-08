using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
    public static bool paused;
    public static float time;
    public static float deltaTime;

    float lastFrame;
    float pausedOffset;
	
    void Awake()
    {
        pausedOffset = 0;
        deltaTime = 0;
    }
    void Update()
    {
        if(!paused)
        {
            if (Time.timeScale != 1) Time.timeScale = 1;
            lastFrame = time;
            time = Time.time - pausedOffset;
            deltaTime = time - lastFrame;
        }
        else
        {
            if (Time.timeScale != 0) Time.timeScale = 0;
            deltaTime = 0;
            pausedOffset += Time.deltaTime;
        }
    }

    public static IEnumerator WaitForSeconds(float t)
    {
        float startTime = time;
        while(true)
        {
            if (time - startTime > t) break;
            yield return null;
        }
    }
	
}
