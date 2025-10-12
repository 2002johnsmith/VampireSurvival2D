using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string WeaponName;
    public Sprite Icon;
    public GameObject bullet;
    public int IdWeapon;
    public float Damage;
    public float Cooldown;
    public float CurrentCooldown;
    public float Speed;
    public int count;
    public float size;
    public float duration;
    public TypeWeapon typeWeapon;
    public TypeTarget targetWeapon;

    public Transform currentTarget;
    public float targetSearchTimer;
    public float targetSearchCooldown = 0.02f;
    public WeaponData_So Data;
    public Transform player;




    public void SetData()
    {
        WeaponName = Data._WeaponName;
        Icon = Data._Icon;
        bullet = Data._bullet;
        IdWeapon = Data._IdWeapon;
        Damage = Data._Damage;
        Cooldown = Data._Cooldown;
        Speed = Data._Speed;    
        count = Data._count;
        size = Data._size;
        typeWeapon = Data._TypeWeapon;
        targetWeapon = Data._target;
        CurrentCooldown = Cooldown;
        duration = Data._duration;
    }


    public abstract void Attack();

    public virtual void WeaponShoot()
    {
        if (targetWeapon != TypeTarget.None) 
        {
            UpdateTarget();
        }

        CurrentCooldown -= Time.deltaTime;
        if (CurrentCooldown <= 0)
        {
            Attack();
            CurrentCooldown = Cooldown; 
        }
    }
    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
    protected virtual void UpdateTarget()
{
    targetSearchTimer -= Time.deltaTime;
    if (targetSearchTimer <= 0)
    {
        Transform previousTarget = currentTarget;
        currentTarget = FindTarget();
        targetSearchTimer = targetSearchCooldown;

        // DEBUG: Ver si cambió el target
        if (previousTarget != currentTarget)
        {
            Debug.Log($"Target cambiado: {(previousTarget != null ? previousTarget.name : "NULL")} -> {(currentTarget != null ? currentTarget.name : "NULL")}");
        }
    }

    // Si el target actual murió, buscar nuevo inmediatamente
    if (currentTarget != null && !currentTarget.gameObject.activeInHierarchy)
    {
        Debug.Log("Target murió, buscando nuevo...");
        currentTarget = FindTarget();
        targetSearchTimer = targetSearchCooldown; // Reset timer
    }
}
    protected virtual Transform FindTarget()
    {
        return EnemyManager.Instance.MoreClose(player);
    }
}
