using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class CharacterEvents
{
    // character and damage value
    public static UnityAction<GameObject, int> characterDamaged;
    // character and health amount
    public static UnityAction<GameObject, int> characterHealed;
}