using System.Collections;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private bool isRotatable = true;

    public float moveDownSpeed = 1f;
    public Sprite shapeSprite;

    public void MoveLeftFNC()
    {
        transform.Translate(Vector3.left, Space.World);
    }
    public void MoveRightFNC()
    {
        transform.Translate(Vector3.right,Space.World);
    }
    public void MoveDownFNC()
    {
        transform.Translate(Vector3.down * moveDownSpeed, Space.World);
    }
    public void MoveUpFNC()
    {
        transform.Translate(Vector3.up, Space.World);
    }
    public void RotateRightFNC()
    {
        if (isRotatable)
        {
            transform.Rotate(0, 0, 90);
        }
    }
    public void RotateLeftFNC()
    {
        if (isRotatable)
        {
            transform.Rotate(0, 0, -90);
        }
    }
    
    IEnumerator HareketRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveSpeed);
            MoveDownFNC();
        }
    }
}
