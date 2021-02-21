using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIHelper
{
	private readonly float _chaseDistance = 5.0F;
	private readonly float _blockDistance = 2.5F;

	public Character BossCharacter { get; }
	public Character PlayerCharacter { get; }

	public BossAIHelper(Animator bossAnimator)
	{
		BossCharacter = bossAnimator.GetComponentInParent<Character>();
		PlayerCharacter = BossCharacter.Player;
	}

	public bool ShouldChasePlayer()
	{
		return Vector3.Distance(BossCharacter.transform.position, PlayerCharacter.transform.position) >= _chaseDistance;
	}

	public bool CanTryBlock()
	{
		return Vector3.Distance(BossCharacter.transform.position, PlayerCharacter.transform.position) < _blockDistance && PlayerCharacter.IsSwingingSword;
	}

	public bool TryBlock()
	{
		return Random.value < 1.0F - 0.5F * BossCharacter.CharacterStats.CurrentHealthPercent;
	}
}
