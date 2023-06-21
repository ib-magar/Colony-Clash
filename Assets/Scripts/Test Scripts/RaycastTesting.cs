
  using UnityEngine;

public class RaycastTesting : MonoBehaviour
{
    public LayerMask targetLayer;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    Vector2 pos;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

            if (hit.collider != null)
            {
                isDragging = true;
                pos = hit.collider.transform.position;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            
            //transform.position = pos;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

            if (hit.collider != null)
            {
                Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = hit.collider.transform.position;
            }
        }
    }
}

    

