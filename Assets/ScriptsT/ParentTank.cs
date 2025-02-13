using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ParentTank : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab; // Bullet prefab to instantiate
    [SerializeField] protected ParticleSystem smokeParticle;
    [SerializeField] protected ParticleSystem fireParticle;
    [SerializeField] protected AudioSource soundEffect;
    [SerializeField] protected ParticleSystem smokeBeforeEnd;
    [SerializeField] protected ParticleSystem particleFame;
    [SerializeField] protected ParticleSystem explosion;
    [SerializeField] protected AudioSource tankDestroyed;
    [SerializeField] Button restartButton;
     
    protected float attackForce = 250f;
    protected int totalPower = 100;
   protected PlayerHitEvent playerHitEvent=new PlayerHitEvent();
    protected EnemyHitEvent enemyHitEvent=new EnemyHitEvent();
    protected int damage=5;
    protected bool gameEnd=false;
    protected bool  bulletEnabled = true;


    virtual protected void Start()
    {
        EventManager.AddEvenInvoker(this);
    }
    protected abstract void FireBullet(Vector3 bulletDirection);
    virtual protected void TakeDamage(int damage)
    {
        totalPower=Mathf.Max(0,totalPower-damage);
        if (totalPower <= 10) {
            smokeBeforeEnd.Play();
        }
        if (totalPower <= 0) {
            particleFame.Play();
            explosion.Play();
            tankDestroyed.Play();
            gameEnd=true;
            restartButton.gameObject.SetActive(true);
        }
    }
    protected IEnumerator BulletFireAllowed()
    {
        yield return new WaitForSeconds(1);
        bulletEnabled = true;   
    }
    public void AddplayerHitEvent(UnityAction<int> listener)
    {
        playerHitEvent.AddListener(listener);
    }
    public void AddenemyHitEvent(UnityAction<int> listener) { 
      enemyHitEvent.AddListener(listener);
    }


}
