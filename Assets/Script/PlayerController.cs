using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int damageToEnemy = 20; // D�g�ts inflig�s � l'ennemi
    public Transform enemy; // R�f�rence � l'ennemi
    private bool canDealDamage = false; // Variable pour contr�ler si le joueur peut infliger des d�g�ts

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mechant") && canDealDamage)
        {
            MechantScript enemyScript = other.GetComponent<MechantScript>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damageToEnemy);
                Debug.Log("L'ennemi a pris " + damageToEnemy + " de d�g�ts !");
                canDealDamage = false; // D�sactiver les d�g�ts apr�s les avoir inflig�s
            }
        }
    }

    void Update()
    {
        // Si j'appuie sur "V", le joueur peut infliger des d�g�ts
        if (Input.GetKeyDown(KeyCode.V))
        {
            canDealDamage = true;

            if (enemy != null)
            {
                MechantScript enemyScript = enemy.GetComponent<MechantScript>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damageToEnemy);
                    Debug.Log("L'ennemi a pris " + damageToEnemy + " de d�g�ts !");
                }
            }
        }
    }
}
