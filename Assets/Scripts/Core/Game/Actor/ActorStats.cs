using System;


[Serializable]
public class ActorStats
{
    private float strength;
    private float agility;
    private float speed;
    private float sturdiness;
    private float mind;
    private float intelligence;
    private float perception;
    private float luck;

    public float Strength { get { return strength; } private set { strength = value; } }
    public float Agility { get { return agility; } private set { agility = value; } }
    public float Speed { get { return speed; } private set { speed = value; } }
    public float Sturdiness { get { return sturdiness; } private set { sturdiness = value; } }
    public float Mind { get { return mind; } private set { mind = value; } }
    public float Intelligence { get { return intelligence; } private set { intelligence = value; } }
    public float Perception { get { return perception; } private set { perception = value; } }
    public float Luck { get { return luck; } private set { luck = value; } }
}
