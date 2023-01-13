using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerUI;
    private float timer;
    public bool activeTimer = true;

    private void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        activeTimer = false;
    }

    private IEnumerator UpdateTimer()
    {
        timer += Time.deltaTime;
        timerUI.text = TimeSpan.FromSeconds(timer).ToString("mm':'ss");
        yield return null;
        if (activeTimer == true)
        {
            StartCoroutine(UpdateTimer());
        }
    }
}
