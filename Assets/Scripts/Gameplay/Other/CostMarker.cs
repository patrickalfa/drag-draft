using UnityEngine;

public class CostMarker : MonoBehaviour
{
    [SerializeField]
    private Card _card;
    private SpriteRenderer _sprite;
    private SpriteRenderer _cardSprite;

    protected virtual void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _cardSprite = _card.transform.Find("Sprite").GetComponent<SpriteRenderer>();

        UpdateCost();
    }

    protected virtual void Update()
    {
        _sprite.sortingOrder = _cardSprite.sortingOrder + 1;
    }

    public void UpdateCost()
    {
        int costID = (_card.cost >= 0 ? _card.cost : GraphicsManager.instance.costSprites.Length - 1);
        _sprite.sprite = GraphicsManager.instance.costSprites[costID];
    }
}
        