using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100; // Santé maximale
    public int currentHealth;   // Santé actuelle

    public Image healthBarFill; // Référence à l'image de la barre de vie (couleur qui diminue)

    public int damageFromMonster = 10; // Dégâts infligés par un monstre
    public float damageInterval = 5f; // Intervalle de temps entre chaque dégât en secondes

    private Animator animator;
    private bool isTakingDamage = false; // Flag pour savoir si le joueur prend des dégâts
    private float nextDamageTime = 0f; // Temps pour le prochain dégât

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHealthBar();
        Debug.Log(gameObject.name + " Initial Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        Debug.Log(gameObject.name + " Health after damage: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
            Debug.Log("Health bar updated. Fill amount: " + healthBarFill.fillAmount);
        }
        else
        {
            Debug.LogError("Health Bar Fill is not assigned!");
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    // Utilise OnCollisionStay pour infliger des dégâts en continu
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mechant") && Time.time >= nextDamageTime)
        {
            TakeDamage(damageFromMonster);
            nextDamageTime = Time.time + damageInterval; // Programme les prochains dégâts
            Debug.Log("Personnage touché par un monstre. Dégâts infligés : " + damageFromMonster);
        }
    }

    // Méthode de test pour infliger des dégâts manuellement (en appuyant sur la touche Espace)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
}
