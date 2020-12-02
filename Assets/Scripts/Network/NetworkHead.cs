using System.Collections.Generic;
using UnityEngine;

public class NetworkHead : MonoBehaviour
{
    [SerializeField] private FoodGenerator FoodGenerator;
    [SerializeField] float FoodX, FoodY;
    [SerializeField] private List<GameObject> Foods=new List<GameObject>();
    [SerializeField] private Dick Dick;

    void Start()
    {
        Dick = GetComponent<Dick>();
        FoodGenerator = GameObject.Find("FoodGenerator").GetComponent<FoodGenerator>();
       ChangeClosenFood();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Food")
        {
            print("jjj");
            Foods.Add(other.gameObject);
        }
    }
   
    void Update()
    {
    }
    public void ChangeClosenFood()
    {
        List<GameObject> food = FoodGenerator.Food;
        if(food.Count>0)
        {
            
            FoodX = food[0].transform.position.x;
            FoodY = food[0].transform.position.z;
            float dickToFoodX = food[0].transform.position.x - gameObject.transform.position.x;
            float dickToFoodY = food[0].transform.position.z - gameObject.transform.position.z;
            for (int i = 1; i < food.Count; i++)
            {
                if (food[i].activeSelf)
                {
                    float nextFoodX = food[i].transform.position.x - gameObject.transform.position.x;
                    float nextFoodY = food[i].transform.position.z - gameObject.transform.position.z;

                    float nextDistance = Mathf.Sqrt(nextFoodX * nextFoodX + nextFoodY * nextFoodY);
                    float distance = Mathf.Sqrt(dickToFoodX * dickToFoodX + dickToFoodY * dickToFoodY);
                    if (nextDistance < distance)
                    {
                        dickToFoodX = food[i].transform.position.x - gameObject.transform.position.x;
                        dickToFoodY = food[i].transform.position.z - gameObject.transform.position.z;
                        FoodX = food[i].transform.position.x;
                        FoodY = food[i].transform.position.z;

                    }
                }
                //Foods.Clear();


            }
        }
        Dick.DistansStart = Dick.Distans(GetClosenFoodX(), GetClosenFoodY());
    }
    public float GetClosenFoodX()
    {
        return FoodX/1000f;
    }
    public float GetClosenFoodY()
    {
        return FoodY / 1000f;
    }
}
