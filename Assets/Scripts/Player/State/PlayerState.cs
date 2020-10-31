using System.Collections;

public abstract class PlayerState
{
    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Attack()
    {
        yield break;
    }
    public virtual IEnumerator Block()
    {
        yield break;
    }
    public virtual IEnumerator OnAir()
    {
        yield break;
    }

}
