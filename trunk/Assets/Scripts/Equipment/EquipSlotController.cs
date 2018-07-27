using UnityEngine;
using System.Collections;
using System;

public enum EquipmentType
{
    ARMOR,
    WEAPON,
    INVULNIRABILITY,
    MAGNET
}

public class EquipSlotController : MonoBehaviour {

    public EquipmentType equipType;

    public BaseEquipmentController equipController;
    public EquipSlotModel slotModel { get; private set; }


    private void UninstantiateItem()
    {
        equipController.Release();
        equipController.gameObject.transform.parent = null;
        Destroy(equipController.gameObject);
        slotModel.UnequipComplete();
    }

    public void Init(EquipSlotModel slotModel)
    {
        this.slotModel = slotModel;
        this.slotModel.onEquipItem += EquipItem;

        if (equipController != null)
        {
            if (equipController.equipType != equipType)
            {
                Debug.Log(string.Format("Can't equip item. Mismatch types: {0} >> {1}", equipType.ToString(), equipController.equipType.ToString()));
                return;
            }
            InstantiateItem(equipController.gameObject);
        }
    }

    void EquipItem(EquipModel equipModel)
    {
        GameObject go = ObjectPool.instance.GetObject(equipModel.data);
        if (go == null)
        {
            Debug.Log(string.Format("Can't equip item. No prefab found:" + equipModel.data.prefabName));
            return;
        }
        InstantiateItem(go);
    }

    private void InstantiateItem(GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        Transform t = go.transform;
        t.parent = this.transform;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        go.SetActive(true);

        equipController = go.GetComponent<BaseEquipmentController>();
        equipController.onUnequipComplete += UninstantiateItem;
    }

    void OnDestroy()
    {
        slotModel = null;
    }
}
