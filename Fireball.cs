using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float damage = 10;
    void FixedUpdate()
    {
        MoveFixdUpdate();
    }
    private void MoveFixdUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

   
    private void DestroyFireball()
    {
      Destroy(gameObject);
    }
    private void Start()
    {
        Invoke("DestroyFireball", lifetime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        DamageEnemy(collision);
        DestroyFireball();
    }
    
    private void DamageEnemy(Collision collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(damage);
        }
    }
}
