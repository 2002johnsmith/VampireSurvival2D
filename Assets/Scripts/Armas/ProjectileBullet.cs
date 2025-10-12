using UnityEngine;

public class ProjectileBullet : BulletBase
{
    protected Vector2 direction;
    protected Transform target;
    protected int currentPenetrations = 0;
    protected int maxPenetrations = 0;

    public void InitializeProjectile(float dmg, float spd, float sz, float dur, int penetrations)
    {
        Initialize(dmg, spd, sz, dur);
        maxPenetrations = penetrations;
        currentPenetrations = 0;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Rotar el sprite hacia la dirección
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    protected override void Move()
    {
        // Si tenemos target y está vivo, seguirlo
        if (target != null && target.gameObject.activeInHierarchy)
        {
            direction = (target.position - transform.position).normalized;
        }

        // Movimiento lineal
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    protected override void Update()
    {
        Move();
        base.Update(); 
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                OnHitEnemy(enemy);

                // MANEJAR PERFORACIÓN
                currentPenetrations++;
                if (currentPenetrations >= maxPenetrations)
                {
                    DestroyBullet();
                }
            }
        }

        if (other.CompareTag("Wall"))
        {
            DestroyBullet();
        }
    }

    protected override void OnHitEnemy(Enemy enemy)
    {
        // Efectos al golpear enemigo (partículas, sonido, etc.)
        // Pero NO destruir la bala automáticamente (depende de la perforación)
    }
}
