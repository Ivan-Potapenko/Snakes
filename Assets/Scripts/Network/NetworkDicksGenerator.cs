using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NetworkDicksGenerator : MonoBehaviour
{
    [SerializeField] private float BiasData = 1;
    [SerializeField] private List<int> NeuronCount = new List<int>();
    [SerializeField] private List<bool> BiasNeuron = new List<bool>();
    [SerializeField] private float SynapsisRange = 10;

    [SerializeField] private bool NewGeneretion = true;

    [SerializeField] private int ScoreMinRandom = 2;
    [SerializeField] private List<StreamReader> SynapsisReader = new List<StreamReader>();
    [SerializeField] private List<StreamWriter> SynapsisWriter = new List<StreamWriter>();
    [SerializeField] private bool isRandomSynapsis = false;

    [SerializeField] private int TopCount = 5, DicksCount = 50;
    [SerializeField] public GameObject DicksPrefab;
    [SerializeField] private List<GameObject> NetworkDicks = new List<GameObject>();
    [SerializeField] private List<NetworkStart> NetworkStartDick = new List<NetworkStart>();
    [SerializeField] private List<List<float>> Synapsis = new List<List<float>>();

    public FoodGenerator FoodGenerator;
    [SerializeField] private List<NetworkStart> TopNetworkStartDick = new List<NetworkStart>();
    [SerializeField] private float LearningTimeSec = 30;
    [SerializeField] private int Generation = 0;
    [SerializeField] private int BestScoreOfPrevGeneretion = 0;


    [SerializeField] private float MaxX = 100, MaxZ = 100, MinX = -100, MinZ = -100, DickY = 11;

    void Start()
    {


    }

    private void ReadSynapsis()
    {
        NetworkStartDick.Clear();
        Synapsis.Clear();

        for (int i = 0; i < NetworkDicks.Count; i++)
        {
            Destroy(NetworkDicks[i]);

        }
        NetworkDicks.Clear();
        SynapsisReader.Clear();

        for (int i = 0; i < TopCount; i++)
        {

            if (File.Exists("synapsis_" + i + ".txt"))
            {
                FileStream fileStream = new FileStream("synapsis_" + i + ".txt", FileMode.Open);

                SynapsisReader.Add(new StreamReader(fileStream));

                List<float> synapsis = new List<float>();
                while (!SynapsisReader[i].EndOfStream)
                {
                    string synapsisString = SynapsisReader[i].ReadLine();
                    synapsisString.Replace(',', '.');
                    float f;
                    if (float.TryParse(synapsisString, out f))
                    {
                        synapsis.Add(float.Parse(synapsisString));

                    }
                }
                Synapsis.Add(synapsis);

                GameObject dick = Instantiate(DicksPrefab);
                dick.transform.position = new Vector3(Random.Range(MinX, MaxX), DickY, Random.Range(MinZ, MaxZ));
                dick.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

                NetworkDicks.Add(dick);
                NetworkStartDick.Add(NetworkDicks[i].GetComponent<NetworkStart>());
                NetworkStartDick[i].NetworkStartSet(BiasData, NeuronCount, BiasNeuron, SynapsisRange);

                NetworkStartDick[i].SetSynapsis(Synapsis[i]);
                SynapsisReader[i].Close();
            }
            else
            {
                print("SynapsisReader is not assign");
                GameObject dick = Instantiate(DicksPrefab);
                dick.transform.position = new Vector3(Random.Range(MinX, MaxX), DickY, Random.Range(MinZ, MaxZ));
                dick.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
                NetworkDicks.Add(dick);
                NetworkStartDick.Add(NetworkDicks[i].GetComponent<NetworkStart>());
                NetworkStartDick[i].NetworkStartSet(BiasData, NeuronCount, BiasNeuron, SynapsisRange);
                NetworkStartDick[i].RandomSynapsis();
                List<float> synapsis = NetworkStartDick[i].GetSynapsis();
                Synapsis.Add(synapsis);

                //SynapsisReader[i].Close();
            }


        }

    }
    private void WriteSynapsis()
    {
        FindTopNetworks();
        for (int i = 0; i < TopCount; i++)
        {

            SynapsisWriter.Add(new StreamWriter(new FileStream("synapsis_" + i + ".txt", FileMode.OpenOrCreate)));
            // SynapsisWriter[i].Dispose();

            List<float> synapsis = TopNetworkStartDick[i].GetSynapsis();
            for (int j = 0; j < synapsis.Count; j++)
            {
                SynapsisWriter[i].WriteLine((float)synapsis[j]);

            }

            SynapsisWriter[i].Close();


        }
        SynapsisWriter.Clear();
        TopNetworkStartDick.Clear();
        Synapsis.Clear();
    }



    private void FindTopNetworks()
    {
        for (int i = 0; i < TopCount; i++)
        {
            TopNetworkStartDick.Add(NetworkStartDick[i]);

        }
        for (int i = TopCount - 1; i < NetworkStartDick.Count; i++)
        {

            ReplaceTop(NetworkStartDick[i], 0);
        }
        /* for (int i = 0; i < TopCount; i++)
         {
             print("wdwqdqw" + NetworkStartDick[i].GetNetworkScore());

         }*/
        BestScoreOfPrevGeneretion = TopNetworkStartDick[0].GetNetworkScore();
        for (int i = 1; i < TopCount; i++)
        {
            if (BestScoreOfPrevGeneretion < TopNetworkStartDick[i].GetNetworkScore())
                BestScoreOfPrevGeneretion = TopNetworkStartDick[i].GetNetworkScore();

        }
        for (int q = 0; q < TopNetworkStartDick.Count; q++)
        {
            if (TopNetworkStartDick[q].GetNetworkScore() < ScoreMinRandom)
            {
                isRandomSynapsis = true;
                break;
            }
        }
    }

    private void ReplaceTop(NetworkStart networkStart, int plusJ)
    {
        for (int j = plusJ; j < TopCount; j++)
        {
            if (TopNetworkStartDick[j].GetNetworkScore() < networkStart.GetNetworkScore())
            {
                ReplaceTop(TopNetworkStartDick[j], j + 1);
                TopNetworkStartDick[j] = networkStart;
            }
        }
    }






    private void AddDicks()
    {
        ReadSynapsis();
        for (int i = 0; i < DicksCount / (TopCount * TopCount); i++)
        {
            for (int j = 0; j < TopCount; j++)
            {
                for (int k = 0; k < TopCount; k++)
                {
                    //  print(j+" "+k);



                    Synapsis.Add(AddSynapsis(Synapsis[j], Synapsis[k], isRandomSynapsis));
                    GameObject dick = Instantiate(DicksPrefab);
                    dick.transform.position = new Vector3(Random.Range(MinX, MaxX), DickY, Random.Range(MinZ, MaxZ));
                    dick.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

                    NetworkDicks.Add(dick);

                    NetworkStartDick.Add(NetworkDicks[Synapsis.Count - 1].GetComponent<NetworkStart>());

                    NetworkStartDick[Synapsis.Count - 1].NetworkStartSet(BiasData, NeuronCount, BiasNeuron, SynapsisRange);
                    NetworkStartDick[Synapsis.Count - 1].SetSynapsis(Synapsis[Synapsis.Count - 1]);
                }
            }
        }
        isRandomSynapsis = false;
        IgnorePartsCollision(NetworkDicks);
    }



    private void IgnorePartsCollision(List<GameObject> partsOfDick)
    {

        for (int i = 0; i < partsOfDick.Count - 1; i++)
        {

            for (int j = i + 1; j < partsOfDick.Count; j++)
            {
                for (int k= 0; k < partsOfDick[i].transform.childCount; k++)
                {

                    SphereCollider colliderIgnore = partsOfDick[i].transform.GetChild(k).GetComponent<SphereCollider>();

                    for (int q = 0; q < partsOfDick[j].transform.childCount; q++)
                    {


                        SphereCollider ignoredCollider = partsOfDick[j].transform.GetChild(q).GetComponent<SphereCollider>();


                        Physics.IgnoreCollision(colliderIgnore, ignoredCollider, true);

                    }


                }




                
            }


        }


    }





    private List<float> AddSynapsis(List<float> sinapsis_1, List<float> sinapsis_2, bool isRandomSynapsis)
    {
        List<float> outputSynapsis = new List<float>();


        for (int i = 0; i < sinapsis_1.Count - NeuronCount[NeuronCount.Count - 1]; i++)
        {

            if (isRandomSynapsis)
            {
                outputSynapsis.Add(Random.Range(-SynapsisRange, SynapsisRange) / SynapsisRange);
            }
            else
            {
                if (Random.Range(0, 50) == 0)
                {
                    outputSynapsis.Add(Random.Range(-SynapsisRange, SynapsisRange) / SynapsisRange);

                }
                else
                if (Random.Range(0,20) == 0)
                {
                    if(sinapsis_1[i]> sinapsis_2[i])
                    {
                        outputSynapsis.Add(Random.Range(sinapsis_2[i]*100f, sinapsis_1[i] * 100f)/100f);

                    }
                    else
                    {
                        outputSynapsis.Add(Random.Range(sinapsis_1[i] * 100f,sinapsis_2[i] * 100f) / 100f);

                    }

                }
                else
                if (Random.Range(0, 2) == 0)
                {
                    outputSynapsis.Add(sinapsis_1[i]);

                }
                else
                {
                    outputSynapsis.Add(sinapsis_2[i]);

                }




            }



        }

        for (int i = sinapsis_1.Count - NeuronCount[NeuronCount.Count - 1] - 1; i < sinapsis_1.Count; i++)
        {


            outputSynapsis.Add(1);

        }

        return outputSynapsis;
    }

    void Update()
    {
        if (NewGeneretion)
        {
            StartCoroutine(DickDo());
            NewGeneretion = false;
        }
    }
    private IEnumerator DickDo()
    {
        FoodGenerator.ReDo();
        AddDicks();
        yield return new WaitForSeconds(LearningTimeSec);
        WriteSynapsis();
        Generation++;
        NewGeneretion = true;

        yield return null;
    }
}

