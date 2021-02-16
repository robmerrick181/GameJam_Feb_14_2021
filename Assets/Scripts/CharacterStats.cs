using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats: MonoBehaviour
{
    public int StartingHealthPoints = 100;
    public int StartingStrength = 10;
    public int CurrentHealthPoints;
    public int CurrentStrength;

    public void ChangeStrength(int _strength)
    {
        CurrentStrength += _strength;
    }

    public void ResetStrength()
    {
        CurrentStrength = StartingStrength;
    }

    public void ChangeHealth(int _health)
    {
        CurrentHealthPoints += _health;
    }

    public void ResetHealth()
    {
        CurrentHealthPoints = StartingHealthPoints;
    }
}
