using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TMP_Text _bulletsText;

    void Start()
    {
        _bulletsText.text = "Ammo: 50";
    }

    public void SetBullets(int p_bullets)
    {
        _bulletsText.text = $"Ammo: {p_bullets}";
    }
    
}
