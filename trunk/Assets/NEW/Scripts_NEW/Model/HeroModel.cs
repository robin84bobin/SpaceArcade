using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HeroModel : ActorModel
{
    public string PrefabName { get { return _data.prefab; } }

    public Parameter Speed = new Parameter(ParamName.SPEED, 1f, 5f, 0f);
    public Parameter Health = new Parameter(ParamName.HEALTH, 100f, 100f, 0f);


    protected override void OnDefeat()
    {
        base.OnDefeat();
        UnequipAll();
        RemoveAllDots();
    }

    public void Respawn()
    {

    }
}


