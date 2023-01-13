using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private Transform cardGrid;

    private const string modeKey = "GameMode";
    private const string numberKey = "NumberCards";

    List<string> cardList = new List<string>();
    List<string> colorList = new List<string>() { "R", "G", "B", "Y" };
    List<int> arithmeticNumberList = new List<int>();

    private void Start()
    {
        Generator();
    }

    private void Generator()
    {
        //Generator
        int maxValue = 30;
        while (cardList.Count < PlayerPrefs.GetInt(numberKey) switch
        {
            int i when i == 0 => 10,
            int i when i == 1 => 20,
            int i when i == 2 => 30,
            int i when i == 3 => 40,
            int i when i == 4 => 50,
            _ => 10
        })
        {
            switch (PlayerPrefs.GetInt(modeKey))
            {
                default:
                case 0:
                    //Classic generator
                    string newClassicCard = Random.Range(0, 10) + " " + colorList[Random.Range(0, colorList.Count)];
                    if (!cardList.Contains(newClassicCard))
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            cardList.Add(newClassicCard);
                        }
                    }
                    break;
                case 1:
                    //Arithmetic generator
                    float firstNumber;
                    float secondNumber;
                    int numberResult = Random.Range(0, maxValue);
                    if (!arithmeticNumberList.Contains(numberResult))
                    {
                        arithmeticNumberList.Add(numberResult);
                        int colorID = Random.Range(0, colorList.Count);
                        for (int j = 0; j <= 1; j++)
                        {
                            string arithmeticTask = "";
                            switch (Random.Range(0, 4))
                            {
                                default:
                                case 0:
                                    firstNumber = Random.Range(-maxValue, maxValue);
                                    secondNumber = numberResult - firstNumber;
                                    arithmeticTask = firstNumber + "+" + (secondNumber >= 0 ? secondNumber : "(" + secondNumber + ")");
                                    break;
                                case 1:
                                    firstNumber = Random.Range(-numberResult, numberResult);
                                    secondNumber = numberResult + firstNumber;
                                    arithmeticTask = secondNumber + "-" + (firstNumber >= 0 ? firstNumber : "(" + firstNumber + ")");
                                    break;
                                case 2:
                                    while (true)
                                    {
                                        firstNumber = Random.Range(-maxValue, maxValue);
                                        if (numberResult % firstNumber == 0)
                                        {
                                            secondNumber = numberResult / firstNumber;
                                            arithmeticTask = firstNumber + "*" + (secondNumber >= 0 ? secondNumber : "(" + secondNumber + ")");
                                            break;
                                        }
                                    }
                                    break;
                                case 3:
                                    while (true)
                                    {
                                        firstNumber = Random.Range(-numberResult, numberResult);
                                        secondNumber = numberResult * firstNumber;
                                        if (secondNumber.ToString().Length <= 2 && secondNumber != 0)
                                        {
                                            arithmeticTask = secondNumber + "/" + (firstNumber >= 0 ? firstNumber : "(" + firstNumber + ")");
                                            break;
                                        }
                                    }
                                    break;
                            }
                            string newArithmeticCard = arithmeticTask + " " + colorList[colorID] + " " + numberResult;
                            cardList.Add(newArithmeticCard);
                        }
                    }
                    break;
            }
        }
        //Randomizer
        for (int i = 0; i < cardList.Count; i++)
        {
            int j = Random.Range(0, i + 1);
            var temp = cardList[j];
            cardList[j] = cardList[i];
            cardList[i] = temp;
        }
        //Creation gameObject
        foreach (var c in cardList)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardGrid);
            cardGO.name = c;
        }
    }
}
