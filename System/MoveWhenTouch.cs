using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhenTouch : MonoBehaviour {
    GamePlay gp;
    private void Awake()
    {
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        StartCoroutine(CheckStart());
    }
    IEnumerator CheckStart()
    {
        while (!gp.playing)
        {
            yield return null;
        }
        Time.timeScale = 1;
    }
}
