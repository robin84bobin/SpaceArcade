using UnityEngine;

public class ScrollablePart: MonoBehaviour
{
    public Vector3 bounds { get; private set; }
        
    public void Init()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        bounds = col.bounds.size;
    }

    public float GetLength()
    {
        var l = bounds != null ? bounds.x : 15f;
        return l;
    }

    Vector3 _moveVector;
    public void Move(float speed, DIRECTION dir)
    {
        _moveVector = dir.Equals(DIRECTION.HORIZONTAL) ? new Vector3(speed, 0f, 0f) : new Vector3(0f, speed, 0f);
        transform.position = transform.position + _moveVector;
    }

}