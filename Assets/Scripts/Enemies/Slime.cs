using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{
    
    #region Triggers
	public override void OnTriggerEnter2D(Collider2D collision)
	{
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            //Corremos hacia el jugador
            // Debug.Log("Player Detectado");
            base.SetNewDestination(collision.transform.position);
        }
	}
	#endregion
}
