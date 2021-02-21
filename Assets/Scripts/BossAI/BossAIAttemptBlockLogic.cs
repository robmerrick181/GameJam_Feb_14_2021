using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIAttemptBlockLogic : StateMachineBehaviour
{
	private BossAIHelper _helper;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_helper = new BossAIHelper(animator);

		if(_helper.BossCharacter.IsDead || _helper.PlayerCharacter.IsDead)
		{
			animator.SetTrigger("IdleLogic");
			return;
		}

		if(_helper.TryBlock())
		{
			animator.SetTrigger("Block");
		}
		else
		{
			animator.SetTrigger("TakeDamage");
		}
	}
}
