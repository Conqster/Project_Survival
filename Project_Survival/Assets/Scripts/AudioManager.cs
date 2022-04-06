using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundFXCat { FootStep, Jump, BossDeath, EnemyHitPlayer,
    PlayerShoot, Water, EnemyDeath, PlayerDeath, EnemyShoot, BossAttack, SkeletonDeath, SkeletonAttack}
    public GameObject audioObject;
    public AudioClip[] footSteps;
    public AudioClip[] jumpAudio;
    public AudioClip[] bossDeathAudio;
    public AudioClip[] enemyhitPlayerAudio;
    public AudioClip[] playerShootAudio;
    public AudioClip[] waterAudio;
    public AudioClip[] enemyDeathAudio;
    public AudioClip[] playerDeathAudio;
    public AudioClip[] enemyShootAudio;
    public AudioClip[] bossAttackAudio;
    public AudioClip[] skeletonDeath;
    public AudioClip[] skeletonAttack;

    public void AudioTrigger(SoundFXCat audioType, Vector3 audioPosition, float volume)
    {
        GameObject newAudio = GameObject.Instantiate(audioObject, audioPosition, Quaternion.identity);
        ControlAudio ca = newAudio.GetComponent<ControlAudio>();
        switch (audioType)
        {
            case (SoundFXCat.SkeletonAttack):
            ca.myClip = skeletonAttack[Random.Range(0, skeletonAttack.Length)];
            break;
            case (SoundFXCat.SkeletonDeath):
            ca.myClip = skeletonDeath[Random.Range(0, skeletonDeath.Length)];
            break;
            case (SoundFXCat.BossDeath):
            ca.myClip = bossDeathAudio[Random.Range(0, bossDeathAudio.Length)];
            break;
            case (SoundFXCat.Water):
            ca.myClip = waterAudio[Random.Range(0, waterAudio.Length)];
            break;
            case (SoundFXCat.FootStep):
            ca.myClip = footSteps[Random.Range(0, footSteps.Length)];
            break;
            case (SoundFXCat.PlayerShoot):
            ca.myClip = playerShootAudio[Random.Range(0, playerShootAudio.Length)];
            break;
            case (SoundFXCat.EnemyHitPlayer):
            ca.myClip = enemyhitPlayerAudio[Random.Range(0, enemyhitPlayerAudio.Length)];
            break;
            case (SoundFXCat.Jump):
            ca.myClip = jumpAudio[Random.Range(0, jumpAudio.Length)];
            break;
            case (SoundFXCat.PlayerDeath):
            ca.myClip = playerDeathAudio[Random.Range(0, playerDeathAudio.Length)];
            break;
            case (SoundFXCat.BossAttack):
            ca.myClip = bossAttackAudio[Random.Range(0, bossAttackAudio.Length)];
            break;
            case (SoundFXCat.EnemyShoot):
            ca.myClip = enemyShootAudio[Random.Range(0, enemyShootAudio.Length)];
            break;
            case (SoundFXCat.EnemyDeath):
            ca.myClip = enemyDeathAudio[Random.Range(0, enemyDeathAudio.Length)];
            break;

        }

        ca.volume = volume;
        ca.StartAudio();
    }
} 
   