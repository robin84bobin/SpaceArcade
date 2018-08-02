using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WeaponModel : EquipModel
{
    public WeaponData _data { get; private set; }
    public Parameter Ammo { get; private set; }
    private float _nextShotTime = 0f;


    public WeaponModel(EquipData data = null) : base(data)
    {
    }

    public override void Init(EquipData data = null)
    {
        _data = data as WeaponData;
        Ammo = new Parameter(ParamName.AMMO, 0f, _data.maxAmmo, 0f);
        Ammo.onMinValue += TryUnequip;
    }

    public event Action onAttack = delegate {};

    public void TryAttack()
    {
        //can't attack if ammo is empty
        if (!Ammo.InRange) return;

        //can't attack if shot period did not expire
        if (Time.time > _nextShotTime)
        {
            //Debug.Log("nextShotTime:" + _nextShotTime.ToString());
            _nextShotTime = Time.time + _data.attackPeriod;
            Attack();
        }
    }

    private void Attack()
    {
        //Decrease ammo value.
        Ammo.ChangeValue(-1);
        onAttack.Invoke();
    }

    private void TryUnequip()
    {
        if (_data.uneuipOnEmpty)
        {
            Unequip();
        }
    }
}
