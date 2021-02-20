using UnityEngine;

[RequireComponent(typeof(Character))]
public class BossAI : MonoBehaviour
{
    [SerializeField] private Character _player;

    private float _timeToNextMove;
    private Character _character;

	private void Start()
	{
        //SetRandomTimeToNextMove();
        //_character = GetComponent<Character>();
	}

	private void Update()
    {
		//if(_player.IsDead)
		//{
		//	_character.Gloat();
		//}
		//else
		//{
		//	if(Vector3.Distance(transform.position, _player.transform.position) > 2.5F)
		//	{
		//		SetRandomTimeToNextMove();
		//		_character.MoveXZ(_player.transform.position - transform.position, false, null);
		//		transform.LookAt(_player.transform.position);
		//	}
		//	else
		//	{
		//		_character.MoveXZ(Vector3.zero, false, null);

		//		if(_player.IsSwingingSword)
		//		{
		//			_character.Jump();
		//			_character.SwingSword();
		//		}
		//		else if(!_character.IsJumping && _timeToNextMove <= 0)
		//		{
		//			SetRandomTimeToNextMove();
		//			_character.SwingSword();
		//		}

		//		_timeToNextMove -= Time.deltaTime;
		//	}
		//}
	}

    private void SetRandomTimeToNextMove()
	{
        _timeToNextMove = Random.Range(0.5F, 3.0F);
    }
}
