using UnityEngine;

public  class PrefabHelper
{
    public const string Invulnirability = "Prefabs/Equipment/Invulnirability/Invulnirability";
    public const string Explosion = "Prefabs/ParticleEffects/Explosion";

    public static void Instantiate(string path, Vector3 pos, Quaternion quaternion, Transform parent = null)
    {
        GameObject go = Resources.Load(path) as GameObject;
        go.transform.SetParent(parent);
        GameObject.Instantiate(go, pos, quaternion);
    }
}
