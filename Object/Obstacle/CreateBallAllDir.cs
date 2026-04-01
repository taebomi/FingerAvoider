using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBallAllDir : MonoBehaviour {
    public float rotateSpeed;
    public float createSpeed;
    public int createNum;
    int ballnum;
    public GameObject ball;
    GameObject[] balls;
    GamePlay gp;
    Coroutine ballMakeC;
    Coroutine rotateC;
    void Start ()
    {
        balls = new GameObject[createNum];
        ballnum = 0;
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        StartCoroutine(CheckStart());
    }
    private void OnDisable()
    {
        for (int i = 0; i < ballnum; i++)
        {
            if (balls[i].gameObject != null)
            {
                balls[i].SetActive(false);
            }
        }
        StopCoroutine(rotateC);
        StopCoroutine(ballMakeC);
    }
    private void OnEnable()
    {
        ballnum = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(CheckStart());
    }
    IEnumerator CheckStart()
    {
        while (!gp.playing)
        {
            yield return null;
        }
        rotateC =  StartCoroutine(RotateF());
        ballMakeC = StartCoroutine(CreateF());
    }
    IEnumerator RotateF()
    {
        while (ballnum < createNum)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed));
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator CreateF()
    {
        while (ballnum < createNum)
        {
            if (balls[ballnum] == null)
                balls[ballnum] = Instantiate(ball, transform.position, transform.rotation);
            else
            {
                balls[ballnum].transform.position = transform.position;
                balls[ballnum].transform.rotation = transform.rotation;
                balls[ballnum].SetActive(true);
        }
            ballnum++;
            yield return new WaitForSeconds(createSpeed);
        }
        gp.SetFinishPos(0, 14);
    }
}
