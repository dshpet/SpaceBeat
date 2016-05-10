﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

  public float speed = 10.0F;
  public float rotationSpeed = 10.0F;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    //float translation = Input.GetAxis("Vertical") * speed;
    //float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
    //translation *= Time.deltaTime;
    //rotation *= Time.deltaTime;
    transform.Translate(0, 0, speed * Time.deltaTime);
    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
  }
}
