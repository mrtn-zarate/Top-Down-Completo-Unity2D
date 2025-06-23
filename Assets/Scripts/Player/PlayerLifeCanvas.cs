using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeCanvas : MonoBehaviour
{
	#region Public references
	[SerializeField] private LifeSystem lifeSystemReference;
	[SerializeField] private Image barReference;
	#endregion

	void Start()
	{
		lifeSystemReference.OnRemainingLife.AddListener((value) => SetVisualLife(value));
	}

	public void SetVisualLife(float value)
	{
		barReference.fillAmount = value / 100f;
	}
}
