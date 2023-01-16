using UnityEngine;

public class AudioPlaySFX : MonoBehaviour
{
    public void PlaySFX(AudioClip sfx)
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        audioSource.clip = sfx;
        audioSource.Play();
    }
}
