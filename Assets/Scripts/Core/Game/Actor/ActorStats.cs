using System;
using UnityEngine;

[Serializable]
public class ActorStats
{

    [SerializeField] private float strength;
    [SerializeField] private float agility;
    [SerializeField] private float speed;
    [SerializeField] private float sturdiness;
    [SerializeField] private float mind;
    [SerializeField] private float intelligence;
    [SerializeField] private float perception;
    [SerializeField] private float luck;

    public float Strength { get { return strength; } private set { strength = value; } }
    
    public float Agility { get { return agility; } private set { agility = value; } }
    
    public float Speed { get { return speed; } private set { speed = value; } }
    
    public float Sturdiness { get { return sturdiness; } private set { sturdiness = value; } }
    
    public float Mind { get { return mind; } private set { mind = value; } }
    
    public float Intelligence { get { return intelligence; } private set { intelligence = value; } }
    
    public float Perception { get { return perception; } private set { perception = value; } }
    
    public float Luck { get { return luck; } private set { luck = value; } }
}
