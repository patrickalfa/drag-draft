using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipCard : Tooltip
{
    protected override void Hold()
    {
        Card card = GetComponent<Card>();
        Sprite sprite = card.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
        
        UIManager.instance.SetActive("PnlTooltip", true);
        UIManager.instance.ChangeText("PnlTooltip/TxtHeader", "COST: " + card.cost + " AP");
        UIManager.instance.ChangeText("PnlTooltip/TxtDescription", card.description);
        UIManager.instance.ChangeImage("PnlTooltip/ImgIcon", sprite);
    }
}