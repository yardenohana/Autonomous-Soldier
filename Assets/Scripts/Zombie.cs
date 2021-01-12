using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    // start health score
    public const int MaxHealth = 100;

    private Vector3 startPosition;
    private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        this.currentHealth = MaxHealth;
    }


    /*
     * Input: the amount of damage being caused from the shot.
     * Output: void
     */
    public void RecieveShot(int damage)
    {
        ApplyDamage(damage);
    }


    /*
     * The function removes the damage from the agents' health.
     * Input: the amount of damage
     * Output: void
     */
    private void ApplyDamage(int damage)
    {
        this.currentHealth -= damage;

        if (this.currentHealth <= 0)
        {
            Die();
        }
    }


    /*
     * Our player will be respawned at
     * a specific start location in the arena.
     */
    private void Respawn()
    {
        transform.position = this.startPosition;
        this.currentHealth = MaxHealth; // set health to the maximum
    }


    /*
     * In case of dying, our zombie will respawn.
     */
    private void Die()
    {
        Debug.Log("I died!");
        Respawn();
    }


    #region Debug
    //As a testing function
    public void OnMouseDown()
    {
        RecieveShot(MaxHealth);
    }
    #endregion
}
