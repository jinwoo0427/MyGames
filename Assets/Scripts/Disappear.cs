using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    [SerializeField]
    private float time = 0.5f;


    void Start()
    {
        Destroy(gameObject, time);
    }

    void Update()
    {
        
    }
}
