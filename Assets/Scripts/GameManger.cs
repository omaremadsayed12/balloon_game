using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;
using RTLTMPro;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private GameObject balloon;
    [SerializeField]
    private Text text;
    private int number1,number2;
    [SerializeField]
    private GameObject stopper;
    [SerializeField]
    private Texture2D cursor;
    [SerializeField]
    private Sprite arabic,english;
    [SerializeField]
    private Font arabicFont, englishFont;
    private string originalText;
    private Color Correct, Wrong;
    [SerializeField]
    private Button languageButton;
    private bool isArabic = false;
    public void Awake()
    {
        newLevel();
        ColorUtility.TryParseHtmlString("#7DFF4C", out Correct);
        ColorUtility.TryParseHtmlString("#FF4B39", out Wrong);
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void countBalloon()
    {
        GameObject[] balloons= GameObject.FindGameObjectsWithTag("Balloon");
        text.text = number1 + " - " + number2 + " = " + balloons.Length;
        if (isArabic)
        {
            changeToArabic();
        }
        if (balloons.Length == number1 - number2)
        {
            text.color = Correct;
            Invoke(nameof(newLevel), 3f);
            stopper.SetActive(false);
            
        }
        else
        {
            text.color = Wrong;
            for (int i =0;i<number1- balloons.Length; i++)
            {
                spawnBalloon();
            }
        }
    }
    private void spawnBalloon()
    {
        GameObject newBalloon = Instantiate(balloon);
        newBalloon.transform.position = new Vector3(0,-5.0f, 0.0f);
    }
    private void newLevel()
    {
        text.color = Color.white;
        stopper.SetActive(true);
        number1 = Random.Range(3, 10);
        number2 = Random.Range(0, number1);
        text.text= text.text = number1 + " - " + number2 + " = ?";
        if (isArabic)
        {
            changeToArabic();
        }
        for (int i = 0; i < number1; i++)
        {
            spawnBalloon();
        }
    }
    public void switchLanguage()
    {
        if (!isArabic)
        {
            languageButton.image.sprite = english;
            isArabic=true;
            text.font = arabicFont;
            changeToArabic();
        }
        else if (isArabic)
        {
            languageButton.image.sprite = arabic;
            isArabic = false;
            text.font = englishFont;
            text.text = originalText;
        }
    }
    private void changeToArabic()
    {
        originalText = text.text;
        text.text = ArabicFixer.Fix(text.text.ToString(), false, true);
    }

}
