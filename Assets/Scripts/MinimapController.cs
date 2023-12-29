using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Sprite normalIcon;
    public Sprite yellowIcon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            Vector3 roomPosition = other.transform.position;

            SpriteRenderer iconRenderer = GetComponent<SpriteRenderer>();
            iconRenderer.sprite = yellowIcon;
        }
    }
}