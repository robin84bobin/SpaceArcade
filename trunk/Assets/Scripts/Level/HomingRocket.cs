using System;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : Bullet {

    [Range(0,180)]
    public float fovAngle = 120f;
    [Range(0,120)]
    public float distance = 80f;

    public GameObject targetIndicator;

    private List<string> targetTags;
    private Radar _radar;

    protected override void Start () {
        base.Start();
        if (targetTags == null) {
            targetTags = GetComponent<CollisionImpact>().targetTags;
        }

        _radar = gameObject.AddComponent<Radar>();
        _radar.fovAngle = fovAngle;
        _radar.distance = distance;
        _radar.targetTags = targetTags;
    }
	
    override protected void Update()
    {
        base.Update();
        transform.Rotate(Vector3.forward, 2f);

        bool hasTarget = _radar.target != null;
        targetIndicator.SetActive(hasTarget);
        if (hasTarget)
        {
            LookTo(_radar.target.transform);
        }
    }

    private void LookTo(Transform target)
    {
        Vector3 targetDir = target.position - transform.position;
        float step = 0.5f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
