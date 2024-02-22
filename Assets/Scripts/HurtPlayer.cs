using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HurtPlayer : MonoBehaviour
{
    public Image[] hearts;
    public AudioClip damageSound;
    [SerializeField] private GameObject deadScreen;

    void Start()
    {
        hearts = GetComponentsInChildren<Image>();
        deadScreen.SetActive(false);
    }

    public void Hurt()
    {
        if (hearts.Length > 0)
        {
            Image heart = hearts.Last();
            Destroy(heart);

            List<Image> tempHearts = hearts.ToList();
            tempHearts.RemoveAt(tempHearts.Count - 1);
            hearts = tempHearts.ToArray();
        }

        if (hearts.Length <= 0)
        {
            deadScreen.SetActive(true);
            if (UIManager.Instance)
            {
                UIManager.Instance.pickedWeapon.SetActive(false);
            }
            Time.timeScale = 0;
        }
    }
}