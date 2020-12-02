using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDick : MonoBehaviour
{
    [SerializeField] private Dick Dick;
    private Rigidbody HeadRigidbody;
    public Rigidbody Rigidbody;
    void Start()
    {
        Dick = GetComponent<Dick>();
    }

    public void MoveHead(string direction)
    {
        if (!HeadRigidbody)
        {
            HeadRigidbody = Dick.GetHeadRigidbody();

        }
        switch (direction)
        {
            case "left":
                 HeadRigidbody.AddTorque(-Dick.GetTorqueForce(), ForceMode.Force);
              //  HeadRigidbody.AddForce(Dick.GetSpeedVector(-45), ForceMode.Impulse);
                //   HeadRigidbody.velocity = Dick.GetSpeedVector(45);

                break;

            case "right":
                   HeadRigidbody.AddTorque(Dick.GetTorqueForce(), ForceMode.Force);
             //  HeadRigidbody.AddForce(Dick.GetSpeedVector(45), ForceMode.Impulse);
                //  HeadRigidbody.velocity=Dick.GetSpeedVector(-45);



                break;

            case "forward":

                  HeadRigidbody.AddForce(Dick.GetSpeedVector(0), ForceMode.VelocityChange);
                //  HeadRigidbody.velocity = Dick.GetSpeedVector(180);

             //   HeadRigidbody.AddForce(new Vector3(-10, 0, 0), ForceMode.VelocityChange);


                break;

            case "back":
                HeadRigidbody.AddForce( Dick.GetSpeedVector(180), ForceMode.VelocityChange);
                //HeadRigidbody.velocity = Dick.GetSpeedVector(0);
               // HeadRigidbody.AddForce(new Vector3(10,0,0), ForceMode.VelocityChange);

                break;
        }
    }



}
