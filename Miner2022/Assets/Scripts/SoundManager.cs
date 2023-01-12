using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   
    [Header("Slime")]
    [SerializeField] AudioClip slimeDeathSFX;
    [SerializeField] float slimeDeathVolume = 1f;

    [Header("PickAxe")]
    [SerializeField] AudioClip axeThrowSFX;
    [SerializeField] float axeThrowVolume = 1f;

    [Header("Platform")]
    [SerializeField] AudioClip platformCrumbleSFX;
    [SerializeField] float platformCrumbleVolume = 1f;

    [Header("Player")]
    [SerializeField] AudioClip playerJumpSFX;
    [SerializeField] float playerJumpVolume = 1f;
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] float playerDeathVolume = 1f;

    [Header("Coin")]
    [SerializeField] AudioClip coinCollectedSFX;
    [SerializeField] float coinCollectedVolume = 1f;

    [Header("Blob")]
    [SerializeField] AudioClip blobDeathSFX;
    [SerializeField] float blobDeathVolume = 1f;



    public void PlaySlimeDeathSFX()
    {
        PlayClipAtCamera(slimeDeathSFX, slimeDeathVolume);
    }

    public void PlayAxeThrowSFX()
    {
       PlayClipAtCamera(axeThrowSFX, axeThrowVolume);
    }

    public void PlayPlatformCrumbleSFX()
    {
        PlayClipAtCamera(platformCrumbleSFX, platformCrumbleVolume);
    }

    public void PlayPlayerJumpSFX()
    {
        PlayClipAtCamera(playerJumpSFX, playerJumpVolume);
    }

    public void PlayerPlayerDeathSFX()
    {
        PlayClipAtCamera(playerDeathSFX, playerDeathVolume);
    }

    public void PlayCoinCollectedSFX()
    {
        PlayClipAtCamera(coinCollectedSFX, coinCollectedVolume);
    }

    public void PlayBlobDeathSFX()
    {
        PlayClipAtCamera(blobDeathSFX, blobDeathVolume);
    }

    private void PlayClipAtCamera(AudioClip SFX, float volume)
    {
        AudioSource.PlayClipAtPoint(SFX, Camera.main.transform.position, volume);
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
