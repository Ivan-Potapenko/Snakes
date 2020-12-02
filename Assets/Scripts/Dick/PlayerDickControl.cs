using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDickControl : MonoBehaviour
{
    [SerializeField] private MoveDick MoveDick;

    void Start()
    {
        MoveDick = GetComponent<MoveDick>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            MoveDick.MoveHead("forward");
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            MoveDick.MoveHead("back");
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveDick.MoveHead("left");
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            MoveDick.MoveHead("right");
        }
    }
}
