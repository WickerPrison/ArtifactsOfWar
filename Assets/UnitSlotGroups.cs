using UnityEngine;
using System.Linq;

public class UnitSlotGroups : MonoBehaviour
{
    public UnitSlot[] playerFrontline;
    public UnitSlot[] playerBackline;
    public UnitSlot[] playerCollapsedline;
    private UnitSlot[] _playerBackAndFront;
    public UnitSlot[] playerBackAndFront
    {
        get
        {
            if(_playerBackAndFront == null)
            {
                _playerBackAndFront = playerFrontline.Concat(playerBackline).ToArray();
            }
            return _playerBackAndFront;
        }
        private set { }
    }
    private UnitSlot[] _allPlayerSlots;
    public UnitSlot[] allPlayerSlots
    {
        get
        {
            if (_allPlayerSlots == null)
            {
                _allPlayerSlots = playerBackAndFront.Concat(playerCollapsedline).ToArray();
            }
            return _allPlayerSlots;
        }
        private set { }
    }

    public UnitSlot[] enemyFrontline;
    public UnitSlot[] enemyBackline;
    public UnitSlot[] enemyCollapsedline;
    UnitSlot[] _enemyBackAndFront;
    public UnitSlot[] enemyBackAndFront
    {
        get
        {
            if(_enemyBackAndFront == null)
            {
               _enemyBackAndFront = enemyFrontline.Concat(enemyBackline).ToArray();
            }
            return _enemyBackAndFront;
        }
        private set { }
    }
    UnitSlot[] _allEnemySlots;
    public UnitSlot[] allEnemySlots
    {
        get
        {
            if(_allEnemySlots == null)
            {
                _allEnemySlots = enemyBackAndFront.Concat(enemyCollapsedline).ToArray();
            }
            return _allEnemySlots;
        }
        private set { }
    }

    private static UnitSlotGroups _instance;
    public static UnitSlotGroups Instance { get { return _instance; } }

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
}
