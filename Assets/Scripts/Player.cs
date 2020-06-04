using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 4, jumpPow = 2400f;
    public bool grounded = true, doubleJump = false;
    
    public bool onMovingPlatRL = false;
    public Vector2 vecterPlatRL; //vận tốc của địa hình

    public bool attacking = false;
    public const float timeAttackDelay = 0.7f;//thời gian nghĩ
    private float attackdelay = 0;
    public GameObject sword;
    public Transform shootPoint;

    public int ourHealth = 1, maxhealth = 5;
    bool wounding = false;//đang bị thương
    float timeWounding;//thời gian tái bị thương

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D r2;
    public Animator anim;

    public GameMaster gm;
    public Sounds sound;

    public float h = 0;// nhận hướng di chuyển trái phải

    private void Awake()
    {
       
    }

    // Use this for initialization
    void Start()
    {
        
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shootPoint = transform.Find("ShootPoint");
        ourHealth = maxhealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sounds>();
        

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));
        

        if (ourHealth <= 0) //kiểm tra máu
            Death();

        if (Input.GetKey(KeyCode.RightArrow))
            MovingRight();
        if (Input.GetKey(KeyCode.LeftArrow))
            MovingLeft();
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            Stop();

        if (Input.GetKeyDown(KeyCode.UpArrow))//nhảy
            Jump();

        if (Input.GetKeyDown(KeyCode.V))//tấn công
            Attack();

        
    }

    void FixedUpdate()
    {
        //h = Input.GetAxis("Horizontal");// nhận hướng di chuyển trái phải
        
        Move();
        
        SetEndAttack();
        SetEndWounding();

    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Gem"))
        {
            sound.Playsound("PlayerTakeGem");
            Destroy(col.collider.gameObject);
            gm.gem += 1;
            PlayerPrefs.SetInt("gem", gm.gem);
            gm.points += 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        grounded = true;
        doubleJump = false;

        if (collider.CompareTag("MovingPlat_RL"))//kiêmr tra địa hình để lấy tốc độ
        {
            onMovingPlatRL = true;
            vecterPlatRL = new Vector2(collider.attachedRigidbody.velocity.x, 0);
        }
        else
        {
            onMovingPlatRL = false;
            vecterPlatRL = new Vector2(0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        grounded = false;
        doubleJump = true;
    }

    public void Jump()
    {
        if (grounded)
        {
            doubleJump = true;
            r2.AddForce(Vector2.up * jumpPow);
        }
        else
        {
            if (doubleJump)
            {
                doubleJump = false;
                r2.velocity = new Vector2(r2.velocity.x, 0);
                r2.AddForce(Vector2.up * jumpPow);
            }
        }

    }

    public void Move()
    {
        if (h > 0)//Input.GetKeyDown(KeyCode.RightArrow)
        {
            //player.transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
            if (onMovingPlatRL)
                r2.velocity = new Vector2(vecterPlatRL.x + speed, r2.velocity.y);
            else
                r2.velocity = new Vector2(speed, r2.velocity.y);

            if (spriteRenderer.flipX)
                Flip();
        }

        if (h < 0)
        {
            //player.transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
            if (onMovingPlatRL)
                r2.velocity = new Vector2(vecterPlatRL.x - speed, r2.velocity.y);
            else
                r2.velocity = new Vector2(-speed, r2.velocity.y);

            if (!spriteRenderer.flipX)
                Flip();
        }

        if (h == 0)
        {
            if (onMovingPlatRL)
                r2.velocity = new Vector2(vecterPlatRL.x, r2.velocity.y);
            else
                r2.velocity = new Vector2(0, r2.velocity.y);
        }
    }
    public void MovingRight()
    {
        h = 1;
    }

    public void MovingLeft()
    {
        h = -1;
    }
    public void Stop()
    {
        h = 0;
    }

    public void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        /*Vector3 Scale;
        Scale = transform.localScale;
        
        Scale.x *= -1;
        transform.localScale = Scale;*/
    }

    public void Death()
    {
        if (PlayerPrefs.GetInt("highscore") < gm.points)
            PlayerPrefs.SetInt("highscore", gm.points);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void Damage(int damage)
    {
        if (!wounding)
        {
            ourHealth -= damage;
            wounding = true;
            timeWounding = 1;
            gameObject.GetComponent<Animation>().Play("Player_damage");
        }
    }

    public void Knockback(float Knockpow/*Vector2 Knockdir*/)
    {
            r2.velocity = new Vector2(r2.velocity.x, 0);
            r2.AddForce(new Vector2(0, jumpPow * Knockpow));
    }

    public void SetEndWounding()
    {
        if (timeWounding > 0)
        {
            timeWounding -= Time.deltaTime;
            //Debug.Log("timeStay = " + timeWounding);
        }
        else
        {
            wounding = false;
        }
    }

    public void Attack()
    {
        if (!attacking)
        {
            Sword();
            attacking = true;
            attackdelay = timeAttackDelay;
        }
    }

    public void SetEndAttack()
    {
        if (attackdelay > 0)
        {
            attackdelay -= Time.deltaTime;
        }
        else
        {
            attacking = false;
        }
    }

    public void Sword()
    {
        GameObject swordClone;
        swordClone = Instantiate(sword, new Vector2(shootPoint.position.x, shootPoint.position.y), this.transform.rotation) as GameObject;
        
        if (spriteRenderer.flipX)
        {
            swordClone.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
            swordClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 30));
        }
        else
        {
            swordClone.GetComponent<Transform>().Rotate(new Vector3(0, 0, -90));
            swordClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 30));
        }
    }
    
   





}
