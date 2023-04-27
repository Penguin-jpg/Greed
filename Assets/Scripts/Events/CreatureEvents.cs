using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class CreatureEvents
{
    // character and damage value
    public static UnityAction<GameObject, int> creatureDamaged;
    // character and health amount
    public static UnityAction<GameObject, int> creatureHealed;
}