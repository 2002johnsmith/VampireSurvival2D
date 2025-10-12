using System.Collections.Generic;
using UnityEngine;

public class Orbital : WeaponBase
{
    public List<GameObject> activeOrbitals = new List<GameObject>();
    public float currentRadius = 2f;
    public bool isActive = false;
    public float activeDuration;

    // ELIMINA: private float inactiveDuration; // No necesitamos esto

    public override void Attack()
    {
        if (!isActive)
        {
            StartOrbitalCycle();
        }
    }

    protected override Transform FindTarget()
    {
        return null; // Las orbitales no necesitan target individual
    }

    public override void WeaponShoot()
    {
        // IMPORTANTE: Llama al WeaponShoot base para manejar CurrentCooldown
        base.WeaponShoot();

        if (isActive)
        {
            // Mantener órbitas mientras está activo
            MaintainOrbits();
            activeDuration -= Time.deltaTime;

            if (activeDuration <= 0)
            {
                EndOrbitalCycle();
            }
        }
        // ELIMINADO: else con CurrentCooldown -= Time.deltaTime (ya lo hace la clase base)
    }

    private void StartOrbitalCycle()
    {
        isActive = true;
        activeDuration = duration; // Duración activas

        CreateOrbitals();
        Debug.Log($"Órbitas ACTIVADAS - Duración: {activeDuration}s");
    }

    private void EndOrbitalCycle()
    {
        isActive = false;
        // NO modificar CurrentCooldown aquí - ya se maneja en la clase base
        ClearOrbitals();
        Debug.Log("Órbitas DESACTIVADAS");
    }

    private void CreateOrbitals()
    {
        ClearOrbitals();

        Debug.Log($"Creando {count} órbitas");

        for (int i = 0; i < count; i++)
        {
            if (bullet == null)
            {
                Debug.LogError("❌ No hay prefab de bala asignado para la órbita");
                continue;
            }

            GameObject orbital = Instantiate(bullet, player.position, Quaternion.identity);
            OrbitalBullet orbitalBehavior = orbital.GetComponent<OrbitalBullet>();

            if (orbitalBehavior != null)
            {
                orbitalBehavior.InitializeOrbital(player, i, count, currentRadius, Damage, size, Speed);
                Debug.Log($"✅ Órbita {i + 1} creada");
            }
            else
            {
                Debug.LogError("❌ El prefab de bala no tiene componente OrbitalBullet");
            }

            activeOrbitals.Add(orbital);
        }
    }

    private void MaintainOrbits()
    {
        // Limpiar órbitas nulas
        for (int i = activeOrbitals.Count - 1; i >= 0; i--)
        {
            if (activeOrbitals[i] == null)
            {
                activeOrbitals.RemoveAt(i);
            }
        }

        // Recrear si faltan (por ejemplo, si fueron destruidas)
        if (activeOrbitals.Count < count)
        {
            Debug.Log($"Recreando órbitas faltantes: {activeOrbitals.Count}/{count}");
            CreateOrbitals();
        }
    }

    private void ClearOrbitals()
    {
        foreach (GameObject orbital in activeOrbitals)
        {
            if (orbital != null)
            {
                Destroy(orbital);
            }
        }
        activeOrbitals.Clear();
        Debug.Log("Órbitas limpiadas");
    }

    // NUEVO: Debug para ver el estado
    private float debugTimer = 0f;
    void Update()
    {
        debugTimer += Time.deltaTime;
        if (debugTimer >= 2f)
        {
            debugTimer = 0f;
            Debug.Log($"🌀 {WeaponName} | Activa: {isActive} | " +
                     $"Duración: {activeDuration:F1}s | Cooldown: {CurrentCooldown:F1}s | " +
                     $"Órbitas: {activeOrbitals.Count}/{count}");
        }
    }
}