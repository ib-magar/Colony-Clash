using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class bullet : MonoBehaviour
{

    public float speed = 10f;
    public float damage = 1f;
    public Vector2 direction = Vector2.right;
    public LayerMask collisionLayer;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init(Vector2 dir, float speed=10f)
    {
        direction = dir;
        this.speed = speed;
    }
    private void Update()
    {
        // Move the bullet in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);
        
       // transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + (direction * speed), speed * Time.deltaTime);
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            // Check if the collided object is the desired layer
            if (collision.collider.TryGetComponent(out LivingEntity _enemy))
            {
                _enemy.takeDamage(damage);
                Destroy(gameObject);
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision != null)
        {
            // Check if the collided object is the desired layer
            if (collision.TryGetComponent(out LivingEntity _enemy))
            {
                _enemy.takeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

}
