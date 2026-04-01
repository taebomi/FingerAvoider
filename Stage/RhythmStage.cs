using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmStage : MonoBehaviour {
    GamePlay gp;
    float[,] noteInfo = {
        { 0, 2},{0.5f, 2},{1f, 1},{1.5f,1 },{2f, 4},{2.125f, 3},{2.25f,2}, {4.125f,3 },{4.625f, 3},{5.125f, 4}
        ,{5.625f, 4},{6.25f, 1},{6.375f, 2},{6.5f, 3},{6.8f, 3},{7.75f, 4}, {8.375f,3 }, {8.875f,3 }, {9.375f,2 }, {9.875f, 2}
        , {10.5f, 1}, {10.625f, 4}, {10.75f, 3}, {11, 3}, {12.625f, 2}, {13.125f, 2}, {13.625f, 3}, {14.125f, 3}, {14.625f, 4}, {14.75f, 1}
        , {14.875f,2 }, {15.25f, 2}, {15.75f,1 }, {15.875f, 4}, {16f,2 }, {16.25f, 2}, {16.5f ,1}, {16.75f ,1}, {17.25f ,2}, {17.75f ,2}
        , {18.375f ,4}, {18.875f ,3}, {19.375f ,3}, {19.875f ,4}, { 20.5f,1}, {21f ,2}, {21.5f ,2}, {22f ,1}, { 22.5f,4}, {23f ,3}
        , {23.5f,3}, {24.125f ,4}, {24.375f, 1}, {24.625f, 1}, {25.125f, 2}, {25.625f, 4}, {26.25f, 1}, {26.75f,2 }, {27.25f, 2}, {27.75f, 3}
        , {28.25f, 1}, {28.875f, 4}, {29f, 3}, {29.375f, 1}, {29.875f, 2}, {31, 1}, {31.5f, 2}, {31.75f, 3}, {32f, 3}, {33.625f ,4}
        , { 34.125f,4}, { 34.125f,3}, { 35.75f,2}, { 35.875f,1}, { 36f,4}, { 36.25f,3}, {36.25f,4 }, { 37.5f,1}, {37.625f ,2}, {37.875f ,3}
        , {38.375f,3 }, {38.875f ,1}, { 39.5f,3}, { 40f,4}, { 40.5f,2}, { 41f,2}, {41.5f ,3}, { 41.625f,2}, {41.75f ,4}, {42f ,2}
        , {42.5f,1 }, { 43f,1}, { 43.625f,1}, { 44.125f,4}, { 44.25f,3}, {44.375f ,2}, { 44.75f,2}, {45.25f ,3}, {45.75f ,1}, { 46.25f,2}
        , {46.75f ,3}, { 47.25f,1}
    };
    int noteNum = 0;
    int currentNoteNum = 0;
    int maxNum=102;
    int k = 0;
    float yPos;
    GameObject[] note = new GameObject[100];
    GameObject[] explosion = new GameObject[10];
    public GameObject noteParent;
    public GameObject notePrefab;
    public GameObject explosionPrefab;
    public AudioClip bgm;
    float tickSum;
    float tick;
    Transform playerTr;
    void Start () {
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        maxNum = noteInfo.GetLength(0);
        for (int i = 0; i < 100; i++)
        {
            note[i] = Instantiate(notePrefab, new Vector3(-50, 0, 0), Quaternion.identity, noteParent.transform) as GameObject;
            note[i].SetActive(false);
        }
        for(int i = 0; i < 10; i++)
        {
            explosion[i] = Instantiate(explosionPrefab, new Vector3(-50, 0, 0), Quaternion.identity, noteParent.transform) as GameObject;
            explosion[i].SetActive(false);
        }
        StartCoroutine(CheckStart());
	}
    public int GetID()
    {
        return noteNum;
    }
    void Play() {
        tick = 0;
        tickSum = Time.time;
        PlayNote();
        Invoke("PlayBGM",  3.75f);
    }
    void PlayBGM()
    {
        //GameSystem.instance.PlayBGM(bgm,10.5f);
        GameSystem.instance.PlayBGM(bgm);
    }
    void PlayNote()
    {
        note[noteNum].SetActive(true);
        yPos = noteInfo[currentNoteNum, 1];
        if (yPos == 1)
        {
            note[noteNum].transform.position = new Vector2(-38f, 8.69f);
            note[noteNum].GetComponent<Note>().lineNum = 1;
        }
        else if (yPos == 2)
        {
            note[noteNum].transform.position = new Vector2(-38f, 2.89f);
            note[noteNum].GetComponent<Note>().lineNum = 2;
        }
        else if (yPos == 3)
        {
            note[noteNum].transform.position = new Vector2(-38f, -2.89f);
            note[noteNum].GetComponent<Note>().lineNum = 3;
        }
        else
        {
            note[noteNum].transform.position = new Vector2(-38f, -8.69f);
            note[noteNum].GetComponent<Note>().lineNum = 4;
        }
        tickSum += noteInfo[noteNum + 1, 0] - noteInfo[noteNum, 0];
        tick = tickSum - Time.time;
        if (currentNoteNum != maxNum - 1)
            Invoke("PlayNote", tick);
        else
            StartCoroutine(Clear());
        currentNoteNum++;
        noteNum++;
        if (currentNoteNum % 100==0)
            noteNum = 0;
    }
    IEnumerator Clear()
    {
        float j = 1;
        while (j > 0)
        {
            GameSystem.instance.ChangeBGMVolume2(j);
            j -= 0.025f;
            yield return new WaitForSeconds(0.1f);
        }
        GameSystem.instance.StopBGM();
        GameSystem.instance.ChangeBGMVolume(PlayerPrefs.GetFloat("BGM"));
        if(gp.playing)
            gp.GameClear();
    }
    IEnumerator CheckStart()
    {
        while (true)
        {
            if (gp.playing)
                break;
            yield return new WaitForSeconds(0.5f);
        }
        Play();
    }
    IEnumerator SetFalseBomb(int n)
    {
        yield return new WaitForSeconds(0.5f);
        explosion[n].SetActive(false);
    }
    public void Bomb(int n)
    {
        if (n == 1)
        {
            if (playerTr.position.y >= 5.8f)
                gp.GameOver();
            for (k = 0; k < 10; k++)
            {
                if (!explosion[k].activeSelf)
                {
                    explosion[k].transform.position = new Vector3(22.6f,8.9f,0f);
                    explosion[k].SetActive(true);
                    StartCoroutine(SetFalseBomb(k));
                    break;
                }
            }
        }
        else if (n == 2)
        {
            if (playerTr.position.y <= 5.8f&&playerTr.position.y>=0)
                gp.GameOver();
            for (k = 0; k < 10; k++)
            {
                if (!explosion[k].activeSelf)
                {
                    explosion[k].transform.position = new Vector3(22.6f, 2.8f, 0f);
                    explosion[k].SetActive(true);
                    StartCoroutine(SetFalseBomb(k));
                    break;
                }
            }
        }
        else if (n == 3)
        {

            if (playerTr.position.y >= -5.8f && playerTr.position.y <= 0)
                gp.GameOver();
            for (k = 0; k < 10; k++)
            {
                if (!explosion[k].activeSelf)
                {
                    explosion[k].transform.position = new Vector3(22.6f, -2.8f, 0f);
                    explosion[k].SetActive(true);
                    StartCoroutine(SetFalseBomb(k));
                    break;
                }
            }
        }
        else
        {

            if (playerTr.position.y <= -5.8f)
                gp.GameOver();
            for (k = 0; k < 10; k++)
            {
                if (!explosion[k].activeSelf)
                {
                    explosion[k].transform.position = new Vector3(22.6f, -8.9f, 0f);
                    explosion[k].SetActive(true);
                    StartCoroutine(SetFalseBomb(k));
                    break;
                }
            }
        }
    }
}
