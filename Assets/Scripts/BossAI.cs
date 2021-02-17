using UnityEngine;

[RequireComponent(typeof(Character))]
public class BossAI : MonoBehaviour
{
    [SerializeField] private Character _player;

    private float _timeToNextMove;
    private Character _character;

	private void Start()
	{
        SetRandomTimeToNextMove();
        _character = GetComponent<Character>();
	}

	private void Update()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) > 2.5F)
		{
            transform.LookAt(_player.transform);
            _character.MoveXZ(_player.transform.position - transform.position);
		}
        else if(_player.IsSwingingSword)
		{
            _character.Jump();
		}
        else if(!_character.IsJumping && _timeToNextMove <= 0)
		{
            SetRandomTimeToNextMove();
            _character.SwingSword();
        }
        
        _timeToNextMove -= Time.deltaTime;
    }

    private void SetRandomTimeToNextMove()
	{
        _timeToNextMove = Random.Range(0.1F, 1.0F);
    }
}
