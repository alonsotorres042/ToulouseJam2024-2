using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambioDePantallas : MonoBehaviour
{
    public Image VentanaDeTransicion;
    public float DuracionTransicion;
    public int TiempoMostrarTitulo;
    [Header("Menu")]
    public GameObject _PantallaMenu;
    public GameObject _PantallaPerdiste;
    public GameObject _PantallaGanaste;

    [Header("Dias")]
    public List<GameObject> Dias; //0 = Lunes; 1= martes; 3= Miercoles; 4= jueves; 5=Viernes; 6=Sabado
    public List<Text> _TitulosDias;
    int day=0;

    public void PasaraDia()
    {
        VentanaDeTransicion.DOKill();
        if (day < Dias.Count)
        {
            if (day>=1)
            {
                Dias[day - 1].gameObject.SetActive(false);
            }
            Dias[day].gameObject.SetActive(true);
            
        }
        day++;
        CerrarTransicion();
    }
    public void LlamarAtransicion()
    {
        VentanaDeTransicion.gameObject.SetActive(true);
        _TitulosDias[day].gameObject.SetActive(true);
        _TitulosDias[day].DOColor(new Color(_TitulosDias[day].color.r, _TitulosDias[day].color.g, _TitulosDias[day].color.b, 1), DuracionTransicion);
        VentanaDeTransicion.DOColor(new Color(VentanaDeTransicion.color.r, VentanaDeTransicion.color.g, VentanaDeTransicion.color.b, 1), DuracionTransicion).OnComplete(MantenerScreen);
        void MantenerScreen()
        {
            StartCoroutine(TituloDelDia());
        }
    }
    public void CerrarTransicion()
    {
        VentanaDeTransicion.DOColor(new Color(VentanaDeTransicion.color.r, VentanaDeTransicion.color.g, VentanaDeTransicion.color.b, 0), DuracionTransicion).OnComplete(DesactivarPantalla);
        
        _TitulosDias[day-1].DOColor(new Color(_TitulosDias[day].color.r, _TitulosDias[day].color.g, _TitulosDias[day].color.b, 0), DuracionTransicion);
        void DesactivarPantalla()
        {
            _TitulosDias[day-1].gameObject.SetActive(false);
            VentanaDeTransicion.gameObject.SetActive(false);
        }
    }
    IEnumerator TituloDelDia()
    {
        yield return new WaitForSeconds (TiempoMostrarTitulo);
        PasaraDia();

    }
    
}
