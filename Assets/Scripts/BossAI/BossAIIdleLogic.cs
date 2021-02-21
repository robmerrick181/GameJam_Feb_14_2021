using UnityEngine;

public class BossAIIdleLogic : StateMachineBehaviour
{
	private static bool _hasIdled = false;
	
	private BossAIHelper _helper;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_helper = new BossAIHelper(animator);

		if(_helper.BossCharacter.IsDead || _helper.PlayerCharacter.IsDead)
		{
			_hasIdled = false;
			animator.SetTrigger("ActionDecisionLogic");
			return;
		}

		if(_hasIdled && Random.value < 0.75)
		{
			_hasIdled = false;
			animator.SetTrigger("ActionDecisionLogic");
		}
		else if(Random.value < 0.25)
		{
			animator.SetTrigger("StrafeAroundPlayer");
		}
		else if(Random.value < 0.25)
		{
			animator.SetTrigger("WalkAwayFromPlayer");
		}
		else if(Random.value < 0.25)
		{
			animator.SetTrigger("WalkTowardsPlayer");
		}
		else
		{
			animator.SetTrigger("Wait");
		}

		_hasIdled = true;
	}
}
