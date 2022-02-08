using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int speed = 3;
    public int maxHealth = 5;
    private int curHealth;
    public int health { get { return curHealth; } }
    private Rigidbody2D regibody;
    private float horizontal;
    private float vertical;
    // 在第一次帧更新之前调用 Start
    void Start()
    {
        regibody = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
    }
    // 每帧调用一次 Update
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        regibody.MovePosition(position);
    }

    public void changeHealth(int count)
    {
        curHealth = Mathf.Clamp(curHealth + count, 0, maxHealth);
        Debug.Log(curHealth + "/" + maxHealth);
    }
}
