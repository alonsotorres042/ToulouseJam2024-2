using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] float levelLastingTime;
    [SerializeField] TMPro.TextMeshProUGUI timer;

    IEnumerator MyTimerRef;

    // Start is called before the first frame update
    void Start()
    {
        MyTimerRef = MyTimer(levelLastingTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StopCoroutine(MyTimerRef);
            StartCoroutine(MyTimerRef);
        }
    }
    public IEnumerator MyTimer(float limitTime)
    {
        float time = 0;
        while (time < limitTime)
        {
            time += Time.deltaTime;

            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return null;
        }

        Debug.Log("FinishedTime");
        StopCoroutine(MyTimerRef);
    }
}