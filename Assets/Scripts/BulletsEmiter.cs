using UnityEngine;
using System.Collections;
using Assets.Scripts.Helpers;

public class BulletsEmiter : MonoBehaviour {


	[SerializeField]
	private float speed;

	[SerializeField]
    private Transform spawnPoint;

	[SerializeField]
    private Bullet prefab;

	[SerializeField]
    private ParticleSystem explosionPrefab;



    private GameObjectPool<Bullet> _bulletsPool; 
    private GameObjectPool<ParticleSystem> _effectsPool; 

	void Start ()
	{
        _bulletsPool = new GameObjectPool<Bullet>(gameObject.AddChild("_bulletsPool").transform, prefab, 30);
        _effectsPool = new GameObjectPool<ParticleSystem>(gameObject.AddChild("_effectsPool").transform, explosionPrefab, 30);
	}
	
	void Update () 
    {
          if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
          {
	          Bullet bullet = CreateBullet();
	          bullet.transform.position = spawnPoint.position;
	          bullet.transform.rotation = spawnPoint.rotation;
	          Vector3 dir = spawnPoint.TransformDirection(Vector3.forward);
	          bullet.GetComponent<Rigidbody>().AddForce(dir * speed, ForceMode.Impulse);
          }
	}

    private Bullet CreateBullet()
    {
        Bullet bullet = _bulletsPool.GetObject();

        bullet.transform.parent = transform;
        bullet.transform.localScale = prefab.transform.localScale;
        bullet.OnHit += onHit;
        bullet.gameObject.SetActive(true);
        return bullet;
    }

    private void onHit(Vector3 position, Bullet bullet)
    {
        StartCoroutine(effectCoroutine(_effectsPool.GetObject(), position));
        bullet.OnHit -= onHit;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _bulletsPool.ReleaseObject(bullet);
    }

    private IEnumerator effectCoroutine(ParticleSystem effect, Vector3 position)
    {
        effect.gameObject.SetActive(true);
        effect.transform.parent = transform;
        effect.transform.localScale = Vector3.one;
        effect.transform.localPosition = position;
        effect.Play();
        while (effect.isPlaying)
        {
            yield return null;
        }

        _effectsPool.ReleaseObject(effect);
    }
}
