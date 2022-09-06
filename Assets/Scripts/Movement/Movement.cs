using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body2D;
    [SerializeField]private float movementSpeed;
    [SerializeField]private float height;
    private Collider2D lookdownCol;
    private LayerMask whatIsGround;
    private bool facingRight = true;
    public bool FacingRight { get { return this.facingRight; } }


    public void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
        whatIsGround |= 1 << LayerMask.NameToLayer("Ground");
    }

    public bool GroundCheck()
    {
        Vector2 basePosition = new Vector2(transform.position.x, transform.position.y - height);
        lookdownCol = Physics2D.OverlapCircle(basePosition, 0.3f, whatIsGround);

        if (lookdownCol != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void Move()
    {

        if (facingRight)
        {
            body2D.velocity = new Vector2(movementSpeed, body2D.velocity.y);
        }
        else
        {
            body2D.velocity = new Vector2(-movementSpeed, body2D.velocity.y);
        }
    }

    public void StopMovement()
    {
        body2D.velocity = new Vector2(0, body2D.velocity.y);
    }


    public bool TargetReached(Vector2 target)
    {
        if (Vector2.Distance(transform.position, target) <= 0.5f)
        {
            return true;
        }

        return false;
    }

    public void Flip()
    {
        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
    }

    public void FaceTarget(Vector2 position)
    {
        if (facingRight && transform.position.x > position.x)
        {
            Flip();
        }else if (!facingRight && transform.position.x < position.x)
        {
            Flip();
        }
    }

    public void FollowCurve(BezierCurve bezierCurve,float time)
    {
        transform.position = bezierCurve.BezierCurvePoint(time);
    }

    public void RigidBodyIsKinematic(bool value)
    {
        body2D.isKinematic = value;
    }
}