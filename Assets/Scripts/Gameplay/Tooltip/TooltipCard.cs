using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipCard : Tooltip
{
    protected override void Hold()
    {
        Card card = GetComponent<Card>();
        Sprite sprite = card.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
        
        UIManager.instance.SetActive("Panel", true);
        UIManager.instance.ChangeText("Panel/TxtHeader", "COST: " + card.cost + " AP");
        UIManager.instance.ChangeText("Panel/TxtDescription", card.description);
        UIManager.instance.ChangeImage("Panel/ImgIcon", sprite);
    }
}