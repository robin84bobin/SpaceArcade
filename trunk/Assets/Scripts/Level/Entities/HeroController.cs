using System;
using UnityEngine;

public class HeroController : ActorController
{

    protected override void OnDeath()
    {
        StopEffects();
        gameObject.SetActive(false);
        Explode();
    }


    internal void OnSpawn(Vector3 position, bool respawn = false)
    {
        gameObject.SetActive(true);
        transform.position = position;

        if (respawn)
        {
            StopEffects();
            SetInvulnirable();
            SendMessage("OnRespawn");
        }

        //Fire global event HeroRespawnEvent
        EventManager.Get<HeroSpawnEvent>().Publish();
    }

    private void SetInvulnirable()
    {
        //TODO реализовать применение неуязвимости
        /*
        EquipData invulnirData;
        actorModel.Equip(invulnirData);
        */
    }
}

