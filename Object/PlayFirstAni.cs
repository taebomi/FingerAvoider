using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFirstAni : MonoBehaviour
{
    Animator ani;
    private void Awake()
    {
        ani = GetComponent<Animator>();

    }
    public void OnEnable()
    {
        ani.Play("Normal", -1, 0);
    }
}
