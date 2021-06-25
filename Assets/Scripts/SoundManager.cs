using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip ui;   
    public AudioClip razer;   
    public AudioClip coin;   
    AudioSource myAudio; 
    public static SoundManager instance; 

    void Awake()  
    {
        if (SoundManager.instance == null)  
            SoundManager.instance = this;  
    }
    void Start()
    {
        myAudio = GetComponent<AudioSource>();  
    }

    public void UiSound()
    {
        myAudio.PlayOneShot(ui);
    }
    public void RazerSound()
    {
        myAudio.PlayOneShot(razer);
    } 
    public void CoinSound()
    {
        myAudio.PlayOneShot(coin);
    }

    void Update()
    {

    }

}
