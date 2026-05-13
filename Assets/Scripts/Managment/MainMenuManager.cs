using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectSlider;


    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", -40f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVol", -10f);
        mainPanel .SetActive(true);
        optionsPanel .SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Options()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void OptionsBackBtn()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnMusicVolumeSliderChanged(float value)
    {
        SoundManager.instance.SetMusicVolume(value);
    }
    public void OnEffectVolumeSliderChanged(float value)
    {
        SoundManager.instance.SetEffectVolume(value);
    }







}
