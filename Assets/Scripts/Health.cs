using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int StartingHealthPoints = 100;
    public int CurrentHealthPoints;

    public void ChangeHealth(int _health)
    {
        CurrentHealthPoints += _health;
    }

    public void ResetHealth()
    {
        CurrentHealthPoints = StartingHealthPoints;
    }
}
