using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {
    GameObject s;
    public AudioClip bgm;
	void Awake ()
    {
        s = Instantiate(Resources.Load("Story/" + "Story" + PlayerPrefs.GetFloat("Story"))) as GameObject;
        PlayerPrefs.SetFloat("Story", PlayerPrefs.GetFloat("Story") + 0.5f);
        GameSystem.instance.PlayBGM(bgm);
    }
    public void GoNext()
    {
        s.SendMessage("GoNext");
    }
}
