using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ScriptableObject that saves the player stats
    [SerializeField]
    PlayerMovement_ScriptableObject playerStats;

    // Movement
    Vector2 direction;

    // Dash
    private bool cdFinished = true;
    private bool dashing;
    SpriteRenderer playerColour;

    private void Start()
    {
        playerColour = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Raw returns 0-1, otherwise returns a growing number from 0 to 1.
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        // Dash attempt
        if (Input.GetButtonDown("Jump") && cdFinished)
        {
            dashing = true;
        }
    }

    // Good for physics 
    void FixedUpdate()
    {
        // Move as long as player is not dashing
        if (!dashing)
        {
            Movement(direction);
        }
        else
        {
            Dash();
        }
    }

    // Moves the player in the direction given by the inputs. It goes faster the higher the 'speed'
    void Movement(Vector2 direction)
    {
        playerStats.PlayerRB.velocity = direction * playerStats.Speed;
    }

    #region Dashing

    // Dashing
    void Dash()
    {
        // If player is inputing any direction, start dashing
        if (direction.y != 0 || direction.x != 0)
        {
            cdFinished = false;
            StartCoroutine(StartDash());
        }
        else
        {
            dashing = false;
        }
    }

    // Start dashing, move in last direction given for DashDuration, then finish dashing.
    IEnumerator StartDash()
    {
        // Change players colour for a visual cue, push player in direction. finish dashing, back to normal colour.
        while(dashing)
        {
            StartCoroutine(DashCooldown());
            playerColour.color = new Color(10, 10, 10);
            playerStats.PlayerRB.AddForce(direction.normalized * playerStats.DashSpeed, ForceMode2D.Impulse);
            Debug.Log(direction.normalized);
            yield return new WaitForSeconds(playerStats.DashDuration);
            dashing = false;
        }
        Debug.Log("Timer"); 

        playerColour.color = Color.red;
    }

    // Wait until the dash is off cooldown 
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(playerStats.DashCD);
        cdFinished = true;
    }

    #endregion

    /// INFO ///
    // Freeze rotation to avoid character rotating after colliding with wall, can also be done freezing the Z rotation on the inspector
    // playerStats.PlayerRB.freezeRotation = true;
}
