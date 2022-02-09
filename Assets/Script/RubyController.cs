using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int speed = 3;
    public int maxHealth = 5;
    public GameObject bulletPrefabe;
    private int curHealth;
    public int health { get { return curHealth; } }
    private Rigidbody2D rigibody;
    private float horizontal;
    private float vertical;
    private bool isInvinciple = true;
    private float damageTimer;
    private float invincipleTime = 2.0f;
    private Animator animator;
    private Vector2 lookDirection = new Vector2(1, 0);
    // 在第一次帧更新之前调用 Start
    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        damageTimer = invincipleTime;
        animator = GetComponent<Animator>();
    }
    // 每帧调用一次 Update
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        if (isInvinciple)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer < 0)
            {
                isInvinciple = false;
                damageTimer = invincipleTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            launch();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigibody.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
                NonPlayerCharacter npcController = hit.collider.gameObject.GetComponent<NonPlayerCharacter>();
                npcController.ShowDialogue();
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigibody.MovePosition(position);
    }

    public void changeHealth(int count)
    {
        if (!isInvinciple)
        {
            if (count < 0)
            {
                animator.SetTrigger("Hit");
            }
            curHealth = Mathf.Clamp(curHealth + count, 0, maxHealth);
            isInvinciple = true;
            UIHealthBar.instance.SetValue(curHealth / (float)maxHealth);
            Debug.Log("health: " + curHealth + "/" + maxHealth);
        }
    }

    void launch()
    {
        GameObject bullet = Instantiate(bulletPrefabe, rigibody.position + Vector2.up * 0.5f, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.launch(lookDirection, bulletController.shootForce);
        animator.SetTrigger("Launch");
    }
}
