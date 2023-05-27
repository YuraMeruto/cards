abstract public class AnimationUpdateBase
{
    abstract public void Update();
    abstract public void Finish();

    protected bool is_finish = false;
    protected bool is_destory = true;

    public bool IsFinish { get { return is_finish; } }

    public bool IsDestory { get { return is_destory; } }
}
