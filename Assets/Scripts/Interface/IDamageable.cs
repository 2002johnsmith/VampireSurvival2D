using UnityEngine;

public interface IDamageable
{
    float MaxHealth {  get;}
    float CurrentHealth { get;}
    void TakeDamage(float Damage);
    void SetDamageable(float Health);
}
