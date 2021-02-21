using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIWalkTowardsPlayer : StateMachineBehaviour
{
	private float _idleTimeRemaining;
	private BossAIHelper _helper;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_idleTimeRemaining = Random.Range(1.0F, 3.0F);
		_helper = new BossAIHelper(animator);

		if(_helper.BossCharacter.IsDead || _helper.PlayerCharacter.IsDead)
		{
			animator.SetTrigger("IdleLogic");
			return;
		}
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(_helper.BossCharacter.IsDead || _helper.PlayerCharacter.IsDead || _idleTimeRemaining <= 0)
		{
			animator.SetTrigger("IdleLogic");
			return;
		}

		if(_helper.CanTryBlock())
		{
			animator.SetTrigger("TryBlock");
			return;
		}

		_helper.BossCharacter.MoveXZ(0.75F * _helper.BossCharacter.transform.forward, targetSystemEngaged: true, _helper.PlayerCharacter);
		_idleTimeRemaining -= Time.deltaTime;
	}
}
