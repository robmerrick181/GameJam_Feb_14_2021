using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIAttackLogic : StateMachineBehaviour
{
	private static bool _hasAttacked = false;
	private float _attackDecisionDelay;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("Entered AttackLogic");
		_attackDecisionDelay = Random.Range(0.1F, 1.0F);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(_attackDecisionDelay <= 0.0F)
		{
			_attackDecisionDelay = Random.Range(0.1F, 1.0F);

			if(!_hasAttacked || Random.value < 0.75F)
			{
				_hasAttacked = true;

				if(Random.value <= 0.35F)
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
