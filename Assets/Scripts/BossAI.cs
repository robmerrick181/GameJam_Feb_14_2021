using UnityEngine;

[RequireComponent(typeof(Character))]
public class BossAI : MonoBehaviour
{
    private float _timeToNextMove;
    private Character _character;

	private void Start()
	{
        SetRandomTimeToNextMove();
        _character = GetComponent<Character>();
	}

	private void Update()
    {
        if(_timeToNextMove <= 0)
		{
            SetRandomTimeToNextMove();
            _character.SwingSword();
        }

        _timeToNextMove -= Time.deltaTime;
    }

    private void SetRandomTimeToNextMove()
	{
        _timeToNextMove = Random.Range(0.1F, 3.0F);
    }
}
