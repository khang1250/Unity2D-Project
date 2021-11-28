using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    Vector3 direction;
    public float damage = 50;
    

    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerMoveControls.instance.transform.position - transform.position;
        direction.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player")) {
            PlayerStats.instance.TakeDamage(damage);
            Destroy(gameObject);
        }

    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
