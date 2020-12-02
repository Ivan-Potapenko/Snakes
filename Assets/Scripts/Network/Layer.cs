using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer 
{
    [SerializeField] public List<Neuron> Neurons = new List<Neuron>();
    [SerializeField] private bool BiasNeuron = false;
    [SerializeField] private float BiasNeuronData;

    public Layer(List<List<float>> synapsis, string typeOfLayer, bool biasNeuron, float biasNeuronData)
    {
        BiasNeuron = biasNeuron;
        BiasNeuronData = biasNeuronData;
        AddNeurons(synapsis, typeOfLayer, biasNeuron);

    }

    private void AddNeurons(List<List<float>> synapsis, string typeOfLayer, bool biasNeuron)
    {
        if (!biasNeuron)
        {
            for (int i = 0; i < synapsis.Count; i++)
            {

                Neurons.Add(new Neuron(typeOfLayer, synapsis[i], BiasNeuronData));
            }
        }
        else
        {
            for (int i = 0; i < synapsis.Count - 1; i++)
            {
                Neurons.Add(new Neuron(typeOfLayer, synapsis[i], BiasNeuronData));
            }

            Neurons.Add(new Neuron("bias", synapsis[synapsis.Count - 1], BiasNeuronData));
        }

    }

    public List<float> LayerOutput(List<float> layerInput, int nextLayerCount, bool outLayer)
    {

        List<float> layerOutput = new List<float>();


        for (int neuronInpNum = 0; neuronInpNum < layerInput.Count; neuronInpNum++)
        {
            List<float> neuronOutput = Neurons[neuronInpNum].NeuronOutput(layerInput[neuronInpNum]);

            if (outLayer)
            {


                layerOutput.Add(neuronOutput[0]);
            }
            else
            {
                for (int neuronOutNum = 0; neuronOutNum < nextLayerCount; neuronOutNum++)
                {

                    if (layerOutput.Count >= neuronOutNum + 1)
                    {
                        layerOutput[neuronOutNum] += neuronOutput[neuronOutNum];
                    }
                    else
                    {
                        layerOutput.Add(neuronOutput[neuronOutNum]);
                    }


                }
            }
        }
        return layerOutput;
    }

}
