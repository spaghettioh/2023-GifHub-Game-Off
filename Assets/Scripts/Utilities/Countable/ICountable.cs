public interface ICountable
{
    public float Current { get; }
    public float Start { get; }
    public float Increment(float amount);
    public float Decrement(float amount);
    public float Set(float amount);
    public float Reset();
}
