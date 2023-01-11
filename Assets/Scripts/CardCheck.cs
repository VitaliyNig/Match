using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardCheck : MonoBehaviour
{
    private Color matchBlack;
    private Color matchYellow;
    private Color matchGreen;
    private Color matchBlue;
    private Color matchRed;
    private GameObject cardGO;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#1E1E1E", out matchBlack);
        ColorUtility.TryParseHtmlString("#F8D03F", out matchYellow);
        ColorUtility.TryParseHtmlString("#2AD5AE", out matchGreen);
        ColorUtility.TryParseHtmlString("#385CC7", out matchBlue);
        ColorUtility.TryParseHtmlString("#FF7175", out matchRed);
    }

    public void Check()
    {
        cardGO = this.gameObject;
        GameObject.Find("EventScripts").GetComponent<CardManager>().GetCardMatching(cardGO);
        string[] cardCode = cardGO.name.Split(' ');
        
        cardGO.GetComponentInChildren<TextMeshProUGUI>().text = cardCode[0].ToString();
        switch (cardCode[1])
        {
            case "R":
                cardGO.GetComponent<Image>().color = matchRed;
                break;
            case "G":
                cardGO.GetComponent<Image>().color = matchGreen;
                break;
            case "B":
                cardGO.GetComponent<Image>().color = matchBlue;
                break;
            case "Y":
                cardGO.GetComponent<Image>().color = matchYellow;
                break;
            default:
                cardGO.GetComponent<Image>().color = matchBlack;
                break;
        }
    }
}
