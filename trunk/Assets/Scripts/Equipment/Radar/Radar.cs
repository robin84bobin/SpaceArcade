using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public float fovAngle;
    public float distance;

    public List<string> targetTags;

    private Vector3 _fwd;
    private RaycastHit _hit = new RaycastHit();

    public GameObject target { get; private set; }

    void Update()
    {
        target = FindTarget();
    }

    private GameObject FindTarget()
    {
        _fwd = transform.TransformDirection(Vector3.forward);
        for (int i = -(int)(fovAngle * .5); i < (int)(fovAngle * .5); i += 5)
        {
            var vec = Quaternion.Euler(0, 0, i) * _fwd;
            Debug.DrawRay(transform.position, vec * distance, Color.cyan);
            if (Physics.Raycast(transform.position, vec, out _hit, distance))
            {
                foreach (string tag in targetTags)
                {
                    if (_hit.collider.CompareTag(tag))
                    {
                        return _hit.collider.gameObject;
                    }
                }
                
            }
        }

        return null;
    }
}
