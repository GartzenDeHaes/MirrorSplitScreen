using UnityEngine;
using UnityEngine.AI;

using Portland.Unity;

namespace Mirror.Examples.SplitScreen
{
	public sealed class Tank : NetworkBehaviour
	{
		//[SerializeField] Camera _camera;
		public PlayerContext PlayerContext;

		[Header("Components")]
		public Animator animator;
		public TextMesh healthBar;
		public Transform turret;
		public Transform tankModel;

		[Header("Movement")]
		public float rotationSpeed = 100;

		[Header("Firing")]
		public GameObject projectilePrefab;
		public Transform projectileMount;

		[Header("Stats")]
		[SyncVar] public int health = 4;

		[SerializeField] float _speed = 1;

		float _fireCooldown;

		void Update()
		{
			// always update health bar.
			// (SyncVar hook would only update on clients, not on server)
			healthBar.text = new string('-', health);

			// take input from focused window only
			if (!Application.isFocused) return;

			_fireCooldown -= Time.deltaTime;

			// movement for local player
			if (PlayerContext != null && PlayerContext.IsLocalPlayer)
			{
				// rotate
				float horizontal = PlayerContext.MoveDelta.x;
				tankModel.transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

				// move
				float vertical = PlayerContext.MoveDelta.y;
				//Vector3 forward = tankModel.transform.TransformDirection(tankModel.transform.forward);
				//agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;
				transform.position += tankModel.transform.forward * Mathf.Max(vertical, 0) * _speed * Time.deltaTime;

				animator.SetBool("Moving", vertical != 0f);

				// shoot
				if (PlayerContext.IsFirePressed && _fireCooldown < 0.01f)
				{
					_fireCooldown = 1f;
					CmdFire();
				}

				RotateTurret();
			}
		}

		// this is called on the server
		[Command]
		void CmdFire()
		{
			GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);
			NetworkServer.Spawn(projectile);
			RpcOnFire();
		}

		// this is called on the tank that fired for all observers
		[ClientRpc]
		void RpcOnFire()
		{
			animator.SetTrigger("Shoot");
		}

		[ServerCallback]
		void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<Projectile>() != null)
			{
				--health;
				if (health == 0)
					NetworkServer.Destroy(gameObject);
			}
		}

		void RotateTurret()
		{
			turret.transform.Rotate(new Vector3(0, PlayerContext.LookDelta.x, 0) * Time.deltaTime * 60);
		}
	}
}
