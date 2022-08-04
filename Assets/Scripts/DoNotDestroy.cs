using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public void OnEnable()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
