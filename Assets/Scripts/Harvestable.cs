using Assets.Scripts;
using Assets.Scripts.Helpers;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class Harvestable : MonoBehaviour
{
    public SoundEffect idleSound;
    public SoundEffect pickUpSound;

    private float _timeElapsed;
    public float idleSoundInterval = 5.0f;

    public float gravity = -80f;

    private CharacterController2D _characterController;

    public void Awake()
    {
        tag = Tags.Harvestable;

        _characterController = GetComponent<CharacterController2D>();

        if (idleSound != null)
        {
            idleSound = Instantiate(idleSound) as SoundEffect;
            idleSound.transform.parent = transform;
        }

        if (pickUpSound != null)
        {
            pickUpSound = Instantiate(pickUpSound) as SoundEffect;
            //pickUpSound.transform.parent = transform;
        }
    }

    public void Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed > idleSoundInterval)
        {
            idleSound.Do(s => s.PlayEffect());
            _timeElapsed = 0;
        }

        if (_characterController != null && !_characterController.isGrounded)
        {
            _characterController.move(new Vector3(_characterController.velocity.x, _characterController.velocity.y + gravity * Time.deltaTime) * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            collision.gameObject.GetComponent<Player>().Harvest(this);
        }
	}
}
