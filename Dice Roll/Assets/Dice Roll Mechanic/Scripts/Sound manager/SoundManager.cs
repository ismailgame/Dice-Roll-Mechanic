using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i;

    private void Awake()
    {
        //There should be one sound manager
        if (i != null && i != this)
            Destroy(gameObject);

        i = this;
    }

    //Audio Source Prefab
    [field: SerializeField] public GameObject audioSourcePrefab { get; private set; }

    [Header("Audio Clips")]
    [SerializeField] AudioClip[] diceHitDiceSounds;
    public AudioClip[] diceHitGorundSounds;
    public AudioClip rollButtonSound;

    public AudioClip ChosedClipDice()
    {
        return diceHitDiceSounds[Random.Range(0, diceHitDiceSounds.Length)];
    }

    public AudioClip ChosedClipGround()
    {
        return diceHitGorundSounds[Random.Range(0, diceHitGorundSounds.Length)];
    }

}
