using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [SerializeField] private float _health;
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value <= 0) value = 0;
            onHealthChanged?.Invoke(_health, value);
            _health = value;
            if (_health == 0 && !isDeath)
            {
                OnDeath.Invoke();
                isDeath = true;
            }
        }
    }
    [SerializeField] protected bool isDeath = false;
    [SerializeField] protected UnityEvent<float, float> onHealthChanged;
    [SerializeField] protected UnityEvent OnDeath;

    public void addOnHealthChangeListener(UnityAction<float, float> action)
    {
        onHealthChanged.AddListener(action);
    }

    public virtual void TakeDamage(float damage)
    {
        health = health - damage;
    }
}
