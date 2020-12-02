using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour
{
    [SerializeField] private List<GameObject> PartsOfDick=new List<GameObject>();
    [SerializeField] private int IgnoreCount = 3;
    [SerializeField] private Vector3 ForceTorque = new Vector3(0, 4f, 0);//,DirectionHead=new Vector3();
    [SerializeField] private Vector3 Speed = new Vector3(4f, 0, 0);
    [SerializeField] private int Score=0;
    [SerializeField] private NetworkHead NetworkHead;
    [SerializeField] public float DistansStart,positoinX;

    void Start()
    {
        

        for (int i=0;i<gameObject.transform.childCount;i++)
        {
            PartsOfDick.Add(gameObject.transform.GetChild(i).gameObject);

            

        }
        IgnorePartsCollision(IgnoreCount, PartsOfDick);


       NetworkHead = GetComponent<NetworkHead>();
      /*  NetworkHead.ChangeClosenFood();
        DistansStart = Distans(NetworkHead.GetClosenFoodX(), NetworkHead.GetClosenFoodY());*/

    }
    
    
    public float Distans(float x,float y)
    {
        x = x - gameObject.transform.localPosition.x / 1000f;
        y = y - gameObject.transform.localPosition.z / 1000f;
        print("dfsd" + gameObject.transform.localPosition.x);
        return Mathf.Sqrt(x*x+y*y);
    }
    private void IgnorePartsCollision(int ignoreCount, List<GameObject> partsOfDick)
    {
        if (ignoreCount > 0)
        {
            for (int i = 0; i < partsOfDick.Count - 1; i++)
            {

                int toIgnoredColliderCount = i + ignoreCount + 1;
                if (toIgnoredColliderCount > partsOfDick.Count)
                {
                    toIgnoredColliderCount = partsOfDick.Count;
                }


                for (int j = i + 1; j < toIgnoredColliderCount; j++)
                {

                    SphereCollider colliderIgnore = partsOfDick[i].GetComponent<SphereCollider>();

                    SphereCollider ignoredCollider = partsOfDick[j].GetComponent<SphereCollider>();


                    Physics.IgnoreCollision(colliderIgnore, ignoredCollider, true);
                }


            }
        }
        else
        {
            print("IGNORE dick in parts COUNT " + gameObject.name + " <= 0");
        }

    }


    public void SetScorePlus(int scorePlus)
    {
        NetworkHead = GetComponent<NetworkHead>();

        NetworkHead.ChangeClosenFood();
        DistansStart = Distans(NetworkHead.GetClosenFoodX(), NetworkHead.GetClosenFoodY());
        Score +=(int)((DistansStart-Distans(NetworkHead.GetClosenFoodX(), NetworkHead.GetClosenFoodY())));
        print(Distans(NetworkHead.GetClosenFoodX(), NetworkHead.GetClosenFoodY()));
        Score += scorePlus;
       // print(Score);
    }


    public int GetScore()
    {
        print(Distans(NetworkHead.GetClosenFoodX(), NetworkHead.GetClosenFoodY())+"    "+ DistansStart);

        Score += (int)(100*(DistansStart-Distans(NetworkHead.GetClosenFoodX(), NetworkHead.GetClosenFoodY())));
        return Score;
    }


    public float Normalized(float angle)
    {
        if(angle>360)
        {
            return angle-360  ;
        }
        else
        {
            return angle;

        }
    }

    public Vector3 GetSpeedVector(int angelPlus)
    {
        float angle = PartsOfDick[0].gameObject.transform.eulerAngles.y + angelPlus;

        angle = Normalized(angle);
    

        Vector3 speedVector =new Vector3();

        speedVector.x = Speed.x * Mathf.Cos(angle * Mathf.Deg2Rad) + Speed.z * Mathf.Sin(angle * Mathf.Deg2Rad);
        speedVector.z = Speed.z * Mathf.Cos(angle * Mathf.Deg2Rad) - Speed.x * Mathf.Sin(angle * Mathf.Deg2Rad);

        

        return speedVector;
        
    }
 
    public Vector3 GetTorqueForce()
    {
        return ForceTorque;
    }
    public void SetTorqueForce(Vector3 torqueForce)
    {
        ForceTorque = torqueForce;
    }

    public Rigidbody GetHeadRigidbody()
    {
        return PartsOfDick[0].GetComponent<Rigidbody>();
    }

    private void Update()
    {
        positoinX = gameObject.transform.position.x;
    }

}
