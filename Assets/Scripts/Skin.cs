using UnityEngine;
using System;
[Serializable]
public class Skin
{
    public int skinIndex;
    public float attackDamage = 1f;
    public float health = 1f;
    public float stamina = 1f;
    public float criticalDamage = 1f;
    public float attackSpeed = 1f;
    public float protection = 1f;
    public int price = 100;
    public Sprite spriteSkinBody;

    public Sprite spriteSkinHand;
    private string ReplacingTickADot(float number)
    {
        string valueString = number.ToString();
        return valueString = valueString.Replace(",",".");
    }
}
