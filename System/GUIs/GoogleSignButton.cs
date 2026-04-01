using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleSignButton : MonoBehaviour
{
    Text text;
    Font f;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        f = text.font;
        StartCoroutine(CheckState());
    }
    IEnumerator CheckState()
    {
        while (true)
        {
            if (GPGSManager.instance.CheckLogin())
            {
                if (GameSystem.instance.isKorean)
                {
                    text.text = "구글 계정 로그아웃";
                    text.font = f;
                }
                else
                {
                    text.font = GameSystem.instance.font;
                    text.text = "Sign out of Google Account";
                } 
            }
            else
            {
                if (GameSystem.instance.isKorean)
                {
                    text.font = f;
                    text.text = "구글 계정 로그인";
                }
                else
                {
                    text.font = GameSystem.instance.font;
                    text.text = "Sign in to Google Account";
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
