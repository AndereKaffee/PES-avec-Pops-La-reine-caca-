using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite hoverSprite;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnHoverEnter()
    {
        image.sprite = hoverSprite;
    }

    public void OnHoverExit()
    {
        image.sprite = normalSprite;
    }
}