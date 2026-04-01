using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Statics : MonoBehaviour {
    public Text tPlayCount;
    public Text tDeathCount;
    public Text tEarnGold;
    // Use this for initialization
    void Start () {
        tPlayCount.text = PlayerPrefs.GetInt("PlayTime").ToString();
        tDeathCount.text = PlayerPrefs.GetInt("DeadNumber").ToString();
        tEarnGold.text = PlayerPrefs.GetInt("EarnGold").ToString();
    }
}
