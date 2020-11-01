using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerState
{
    public override void Walk()
    {
        // base._rb.velocity = new Vector2(0, base._rb.velocity.y);

        // if (Input.GetAxisRaw("Horizontal") != 0)
        // {
        //     base._rb.velocity = new Vector2(base._speed * Input.GetAxisRaw("Horizontal"), base._rb.velocity.y);
        // }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (base._currentJumps < base._jumpCount)
        //     {
        //         base._rb.AddForce(new Vector2(0, base._jumpForce), ForceMode2D.Impulse);
        //         base._currentJumps++;
        //     }
        // }
    }
}
