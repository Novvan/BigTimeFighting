using System.Collections;

public abstract class PlayerState
{
    public virtual void Idle()
    { }
    public virtual void Punch()
    { }
    public virtual void Kick()
    { }
    public virtual void Block()
    { }
    public virtual void Dash()
    { }
    public virtual void Jump()
    { }
    public virtual void Walk()
    { }
    public virtual void GetHit()
    { }
    public virtual void Win()
    { }
    public virtual void Lose()
    { }

}
