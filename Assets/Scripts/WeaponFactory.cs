using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    private static Dictionary<string, WeaponData_So> weaponCache = new Dictionary<string, WeaponData_So>();
    private static bool isInitialized = false;

    // MÉTODO MÁS CONFIABLE - llamar manualmente desde GameManager
    public static void InitializeFactory()
    {
        if (isInitialized) return;

        weaponCache.Clear();

        // Cargar todas las armas desde Resources
        WeaponData_So[] allWeapons = Resources.LoadAll<WeaponData_So>("Weapons");

        if (allWeapons.Length == 0)
        {
            Debug.LogError("WeaponFactory: No se encontraron armas en Resources/Weapons/");
            return;
        }

        foreach (var weapon in allWeapons)
        {
            if (weapon != null && !string.IsNullOrEmpty(weapon._WeaponName))
            {
                if (!weaponCache.ContainsKey(weapon._WeaponName))
                {
                    weaponCache.Add(weapon._WeaponName, weapon);
                    Debug.Log($"WeaponFactory: Cargada {weapon._WeaponName}");
                }
            }
        }

        isInitialized = true;
        Debug.Log($"WeaponFactory: Inicializado con {weaponCache.Count} armas");
    }

    public static WeaponBase CreateWeapon(WeaponData_So weaponData, Transform parent)
    {
        if (weaponData == null)
        {
            Debug.LogError("WeaponFactory: weaponData es null");
            return null;
        }

        // Asegurar que la factory esté inicializada
        if (!isInitialized)
        {
            InitializeFactory();
        }

        GameObject weaponObj = new GameObject(weaponData._WeaponName + "_Weapon");
        weaponObj.transform.SetParent(parent);
        weaponObj.transform.localPosition = Vector3.zero;

        WeaponBase weapon = null;

        switch (weaponData._TypeWeapon)
        {
            case TypeWeapon.Burst:
                weapon = weaponObj.AddComponent<Burst>();
                break;
            case TypeWeapon.Orbital:
                weapon = weaponObj.AddComponent<Orbital>();
                break;
            case TypeWeapon.Area:
               // weapon = weaponObj.AddComponent<AreaWeapon>();
                break;
            default:
                Debug.LogWarning($"Tipo de arma no reconocido: {weaponData._TypeWeapon}, usando Burst por defecto");
                weapon = weaponObj.AddComponent<Burst>();
                break;
        }

        // Asignar datos
        weapon.Data = weaponData;
        weapon.SetData();
        weapon.SetPlayer(parent); // El parent debería ser el Player

        Debug.Log($"WeaponFactory: Creada arma {weaponData._WeaponName}");

        return weapon;
    }

    public static WeaponData_So GetWeaponDataByName(string weaponName)
    {
        // Asegurar inicialización
        if (!isInitialized)
        {
            InitializeFactory();
        }

        if (weaponCache.ContainsKey(weaponName))
            return weaponCache[weaponName];

        Debug.LogError($"WeaponFactory: No se encontró el arma '{weaponName}'");

        // Intentar cargarla manualmente como fallback
        WeaponData_So weapon = Resources.Load<WeaponData_So>($"Weapons/{weaponName}");
        if (weapon != null)
        {
            weaponCache.Add(weaponName, weapon);
            return weapon;
        }

        return null;
    }

    // NUEVO: Método para debug
    public static void PrintLoadedWeapons()
    {
        Debug.Log($"=== WEAPONS LOADED ({weaponCache.Count}) ===");
        foreach (var weapon in weaponCache)
        {
            Debug.Log($"- {weapon.Key} ({weapon.Value._TypeWeapon})");
        }
    }
}
