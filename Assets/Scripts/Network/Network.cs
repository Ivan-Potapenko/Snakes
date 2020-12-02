using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Network 
{
    [SerializeField] private List<Layer> Layers=new List<Layer>();
    [SerializeField] private List<List<List<float>>> Synapsis;
    [SerializeField] private List<bool> LayersWithBias;
    [SerializeField] private float BiasData;

    public Network(List<List<List<float>>> sinapsis, List<bool> layersWithBias,float biasData)
    {
        Synapsis = sinapsis;
        LayersWithBias = layersWithBias;
        BiasData = biasData;
        LayersGenerator();
    }

    public List<float> GetSynapsis()
    {
        List<float> outputSynapsis= new List<float>();


        for (int layerNum = 0; layerNum < Synapsis.Count; layerNum++)
        {
            for (int neuronNum = 0; neuronNum < Synapsis[layerNum].Count; neuronNum++)
            {
                for (int nextNeuronNum = 0; nextNeuronNum < Synapsis[layerNum][neuronNum].Count; nextNeuronNum++)
                {

                    outputSynapsis.Add(Synapsis[layerNum][neuronNum][nextNeuronNum]);

                }

                
            }
            
        }
        return outputSynapsis;

    }


    public void LayersGenerator()
    {
        int layersCount = Synapsis.Count;



        for (int layerNumber = 0; layerNumber < layersCount; layerNumber++)
        {


            if (layerNumber == 0)
            {
                Layer layer=new Layer(Synapsis[layerNumber], "input", LayersWithBias[layerNumber], BiasData);
                Layers.Add(layer);
            }
            else
            if (layerNumber == layersCount - 1)
            {
                Layers.Add(new Layer(Synapsis[layerNumber], "output", LayersWithBias[layerNumber], BiasData));
            }
            else
            {
                Layers.Add(new Layer(Synapsis[layerNumber], "hide", LayersWithBias[layerNumber], BiasData));

            }




        }
    }
 
    public List<float> NetworkOutput(List<float> input)
    {
        List<float> output=input;
        
        for(int layerNum=0;layerNum<Layers.Count-1;layerNum++)
        {
            output=Layers[layerNum].LayerOutput(output,Layers[layerNum+1].Neurons.Count,false);
        }

        output = Layers[Layers.Count - 1].LayerOutput(output, Layers[Layers.Count-1].Neurons.Count, true);

        return output;
    }
}








