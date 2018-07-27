using System;
using UnityEngine;
using UnityEngine.Events;

public class ParamName
{
    //Actor params
    public const string HEALTH = "health";
    public const string ARMOR = "armor";
    public const string SPEED = "speed";
    //Level params
    public const string SCORE = "score";
    public const string TIME = "time";
    public const string LIVES = "lives";
    public const string AMMO = "ammo";
}


/// <summary>
/// Stores value of parameter.
/// Invoke callbacks: onMaxValue, onMinValue, onChange.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="maxValue"></param>
/// <param name="minValue"></param>
[Serializable]
public class Parameter
{
    public float minValue;
    public float maxValue;
    [SerializeField]
    private float _defaultValue;

    private float _defaultMinValue;
    private float _defaultMaxValue;

    [NonSerialized]
    public string name;

    [NonSerialized]
    public float value;

    public event Action onMaxValue = delegate { };
    public event Action onMinValue = delegate { };
    public event Action<float, float> onChange = delegate { };

    public bool InRange { get { return value < maxValue && value > minValue; } }

    /// <summary>
    /// Create parameter with name only
    /// </summary>
    /// <param name="name"></param>
    public Parameter(string name)
    {
        this.name = name;
    }

    internal void Reset()
    {
        value = _defaultValue;
        minValue = _defaultMinValue;
        maxValue = _defaultMaxValue;
    }

    /// <summary>
    /// Create parameter without name
    /// </summary>
    /// <param name="defVal"></param>
    /// <param name="maxVal"></param>
    /// <param name="minVal"></param>
    public Parameter(float defVal, float maxVal = float.MaxValue, float minVal = float.MinValue)
    {
        this.name = "empty_name";
        Init(defVal, maxVal, minVal);
    }

    /// <summary>
    /// Create parameter with name, value and value restrictions
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defVal"></param>
    /// <param name="maxVal"></param>
    /// <param name="minVal"></param>
    public Parameter(string name, float defVal, float maxVal = float.MaxValue, float minVal = float.MinValue)
    {
        this.name = name;
        Init(defVal, maxVal, minVal);
    }

    /// <summary>
    /// Initialise parameter value and value restrictions
    /// </summary>
    /// <param name="defVal"></param>
    /// <param name="maxVal"></param>
    /// <param name="minVal"></param>
    public void Init(float defVal, float maxVal = float.MaxValue, float minVal = float.MinValue)
    {
        maxValue = maxVal;
        minValue = minVal;
        // Initialise and restrict values.
        InitValues();
        // Save default values
        _defaultValue = defVal;
        _defaultMinValue = minValue;
        _defaultMaxValue = maxValue;
    }

    /// <summary>
    /// Initialise and restrict values.
    /// </summary>
    public void InitValues()
    {
        if (maxValue < minValue)
        {
            Debug.LogWarning(string.Format("Parameter:{0}  max value:{1} lower than min value: {2}",
                name.ToString(), maxValue.ToString(), minValue.ToString()));
            maxValue = minValue;
        }
        _defaultValue = Mathf.Clamp(_defaultValue, minValue, maxValue);
        value = _defaultValue;
    }

    /// <summary>
    /// Change param value and returns remainder.
    /// </summary>
    public float ChangeValue(float amount)
    {
        return SetValue(value + amount);
    }

    /// <summary>
    /// Set param value and returns remainder.
    /// </summary>
    public float SetValue(float newVal)
    {
        float remainder = 0;
        float oldValue = value;

        if (newVal > maxValue)
        {
            value = maxValue;
            onMaxValue.Invoke();
            onChange.Invoke(oldValue, value);
            remainder = newVal - maxValue;
            return remainder;
        }
        else
        if (newVal <= minValue)
        {
            value = minValue;
            onMinValue.Invoke();
            onChange.Invoke(oldValue, value);
            remainder = newVal - minValue;
            return remainder;
        }

        value = newVal;
        onChange.Invoke(oldValue, value);

        return remainder;
    }

    /// <summary>
    /// Release parameter events
    /// </summary>
    public void Release()
    {
        onChange = delegate { };
        onMinValue = delegate { };
        onMaxValue = delegate { };
    }
}
