using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    private const string numberKey = "NumberCards";

    private int numberCard;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private Transform cardGrid;

    private GameObject firstCard;
    private GameObject secondCard;
    private Color matchBlack;

    [SerializeField]
    private TextMeshProUGUI numberStepUI;
    private int numberStep;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#1E1E1E", out matchBlack);
    }

    private void Start()
    {
        Generator();
    }

    private void Generator()
    {
        switch (PlayerPrefs.GetInt(numberKey))
        {
            case 0:
                numberCard = 10;
                break;
            case 1:
                numberCard = 20;
                break;
            case 2:
                numberCard = 30;
                break;
            case 3:
                numberCard = 40;
                break;
            case 4:
                numberCard = 50;
                break;
            default:
                numberCard = 50;
                break;
        }

        List<string> colorList = new List<string>() { "R", "G", "B", "Y" };
        List<string> cardList = new List<string>();

        while (cardList.Count < numberCard)
        {
            string newCard = Random.Range(0, 10) + " " + colorList[Random.Range(0, colorList.Count)];
            if (!cardList.Contains(newCard))
            {
                cardList.Add(newCard);
                cardList.Add(newCard);
            }
        }

        for (int i = 0; i < cardList.Count; i++)
        {
            int j = Random.Range(0, i + 1);
            var temp = cardList[j];
            cardList[j] = cardList[i];
            cardList[i] = temp;
        }

        foreach (var c in cardList)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardGrid);
            cardGO.name = c;
        }
    }

    public void GetCardMatching(GameObject cardGO)
    {
        if (firstCard == null)
        {
            firstCard = cardGO;
            firstCard.GetComponent<Button>().interactable = false;
            Debug.Log(firstCard.name);
        }
        else if (secondCard == null)
        {
            secondCard = cardGO;
            Debug.Log(secondCard.name);
            Matching();
            NumberStepUpdate();
        }
    }

    private void NumberStepUpdate()
    {
        numberStep++;
        numberStepUI.text = numberStep.ToString();
    }

    private void Matching()
    {
        if (firstCard.name == secondCard.name)
        {
            Debug.Log("MATCH");
            Destroy(firstCard.GetComponent<Button>());
            Destroy(secondCard.GetComponent<Button>());
            firstCard = null;
            secondCard = null;
        }
        else
        {
            foreach (var b in cardGrid.GetComponentsInChildren<Button>())
            {
                b.interactable = false;
            }
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        Remove();
    }

    private void Remove()
    {
        firstCard.GetComponentInChildren<TextMeshProUGUI>().text = "?";
        secondCard.GetComponentInChildren<TextMeshProUGUI>().text = "?";
        firstCard.GetComponent<Image>().color = matchBlack;
        secondCard.GetComponent<Image>().color = matchBlack;
        firstCard = null;
        secondCard = null;
        foreach (var b in cardGrid.GetComponentsInChildren<Button>())
        {
            b.interactable = true;
        }
    }
}
