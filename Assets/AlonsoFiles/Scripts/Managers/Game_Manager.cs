using System.Collections;
using UnityEngine;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public static bool EnHorarioDeSalida = false;
    [SerializeField] float limitTime = 480f; // Tiempo total a alcanzar (en segundos)
    [SerializeField] float timeToReachLimit = 120f; // Tiempo en el que se alcanzará el límite
    [SerializeField] TextMeshProUGUI timer; // Referencia al componente TextMeshProUGUI

    IEnumerator MyTimerRef;

    void Start()
    {
        MyTimerRef = MyTimer(limitTime, timeToReachLimit);
        StopCoroutine(MyTimerRef);
        StartCoroutine(MyTimerRef);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StopCoroutine(MyTimerRef);
            StartCoroutine(MyTimerRef);
        }
    }

    public IEnumerator MyTimer(float limitTime, float timeToReachLimit)
    {
        float currentValue = 780;
        float incrementPerSecond = limitTime / timeToReachLimit; // Incremento por segundo

        while (currentValue < limitTime)
        {
            currentValue += incrementPerSecond * Time.deltaTime;
            currentValue = Mathf.Min(currentValue, limitTime);

            int minutes = Mathf.FloorToInt(currentValue / 60);
            int seconds = Mathf.FloorToInt(currentValue % 60);

            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return null;
        }
        EnHorarioDeSalida = true;
        Debug.Log("Timer reached the limit!");
    }
}
