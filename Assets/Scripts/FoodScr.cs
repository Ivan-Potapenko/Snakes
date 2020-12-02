using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScr : MonoBehaviour
{
    public Dick Dick;
    public int ScorePlus = 1;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="DickHead")
        {
            Dick = collision.gameObject.transform.parent.GetComponent<Dick>();
            Dick.SetScorePlus(ScorePlus);
            gameObject.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
