using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CallMecanic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] GameObject _Canvas;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public RectTransform dropZone; // Asigna el contenedor de basura en el Inspector

    Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void OnDisable()
    {
        transform.position = _startPosition;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Mover el objeto según el puntero del mouse
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaurar la interactividad
      

        // Detectar si el rectángulo del objeto arrastrado se solapa con el rectángulo del contenedor
        if (IsOverlapping(rectTransform, dropZone))
        {
            Debug.Log("¡Soltado sobre la zona de basura!");
            // Opcional: Eliminar el objeto o realizar otra acción
            _Canvas.SetActive(false);
        }
        else
        {
            Debug.Log("No está sobre la zona de basura.");
        }
    }

    // Método para comprobar si dos RectTransform se solapan
    private bool IsOverlapping(RectTransform dragged, RectTransform target)
    {
        Rect draggedRect = GetWorldRect(dragged);
        Rect targetRect = GetWorldRect(target);

        return draggedRect.Overlaps(targetRect);
    }

    // Convierte un RectTransform a un Rect en coordenadas de mundo
    private Rect GetWorldRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        Vector2 size = new Vector2(
            corners[2].x - corners[0].x,
            corners[2].y - corners[0].y
        );
        return new Rect(corners[0], size);
    }
}
