    
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Price")]
    public int Price;


    [Header("Instantiation")]
    public GameObject prefabToInstantiate;
    public LayerMask targetLayer;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject draggedObject;


    private bool _isDragged = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
  

    public void OnPointerDown(PointerEventData eventData)
    {
        //originalPosition = Camera.main.ScreenToWorldPoint(rectTransform.position);

    }
    private Ray ray;
    private RaycastHit2D hit;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (EconomySystem.Instance.checkPrice(Price))
        {

            draggedObject = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
            _isDragged = true;
            draggedObject.GetComponent<LivingEntity>().enabled = false;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;


             ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

            if (hit.collider != null)
            {
                Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggedObject.transform.position = hit.collider.transform.position;
            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragged)
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

            if (hit.collider != null)
            {
                Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggedObject.transform.position = hit.collider.transform.position;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragged = false;
        canvasGroup.alpha = 1f;

         ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

        if (hit.collider != null)
        {
            //Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //draggedObject.transform.position = hit.collider.transform.position;
            if (EconomySystem.Instance.buy(Price))
                Instantiate(prefabToInstantiate, draggedObject.transform.position, Quaternion.identity);
            else Debug.Log("not planted well");
        }

        // Destroy the dragged object
        Destroy(draggedObject);
        draggedObject = null;

        canvasGroup.blocksRaycasts = true;
    }
}

    

