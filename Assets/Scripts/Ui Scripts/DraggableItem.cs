    
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefabToInstantiate; // The prefab to instantiate when dropped
    public LayerMask targetLayer; // The layer of the target GameObject

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject draggedObject;
    private Vector3 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        //Physics2D.queriesHitTriggers = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //originalPosition = Camera.main.ScreenToWorldPoint(rectTransform.position);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedObject = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        draggedObject.GetComponent<LivingEntity>().enabled = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

        if (hit.collider != null)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            draggedObject.transform.position = hit.collider.transform.position;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

        if (hit.collider != null)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            draggedObject.transform.position = hit.collider.transform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

        if (hit.collider != null)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            draggedObject.transform.position = hit.collider.transform.position;
        }

        Instantiate(prefabToInstantiate, draggedObject.transform.position, Quaternion.identity);

        // Destroy the dragged object
        Destroy(draggedObject);
        draggedObject = null;

        // Reset the position of the UI item
        //rectTransform.position = originalPosition;
        canvasGroup.blocksRaycasts = true;
    }
}

    

