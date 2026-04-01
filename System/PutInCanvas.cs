using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInCanvas : MonoBehaviour {
    private void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("StageT").transform);
        transform.localScale= new Vector2(18f,18f);
    }
}
