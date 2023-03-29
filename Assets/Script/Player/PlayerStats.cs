using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int currentHealth;
    public int maxHealth = 3;
    public int damage;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        currentHealth = maxHealth;
    }
}
