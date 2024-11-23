using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradasSalidasAds : MonoBehaviour
{
    [Header("Componentes")]
    RectTransform _TransformCanvas;
    public List<RectTransform> _PosObjetivos ;
    int _NumPosObjetivos ;

    [Header ("SuavidadMov")]
    public float SuavidadDeMov;
    public float SuavidadDeScala;

    bool Inicio=false;

    private void Start()
    {
        _TransformCanvas = GetComponent<RectTransform>();
        RegresarloAsuEstadoInicial();
        if (!Inicio)
        {
            AparecerPopapAD(); //hace aparecer y mover el ad
            Inicio = true;
        }
           
    }
    private void OnEnable()
    {
        if (Inicio)
        {
            RegresarloAsuEstadoInicial();
            AparecerPopapAD(); //hace aparecer y mover el ad
        }
    }

    public void AparecerPopapAD() 
    {
        _NumPosObjetivos ++; //En que punto se encuentra en el primero segundo... etc 0=1 1=2 se va sumando para ir al siguiente punto
        
        
        if (_NumPosObjetivos < _PosObjetivos.Count && _PosObjetivos[_NumPosObjetivos]) // verifica que el numero no salga del array
        {
            this._TransformCanvas.DOScale(Vector3.one, SuavidadDeScala); // escala a su tamaño verdadero
            this._TransformCanvas.DOMove(_PosObjetivos[_NumPosObjetivos].position, SuavidadDeMov).OnComplete(VerificarSiHayMasPuntos); //mueve al punto mencionado
        }
        
    } //Aparecer PopUp o Mover Al Siguiente Punto
    public void CerrarPopApAD()
    {
        this.gameObject.transform.DOKill();
        this._TransformCanvas.DOKill();
        _NumPosObjetivos = 0;
        this._TransformCanvas.DOScale(Vector3.zero, SuavidadDeScala);
        this._TransformCanvas.DOMove(_PosObjetivos[0].position, SuavidadDeMov).OnComplete(DesaparecerEsteObjeto);
    } //Cierra El PopUp
    void DesaparecerEsteObjeto()
    {
        //this.gameObject.transform.parent.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    } //Una vez esta Cerrando Lo Desactiva ya se inicia al cerrar el PopUp
    void RegresarloAsuEstadoInicial()
    {
        _TransformCanvas.position = _PosObjetivos[0].position; //Siempre empieza en el punto Inicial
        _TransformCanvas.localScale = Vector3.zero; // con una escala de cero
    } // Lo regresa A su estado Inicial ya se inicia al cerrar o Desactivar el PopUp
    void VerificarSiHayMasPuntos()
    {
        if(_NumPosObjetivos < _PosObjetivos.Count)
        {
            AparecerPopapAD();
        }
    } //Verifica si Hay mas puntos por recorrer si es así llama a "AparacerPopapAD()"
    private void OnDisable()
    {
        RegresarloAsuEstadoInicial();
    }
}
