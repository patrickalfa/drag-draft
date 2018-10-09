using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HERO_TYPE
{
    NONE,
    WARRIOR,
    ARCHER,
    MAGE
}

public class Hero : Character
{
    public HERO_TYPE type;
}