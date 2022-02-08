using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
   public int speed = 3;
   // 在第一次帧更新之前调用 Start
   void Start()
   { 
   }
   // 每帧调用一次 Update
   void Update()
   {
      float horizontal = Input.GetAxis("Horizontal");
      float vertical = Input.GetAxis("Vertical");
      // Debug.Log(horizontal);
      Vector2 position = transform.position;
      position.x = position.x + speed * horizontal * Time.deltaTime;
      position.y = position.y + speed * vertical * Time.deltaTime;
      transform.position = position;
   }
}
