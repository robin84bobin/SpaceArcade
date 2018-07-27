using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public  class Magnet: BaseEquipmentController
{
    public float time = 10f;
    public float distance = 10f;

    private Timer _timer;

    public override EquipmentType equipType
    {
        get
        {
            return EquipmentType.MAGNET;
        }
    }


    public override void OnEquip(ActorModel owner)
    {
        _owner = owner;
        _timer = new Timer(time);
        _timer.OnComplete(OnCompleteTimer);
        _timer.Start();

    }


    public override void OnUnequip()
    {
        
    }

    private void OnCompleteTimer()
    {
        equipModel.Unequip();
    }

    float step = 0.5f;
    float searchTime = 0f;
    void Update()
    {
        _timer.Update();
        if (Time.time > searchTime)
        {
            searchTime = Time.time + step;
            FindTarget();
        }

        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                Move(targets[i]);
            }
        }

    }

    GameObject[] targets;
    private void FindTarget()
    {
       targets = GameObject.FindGameObjectsWithTag("Bonus");
    }


    void Move(GameObject go)
    {
        if (go == null || _owner == null) return;
        if ((go.transform.position - transform.position).magnitude > distance) return;

        Vector3 targetPos = transform.position;
        go.transform.position = Vector3.Slerp(go.transform.position, targetPos, 2f * Time.deltaTime);
    }
}
