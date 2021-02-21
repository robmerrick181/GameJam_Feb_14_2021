using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIBlockIdle : StateMachineBehaviour
{
	private float _blockTime;
	private BossAIHelper _helper;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_blockTime = 1.0F;
		_helper = new BossAIHelper(animator);

		if(_helper.BossCharacter.IsDead || _helper.PlayerCharacter.IsDead || _blockTime <= 0.0F)
		{
			animator.SetTrigger("IdleLogic");
			return;
		}

		_blockTime -= Time.deltaTime;
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
