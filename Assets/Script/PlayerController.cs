using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int damageToEnemy = 20; // Dégâts infligés à l'ennemi
    public Transform enemy; // Référence à l'ennemi
    private bool canDealDamage = false; // Variable pour contrôler si le joueur peut infliger des dégâts

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mechant") && canDealDamage)
        {
            MechantScript enemyScript = other.GetComponent<MechantScript>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damageToEnemy);
                Debug.Log("L'ennemi a pris " + damageToEnemy + " de dégâts !");
                canDealDamage = false; // Désactiver les dégâts après les avoir infligés
            }
        }
    }

    void Update()
    {
        // Si j'appuie sur "V", le joueur peut infliger des dégâts
        if (Input.GetKeyDown(KeyCode.V))
        {
            canDealDamage = true;

            if (enemy != null)
            {
                MechantScript enemyScript = enemy.GetComponent<MechantScript>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damageToEnemy);
                    Debug.Log("L'ennemi a pris " + damageToEnemy + " de dégâts !");
                }
            }
        }
    }
}
