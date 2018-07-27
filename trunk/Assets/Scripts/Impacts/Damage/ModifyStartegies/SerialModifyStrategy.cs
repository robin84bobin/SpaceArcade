using System;
using System.Collections.Generic;


/// <summary>
/// Apply impact in order of affectedSkills array
/// Apply residual value to next skill
/// </summary>
public class SerialModifyStrategy : IParamChangeStrategy
{
    private string _paramName;
    private float _remainderValue = 0;
    

    public void Apply(float value_, string[] targetParams, Dictionary<string, Parameter> parameters)
    {
        _remainderValue = value_;

        for (int i = 0; i < targetParams.Length; i++)
        {
            _paramName = targetParams[i];
            if (parameters.ContainsKey(_paramName))
            {
                _remainderValue = parameters[_paramName].ChangeValue(_remainderValue);
            }
        }
    }
}
