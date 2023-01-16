using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private Transform cardGrid;
    [SerializeField]
    private TextMeshProUGUI numberStepUI;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private AudioClip audioClip;

    private const string modeKey = "GameMode";
    private Color matchBlack;

    private List<GameObject> tempCardGO = new List<GameObject>();
    private int numberStep;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#1E1E1E", out matchBlack);
    }

    public void GetCardMatching(GameObject cardGO)
    {
        Debug.Log(cardGO.name);

        tempCardGO.Add(cardGO);
        cardGO.GetComponent<Button>().interactable = false;
        if (tempCardGO.Count == 2)
        {
            CheckMatching();
            NumberStepsUpdate();
        }
    }

    private void CheckMatching()
    {
        switch (PlayerPrefs.GetInt(modeKey))
        {
            default:
            case 0:
                if (tempCardGO[0].name == tempCardGO[1].name)
                {
                    TrueMatching();
                }
                else
                {
                    FalseMatching();
                }
                break;
            case 1:
                if (tempCardGO[0].name.Split(' ')[2] == tempCardGO[1].name.Split(' ')[2])
                {
                    TrueMatching();
                }
                else
                {
                    FalseMatching();
                }
                break;
        }
    }

    private void TrueMatching()
    {
        Debug.Log("TrueMatching");

        foreach (var t in tempCardGO)
        {
            Destroy(t.GetComponent<Button>());
        }
        tempCardGO.Clear();
        StartCoroutine(CheckGameOver());
    }

    private void FalseMatching()
    {
        Debug.Log("FalseMatching");

        foreach (var b in cardGrid.GetComponentsInChildren<Button>())
        {
            b.interactable = false;
        }
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        UndoMatching();
    }

    private void UndoMatching()
    {
        Debug.Log("UndoMatching");

        foreach (var t in tempCardGO)
        {
            t.GetComponentInChildren<TextMeshProUGUI>().text = "?";
            t.GetComponent<Image>().color = matchBlack;
        }
        tempCardGO.Clear();
        
        AudioSource audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();

        foreach (var b in cardGrid.GetComponentsInChildren<Button>())
        {
            b.interactable = true;
        }
    }

    private void NumberStepsUpdate()
    {
        numberStep++;
        numberStepUI.text = numberStep.ToString();
    }

    IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(1);
        if (cardGrid.GetComponentsInChildren<Button>().Length == 0)
        {
            Debug.Log("GameOver");
            GameObject.Find("EventScripts").GetComponent<Timer>().StopTimer();
            gameOverUI.SetActive(true);
            foreach (var i in cardGrid.GetComponentsInChildren<Image>())
            {
                var tempColor = i.color;
                tempColor.a = 0.12f;
                i.color = tempColor;
            }
        }
    }
}
