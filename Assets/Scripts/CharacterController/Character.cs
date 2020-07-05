using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

class Character : MonoBehaviour, IPunInstantiateMagicCallback
{
    private Ammunition _ammunition;
    private InGameUI _inGameUI;
    private Shooting _shooting;
    private NetworkEvents _event;
    private CharacterParticleSystem _particleSystem;

    public string uniqueId;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        uniqueId = (string)info.photonView.InstantiationData[0];
    }

    void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        _ammunition = GetComponent<Ammunition>();
        _inGameUI = FindObjectOfType<InGameUI>();
        _shooting = FindObjectOfType<Shooting>();
        _particleSystem = GetComponentInChildren<CharacterParticleSystem>();
        _event = GetComponent<NetworkEvents>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Spawn"))
        {
            SpawnObject spawnObject = collision.gameObject.GetComponent<SpawnObject>();
            if (spawnObject.GetType() == SpawnObjType.AidKit)
            {
                spawnObject.OnTake();
            }
            else if (spawnObject.GetType() == SpawnObjType.Сartridge)
            {
                if (_ammunition.BulletCount < 50)
                {
                    _ammunition.AddBullets(spawnObject.GetValue());
                    spawnObject.OnTake();
                    _inGameUI.SetBullets(_ammunition.BulletCount);
                }
            }
        }
    }   

    public void TakeHit()
    {
        _event.SendEvent(uniqueId, global::EventCode.ParticleBlood);
        _particleSystem.PlayBlood();

        //Debug.Log("HitTaken");
    }

    public void StartShoot()
    {
        _inGameUI.SetBullets(_ammunition.BulletCount);
        _shooting.Shoot();
        _particleSystem.PlayGunFire();
        _event.SendEvent(uniqueId, global::EventCode.ParticleGunFire);
    }

    public bool Shoot()
    {
        if (_ammunition.BulletCount > 0)
        {
            _ammunition.BulletDecrement();
            return true;
        }
        return false;
    }
}
