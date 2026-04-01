
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmStage2 : MonoBehaviour
{
    GamePlay gp;
    float[,] noteInfo = {
        { 0.1f,2},{0.55f, 1},{1.45f, 2},{1.75f,3 },{1.95f, 4},{3.25f, 1},{3.55f,3}, {3.7f,3 }, {4.15f,2 }, {5.05f,4 }
         , {5.35f,4 }, {5.5f,3 }, {6.4f,2 }, {6.85f,1 }, {7.3f,3 }, {7.75f,4 }, {8.65f,3 }, {8.95f,2 }, {9.15f,1 }, {10.45f,4 }
        , {10.75f,2 }, {10.9f,1 }, {12.25f,3 }, {12.55f,3 },{12.7f,1 },{13.6f,2 },{14.5f,3 },{14.95f,2 },{15.4f,3 }, {16.15f,2 }
        , {16.3f,1 }, {16.6f,3 }, {16.75f,4 }, {18.1f,3 }, {18.55f,2 }, {19f,3 }, {19.75f,1 }, {19.9f,2 }, {20.2f,3 }, {20.35f,4 }
        , {21.7f,3 }, {22.15f,2 }, {22.6f,3 }, {23.35f,2 }, {23.5f,1 }, {23.8f,3 }, {23.95f,4 }, {24.85f,2 }, {25.15f,3 }, {25.3f,4}
        , {26.2f,3 }, {26.5f,2 }, {26.65f,3 }, {26.95f,4 }, {27.1f,1 }, {28f,2 }, {28.9f,3 }, {29.35f,2 }, {29.8f,3 }, {30.25f,4 }
        , {30.7f,2 }, {31.15f,1 }, {31.6f,2 }, {32.05f,3 }, {32.5f,4 }, {32.95f,1 }, {33.4f,4 }, {33.85f,2 }, {34.3f,1 }, {34.75f,3 }
        , {35.2f,1 }, {35.65f,4 }, {36.1f,2 }, {36.55f,3 }, {37f,2}, {37.45f,4 }, {37.9f,3 }, {38.35f,1 }, {38.8f,3 }, {39.25f,2 }
        , {39.7f,1 }, {40.15f,4 }, {40.6f,1 }, {41.05f,2 }, {41.5f,4 }, {41.95f,3 }, {42.4f,2 }, {42.85f, 2}, {43.1f, 4}, {43.3f, 1}
        , {43.75f, 3}, {44.2f, 1}, {44.65f, 2}, {45.1f, 4}, {45.55f, 3}, {46f, 4}, {46.45f, 1}, {46.9f, 2}, {47.35f, 3}, {47.8f, 2}
        , {48.25f, 4}, {48.7f, 3}, {49.15f, 1}, {49.6f, 3}, {50.05f, 3}, {50.5f, 4}, {50.95f, 2}, {51.4f, 4}, {51.85f, 3}, {52.3f, 1}
        , {52.75f, 3}, {53.2f, 1}, {53.65f, 4}, {54.1f, 3}, {54.55f, 2}, {55f, 3}, {55.45f, 1}, {55.9f, 2}, {56.35f, 15}, {56.8f, 15}
        , {57.25f,1 }, {57.55f,3 }, {57.7f,4}, {58.15f,2 }, {59.05f,1 }, {59.35f,2 }, {59.5f,3 }, {60.85f,4 }, {61.1f,4 }, {61.3f,4 }
        , {61.75f,1 }, {62.65f,4 }, {62.95f,4 }, {63.1f,3 }, {64f,2 }, {64.45f,1 }, {64.9f,4 }, {65.35f,1 }, {66.25f,2 }, {66.55f,3 }
        , {66.7f,4 }, {68.05f,2 }, {68.35f,2 }, {68.5f,4 }, {69.85f,1 }, {70.15f,1 }, {70.3f,4 }, {71.2f,3 }, {72.1f,1 }, {72.55f,2 }
        , {73f,1 }, {73.75f,3 }, {73.9f,4 }, {74.2f,3 }, {74.35f,2 }, {75.55f,1 }, {75.7f,2 }, {76.15f,1 }, {76.6f,2 }, {77.35f,4 }
        , {77.5f,3 }, {77.8f,2 }, {77.95f,1 }, {79.15f,3 }, {79.3f,2 }, {79.75f,4 }, {80.2f,2 }, {80.95f,2 }, {81.1f,1 }, {81.4f,2 }
        , {81.55f,3 }, {82.45f,4 }, {82.75f,3 }, {82.9f,2 }, {84.1f,2 }, {84.25f,3 }, {84.55f,2 }, {84.7f,1 }, {85.6f,3 }, {86.5f,4 }
        , {86.95f,15 }, {87.4f,4 }, {87.85f,15 }, {88.15f,4 }, {88.3f,1 }, {88.75f,12 }, {89.2f,1 }, {89.65f,12 }, {89.95f,2 },{90.1f,3 }
        , {90.55f,11 }, {91f,3 }, {91.45f,11 }, {91.75f,3 }, {91.9f,2 }, {92.35f,16 }, {92.8f,2 }, {93.25f,16 }, {93.55f,2 }, {93.7f,1 }
        , {94.15f,13 }, {94.6f,4 }, {95.05f,13 }, {95.35f,2 }, {95.5f,1 }, {95.95f,13 }, {96.4f,3 }, {96.85f,13 }, {97.15f,1 }, {97.3f,3 }
        , {97.75f,15 }, {98.2f,4 }, {98.65f,15 }, {98.95f,2 }, {99.1f,1 }, {99.55f,12 }, {100f,12 }, {999f,1 }
};
    int noteNum = 0;
    int currentNoteNum = 0;
    int maxNum = 217;
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
    void Start()
    {   
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        maxNum = noteInfo.GetLength(0);
        for (int i = 0; i < 100; i++)
        {
            note[i] = Instantiate(notePrefab, new Vector3(-50, 0, 0), Quaternion.Euler(0,0,90f), noteParent.transform) as GameObject;
            note[i].SetActive(false);
        }
        for (int i = 0; i < 10; i++)
        {
            explosion[i] = Instantiate(explosionPrefab, new Vector3(-50, 0, 0), Quaternion.identity, noteParent.transform) as GameObject;
            explosion[i].SetActive(false);
        }
        StartCoroutine(CheckStart());
    }
    void Play()
    {
        StartCoroutine(CheckTime());
        Invoke("PlayBGM", 3.9f);
    }
    void PlayBGM()
    {
        GameSystem.instance.PlayBGM(bgm);
    }
    public int GetID()
    {
        return noteNum;
    }
    IEnumerator CheckTime()
    {
        tick = noteInfo[0,0];
        tickSum = Time.time;
        while (currentNoteNum<maxNum-1)
        {
            yield return new WaitForSeconds(tick);
            MakeNote();
            tickSum += noteInfo[currentNoteNum, 0] - noteInfo[currentNoteNum-1, 0];
            tick = tickSum - Time.time;
            yield return null;
        }
        StartCoroutine(Clear());
    }
    void MakeNote()
    {
        yPos = noteInfo[currentNoteNum, 1];
        if (yPos < 10)
        {
            note[noteNum].SetActive(true);
            if (yPos == 1)
            {
                note[noteNum].transform.position = new Vector2(-38f, 8.69f);
                note[noteNum].GetComponent<Note2>().lineNum = 1;
            }
            else if (yPos == 2)
            {
                note[noteNum].transform.position = new Vector2(-38f, 2.89f);
                note[noteNum].GetComponent<Note2>().lineNum = 2;
            }
            else if (yPos == 3)
            {
                note[noteNum].transform.position = new Vector2(-38f, -2.89f);
                note[noteNum].GetComponent<Note2>().lineNum = 3;
            }
            else
            {
                note[noteNum].transform.position = new Vector2(-38f, -8.69f);
                note[noteNum].GetComponent<Note2>().lineNum = 4;
            }
        }
        else if (yPos < 100)
        {
            if (noteNum > 98)
                noteNum = 0;
            note[noteNum].SetActive(true);
            noteNum++;
            note[noteNum].SetActive(true);
            if (yPos == 11)
            {
                note[noteNum-1].transform.position = new Vector2(-38f, 8.69f);
                note[noteNum-1].GetComponent<Note2>().lineNum = 1;
                note[noteNum].transform.position = new Vector2(-38f, 2.89f);
                note[noteNum].GetComponent<Note2>().lineNum = 2;
            }
            else if (yPos == 12)
            {
                note[noteNum-1].transform.position = new Vector2(-38f, 8.69f);
                note[noteNum-1].GetComponent<Note2>().lineNum = 1;
                note[noteNum].transform.position = new Vector2(-38f, -2.89f);
                note[noteNum].GetComponent<Note2>().lineNum = 3;
            }
            else if (yPos == 13)
            {
                note[noteNum - 1].transform.position = new Vector2(-38f, 8.69f);
                note[noteNum - 1].GetComponent<Note2>().lineNum = 1;
                note[noteNum].transform.position = new Vector2(-38f, -8.69f);
                note[noteNum].GetComponent<Note2>().lineNum = 4;
            }
            else if(yPos == 14)
            {
                note[noteNum - 1].transform.position = new Vector2(-38f, 2.89f);
                note[noteNum - 1].GetComponent<Note2>().lineNum = 2;
                note[noteNum].transform.position = new Vector2(-38f, -2.89f);
                note[noteNum].GetComponent<Note2>().lineNum = 3;
            }
            else if (yPos == 15)
            {
                note[noteNum - 1].transform.position = new Vector2(-38f, 2.89f);
                note[noteNum - 1].GetComponent<Note2>().lineNum = 2;
                note[noteNum].transform.position = new Vector2(-38f, -8.69f);
                note[noteNum].GetComponent<Note2>().lineNum = 4;
            }
            else
            {
                note[noteNum - 1].transform.position = new Vector2(-38f, -2.89f);
                note[noteNum - 1].GetComponent<Note2>().lineNum = 3;
                note[noteNum].transform.position = new Vector2(-38f, -8.69f);
                note[noteNum].GetComponent<Note2>().lineNum = 4;
            }
        }
        currentNoteNum++;
        noteNum++;
        if (noteNum > 99)
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
        if (gp.playing)
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
                    explosion[k].transform.position = new Vector3(22.6f, 8.9f, 0f);
                    explosion[k].SetActive(true);
                    StartCoroutine(SetFalseBomb(k));
                    break;
                }
            }
        }
        else if (n == 2)
        {
            if (playerTr.position.y <= 5.8f && playerTr.position.y >= 0)
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
