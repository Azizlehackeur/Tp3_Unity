using UnityEngine;

public class DeplacementObjet : MonoBehaviour
{
    public float vitesse = 5f;
    public float forceSaut = 7f;
    private bool estAuSol = true;
    private Rigidbody rb;
    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Chercher l'Animator dans l'enfant
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Aucun Animator trouvé dans les enfants de Player");
        }
    }

    void Update()
    {
        float deplacementHorizontal = Input.GetAxis("Horizontal");
        float deplacementVertical = Input.GetAxis("Vertical");

        Vector3 mouvement = new Vector3(deplacementHorizontal, 0f, deplacementVertical);

        if (mouvement.magnitude > 0.1f)
        {
            // Rotation du personnage dans la direction du mouvement
            Quaternion nouvelleRotation = Quaternion.LookRotation(mouvement);
            transform.rotation = Quaternion.Slerp(transform.rotation, nouvelleRotation, 0.15f);

            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        // Appliquer le mouvement au personnage
        transform.Translate(mouvement * vitesse * Time.deltaTime, Space.World);

        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.Space) && estAuSol)
        {
            rb.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
            animator.SetBool("IsJumping", true);
            estAuSol = false;
        }

        // Gestion du combat avec la touche V
        if (Input.GetKeyDown(KeyCode.V)) // Si la touche V est pressée
        {
            animator.SetBool("IsFighting", true);
        }
        else if (Input.GetKeyUp(KeyCode.V)) // Si la touche V est relâchée
        {
            animator.SetBool("IsFighting", false);
        }

        // Afficher l'état actuel de l'Animator
        if (animator != null)
        {
            Debug.Log($"IsRunning défini à : {animator.GetBool("IsRunning")}");
            Debug.Log($"IsJumping défini à : {animator.GetBool("IsJumping")}");
            Debug.Log($"IsFighting défini à : {animator.GetBool("IsFighting")}");
        }
        else
        {
            Debug.LogError("Animator est null!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
        {
            estAuSol = true;
            animator.SetBool("IsJumping", false); // Réinitialise l'état de saut lorsque le personnage atterrit
        }
    }
}
