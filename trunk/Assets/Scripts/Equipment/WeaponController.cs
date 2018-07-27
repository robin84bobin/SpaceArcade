using System;
using UnityEngine;


public class WeaponController : BaseEquipmentController
{

    public override EquipmentType equipType
    {
        get
        {
            return EquipmentType.WEAPON;
        }
    }

    public float shotPeriod = 1f;
    public GameObject projectile;
    public Transform projectileSpawnPoint;

    private Animation _animation;

    void Start()
    {
        _animation = GetComponent<Animation>();
    }

    bool canAttack = true;

    private void Shot()
    {
        //Instantiate projectile.
        GameObject go = ObjectPool.instance.GetObject(projectile.name, false);
        go.transform.SetParent(null);
        go.transform.position = projectileSpawnPoint.position;
        go.transform.rotation = projectileSpawnPoint.rotation;

        //Make projectile not to bring friendly fire damage. 
        var ownerTag = _owner.type;
        var damage = go.GetComponent<CollisionImpact>();
        if (damage != null && damage.targetTags.Contains(ownerTag))
        {
            damage.targetTags.Remove(ownerTag);
        }

        //Play attack animation
        Animate("Attack");
    }

    private void Animate(string clip)
    {
        if (_animation != null)
        {
            _animation.Play(clip);
        }
    }

    public override void OnEquip(ActorModel owner)
    {
        //throw new NotImplementedException();
    }
}