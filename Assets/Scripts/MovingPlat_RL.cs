using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat_RL : MonoBehaviour
{
    public float speed = 2f;
    const int changeDirection = -1;
    public float minX, maxX;
    //Vector2 Move;

    public Rigidbody2D r2;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        //Move = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        r2.velocity = new Vector2(speed, 0);
        //Move.x += speed *Time.deltaTime;
        //this.transform.position = Move;

        FormTo();
    }
    
    private void FormTo()
    {
            if (transform.position.x < minX - 1 && speed < 0)
                speed *= changeDirection;
            if (transform.position.x > maxX - 1 && speed > 0)
                speed *= changeDirection;
    }
    
}
