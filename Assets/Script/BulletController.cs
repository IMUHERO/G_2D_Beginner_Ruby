using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rd;
    public float shootForce = 300;
    // Start is called before the first frame update
    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    public void launch(Vector2 direction, float force){
        rd.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("bullet shoot object: " + other.gameObject);
        RobotController robotController = other.gameObject.GetComponent<RobotController>();
        if(robotController != null){
            robotController.Fix();
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
