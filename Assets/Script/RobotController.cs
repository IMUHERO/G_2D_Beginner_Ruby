using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public int speed;
    public bool isVertical;
    public ParticleSystem smokeEffect;
    // Start is called before the first frame update
    private Rigidbody2D rigibody;
    private float Timer;
    private float reverseTime = 3.0f;
    private int direction = 1;
    private Animator animator;
    private bool broken = true;

    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
        Timer = reverseTime;
        animator = GetComponent<Animator>();
        smokeEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = reverseTime;
            direction = -direction;
        }
    }

    void FixedUpdate()
    {
        if(!broken){
            return;
        }
        Vector2 position = rigibody.position;
        if (isVertical)
        {
            position.y = transform.position.y + direction * speed * Time.deltaTime;
            animator.SetFloat("MoveY", direction);
            animator.SetFloat("MoveX", 0);
        }
        else
        {
            position.x = transform.position.x + direction * speed * Time.deltaTime;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
        rigibody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        RubyController controller = other.gameObject.GetComponent<RubyController>();
        if(controller != null){
            controller.changeHealth(-1);
        }
    }

    public void Fix(){
        broken = false;
        rigibody.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}
