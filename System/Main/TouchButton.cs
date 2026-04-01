using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour {
    GameObject manager;
    private void Start()
    {
        manager = GameObject.Find("ButtonManager");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.name == "Home")
            {
            }
            else if(transform.name == "Leaderboard")
            {
                manager.GetComponent<MainButton>().ShowLeaderBoard();
            }
            else if(transform.name == "Option")
            {
                manager.GetComponent<MainButton>().TurnOnOption();
            }
            else if (transform.name == "Report")
            {
                manager.GetComponent<MainButton>().TurnOnStatics();
            }
            else if (transform.name == "Shop")
            {
            }
            else if (transform.name == "Start")
            {
                manager.GetComponent<MainButton>().SelectStage();
            }
            else if (transform.name == "Achevement")
            {
                manager.GetComponent<MainButton>().ShowAchevement();
            }
            else if (transform.name == "CC")
            {
                manager.GetComponent<MainButton>().TurnOnCC();
            }
            else if(transform.name == "CompetitivePlay")
            {
                manager.GetComponent<MainButton>().TurnOnCompeitivePlay();
            }
        }
    }
}
