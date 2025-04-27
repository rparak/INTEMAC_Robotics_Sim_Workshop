using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable_Object : MonoBehaviour
{
    Vector3 Last_Mouse_Position;

    // This event is sent to all scripts of the GameObject with Collider.
    private void OnMouseDown()
    {
        Last_Mouse_Position = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }

    // OnMouseDrag is called when the user has clicked on a Collider and is still holding down the mouse.
    private void OnMouseDrag()
    {
        Vector3 Delta = Input.mousePosition - Last_Mouse_Position;
        Vector3 Position = transform.position;
        transform.position = Camera.main.ScreenToWorldPoint(Delta);
    }
}
