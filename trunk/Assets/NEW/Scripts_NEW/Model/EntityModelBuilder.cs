using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EntityModelBuilder
{
    private static EntityModelBuilder _instance;
    public static EntityModelBuilder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EntityModelBuilder();
                _instance.Init();
            }
            return _instance;
        }
    }

    private Dictionary<Type, Func<EntityDataInfo, EntityModel>> _builderFuncs;

    void Init()
    {
        _builderFuncs = new Dictionary<Type, Func<EntityDataInfo, EntityModel>>();
        _builderFuncs.Add(typeof(ActorModel), CreateActorModel);

    }

    public TResult Create<TResult>(EntityDataInfo dataInfo)      where TResult : EntityModel
    {
        if (_builderFuncs.ContainsKey(typeof(TResult)))
        {
            return _builderFuncs[typeof(TResult)](dataInfo) as TResult;
        }
        else
        {
            return null;
        }
    }

    ActorModel CreateActorModel(EntityDataInfo dataInfo)
    {
        ActorData data = DataController.Instance.Get<ActorData>(dataInfo.id);
        ActorModel actor = new ActorModel(data);
        return actor;
    }
}
