using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
        public int damage = 1;
    void Update()
    {
     

    transform.Translate(Vector2.up * speed * Time.deltaTime);


    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameManager.instance.AddScore(1);
        }
        if (other.CompareTag("Boss"))
        {
          other.GetComponent<BossEnemy>().TakeDamage(damage);
    Destroy(gameObject);
    }
}

}
