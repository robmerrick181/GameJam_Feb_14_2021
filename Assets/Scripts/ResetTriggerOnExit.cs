using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriggerOnExit : StateMachineBehaviour
{
	[SerializeField] private string _triggerName;

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log($"Resetting Trigger \"{_triggerName}\"");
		animator.ResetTrigger(_triggerName);
	}
}
