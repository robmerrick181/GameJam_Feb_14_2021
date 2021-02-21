using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIHelper
{
	private readonly float _chaseDistance = 5.0F;

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
}
