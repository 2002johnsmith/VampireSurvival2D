using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnEnable()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.RegistrarEnemigo(transform);
        else
            StartCoroutine(RegisterNextFrame());
    }

    private IEnumerator RegisterNextFrame()
    {
        yield return null; 
        EnemyManager.Instance?.RegistrarEnemigo(transform);
    }

    private void OnDisable()
    {
        EnemyManager.Instance?.EliminarEnemigo(transform);
    }
}
