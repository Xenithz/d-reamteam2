using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
#region Public Variables
public static AudioManager auidoInstance;
public AudioSource musicSource;
public AudioSource effectSource;
public AudioClip[] musicClips;
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
    musicSource.Play();
}

public void StopSingleMusicClip()
{
    musicSource.Stop();
}  

public void PlaySingleMusicClipPoint(float startTime,float endTime)
{
    musicSource.PlayScheduled(startTime);
    musicSource.SetScheduledEndTime(AudioSettings.dspTime+(endTime));
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
	int randomIndex=Random.Range(0,musicClips.Length);
	for(int i=0; i<musicClips.Length;i++){
	AudioClip tempClip=musicClips[i];	
	musicClips[i]=musicClips[randomIndex];
	musicClips[randomIndex]=tempClip;
	}
	musicSource.clip=musicClips[randomIndex];
	musicSource.Play();
    }
}
#endregion







