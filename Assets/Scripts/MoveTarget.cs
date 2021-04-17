using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveTarget : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(GetComponent<Rigidbody>().position).z;
        mOffset = GetComponent<Rigidbody>().position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        GetComponent<Rigidbody>().position = GetMouseAsWorldPoint() + mOffset;
    }
}