using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipCharacter : Tooltip
{
    protected override void Hold()
    {
        Character character = GetComponent<Character>();
        Sprite sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
        string description = "";

        description += "HP: " + character.hp + "/" + character.maxHP + "\n";
        description += "Speed: " + character.speed + "\n";
        description += "Damage: " + character.damage + "\n";
        description += "Range: " + character.range;
        
        UIManager.instance.SetActive("PnlTooltip", true);
        UIManager.instance.ChangeText("PnlTooltip/TxtHeader", character.name);
        UIManager.instance.ChangeText("PnlTooltip/TxtDescription", description);
        UIManager.instance.ChangeImage("PnlTooltip/ImgIcon", sprite);
    }
}