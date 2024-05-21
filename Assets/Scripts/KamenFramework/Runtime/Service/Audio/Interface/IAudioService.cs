namespace KamenFramework
{
    public interface IAudioService :IService
    {
        void ChangeMusicVolume(float volume);
        void PlayAudio(string audioName);
        void PreLoadAudio(string audioName);
        void ChangeBgm(string bgmName);
        void UnLoad();
    }
}