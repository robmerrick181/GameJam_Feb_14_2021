using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIAttackLogic : StateMachineBehaviour
{
	private static bool _hasAttacked = false;
	private float _attackDecisionDelay;
	private BossAIHelper _helper;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_attackDecisionDelay = Random.Range(0.1F, 1.0F);
		_helper = new BossAIHelper(animator);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(_helper.BossCharacter.IsDead || (_hasAttacked && _helper.ShouldChasePlayer()))
		{
			_hasAttacked = false;
			animator.SetTrigger("ActionDecisionLogic");
			return;
		}

		_helper.BossCharacter.transform.LookAt(_helper.PlayerCharacter.transform.position);

		if(_attackDecisionDelay <= 0.0F)
		{
			_attackDecisionDelay = Random.Range(0.1F, 1.0F);

			if(!_hasAttacked || Random.value < 0.75F)
			{
				_hasAttacked = true;
				
				if(Random.value < 0.65F)
				{
					animator.SetTrigger("SwordSwing1");
				}
				else
				{
					animator.SetTrigger("SwordJumpAttack");
				}
			}
			else
			{
				_hasAttacked = false;
				animator.SetTrigger("ActionDecisionLogic");
			}
		}

		_attackDecisionDelay -= Time.deltaTime;
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
