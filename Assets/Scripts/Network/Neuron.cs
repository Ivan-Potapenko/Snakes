using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron 
{

    [SerializeField] private Synapsis Synapsis;
    [SerializeField] private TypeOfNeuron TypeOfNeuron;
    [SerializeField] private float BiasNeuronData;

    public Neuron(string typeOfNeuron, List<float> synapsisData, float biasNeuronData)
    {
        TypeOfNeuron = new TypeOfNeuron(typeOfNeuron);
        Synapsis = new Synapsis(synapsisData);
        BiasNeuronData = biasNeuronData;
    }

    private float ActivationFunctionSigmoid(float x)
    {
        var y = 1 / (1 + Mathf.Exp(-x));

        return y;
    }

    private List<float> InputNeuronCalculate(float input)
    {
        List<float> output = new List<float>();
        List<float> synapsis = Synapsis.GetSynopsisData();
        for (int i = 0; i < synapsis.Count; i++)
        {
            output.Add(ActivationFunctionSigmoid(input) * synapsis[i]);
        }
        return output;
    }

    private List<float> OutputNeuronCalculate(float input)
    {
        List<float> output = new List<float>();
        List<float> synapsis = Synapsis.GetSynopsisData();
        for (int i = 0; i < synapsis.Count; i++)
        {
            output.Add(ActivationFunctionSigmoid(input) * synapsis[i]);
        }
        return output;
    }
    private List<float> HideNeuronCalculate(float input)
    {
        List<float> output = new List<float>();
        List<float> synapsis = Synapsis.GetSynopsisData();
        for (int i = 0; i < synapsis.Count; i++)
        {
            output.Add(ActivationFunctionSigmoid(input) * synapsis[i]);
        }
        return output;
    }

    private List<float> BiasNeuronCalculate(float input)
    {
        List<float> output = new List<float>();
        List<float> synopsis = Synapsis.GetSynopsisData();
        for (int i = 0; i < synopsis.Count; i++)
        {
            output.Add(ActivationFunctionSigmoid(BiasNeuronData) * synopsis[i]);
        }
        return output;
    }

    public List<float> NeuronOutput(float input)
    {
        //List<float> output = new List<float>();
       //MonoBehaviour.print(TypeOfNeuron.GetNeuronType());
        switch (TypeOfNeuron.GetNeuronType())
        {
            case 0:
                return InputNeuronCalculate(input);

            case 1:
                return HideNeuronCalculate(input);

            case 2:
                return BiasNeuronCalculate(input);

            case 3:
                return OutputNeuronCalculate(input);

            default :
               MonoBehaviour.print("Error Type");
                return HideNeuronCalculate(input);



        }
    }

}
