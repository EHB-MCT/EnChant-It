using Meta.WitAi.Requests;
using Oculus.Voice;
using UnityEngine;

public class VoiceActivation : MonoBehaviour
{
    [SerializeField] private AppVoiceExperience _appVoiceExperience;
    [SerializeField] private bool _activateImmediately = false;
    [SerializeField] private float cooldownTime = 3f;
    private VoiceServiceRequest _request;
    private bool _isActive = false;
    private float _lastActivationTime;

    private void Start()
    {
        if (_appVoiceExperience == null)
        {
            _appVoiceExperience = FindObjectOfType<AppVoiceExperience>();
        }
    }

    private void Update()
    {
        if (!_isActive && Time.time - _lastActivationTime >= cooldownTime)
        {
            ActivateVoiceService();
        }
    }

    private void ActivateVoiceService()
    {
        _request = _activateImmediately ? _appVoiceExperience.ActivateImmediately(GetRequestEvents()) :
                                          _appVoiceExperience.Activate(GetRequestEvents());
        _lastActivationTime = Time.time;
    }

    private VoiceServiceRequestEvents GetRequestEvents()
    {
        VoiceServiceRequestEvents events = new VoiceServiceRequestEvents();
        events.OnInit.AddListener(OnInit);
        events.OnComplete.AddListener(OnComplete);
        return events;
    }

    private void OnInit(VoiceServiceRequest request)
    {
        _isActive = true;
    }

    private void OnComplete(VoiceServiceRequest request)
    {
        _isActive = false;
    }
}
