public class RepairPower
{
    public int Value { get; }

    public RepairPower(int value)
    {
        Value = value;
    }
    
    public static RepairPower operator+ (RepairPower a, RepairPower b)
    {
        return new RepairPower(a.Value + b.Value);
    }
}