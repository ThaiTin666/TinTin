using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoint : MonoBehaviour
{

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if(player.spriteRenderer.flipX)
            transform.position = new Vector2(player.transform.position.x - 0.7f, player.transform.position.y);
        else
            transform.position = new Vector2(player.transform.position.x + 0.7f, player.transform.position.y);

    }
}
