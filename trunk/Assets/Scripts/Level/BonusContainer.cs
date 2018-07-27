using UnityEngine;
using System.Collections;

public class BonusContainer : MonoBehaviour {

    public Transform bonusHolder;

    Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        if (anim != null)
        {
            anim.wrapMode = WrapMode.Loop;
            anim.Play("BonusSphereAnimation");
        }
    }

    void OnGetFromPool()
    {
        Start();
    }

    void DestroyOnApplyImpact()
    {
        if (anim != null)
        {
            anim.wrapMode = WrapMode.Once;
            anim.Play("BonusDestroyAnimation");
        }
        else
        {
            Destroy();
        }
    }

    void Destroy()
    {
        if (anim != null)
        {
            anim.Stop();
        }
        ObjectPool.instance.PoolObject(gameObject);
        //Destroy(gameObject);
    }
}
