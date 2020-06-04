using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public Player player;
    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            player.Damage(1);
            player.Knockback(1f/* player.transform.position*/);
            
        }
        if (collider.CompareTag("Enemy"))
            collider.SendMessageUpwards("Damage", 100);

    }
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            player.Damage(1);
            player.Knockback(1f/* player.transform.position*/);

        }
        if (collider.CompareTag("Enemy"))
            collider.SendMessageUpwards("Damage", 100);
    }



    }
