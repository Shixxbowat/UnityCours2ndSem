using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;

    private WeaponController weaponController;

    private void Start()
    {
        health = maxHealth;
        weaponController = GetComponent<WeaponController>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var player = FindObjectOfType<InputsReceiver>();

        var direction = player.transform.position - transform.position;
        weaponController.SetDirection(direction);
        if(player != null)
        {
            weaponController.Shooting = true;
            weaponController.Shoot();
            
        }
            
    }
}
