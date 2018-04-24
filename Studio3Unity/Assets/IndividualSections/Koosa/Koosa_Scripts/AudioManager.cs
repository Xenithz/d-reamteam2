using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
#region Public Variables
public static AudioManager auidoInstance;
public AudioSource backGroundMusicSource;
public AudioSource effectSource;
public AudioClip[] backGroundMusicClips;
public AudioClip[] effectClips;
#endregion

#region Unity Functions
void Awake()
{
	if( auidoInstance==null)
		auidoInstance=this;
	
	else if( auidoInstance!=this)
		Destroy(gameObject);

	    DontDestroyOnLoad (gameObject);
}
#endregion

#region My Functions
public void PlaySingleMusicClip()
{
    backGroundMusicSource.Play();
}
void Update()
{
	if(!backGroundMusicSource.isPlaying){}
		//MusicShuffle();
}

public void StopSingleMusicClip()
{
    backGroundMusicSource.Stop();
}  

public void PlaySingleMusicClipPoint(float startTime,float endTime)
{
    backGroundMusicSource.PlayScheduled(startTime);
    backGroundMusicSource.SetScheduledEndTime(AudioSettings.dspTime+(endTime));
}

public void PlaySingleEffect()
{
    effectSource.Play();
}

public void StopSingleEffect()
{
    effectSource.Stop();
}

public void PlaySingleEffectPoint(float startTime,float endTime)
{
	effectSource.PlayScheduled(startTime);
    effectSource.SetScheduledEndTime(AudioSettings.dspTime+(endTime));
}
public void MusicShuffle()
{
	int randomIndex=Random.Range(0,backGroundMusicClips.Length);
	for(int i=0; i<backGroundMusicClips.Length;i++)
    {
	AudioClip tempClip=backGroundMusicClips[i];	
	backGroundMusicClips[i]=backGroundMusicClips[randomIndex];
	backGroundMusicClips[randomIndex]=tempClip;
	}
	backGroundMusicSource.clip=backGroundMusicClips[randomIndex];
	backGroundMusicSource.Play();
}
 public void PlaySFX(AudioSource sourcePlay, int index,float startTime, float endTime, AudioClip[] audioClips)
    {
        sourcePlay.clip = audioClips[index];
        sourcePlay.SetScheduledStartTime(startTime);
        sourcePlay.SetScheduledEndTime(endTime);
        sourcePlay.Play();
    }
    public void Playeffect(int index)
    {
        effectSource.PlayOneShot(effectClips[index]);
    }
    public void PlaySpawn(int index)
    {
        backGroundMusicSource.PlayOneShot(effectClips[index]);
    }
	
}
#endregion







