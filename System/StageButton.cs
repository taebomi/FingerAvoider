using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour {
    int num;
    int n;
    int tempN;
    GameObject G;
    GameObject G2;
    public Sprite[] s;
    public void Start()
    {
        G = transform.GetChild(1).gameObject;
        G2 = transform.GetChild(2).gameObject;
        num = int.Parse(GetComponentInChildren<Text>().text);
        n = int.Parse(transform.parent.name);
        if (PlayerPrefs.GetInt("ClearNumber") - (n-1)*25 < num-1)
        {
            GetComponent<Button>().interactable = false;
        }
        if(PlayerPrefs.GetInt("ClearNumber"+ n + "-" + num) > 5){
            G.SetActive(true);
        }
        tempN = PlayerPrefs.GetInt("ClearMedal" + n + "-" + num);
        if (tempN < 1)
        {
            // 노메달
        }
        else if (tempN < 2)
        {
            G2.SetActive(true);
        }
        else if (tempN < 3)
        {
            G2.SetActive(true);
            G2.GetComponent<SpriteRenderer>().sprite = s[0];
        }
        else if (tempN < 4)
        {
            G2.SetActive(true);
            G2.GetComponent<SpriteRenderer>().sprite = s[1];
        }
        else if (tempN < 5)
        {
            G2.SetActive(true);
            G2.GetComponent<SpriteRenderer>().sprite = s[2];
        }
    }
    public void StageSelectPress()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<StageSystem>().StageSelect(n,num);
    }
}
