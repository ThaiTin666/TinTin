using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //public Rigidbody2D r2;

    //private float speedToRotate = 0;
    //public bool chem = true;
    //public float maxUp = 0f, maxDown = -0.8f;
    private bool flip;

    private GameObject player;
    // public float smoothtimeX = 0.0f, smoothtimeY = 0.0f;

    
    public Sounds sound;
    // Start is called before the first frame update
    void Start()
    {
        //r2 = gameObject.GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        flip = player.GetComponent<SpriteRenderer>().flipX;

        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sounds>();

    }

    // Update is called once per frame
    void Update()
    {
        if (flip != player.GetComponent<SpriteRenderer>().flipX)
            Destroy(gameObject);
        
    }
    void FixedUpdate()
    {
        //speedToRotate += speedToRotate;
        //transform.Rotate(new Vector3(0, 0, speedToRotate * Time.deltaTime));
        //Debug.Log("transform.localRotation.z  = " + transform.localRotation.z);//MoveWithPlayer();
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.collider.CompareTag("Ground") || col.collider.CompareTag("MovingPlat_RL"))// || col.collider.CompareTag("Player"))
        if (col.collider.CompareTag("Enemy"))
        {
            col.collider.SendMessageUpwards("Damage", 1);
            
        }
            
        if (col.collider.CompareTag("Bomb"))
        {
            sound.Playsound("SwordvsBomb");
            Destroy(col.collider.gameObject);
        }
        if (!col.collider.CompareTag("Player"))
            Destroy(gameObject);

    }
    private void OnCollisionStay2D(Collision2D col)
    {
        //if (col.collider.CompareTag("Ground") || col.collider.CompareTag("MovingPlat_RL"))// || col.collider.CompareTag("Player"))
        
    }

    /*private void MoveWithPlayer()
    {
        float posX = Mathf.SmoothDamp(this.transform.position.x, player.transform.position.x + SideOfSword(), ref velocity.x, smoothtimeX);
        float posY = Mathf.SmoothDamp(this.transform.position.y, player.transform.position.y, ref velocity.y, smoothtimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
        
    }
    private float SideOfSword()
    {
        if (player.GetComponent<SpriteRenderer>().flipX == false)
        {
            speedToRotate = -180;
            return 0.7f;
        }
        else
        {
            speedToRotate = 180;
            return -0.7f;
        }
        
    }*/
    

}
