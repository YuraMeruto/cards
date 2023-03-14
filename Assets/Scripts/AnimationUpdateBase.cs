abstract public class AnimationUpdateBase
{
    abstract public void Update();
    abstract public void Finish();

    protected bool is_finish = false;

    public bool IsFinish { get { return is_finish; } }
}
