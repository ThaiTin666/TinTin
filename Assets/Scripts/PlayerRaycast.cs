using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float shootdelay = 0, damage = 1;
    public LayerMask Whattohit;
    public LayerMask Whattohit1;
    public Transform firepoint;

    // Use this for initialization
    void Start()
    {
        firepoint = transform.Find("ShootPoint");
    }

    // Update is called once per frame
    void Update()
    {
        shootdelay += Time.deltaTime;
        Shot();
        if (shootdelay >= 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                shootdelay = 0;
                //shot();
            }
        }
    }

    void Shot()
    {
        Vector2 mousePos = new Vector2
             (Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
             Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firepointpos = new Vector2(firepoint.position.x, firepoint.position.y);

        RaycastHit2D hit1 = Physics2D.Raycast(firepointpos, (mousePos - firepointpos), 50f, Whattohit1);
        //Debug.DrawLine(firepointpos, hit1.point, Color.cyan);

        RaycastHit2D hit = Physics2D.Raycast(firepointpos, (mousePos - firepointpos), 50f, Whattohit);
        
        if (hit.collider != null)
        {
            //Debug.DrawLine(firepointpos, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name);
            //hit.collider.SendMessageUpwards("Damage", damage);
        }
        
            

    }
}
