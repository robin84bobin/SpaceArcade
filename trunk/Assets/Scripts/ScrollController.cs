using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DIRECTION
{
    HORIZONTAL,
    VERTICAL
}

public class ScrollController : MonoBehaviour {

    private Vector3 removeBound = new Vector3(-15, 0f, 0f);
    public DIRECTION direction = DIRECTION.HORIZONTAL;
    public float speed = -0.01f;
    public int partCnt = 3;
    public float partSize;
    public List<ScrollablePart> parts;
    private List<ScrollablePart> currentParts;

    void Start()
    {
        currentParts = new List<ScrollablePart>();
        CreateParts();
    }

    
    private void CreateParts()
    {
        for (int i = 0; i < partCnt; i++)
        {
            var part = NextPart();
            CreatePart(part);
        }
    }

    private void CreatePart(ScrollablePart part)
    {
        GameObject partGO = GameObject.Instantiate(part.gameObject);
        PartController newPart = partGO.GetComponent<PartController>();
        newPart.Init();

        float nextPartPos;
        if (currentParts.Count > 0)
        {
            var lastPart = currentParts[currentParts.Count - 1];
            nextPartPos = direction.Equals(DIRECTION.HORIZONTAL) ?
                lastPart.transform.position.x + newPart.bounds.x :
                lastPart.transform.position.y + newPart.bounds.y;
        }
        else
        {
            nextPartPos = direction.Equals(DIRECTION.HORIZONTAL) ?
            transform.position.x :
            transform.position.y;
        }

        partGO.transform.position = direction.Equals(DIRECTION.HORIZONTAL) ?
            new Vector3(nextPartPos, transform.position.y, transform.position.z):
            new Vector3(transform.position.x, nextPartPos, transform.position.z);
        partGO.transform.rotation = Quaternion.identity;
        currentParts.Add(newPart);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < currentParts.Count; i++)
        {
            var part = currentParts[i];
            part.Move(Time.deltaTime / speed, direction);
            CheckRemovePart(part);
        }

        if (currentParts.Count < partCnt)
        {
            AddPart();
        }
    }

    float pos;
    float boundPos;
    private void CheckRemovePart(ScrollablePart part)
    {
        pos = direction.Equals(DIRECTION.HORIZONTAL) ? part.transform.position.x : part.transform.position.y;
        boundPos = direction.Equals(DIRECTION.VERTICAL) ? removeBound.x : removeBound.y;
        if (pos < boundPos)
        {
            currentParts.Remove(part);
            Destroy(part.gameObject);
        }
    }

    private void AddPart()
    {
        var part = NextPart();
        CreatePart(part);
    }

    int _lastPartIndex = -1;
    ScrollablePart NextPart()
    {
        _lastPartIndex = _lastPartIndex++ < parts.Count - 1 ? _lastPartIndex : 0;
        return parts[_lastPartIndex];
    }
}
