using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipCharacter : Tooltip
{
    protected override void Hold()
    {
        Character character = GetComponent<Character>();
        Sprite sprite = character.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
        string description = "";

        description += "HP: " + character.hp + "/" + character.maxHP + "\n";
        description += "Speed: " + character.speed + "\n";
        description += "Damage: " + character.damage + "\n";
        description += "Range: " + character.range;
        
        UIManager.instance.SetActive("Panel", true);
        UIManager.instance.ChangeText("Panel/TxtHeader", character.name);
        UIManager.instance.ChangeText("Panel/TxtDescription", description);
        UIManager.instance.ChangeImage("Panel/ImgIcon", sprite);
    }
}