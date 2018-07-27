using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    float _duration;

	void Start ()
    {
        Init();
	}

    void OnGetFromPool()
    {
        Init();
    }

    void Init()
    {
        var ps = GetComponent<ParticleSystem>();
        ps.Play();
        _duration = ps.duration;
        StartCoroutine(DestroyOnStop());
    }

    IEnumerator DestroyOnStop()
    {
        yield return new WaitForSeconds(_duration);
        ObjectPool.instance.PoolObject(gameObject);
    }
	
}
