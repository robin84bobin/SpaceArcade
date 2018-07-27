using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropController : MonoBehaviour {

    public GameObject[] dropList;
    public List<string> dropPrefabNames;
    public int amount;

	void OnDropReward()
    {
        if (dropList.Length > 0) Main.Instance.Drop.Drop(transform, dropList);
        if (dropPrefabNames.Count > 0) {
            Main.Instance.Drop.Drop(transform, null, dropPrefabNames);
        }
    }
}
