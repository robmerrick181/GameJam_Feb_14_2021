using System;
using UnityEngine;

public class CharacterStats: MonoBehaviour
{
    [SerializeField] private float _startingHealthPoints = 100.0F;
    [SerializeField] private float _startingStrength = 10.0F;

    private float _currentHealthPoints;
    private float _currentStrength;
    private Action _deathCallback = null;

    public float StartingHealthPoints => _startingHealthPoints;
    public float StartingStrength => _startingStrength;
    public float CurrentHealthPoints => _currentHealthPoints;
    public float CurrentStrength => _currentStrength;

	private void Start()
	{
		_currentHealthPoints = _startingHealthPoints;
        _currentStrength = _startingStrength;
	}

    public void SetDeathCallback(Action callback)
    {
        _deathCallback = callback;
    }

	public void ChangeStrength(float _strength)
    {
        _currentStrength += _strength;
    }

    public void ResetStrength()
    {
        _currentStrength = _startingStrength;
    }

    public void ChangeHealth(float _health)
    {
        _currentHealthPoints += _health;

        if(_currentHealthPoints <= 0)
		{
            _deathCallback.Invoke();
		}
    }

    public void ResetHealth()
    {
        _currentHealthPoints = _startingHealthPoints;
    }
}
