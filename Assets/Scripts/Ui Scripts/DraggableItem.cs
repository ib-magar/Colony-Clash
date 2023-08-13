
using UnityEngine;
using UnityEngine.EventSystems;
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
    private GameplayUiScript _uiScript;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        _uiScript=GameObject.FindObjectOfType<GameplayUiScript>();  
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
            if(draggedObject.TryGetComponent<PolygonCollider2D>(out PolygonCollider2D c)) { c.enabled = false; }
            _isDragged = true;
            //draggedObject.GetComponent<LivingEntity>().enabled = false;
            if (draggedObject.TryGetComponent<LivingEntity>(out LivingEntity l)) l.enabled = false;
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
    GameObject _currentBlock=null;
    [Range(0,1)] public float _targetedAlpha = .01f;
    [Range(0, 1)] public float _originalAlpha = 0f;
    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragged)
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);
            if (hit.collider != null)
            {

                Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggedObject.transform.position = hit.transform.transform.position;
                if(_currentBlock!=hit.transform.gameObject)
                {
                    if(_currentBlock == null)
                    {
                        _currentBlock = hit.transform.gameObject;
                    Color c = _currentBlock.GetComponent<SpriteRenderer>().color;
                    c.a = _targetedAlpha;
                    _currentBlock.GetComponent<SpriteRenderer>().color = c;
                    }
                    else
                    {
                        Color o = _currentBlock.GetComponent<SpriteRenderer>().color;
                        o.a = _originalAlpha;
                        _currentBlock.GetComponent<SpriteRenderer>().color = o;

                        _currentBlock = hit.transform.gameObject;
                        Color c = _currentBlock.GetComponent<SpriteRenderer>().color;
                        c.a = _targetedAlpha;
                        _currentBlock.GetComponent<SpriteRenderer>().color = c;
                    }
                }
               
            }
        }
    }
    [Header("Is not crafts")]
    public bool _isNotCraft=true;
    public bool isWorkerAnt = false;
    [Header("sounds")]
    public AudioClip _dropClip;
    [Header("info")]
    public InfoObject _outofCapacityPrefab;
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
            if(isWorkerAnt)
            {
                if(!WorkPlaceCapacity.Instance.isCapacityFull())
                {
                    checkAndGenerate();
                }
                else
                {
                    //generate capacity is full.
                  _uiScript.OutOfCapacityInfo();
                }

            }
            else
            {
                checkAndGenerate();
            }


            Color c = _currentBlock.GetComponent<SpriteRenderer>().color;
            c.a = _originalAlpha;
            _currentBlock.GetComponent<SpriteRenderer>().color = c;
         }
        Color o = _currentBlock.GetComponent<SpriteRenderer>().color;
        o.a = _originalAlpha;
        _currentBlock.GetComponent<SpriteRenderer>().color = o;


        // Destroy the dragged object
        Destroy(draggedObject);
        draggedObject = null;
        canvasGroup.blocksRaycasts = true;
    }
    void checkAndGenerate()
    {
        if (EconomySystem.Instance.buy(Price))
        {
            SoundManager.Instance.PlaySoundEffect(_dropClip, true, .4f);
            GameObject g = Instantiate(prefabToInstantiate, draggedObject.transform.position, Quaternion.identity);
            if (g.TryGetComponent<PolygonCollider2D>(out PolygonCollider2D a)) { a.enabled = true; }
            if (_isNotCraft && !isWorkerAnt)
            {
                hit.collider.gameObject.layer = default;
            }
            if (_isNotCraft)
                g.transform.parent = hit.collider.transform;


            if (isWorkerAnt) WorkPlaceCapacity.Instance.UpdateWorkerAntCapacity();

        }
        else Debug.Log("not planted well");
    }
}

    

