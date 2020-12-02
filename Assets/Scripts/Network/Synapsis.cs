using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapsis 
{
    [SerializeField] private List<float> SynopsisData=new List<float>(); 

    public Synapsis(List<float> synopsisData)
    {
        SynopsisData = synopsisData;
    }

    public List<float> GetSynopsisData()
    {
        return SynopsisData;
    }
                                    
}
