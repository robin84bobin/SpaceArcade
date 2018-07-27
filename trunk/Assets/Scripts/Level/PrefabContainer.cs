using UnityEngine;
using System.Collections;

public class PrefabContainer : MonoBehaviour {

    public GameObject prefab;

    public string type;
    public int id;

	void Awake()
    {
        //ReadData();
        //return;

        //create gameobject
        GameObject go = ObjectPool.instance.GetObject(prefab.name);//Instantiate(prefab);
        go.transform.parent = this.transform.parent;
        go.transform.position = this.transform.position;
        go.transform.rotation = this.transform.rotation;
        //destroy container
        Destroy(this.gameObject);
    }

  /*  void ReadData()
    {
        GameObject go = EntityBuilder.Instance.Create(type, id);
        go.transform.parent = this.transform.parent;
        go.transform.position = this.transform.position;
        go.transform.rotation = this.transform.rotation;
        //destroy container
        Destroy(this.gameObject);
    }*/
}
