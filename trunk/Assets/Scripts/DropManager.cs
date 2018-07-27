using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager: MonoBehaviour
{
    public float dropItemDelay = 0.02f;
    public float dropRadius = 1f;


    public void Drop(Transform tr, GameObject[] dropList = null, List<string> dropPrefabNames = null)
    {
        int cnt = dropList.Length;
        //int cnt = dropPrefabNames.Count;
        if (cnt <= 0) return;
        Vector3 startPos = tr.localPosition;
        Transform dropParent = tr.parent;
        
        //calculate drop positions
        Vector3[] position = new Vector3[cnt];
        position[0] = startPos;
        if (cnt > 1)
        {
            float angleDelta = 360 / (cnt - 1);
            for (int i = 1; i < cnt; i++)
            {
                float angle = (i-1) * angleDelta;
                Vector3 delta = new Vector3(0f, dropRadius, 0f) * dropRadius;
                delta = Quaternion.Euler(0f, 0f, angle) * delta;
                Vector3 pos = startPos + delta;
                position[i] = pos;
            }
        }
        
        //drop items
        float delay = 0f;
        for (int i = 0; i < cnt; i++)
        {
            delay = dropItemDelay * i;
            DropItem(dropList[i], dropParent, startPos, position[i], delay);
            //DropItem(dropPrefabNames[i], dropParent, startPos, position[i], delay);
        }
    }


    void DropItem(GameObject drop, Transform dropParent, Vector3 startPos, Vector3 endPos, float delay = 0f)
    {
        DropItem(drop.name, dropParent, startPos, endPos, delay);
    }

    void DropItem(string dropPrefabName, Transform dropParent, Vector3 startPos, Vector3 endPos, float delay = 0f)
    {
        GameObject go = ObjectPool.instance.GetObject(dropPrefabName);
        if (go == null)
        {
            go = GameObject.Instantiate(Resources.Load(dropPrefabName)) as GameObject;
        }
        go.transform.SetParent(dropParent);
        go.transform.localPosition = startPos;
        go.transform.rotation = Quaternion.identity;

        Tween tween = go.AddComponent<Tween>();
        tween.targetPos = endPos;
        tween.delay = delay;
    }
}
