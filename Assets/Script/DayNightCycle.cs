using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun; // Le Directional Light représentant le soleil
    public float dayDuration = 120.0f; // Durée d'un cycle jour/nuit en secondes (2 minutes pour 24h)
    public float intensityAtNoon = 1.0f; // Intensité maximale de la lumière à midi
    public float intensityAtNight = 0.0f; // Intensité minimale de la lumière la nuit
    public float rotationSpeed = 360f / 24f; // Vitesse de rotation pour un cycle de 24 heures

    private float currentTime = 0.0f; // Temps actuel du cycle jour/nuit

    void Update()
    {
        // Avancer le temps en fonction de la durée du cycle
        currentTime += Time.deltaTime * (24.0f / dayDuration);
        if (currentTime >= 24.0f)
        {
            currentTime = 0.0f; // Réinitialiser le temps après 24 heures
        }

        // Calculer la rotation du soleil en fonction du temps actuel
        float sunRotation = (currentTime / 24.0f) * 360.0f - 90.0f;
        sun.transform.rotation = Quaternion.Euler(sunRotation, 170.0f, 0.0f);

        // Calculer l'intensité de la lumière en fonction du temps
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
