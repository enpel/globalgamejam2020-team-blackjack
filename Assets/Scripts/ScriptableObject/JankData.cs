using UnityEngine;

[CreateAssetMenu( menuName = "BlackJack/Create New JankData", fileName = "JankData" )]
public class JankData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _screw;
    [SerializeField] private int _level;
    [SerializeField] private int _reward;


    [SerializeField] private Sprite _sprite;
    
    public Jank Jank => new Jank(new DisplayName(_name), new Screw(_screw), new Level(_level), new Reward(new RepairCoin(_reward)));
    public Sprite Sprite => _sprite;

}
