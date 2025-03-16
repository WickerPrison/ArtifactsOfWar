using UnityEngine;

public class TurnMeter : MonoBehaviour
{
    [SerializeField] SpriteRenderer fill;

    private void Start()
    {
        fill.color = GameManager.Instance.colorScheme.turnMeter;
    }
}
