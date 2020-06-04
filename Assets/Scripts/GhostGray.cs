using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGray : MonoBehaviour
{
    private int Health = 3;
    private const int damageOfGhost = 1;
    public float speed = 1;
    const int changeDirection = -1;
    public bool detectPlayer = false;
    public float minX, maxX;
    Vector2 move;

    public GameObject bomb;
    public bool attacking = false;
    public const float timeAttack = 3f;//thời gian nghĩ
    private float attack = 0, sideOfBomb = 0.5f;
    private float power = 20f;
    public GameObject gem;

    private GameObject player;
    public float smoothtimeX = 0.5f;
    private Vector2 velocity;
    public Rigidbody2D r2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        r2 = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Health <= 0)
        {
            Gem();
            Destroy(gameObject);
        }

        SetDetectPlayer();

    }
    
    void FixedUpdate()
    {
        //r2.velocity = new Vector2(speed, 0);

        if (detectPlayer)
        {
            MoveToPlayer();
            Attack();
            SetEndAttack();
        }
        else
           FormTo();

    }
    
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
        
    //}
    //private void OnTriggerStay2D(Collider2D collider)
    //{
        
    //}
    //private void OnTriggerExit2D(Collider2D collider)
    //{
       
    //}

    private void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    private void FormTo()
    {
        if (transform.position.x < minX && speed < 0)
            speed *= changeDirection;
        if (transform.position.x > maxX && speed > 0)
            speed *= changeDirection;

        move = this.transform.position;
        move.x += speed * Time.deltaTime;
        this.transform.position = move;

    }

    void Damage(int damage)
    {
        Health -= damage;
    }
    private void SetDetectPlayer()
    {
        if((player.transform.position.x < this.transform.position.x + 6) 
            && (player.transform.position.x > this.transform.position.x - 6)
            && (player.transform.position.y < this.transform.position.y + 1) 
            && (player.transform.position.y > this.transform.position.y - 0.5f))
        {
            detectPlayer = true;
            SideOfPlayer();
        }
        else
            detectPlayer = false;
    }

    private void MoveToPlayer()
    {
        move = this.transform.position;
        if (player.transform.position.x > this.transform.position.x + 3)
        {
            move.x += 2 * Time.deltaTime;
            this.transform.position = move;
        }
        if (player.transform.position.x < this.transform.position.x - 3)
        {
            move.x += -2 * Time.deltaTime;
            this.transform.position = move;
        }
    }
    
    public void Bomb()
    {
        GameObject bombClone;
        bombClone = Instantiate(bomb,new Vector2(this.transform.position.x + sideOfBomb, this.transform.position.y), this.transform.rotation) as GameObject;
        bombClone.GetComponent<Rigidbody2D>().AddForce (VectorAttachPlayer());
    }

    private Vector2 VectorAttachPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        return direction * power;
    }
    private void SideOfPlayer()
    {
        if (player.transform.position.x > this.transform.position.x)
            sideOfBomb = 1f;
        else
             sideOfBomb = -1f;
    }

    public void Attack()
    {
        if (!attacking)
        {
            Bomb();
            attacking = true;
            attack = timeAttack;
        }
    }
    public void SetEndAttack()
    {
        if (attack > 0)
            attack -= Time.deltaTime;
        else
            attacking = false;
    }
    public void Gem()
    {
        GameObject gemClone;
        gemClone = Instantiate(gem, new Vector2(this.transform.position.x, this.transform.position.y), this.transform.rotation) as GameObject;
    }
}
