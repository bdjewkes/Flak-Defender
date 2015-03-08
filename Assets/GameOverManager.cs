using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour {
    public GameObject player;
    public GameObject baseShip;
    public GameObject GOCam;
    public GameObject GOObj;

    bool gameOver;

    void Start()
    {
        gameOver = false;
    }
    void Update()
    {
        if((player == null || baseShip == null) && !gameOver)
        {
            StartCoroutine(RunGameOver());
        }
    }
    IEnumerator RunGameOver()
    {
        gameOver = true;
        yield return new WaitForSeconds(1.5f);
        foreach(var obj in GetComponent<StartGame>().startObjs)
        {
            obj.SetActive(false);
        }
        Camera.main.gameObject.SetActive(false);
        GOObj.SetActive(true);
        GOCam.SetActive(true);
        GameTime.paused = true;
    }


    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


}
