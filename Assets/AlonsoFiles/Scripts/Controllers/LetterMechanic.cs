using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LetterMechanic : MonoBehaviour
{
    [SerializeField] CambioDePantallas cambioDePantallas;
    [SerializeField] TMP_InputField playerInput;
    [SerializeField] TMP_Text targetText;
    [SerializeField] TMP_Text helpingText;
    [SerializeField] Slider depresometer;
    [SerializeField] float correctAnswerDiscount;
    [SerializeField] string[] contentOptions;

    //SHOW ERRORS TEXT
    [SerializeField] TextMeshProUGUI _errors;

    Letter targetLetter = new Letter();
    Tween myTween;

    public CambioDePantallas ManagerScreens;

    [Header("Cartas para Pasar")]
    public Text CartasFaltantes;
    public int CantCartasMaximas;
    int _CantCartas;
    
    AudioSource _audioSource;
    public List<AudioClip> _audioClipList;
    int NumeroAudio;

    void Start()
    {
        _audioSource=GetComponent<AudioSource>();
        _errors.text = "ERRORES:\n";
        RandomizeTargetContent();
        targetText.text = targetLetter.letterContent;
        CartasFaltantes.text = "0/" + CantCartasMaximas;
    }

    void Update()
    {
        playerInput.interactable = (playerInput.text == targetLetter.letterContent) ? false : true;
        if(playerInput.interactable)
        {

            ColorBlock coloress = new ColorBlock();
            coloress.disabledColor = Color.blue;
            playerInput.colors =coloress;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            float currentError = EvaluateErrorPercentage();
            if (currentError == 0)
            {
                _CantCartas++;
                CartasFaltantes.text = _CantCartas + "/" + CantCartasMaximas;
                ManagerScreens.PresionarEnter();
                playerInput.text = "";
                RandomizeTargetContent();
                targetText.text = targetLetter.letterContent;

                myTween.Kill();
                myTween = DOTween.To(() => depresometer.value, x => depresometer.value = x, depresometer.value - correctAnswerDiscount, 1);

                Debug.Log("Success");
                if (_CantCartas>= CantCartasMaximas)
                {
                    ManagerScreens.LlamarAtransicion();
                    
                    Game_Manager.EnHorarioDeSalida = false;

                }
            }
            else
            {
                NumeroAudio = Random.Range(0, _audioClipList.Count - 1);
                _audioSource.PlayOneShot(_audioClipList[NumeroAudio]);
                ShowErrors();
                myTween.Kill();
                myTween = DOTween.To(() => depresometer.value, x => depresometer.value = x, depresometer.value + currentError, 1).SetEase(Ease.Linear);
                if (depresometer.value+currentError >= depresometer.maxValue)
                {
                    cambioDePantallas.EvaluateWin(false);
                }
                Debug.Log("Wrong answer, check for mistakes");
            }
        }
    }
    private void FixedUpdate()
    {
        depresometer.value += Time.deltaTime / 10;
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
        else if (targetLetter.letterContent.Length <= currentInput.Length)
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
        playerInput.text = LimitStringLength(playerInput.text, targetLetter.letterContent.Length);
        return errorPercent;
    }
    public string LimitStringLength(string input, int maxLength)
    {
        if (input.Length > maxLength)
        {
            return input.Substring(0, maxLength); // Toma solo los primeros maxLength caracteres
        }
        return input;
    }
    void ShowErrors()
    {
        string currentInput = playerInput.text;
        _errors.text = "ERRORES:\n";
            for (int i = 0; i < targetLetter.letterContent.Length; i++)
            {
            if( i >= currentInput.Length - 1)
            {
                _errors.text += $"  => {targetLetter.letterContent[i]}\n";
                continue;
            }

             if (currentInput[i] != targetLetter.letterContent[i])
            {
                _errors.text += $"{currentInput[i]} => {targetLetter.letterContent[i]}\n";
                }


            }
        
    }

}