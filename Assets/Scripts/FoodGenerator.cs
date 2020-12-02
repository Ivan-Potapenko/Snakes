using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public List<GameObject> Food;
    public GameObject FoodPrefab;
    public float MaxX=100, MaxZ=100, MinX=-100, MinZ=-100,FoodY=11;

    public int FoodNumber = 0;


    void Start()
    {
        for (int i = 0; i < FoodNumber; i++)
        {
            GameObject food;
            Vector3 FoodPosition = new Vector3(Random.Range(MinX, MaxX), FoodY, Random.Range(MinZ, MaxZ));
            food = Instantiate(FoodPrefab,FoodPosition,Quaternion.identity,gameObject.transform);
            Food.Add(food);
            food.transform.localPosition = new Vector3(food.transform.localPosition.x,FoodY, food.transform.localPosition.z);
        }
    }

    public void ReDo()
    {
        for (int i = 0; i < FoodNumber; i++)
        {
            GameObject food=Food[i];
            food.SetActive(true);
            Vector3 FoodPosition = new Vector3(Random.Range(MinX, MaxX), FoodY, Random.Range(MinZ, MaxZ));
            food.transform.position= FoodPosition;
            food.transform.localPosition = new Vector3(food.transform.localPosition.x, FoodY, food.transform.localPosition.z);
        }
    }
    void Update()
    {

    }
}
