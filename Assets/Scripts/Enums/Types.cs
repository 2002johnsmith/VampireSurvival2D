using UnityEngine;

public enum TypeWeapon//tipo de arma y comportamiento
{
    Burst,
    Orbital,
    Area,
    Pasive
}
public enum TypeUpgrade//que mejora del arma
{
    Damage,
    Cooldown,
    ProyectileCount,
    ProyectileSize,
    ProyectileSpeed,
    Penetration,
    Duration,
}
public enum TypeReward//tipos de experiencia y que accion si la tocas
{
    Small,
    Medium,
    Large,
    Healt,
    Chest
}
public enum TypeTarget{
    None,
    Close, //mascercano
    Random,//aleatorio
    AngleBase//seria de la ultima direcion en la que fue el jugador
}