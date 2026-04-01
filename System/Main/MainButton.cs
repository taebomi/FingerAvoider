using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainButton : MonoBehaviour {
    public GameObject optionForm;
    public GameObject staticsForm;
    public GameObject ccForm;
    public GameObject cPForm;
    public GameObject newMessageForm;
    public GameObject CPB;
    public Slider BGMslider;
    public Slider SEslider;
    public Slider Sensivityslider;
    public Text sensivityT;
    public AudioClip bgm;
    public void Start()
    {
        sensivityT.text = Sensivityslider.value.ToString("N2");
        GameSystem.instance.PlayBGM(bgm);
        GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQBg");
        if (PlayerPrefs.GetFloat("Story") >= 2 && PlayerPrefs.GetInt("CompetitivePlayM") != 1)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().StopSpeed();
            newMessageForm.SetActive(true);
            CPB.SetActive(true);
        }
        if (PlayerPrefs.GetInt("CompetitivePlayM") == 1)
        {
            CPB.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameSystem.instance.Quit();
        }
    }
    public void Ouit()
    {
#if UNITY_ANDROID
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }
    public void PlayDodgeN()
    {
        GameSystem.instance.levelNum = 1;
        GameSystem.instance.LoadScene("Dodge");
    }
    public void PlayDodgeH()
    {
        GameSystem.instance.levelNum = 2;
        GameSystem.instance.LoadScene("Dodge");
    }
    public void PlayDodgeVH()
    {
        GameSystem.instance.levelNum = 3;
        GameSystem.instance.LoadScene("Dodge");
    }
    public void TurnOnCompeitivePlay()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().StopSpeed();
        cPForm.SetActive(true);
    }
    public void SelectStage()
    {
        GameSystem.instance.sceneDeapth = 2;
        GameSystem.instance.LoadScene("StageSelectScene");
    }
    public void TurnOnOption()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().StopSpeed();
        optionForm.SetActive(true);
    }
    public void TurnOnCC()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().StopSpeed();
        ccForm.SetActive(true);
    }
    public void TurnOnStatics()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().StopSpeed();
        staticsForm.SetActive(true);
    }
    public void ShowLeaderBoard()
    {
        GPGSManager.instance.ShowLeaderBoard();
    }
    public void SignInOut()
    {
        GPGSManager.instance.SignInOut();
    }
    public void ShowAchevement()
    {
        GPGSManager.instance.ShowAchevement();
    }
    public void ChangeBGMVolume()
    {
        GameSystem.instance.ChangeBGMVolume(BGMslider.value);
    }
    public void ChangeSEVolume()
    {
        GameSystem.instance.ChangeSEVolume(SEslider.value);
    }
    public void ChangeSensivity()
    {
        GameSystem.instance.ChangeSensivity(Sensivityslider.value);
        sensivityT.text = Sensivityslider.value.ToString("N2");
    }
    public void ResetSensitivity()
    {
        Sensivityslider.value = 1;
        GameSystem.instance.ChangeSensivity(1);
        sensivityT.text = "1.00";
    }
    public void ExitCPF()
    {
        PlayerPrefs.SetInt("CompetitivePlayM", 1);
        EventSystem.current.currentSelectedGameObject.gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().RefreshSpeed();
    }
    public void Exit()
    {
        EventSystem.current.currentSelectedGameObject.gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().RefreshSpeed();
    }
    public void GoMyPage()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=5296741763651498298");
    }
    public void ChangeLanguage()
    {
        GameSystem.instance.isKorean = !GameSystem.instance.isKorean;
        if (GameSystem.instance.isKorean)
            PlayerPrefs.SetInt("Language", 0);
        else
            PlayerPrefs.SetInt("Language", 1);
        GameSystem.instance.LoadScene("mainScene");
    }
}
