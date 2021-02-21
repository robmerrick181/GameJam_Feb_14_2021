using UnityEngine;

public class HealthFill : MonoBehaviour
{
	[SerializeField] private Character _character;

	private RectTransform _healthFillRectTransform;
	private CharacterStats _characterStats;

	private void Start()
	{
		_healthFillRectTransform = GetComponent<RectTransform>();
		_characterStats = _character.GetComponent<CharacterStats>();
	}

	private void Update()
	{
		_healthFillRectTransform.anchorMax = new Vector2(_characterStats.CurrentHealthPercent, _healthFillRectTransform.anchorMax.y);
	}
}
