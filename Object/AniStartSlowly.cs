using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniStartSlowly : MonoBehaviour {
    public float time;
    Animator ani;
    // Use this for initialization
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
	void Start () {
        ani.Play("Wind", -1, time);
    }
    private void OnEnable()
    {
        ani.Play("Wind", -1, time);
    }
}
