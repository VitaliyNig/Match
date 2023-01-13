using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerUI;
    private float timer;

    private void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        StopCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        timer += Time.deltaTime;
        timerUI.text = TimeSpan.FromSeconds(timer).ToString("mm':'ss");
        yield return null;
        StartCoroutine(UpdateTimer());
    }
}
