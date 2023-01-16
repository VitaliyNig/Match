using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private GameObject[] audioGO;

    private void Awake()
    {
        audioGO = GameObject.FindGameObjectsWithTag("Audio");
        if (audioGO.Length > 1)
        {
            for (int i = audioGO.Length - 1; i > 1; i--)
            {
                Destroy(audioGO[i]);
            }
        }
        DontDestroyOnLoad(audioGO[0]);
    }
}
