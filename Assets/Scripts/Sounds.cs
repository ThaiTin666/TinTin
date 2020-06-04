using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip SwordvsBomb, PlayerTakeGem;
    public AudioClip BombExplosion;
    public AudioSource adisrc;
    // Use this for initialization
    void Start()
    {
        BombExplosion = Resources.Load<AudioClip>("BombExplosion");
        SwordvsBomb = Resources.Load<AudioClip>("SwordvsBomb");
        PlayerTakeGem = Resources.Load<AudioClip>("OldSchool16");
        adisrc = GetComponent<AudioSource>();

    }

    public void Playsound(string clip)
    {
        switch (clip)
        {
            case "SwordvsBomb":
                adisrc.clip = SwordvsBomb;
                adisrc.PlayOneShot(SwordvsBomb, 0.18f);
                break;
            case "PlayerTakeGem":
                adisrc.clip = PlayerTakeGem;
                adisrc.PlayOneShot(PlayerTakeGem, 0.15f);
                break;
            case "BombExplosion":
                adisrc.clip = BombExplosion;
                adisrc.PlayOneShot(BombExplosion, 0.6f);
                break;
        }
    }
}