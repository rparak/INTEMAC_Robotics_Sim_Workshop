using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;

    private Camera main_cam;

    private void Awake()
    {
        main_cam = Camera.main;
        if (main_cam == null)
        {
            Debug.LogError("Main Camera not found. Please tag your camera as 'MainCamera'.", this);
        }
    }

    private void OnMouseDown()
    {
        if (main_cam == null) return;

        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 objectScreenPosition = main_cam.WorldToScreenPoint(transform.position);
        offset = objectScreenPosition - mouseScreenPosition;
    }

    private void OnMouseDrag()
    {
        if (main_cam == null) return;

        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 newScreenPosition = mouseScreenPosition + offset;
        Vector3 newWorldPosition = main_cam.ScreenToWorldPoint(new Vector3(newScreenPosition.x, newScreenPosition.y, main_cam.WorldToScreenPoint(transform.position).z));

        transform.position = newWorldPosition;
    }
}
