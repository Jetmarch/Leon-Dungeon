using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOObjects/SOBuff", fileName = "SOBuff")]
public class SOBuff : ScriptableObject
{
    public new LocaleString name;
    public LocaleString description;
    public int maxDurationInTurns;
    public int currentDurationLeft;
    public BuffType type;
    public Sprite icon;
    public SOBuffEffect buffEffect;
}

public enum BuffType
{
    Positive,
    Negative
}
