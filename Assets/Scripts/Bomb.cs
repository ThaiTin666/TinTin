using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody2D r2;
    private int damage = 1;
    //public float speedToRotate = 1;

    public float lifetime;

    public bool end = false;

    public Sounds sound;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();

        lifetime = Random.Range(1.0f,2.0f);

        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void FixedUpdate()
    {
        gameObject.GetComponent<Animator>().SetBool("End", end);
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
        else
            if(lifetime < 0.3 && end == false) 
        {
            end = true;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, 0);
            //gameObject.GetComponent<CircleCollider2D>().radius = 1.42f;
            sound.Playsound("BombExplosion");

        }
            
                
        
        //r2.MoveRotation(r2.rotation - speedToRotate);//xoay và di chuyển do ma sát: friction force

        //r2.velocity = new Vector2(x, r2.velocity.y);

        //Debug.Log("transform.localRotation.z  = " + r2.rotation);

    }
   
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.CompareTag("Player") && end)
        {
            collider.SendMessageUpwards("Damage", damage);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
    
    }
}
