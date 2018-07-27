using UnityEngine;

public class Tween : MonoBehaviour
{
    public float time = 1f;
    public Vector3 targetPos;
    public float delay = 0f;

    float startTime;
    float timeElapse;
    float step = 1f;
    
    void Start()
    {
        timeElapse = time;
        startTime = Time.time + delay;
    }

    void FixedUpdate()
    {
        if (Time.time < startTime)
        {
            return;
        }

        step = 1 - timeElapse / time;

        transform.localPosition = Vector3.Slerp(transform.localPosition, targetPos, step);
        timeElapse -= Time.deltaTime;
        if (transform.localPosition == targetPos)
        {
            Destroy(this);
        }

    }
}
