using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LetterMechanic : MonoBehaviour
{
    [SerializeField] TMP_InputField playerInput;
    [SerializeField] TMP_Text targetText;
    [SerializeField] TMP_Text helpingText;
    [SerializeField] Slider depresometer;
    [SerializeField] float correctAnswerDiscount;
    [SerializeField] string[] contentOptions;

    Letter targetLetter = new Letter();

    void Start()
    {
        RandomizeTargetContent();
        targetText.text = targetLetter.letterContent;
    }

    void Update()
    {
        playerInput.interactable = (playerInput.text == targetLetter.letterContent) ? false : true;

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            float currentError = EvaluateErrorPercentage();
            if (currentError == 0)
            {
                playerInput.text = "";
                RandomizeTargetContent();
                targetText.text = targetLetter.letterContent;
                depresometer.value -= correctAnswerDiscount;
                Debug.Log("Success");
            }
            else
            {
                depresometer.value += currentError;
                Debug.Log("Wrong answer, check for mistakes");
            }
        }
    }
    public void RandomizeTargetContent()
    {
        int rnd = Random.Range(0, contentOptions.Length);

        if (targetLetter.letterContent == contentOptions[rnd])
        {
            RandomizeTargetContent();
        }
        else
        {
            targetLetter.letterContent = contentOptions[rnd];
        }
    }
    public void CompareTexts()
    {
        if (playerInput.text == targetLetter.letterContent)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Fail");
        }
    }
    public float EvaluateErrorPercentage()
    {
        string currentInput = playerInput.text;
        float errorPercent = 0;

        if (targetLetter.letterContent.Length > currentInput.Length)
        {
            for (int i = 0; i < targetLetter.letterContent.Length; i++)
            {
                try
                {
                    if (currentInput[i] != targetLetter.letterContent[i])
                    {
                        errorPercent++;
                        Debug.Log(currentInput[i]);
                    }
                }
                catch
                {
                    errorPercent++;
                }
            }
            errorPercent = (errorPercent / targetLetter.letterContent.Length) * 100;
        }
        else if(targetLetter.letterContent.Length <= currentInput.Length)
        {
            for (int i = 0; i < currentInput.Length; i++)
            {
                try
                {
                    if (currentInput[i] != targetLetter.letterContent[i])
                    {
                        errorPercent++;
                        Debug.Log(currentInput[i]);
                    }
                }
                catch
                {
                    errorPercent++;
                }
            }
            errorPercent = (errorPercent / currentInput.Length) * 100;
        }
        return errorPercent;
    }
}