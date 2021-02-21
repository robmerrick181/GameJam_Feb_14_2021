using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIHelper
{
	public Character BossCharacter { get; }
	public Character PlayerCharacter { get; }

	public BossAIHelper(Animator bossAnimator)
	{
		BossCharacter = bossAnimator.GetComponentInParent<Character>();
		PlayerCharacter = BossCharacter.Player;
	}
}
