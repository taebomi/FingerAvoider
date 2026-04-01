using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour {
    void Disable()
    {
        gameObject.SetActive(false);
    }
}
