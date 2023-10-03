using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Sprite normalIcon;
    public Sprite yellowIcon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room")) // Přizpůsobte tagy místností podle vašeho projektu
        {
            // Získejte pozici místnosti a umístěte ikonku na minimapu
            Vector3 roomPosition = other.transform.position;
            // Nastavte pozici ikonky na minimapě na roomPosition

            // Změňte barvu ikonky na žlutou
            SpriteRenderer iconRenderer = GetComponent<SpriteRenderer>();
            iconRenderer.sprite = yellowIcon;
        }
    }
}