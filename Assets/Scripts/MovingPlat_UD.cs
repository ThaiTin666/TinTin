using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat_UD : MonoBehaviour
{
    public float speed = 2f;
    int changeDirection = -1;
    public float minY, maxY;
    

    public Rigidbody2D r2;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        r2.velocity = new Vector2(0, speed);

        FormTo();
        
    }

    private void FormTo()
    {
        
            if (transform.position.y < minY - 1 && speed < 0)
                speed *= changeDirection;
            if (transform.position.y > maxY - 1 && speed > 0)
                speed *= changeDirection;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
            
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
            
    }
}