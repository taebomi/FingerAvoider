using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChild : MonoBehaviour {
    public GameObject[] wantToDisable;
    public void OnEnable()
    {
        for (int i = 0; i < wantToDisable.Length; i++)
            wantToDisable[i].SetActive(true);
    }
    public void OnDisable()
    {
        for (int i = 0; i < wantToDisable.Length; i++)
        {
            wantToDisable[i].SetActive(false);
        }
        
    }
}
