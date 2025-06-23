using UnityEngine;
using UnityEngine.Events;

public class LifeSystem : MonoBehaviour
{

	[SerializeField] private float life = 100f;
	[HideInInspector] public UnityEvent<float> OnReceiveDamage = new UnityEvent<float>();
	[HideInInspector] public UnityEvent<float> OnReceiveHeal = new UnityEvent<float>();
	[HideInInspector] public UnityEvent<float> OnRemainingLife = new UnityEvent<float>();
	[HideInInspector] public UnityEvent OnDeath = new UnityEvent();


	public void CheckCurrentLife()
	{
		if (GlobalData.currentLife == -1) GlobalData.currentLife = life;
		else
		{
			life = GlobalData.currentLife;
			OnRemainingLife?.Invoke(life);
		}
	}

	public void ReceiveDamage(float damage)
	{

		life -= damage;
		OnReceiveDamage?.Invoke(damage);
		OnRemainingLife?.Invoke(life);
		GlobalData.currentLife = life;

		if (life <= 0)
		{
			GlobalData.currentLife = -1;
			OnDeath?.Invoke();

			//Esperar medio segundo a reproducir animacion de muerte
			// Destroy(this.gameObject);
		}
	}

	public float GetCurrentLife() => life;
	public void AddLife(float value)
	{
		life += value;
		OnReceiveHeal?.Invoke(value);
		OnRemainingLife?.Invoke(life);
		GlobalData.currentLife = life;
	} 
	public void SetLife(float value) => life = value;

}
