using System.Collections.Generic;
using UnityEngine;

    public class ParamChangeStrategies
    {
        public static IParamChangeStrategy Get(string name_)
        {
            if (!StrategyMap.ContainsKey(name_)) {
                Debug.LogError(string.Format("Couldn't get strategy by name \'{0}\'", name_));
                return null;
            }
            return StrategyMap[name_];
        }

        private static readonly ParallelModifyStrategy _parallel = new ParallelModifyStrategy();
        public static ParallelModifyStrategy Parallel
        {
            get { return _parallel; }
        }

        private static readonly SerialModifyStrategy _serial = new SerialModifyStrategy();
        public static SerialModifyStrategy Serial
        {
            get { return _serial; }
        }

        private static readonly Dictionary<string, IParamChangeStrategy> StrategyMap = new Dictionary<string, IParamChangeStrategy>() {
            {"serial", Serial },
            {"parallel", Parallel }
        };
    }

    public interface IParamChangeStrategy
    {
        void Apply(float value_, string[] targetSkills_, Dictionary<string, Parameter> skills_);
    }
