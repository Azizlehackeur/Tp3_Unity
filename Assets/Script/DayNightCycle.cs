using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun; // Le Directional Light repr�sentant le soleil
    public float dayDuration = 120.0f; // Dur�e d'un cycle jour/nuit en secondes (2 minutes pour 24h)
    public float intensityAtNoon = 1.0f; // Intensit� maximale de la lumi�re � midi
    public float intensityAtNight = 0.0f; // Intensit� minimale de la lumi�re la nuit
    public float rotationSpeed = 360f / 24f; // Vitesse de rotation pour un cycle de 24 heures

    private float currentTime = 0.0f; // Temps actuel du cycle jour/nuit

    void Update()
    {
        // Avancer le temps en fonction de la dur�e du cycle
        currentTime += Time.deltaTime * (24.0f / dayDuration);
        if (currentTime >= 24.0f)
        {
            currentTime = 0.0f; // R�initialiser le temps apr�s 24 heures
        }

        // Calculer la rotation du soleil en fonction du temps actuel
        float sunRotation = (currentTime / 24.0f) * 360.0f - 90.0f;
        sun.transform.rotation = Quaternion.Euler(sunRotation, 170.0f, 0.0f);

        // Calculer l'intensit� de la lumi�re en fonction du temps
        if (currentTime <= 6.0f || currentTime >= 18.0f) // Nuit
        {
            sun.intensity = Mathf.Lerp(intensityAtNight, intensityAtNoon, Mathf.InverseLerp(0.0f, 6.0f, currentTime));
        }
        else // Jour
        {
            sun.intensity = Mathf.Lerp(intensityAtNight, intensityAtNoon, Mathf.InverseLerp(18.0f, 24.0f, currentTime));
        }
    }
}
