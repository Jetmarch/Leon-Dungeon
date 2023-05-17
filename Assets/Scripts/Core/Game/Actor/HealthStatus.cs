using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthStatus
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    private Actor owner;
    
    public HealthStatus(Actor owner)
    {
        maxHealth = owner.stats.strength * 10;
        currentHealth = maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(amount + currentHealth, 0f, maxHealth);
    }

    public bool IsDead()
    {
        if(currentHealth <= 0f) return true;
        else return false;
    }

    public bool CanTakeActions()
    {
        //TODO: Добавить проверку на баффы и дебаффы. Если кто-то из них блокирует возможность что-то делать, то возвращаем false
        return true;
    }
}
