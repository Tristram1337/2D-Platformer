using UnityEngine;

public class BossBattleActivator : MonoBehaviour
{
    public BossBattleController theBoss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            theBoss.ActivateBossFight();

            gameObject.SetActive(false);
        }
    }
}