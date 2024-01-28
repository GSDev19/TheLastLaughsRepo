using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // Array de prefabs
    public float shootingPower = 10f;
    public float cooldown = 1.5f;
    float lastShoot = -Mathf.Infinity; // Inicializado a un valor muy pequeño
    [SerializeField] private Transform projectileSpawnPoint;

    public void Shooting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time - lastShoot < cooldown)
            {
                return; // Todavía está en tiempo de recarga
            }

            lastShoot = Time.time;

            // Elegir aleatoriamente un índice para seleccionar un prefab
            int randomIndex = Random.Range(0, projectilePrefabs.Length);
            GameObject projectile = Instantiate(projectilePrefabs[randomIndex], projectileSpawnPoint.position, Quaternion.identity);
            
            // Configurar la velocidad del proyectil
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = new Vector2(shootingPower, 0f);
        }
    }
}
