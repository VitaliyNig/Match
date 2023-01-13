using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleManager : MonoBehaviour
{
    private const string modeKey = "GameMode";
    private const string numberKey = "NumberCards";
    private Color matchBlack;
    private Color matchWhite;
    private Toggle[] toggles;
    private string tagGO;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#1E1E1E", out matchBlack);
        ColorUtility.TryParseHtmlString("#FFFFFF", out matchWhite);
        
        if (!PlayerPrefs.HasKey(modeKey))
        {
            PlayerPrefs.SetInt(modeKey, 0);
        }
        if (!PlayerPrefs.HasKey(numberKey))
        {
            PlayerPrefs.SetInt(numberKey, 0);
        }
    }

    private void Start()
    {
        GameObject thisGO = this.gameObject;
        toggles = thisGO.GetComponentsInChildren<Toggle>();
        tagGO = thisGO.tag;
        toggles[(PlayerPrefs.GetInt(tagGO == "GameMode" ? modeKey : numberKey))].isOn = true;
    }

    public void SelectedToggle()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                PlayerPrefs.SetInt(tagGO == "GameMode" ? modeKey : numberKey, i);
                toggles[i].GetComponentInChildren<TextMeshProUGUI>().color = matchWhite;
            }
            else
            {
                toggles[i].GetComponentInChildren<TextMeshProUGUI>().color = matchBlack;
            }
        }
    }
}