using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoEscritura : MonoBehaviour
{
    AudioSource parlante;
    public List<AudioClip> SonidoTecla;
    int NumSonido;
    // Update is called once per frame
    private void Start()
    {
        parlante = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Random.Range(0,SonidoTecla.Count-1);
            parlante.PlayOneShot(SonidoTecla[NumSonido]);
        }
    }
}
