using UnityEngine;
using System;
using System.Linq;
using UnityEditor;


namespace Puertas.Variables
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant;
        public int ConstantValue;
        public IntVariable Variable;
        public int Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set
            {
                if (UseConstant) ConstantValue = value;
                else Variable.Value = value;
            }
        }
    }
}
