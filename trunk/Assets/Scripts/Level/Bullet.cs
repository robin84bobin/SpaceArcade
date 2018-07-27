using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 10f;
    public float lifeTime = 5f;
    float _deathTime;

    protected virtual void Start()
    {
       // Init();
    }

    protected void OnGetFromPool()
    {
        Init();
    }

    protected virtual void Init()
    {
        _deathTime = Time.time + lifeTime;
    }

	protected virtual void Update () {
        if(Time.time > _deathTime)
        {
            ObjectPool.instance.PoolObject(gameObject);
            return;
        }

        transform.Translate(Vector3.forward * Time.deltaTime / speed);
	}
}
