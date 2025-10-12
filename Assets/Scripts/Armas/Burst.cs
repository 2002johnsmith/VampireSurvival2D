using UnityEngine;

public class Burst : WeaponBase
{
    public float RangeMax = 5f;
    public int currentShotCount = 0;
    public bool isFiring = false;
    public float FireCooldown = 0.1f;
    public float CurrentFireCooldown = 0f;

    protected override Transform FindTarget()
    {
        currentTarget = null;



        switch (targetWeapon)
        {
            case TypeTarget.Close:
                currentTarget = EnemyManager.Instance.MoreClose(player.transform, RangeMax);
                break;
            case TypeTarget.Random:
                currentTarget = EnemyManager.Instance.GetRandomEnemy();
                break;
            case TypeTarget.AngleBase:
                currentTarget = EnemyManager.Instance.MoreClose(player.transform, RangeMax);
                break;
            default:
                currentTarget = EnemyManager.Instance.MoreClose(player.transform, RangeMax);
                break;
        }

        Debug.Log($"Burst.FindTarget() -> {(currentTarget != null ? currentTarget.name : "NULL")}");
        return currentTarget;
    }
    public override void Attack()
    {
        UpdateTarget();

        if (currentTarget == null)
        {
            Debug.Log("No hay target para iniciar ráfaga");
            return;
        }

        if (!isFiring)
        {
            CurrentCooldown -= Time.deltaTime;
            if (CurrentCooldown <= 0)
            {
                StartFiring();
            }
        }
        else
        {
            HandleFiring();
        }
    }
    protected virtual void StartFiring()
    {
        isFiring = true;
        currentShotCount = 0;
        CurrentFireCooldown = 0f;
    }

    protected virtual void HandleFiring()
    {
        CurrentFireCooldown -= Time.deltaTime;
        if (CurrentFireCooldown <= 0 && currentShotCount < count)
        {
            FireSingleShot();
            currentShotCount++;
            CurrentFireCooldown = FireCooldown;
            if (currentShotCount >= count)
            {
                FinishFiring();
            }
        }
    }

    protected virtual void FireSingleShot()
    {
   
        UpdateTarget();

        GameObject bulletObj = Instantiate(bullet, player.position, Quaternion.identity);
        ProjectileBullet projectile = bulletObj.GetComponent<ProjectileBullet>();

        if (projectile != null)
        {
            // Usar Perfo del ScriptableObject
            projectile.InitializeProjectile(Damage, Speed, size, 3f, Data.Perfo);

            if (currentTarget != null)
            {
                projectile.SetTarget(currentTarget);
                Vector2 direction = (currentTarget.position - player.position).normalized;
                projectile.SetDirection(direction);
            }
        }
    }
    private Vector2 GetDefaultDirection()
    {
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            return playerManager.GetLastMovementDirection();
        }
        return Vector2.right;
    }
    protected virtual void FinishFiring()//termino la rafaga
    {
        isFiring = false;
        CurrentCooldown = Cooldown;
    }
}
