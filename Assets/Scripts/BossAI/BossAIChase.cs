using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIChase : StateMachineBehaviour
{
	private BossAIHelper _helper;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_helper = new BossAIHelper(animator);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(_helper.BossCharacter.IsDead || _helper.PlayerCharacter.IsDead)
		{
			animator.SetTrigger("ActionDecisionLogic");
			return;
		}
		
		_helper.BossCharacter.MoveXZ(_helper.PlayerCharacter.transform.position - _helper.BossCharacter.transform.position, false, null);
		
		if(!_helper.ShouldChasePlayer())
		{
			animator.SetTrigger("ActionDecisionLogic");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
