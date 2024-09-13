using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform cible; // Le joueur que la caméra doit suivre
    public float distance = 10.0f; // Distance initiale par rapport au joueur
    public float distanceMin = 5.0f; // Distance minimale
    public float distanceMax = 15.0f; // Distance maximale
    public float vitesseZoom = 1.0f; // Vitesse du zoom
    public float sensibilitéRotation = 4.0f; // Sensibilité de la rotation
    public float limiteRotationVerticaleMin = -20f; // Limite inférieure de la rotation verticale
    public float limiteRotationVerticaleMax = 60f; // Limite supérieure de la rotation verticale

    private float angleX = 0.0f; // Angle de rotation autour de l'axe X (horizontal)
    private float angleY = 0.0f; // Angle de rotation autour de l'axe Y (vertical)
    private bool tourneJoueur = false; // Indique si le joueur doit tourner

    void Start()
    {
        // Initialiser les angles avec la position actuelle de la caméra
        Vector3 angles = transform.eulerAngles;
        angleX = angles.y;
        angleY = angles.x;
    }

    void LateUpdate()
    {
        if (cible != null)
        {
            // Rotation de la caméra avec le mouvement de la souris
            if (Input.GetMouseButton(1)) // Bouton droit de la souris
            {
                angleX += Input.GetAxis("Mouse X") * sensibilitéRotation;
                angleY -= Input.GetAxis("Mouse Y") * sensibilitéRotation;

                // Activer la rotation du joueur
                tourneJoueur = true;
            }
            else if (Input.GetMouseButton(0)) // Bouton gauche de la souris
            {
                angleX += Input.GetAxis("Mouse X") * sensibilitéRotation;
                angleY -= Input.GetAxis("Mouse Y") * sensibilitéRotation;

                // Désactiver la rotation du joueur
                tourneJoueur = false;
            }

            // Limiter l'angle vertical pour éviter de regarder sous le sol ou au-dessus de la tête du joueur
            angleY = Mathf.Clamp(angleY, limiteRotationVerticaleMin, limiteRotationVerticaleMax);

            // Zoomer ou dézoomer avec la molette de la souris
            distance -= Input.GetAxis("Mouse ScrollWheel") * vitesseZoom;
            distance = Mathf.Clamp(distance, distanceMin, distanceMax);

            // Calculer la position et la rotation de la caméra
            Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + cible.position;

            // Appliquer la position et la rotation à la caméra
            transform.rotation = rotation;
            transform.position = position;

            // Si le joueur doit tourner, aligner sa rotation avec celle de la caméra
            if (tourneJoueur)
            {
                Vector3 directionVersCamera = transform.position - cible.position;
                directionVersCamera.y = 0; // Garder la rotation uniquement sur l'axe Y
                cible.rotation = Quaternion.LookRotation(-directionVersCamera);
            }
        }
    }
}
