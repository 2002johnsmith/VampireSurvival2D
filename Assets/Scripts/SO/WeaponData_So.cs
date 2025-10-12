using UnityEngine;
[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Game Data/Weapon Data", order = 1)]
public class WeaponData_So : ScriptableObject
{
    public string _WeaponName;//nombre
    public Sprite _Icon;//iconoeneljuego
    public TypeWeapon _TypeWeapon;
    public GameObject _bullet; //lo que va disparar(inluye pal cuerpo a cuerpo)
    public int _IdWeapon;//referenciar en el juego
    public float _Damage;//daño base
    public float _Cooldown;//tiempo entre ataques
    public float _Speed;//velocidad del proyectil
    public int _count;//cuantos en rafaga
    public float _size;//tamaño
    //avanzado croe
    public float _range;
    public float _duration;
    public TypeTarget _target;
    public int Perfo;//cantidad de eemigos que puede chocar antes de morir
}
