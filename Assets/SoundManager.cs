using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S;
    private AudioSource audio;

    public AudioSource background;
    public AudioClip Ketchup;
    public AudioClip Lever;
    public AudioClip oil;
    public AudioClip plate;
    public AudioClip sizzle;
    public AudioClip soysauce;
    public AudioClip finished;
    public AudioClip water;

    private void Awake()
    {
        S = this;
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        background.loop = true;
        background.Play();

    }

    public void MakeKetchupSound()
    {
        audio.PlayOneShot(Ketchup);
    }

    public void MakeLeverSound()
    {
        audio.PlayOneShot(Lever);
    }

    public void MakeOilSound()
    {
        audio.PlayOneShot(oil);
    }

    public void MakePlateSound()
    {
        audio.PlayOneShot(plate);
    }

    public void MakeSizzleSound()
    {
        audio.PlayOneShot(sizzle);
    }

    public void MakeSoySound()
    {
        audio.PlayOneShot(soysauce);
    }

    public void MakeFinishedSound()
    {
        audio.PlayOneShot(finished);
    }

    public void MakeWaterSound()
    {
        audio.PlayOneShot(water);
    }
}
