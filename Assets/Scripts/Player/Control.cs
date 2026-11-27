using UnityEngine;

//TP2 - Eileen Moscovich.
public sealed class Control : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        ReadInput();
    }

    public void ReadInput()
    {
        if (player == null) return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (player.MovementComponent != null)
        {
            player.MovementComponent.Move(horizontalInput);

            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.MovementComponent.Jump();
            }
        }

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J) && player.AttackComponent != null)
        {
            player.AttackComponent.AttackTarget();
        }
    }
}
