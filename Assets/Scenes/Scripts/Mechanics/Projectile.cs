using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float lifetime;

    // This is meant to be modifiied by the object creating the projectile.
    // Eg. the shoot script
    [HideInInspector]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
        {
            lifetime = 2.0f;

            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            Destroy(gameObject, lifetime);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Stone")
        {
            Destroy(gameObject);
            Debug.Log("IT WORKS");
        }
        Debug.Log(collision.gameObject.name);
    }
}
