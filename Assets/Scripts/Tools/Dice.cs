using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    public static int Roll(DiceType type, int amount = 1)
    {
        int result = 0;

        for(int i = 0; i < amount; i++)
        {
            switch(type)
            {
                case DiceType.d4:
                    result += Mathf.RoundToInt(Random.Range(1, 4));
                    break;
                case DiceType.d6:
                    result += Mathf.RoundToInt(Random.Range(1, 6));
                    break;
                case DiceType.d8:
                    result += Mathf.RoundToInt(Random.Range(1, 8));
                    break;
                case DiceType.d10:
                    result += Mathf.RoundToInt(Random.Range(1, 10));
                    break;
                case DiceType.d12:
                    result += Mathf.RoundToInt(Random.Range(1, 12));
                    break;
                case DiceType.d20:
                    result += Mathf.RoundToInt(Random.Range(1, 20));
                    break;
            }

        }

        return result;
    }
}

public enum DiceType
{
    d4,
    d6,
    d8,
    d10,
    d12,
    d20
}
