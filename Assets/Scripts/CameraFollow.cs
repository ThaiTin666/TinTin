using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothtimeX = 0f, smoothtimeY = 0.15f;
    public bool bound = true;
    public Vector2 minpos = new Vector2(0f, 0f), maxpos;

    private Vector2 velocity;//kiểu giá trị trong Mathf.SmoothDamp
    private GameObject player;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (transform.position.y > player.transform.position.y + 15)
            player.GetComponent<Player>().Death();
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(this.transform.position.x, player.transform.position.x, ref velocity.x, smoothtimeX);
        float posY = Mathf.SmoothDamp(this.transform.position.y, player.transform.position.y, ref velocity.y, smoothtimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bound) //luôn đúng
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minpos.x, maxpos.x),
                Mathf.Clamp(transform.position.y, minpos.y, maxpos.y),
                Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
        }
    }
}
