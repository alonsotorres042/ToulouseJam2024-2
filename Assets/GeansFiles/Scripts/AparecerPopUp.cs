using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerPopUp : MonoBehaviour
{
    public GameObject PopUp;
    int SegundosEnVolverAparecer;
    float timepo;
    public void DefinirContador()
    {
        SegundosEnVolverAparecer=Random.Range(2, 12);
        timepo = SegundosEnVolverAparecer;
    }
    private void FixedUpdate()
    {
        if (timepo > 0)
        {
            timepo -= Time.deltaTime;
        }

        else if (timepo < 0)
        {
            PopUp.SetActive(true);
        }
    }
}
