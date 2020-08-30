using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class OverridePostProcessing : MonoBehaviour
{
	// [SerializeField] private PostProcessVolume postProcessVolume;
	private PostProcessVolume postProcessVolume;
	private PostProcessProfile postProcessProfile;

	// Start is called before the first frame update
	void Start()
	{
		postProcessVolume = gameObject.GetComponent<PostProcessVolume>();

		postProcessProfile = postProcessVolume.profile;
        // postProcessProfile = postProcessVolume.sharedProfile
		
	}


    public void ActivateMonsterPostProcessing()
    {

		Bloom bloom = postProcessProfile.GetSetting<Bloom>();
		bloom.enabled.Override(true);
		bloom.intensity.Override(20f);

		Vignette vignette = postProcessProfile.GetSetting<Vignette>();
		vignette.enabled.Override(true);
		vignette.intensity.Override(1.0f);

	}

	public void DeActivateMonsterPostProcessing()
	{

		Bloom bloom = postProcessProfile.GetSetting<Bloom>();
		bloom.enabled.Override(false);

		Vignette vignette = postProcessProfile.GetSetting<Vignette>();
		vignette.enabled.Override(false);

	}

	void OnDestroy()
	{
		RuntimeUtilities.DestroyProfile(postProcessProfile, true);
	}
}