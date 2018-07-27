using System;
using System.Collections.Generic;


public class ParallelModifyStrategy : IParamChangeStrategy
{
    private string _paramName;

    public void Apply(float value_, string[] targetParams, Dictionary<string, Parameter> parameters)
    {
        for (int i = 0; i < targetParams.Length; i++)
        {
            _paramName = targetParams[i];
            if (parameters.ContainsKey(_paramName))
            {
                parameters[_paramName].ChangeValue(value_);
            }
        }
    }


}
