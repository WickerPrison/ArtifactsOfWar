using UnityEngine;

public class StrategyManager : MonoBehaviour
{
    private static StrategyManager _instance;
    public static StrategyManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PersistData.money = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
