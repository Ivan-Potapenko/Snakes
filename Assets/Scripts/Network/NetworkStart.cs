using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkStart : MonoBehaviour
{
    [SerializeField] private Network Network;
    [SerializeField] private MoveDick MoveDick;
    [SerializeField] private Dick Dick;
    [SerializeField] private float BiasData = 1;
    [SerializeField] private List<int> NeuronCount = new List<int>();
    [SerializeField] private List<bool> BiasNeuron = new List<bool>();
    [SerializeField] private List<List<List<float>>> Synapsis = new List<List<List<float>>>();
    [SerializeField] private float SynapsisRange = 10;
    [SerializeField] List<float> InputData = new List<float>();
    [SerializeField] private float SynapsisCount;
    [SerializeField] private NetworkHead NetworkHead;
    [SerializeField] private bool IsStart = false;
    [SerializeField] List<float> output = new List<float>();


    public void NetworkStartSet(float biasData, List<int> neuronCount, List<bool> biasNeuron, float synapsisRange)
    {

        BiasData = biasData;
        NeuronCount = neuronCount;
        BiasNeuron = biasNeuron;
        SynapsisRange = synapsisRange;

        NetworkHead = GetComponent<NetworkHead>();
        MoveDick = GetComponent<MoveDick>();
        Dick = GetComponent<Dick>();
        for (int i = 1; i < NeuronCount.Count; i++)
        {
            SynapsisCount += NeuronCount[i - 1] * NeuronCount[i];
        }
        InputData.Clear();

        for (int i = 0; i < NeuronCount[0]; i++)
        {
            InputData.Add(0);
        }
    }

    private void Start()
    {



    }

    public void RandomSynapsis()
    {
        List<float> synapsis = new List<float>();
        for (int i = 0; i < SynapsisCount; i++)
        {
            synapsis.Add(Random.Range(-SynapsisRange, SynapsisRange) / SynapsisRange);
        }
        SetSynapsis(synapsis);

    }
    public void SetSynapsis(List<float> synapsis)
    {
        List<List<List<float>>> allsinapsis = new List<List<List<float>>>();
        int synapsisNum = 0;
        for (int layerNum = 0; layerNum < NeuronCount.Count - 1; layerNum++)
        {
            List<List<float>> neuronSinapsis = new List<List<float>>();

            for (int neuronNum = 0; neuronNum < NeuronCount[layerNum]; neuronNum++)
            {
                List<float> nextLayerNeuronSynapsis = new List<float>();

                for (int nextNeuronNum = 0; nextNeuronNum < NeuronCount[layerNum + 1]; nextNeuronNum++)
                {
                    nextLayerNeuronSynapsis.Add(synapsis[synapsisNum]);
                    synapsisNum++;
                }
                neuronSinapsis.Add(nextLayerNeuronSynapsis);
            }
            allsinapsis.Add(neuronSinapsis);
        }

        List<List<float>> neuronSinapsis_2 = new List<List<float>>();

        for (int neuronNum = 0; neuronNum < NeuronCount[NeuronCount.Count - 1]; neuronNum++)
        {
            List<float> nextLayerNeuronSynapsis = new List<float>();

            nextLayerNeuronSynapsis.Add(1);


            neuronSinapsis_2.Add(nextLayerNeuronSynapsis);

        }
        allsinapsis.Add(neuronSinapsis_2);

        Synapsis = allsinapsis;
        IsStart = true;
        Network = new Network(Synapsis, BiasNeuron, BiasData);


    }

    public int GetNetworkScore()
    {
        return Dick.GetScore();
    }

    public List<float> GetSynapsis()
    {
        return Network.GetSynapsis();

    }


    public void SetInputData()
    {
        InputData[0] = NetworkHead.GetClosenFoodX();
        InputData[1] = NetworkHead.GetClosenFoodY();
        InputData[2] = gameObject.transform.position.x / 1000f;
        InputData[3] = gameObject.transform.position.z / 1000f;
    }

    private void Update()
    {
        if (IsStart)
        {


            SetInputData();
            output = Network.NetworkOutput(InputData);



            if (output[0] > 0.5f)
            {
                MoveDick.MoveHead("left");
            }
            else
            if (output[1] > 0.5f)
            {
                MoveDick.MoveHead("right");

            }

            if (output[2] > 0.5f)
            {
                MoveDick.MoveHead("forward");

            }
            else
            if (output[3] > 0.5f)
            {
                MoveDick.MoveHead("back");

            }
            //output.Clear();
        }
    }
}
