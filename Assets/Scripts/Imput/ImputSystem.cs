using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ImputSystem : MonoBehaviour
{
    public static event Action<Vector2> OnMove;
    
    public void Movement(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }
}
