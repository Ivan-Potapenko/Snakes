using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfNeuron 
{
    [SerializeField] private string Type;
    public TypeOfNeuron(string type)
    {
        Type = type;
    }

    public int GetNeuronType()
    {
        

        switch (Type)
        {
            case "input":  return 0;

            case "hide":  return 1;

            case "bias":  return 2;

            case "output":  return 3;

            default:
               MonoBehaviour.print("TypeError");
                return 4;
        }

    }
}
