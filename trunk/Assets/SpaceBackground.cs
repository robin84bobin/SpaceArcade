using UnityEngine;
using System.Collections;

public class SpaceBackground : MonoBehaviour {

    public int maxStars = 500;
    public int universeSize = 10;

    private ParticleSystem.Particle[] points;

    private ParticleSystem _particleSystem;

    private void Create()
    {
        points = new ParticleSystem.Particle[maxStars];
        for (int i = 0; i < maxStars; i++)
        {
            points[i].position = Random.insideUnitSphere * universeSize;
            if (points[i].position.z < 0)
            {
                points[i].position = new Vector3(points[i].position.x, points[i].position.y, - points[i].position.z);
            }
            points[i].startSize = Random.Range(0.01f, 0.1f);
            points[i].startColor = new Color(1, 1, 1, 1f);
        }
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _particleSystem.SetParticles(points, points.Length);
    }

    void Start()
    {
        Create();
    }
}
