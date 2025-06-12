using UnityEngine;
using UnityEngine.Events;

public class LifeSystem : MonoBehaviour
{

	[SerializeField] private float life = 100f;
	[HideInInspector] public UnityEvent<float> OnReceiveDamage = new UnityEvent<float>();
	[HideInInspector] public UnityEvent<float> OnReceiveHeal = new UnityEvent<float>();
	[HideInInspector] public UnityEvent OnDeath = new UnityEvent();

	public void ReceiveDamage(float damage)
	{

		life -= damage;
		OnReceiveDamage?.Invoke(damage);

		if (life <= 0)
		{
			OnDeath?.Invoke();
			Destroy(this.gameObject);
		}
	}

	public float GetCurrentLife() => life;
	public void AddLife(float value) {
		life += value;
		OnReceiveHeal?.Invoke(value);
	} 
	public void SetLife(float value) => life = value;

}
