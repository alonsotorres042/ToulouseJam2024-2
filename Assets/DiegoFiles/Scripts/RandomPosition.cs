using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    RectTransform rectTransform;
    int _randomX;
    int _posRanX;
    int _randomY;


    
    // Start is called before the first frame update
    void Start()
    {
       
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        _randomY = Random.Range(-250, 250);
        _randomX = Random.Range(1, 3);
        switch (_randomX)
        {
            case 1:
                rectTransform.anchoredPosition = new Vector3(506f, _randomY, 0);
                break;
            case 2:
                rectTransform.anchoredPosition = new Vector3(-506f, _randomY, 0);
                break;

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
