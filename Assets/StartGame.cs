using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartGame : MonoBehaviour {
    public GameObject mainCam;
    public GameObject startCam;
    public GameObject startUI;

    public AudioClip startSound;
    public List<GameObject> startObjs;

	void Awake()
    {
        GameTime.paused = true;
        mainCam.gameObject.SetActive(false);
        startUI.SetActive(true);
        startCam.SetActive(true);
    }
	
	public void LaunchClicked()
    {
        StartCoroutine(RunLaunchClicked());      
    }

    IEnumerator RunLaunchClicked()
    {
        GetComponent<AudioSource>().PlayOneShot(startSound, 0.7f);
        GameTime.paused = false;
        yield return new WaitForSeconds(1);
        startCam.SetActive(false);
        foreach(var obj in startObjs)
        {
            obj.SetActive(true);
        }
        mainCam.gameObject.SetActive(true);
        startUI.SetActive(false);
        
        
    }

}
