using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    public float damage;
    public float speed;
    public float size;
    public float duration;
    public float currentduration;


    public virtual void Initialize(float dmg, float spd, float sz, float dur = 0f)
    {
        damage = dmg;
        speed = spd;
        size = sz;
        duration = dur;
        currentduration = duration;
        transform.localScale = Vector3.one * size;
    }

    protected virtual void Update()
    {
        if (duration > 0)
        {
            currentduration -= Time.deltaTime;
            if (currentduration <= 0)
            {
                DestroyBullet();
            }
        }
    }
    protected abstract void Move();
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                OnHitEnemy(enemy);
            }
        }
    }
    protected virtual void OnHitEnemy(Enemy enemy)
    {
        // Comportamiento específico al golpear enemigo
    }

    protected virtual void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
