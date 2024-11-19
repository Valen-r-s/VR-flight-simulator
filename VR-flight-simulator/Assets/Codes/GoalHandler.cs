using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<CollisionHandler>().ReachGoal();
        }
    }
}
