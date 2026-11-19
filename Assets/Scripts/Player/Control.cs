using UnityEngine;

public class Control : MonoBehaviour
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

            if (Input.GetButtonDown("Jump"))
            {
                player.MovementComponent.Jump();
            }
        }

        if (Input.GetButtonDown("Fire1") && player.AttackComponent != null)
        {
            player.AttackComponent.AttackTarget();
        }
    }
}
