using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAITakeDamageOnce : StateMachineBehaviour
{
	private float _blockTime;
	private BossAIHelper _helper;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_blockTime = 0.5F;
		_helper = new BossAIHelper(animator);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(_helper.BossCharacter.IsDead || _helper.PlayerCharacter.IsDead || _blockTime <= 0.0F)
		{
			animator.SetTrigger("IdleLogic");
			return;
		}

		_blockTime -= Time.deltaTime;
	}
}
