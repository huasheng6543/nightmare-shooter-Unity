using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingScript : MonoBehaviour {
    public GameObject SettingDialog;
    public AudioSource BackgroundAudioSource;
    public Light MainLight;
    public void OpenSettingDialog() {
        if (!SettingDialog.activeSelf) {
            Time.timeScale = 0;
            SettingDialog.SetActive(true);
        }
        
    }
    public void CloseSettingDialog() {
        if (SettingDialog.activeSelf) {            
            SettingDialog.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void MusicChanged(float newValue) {
        BackgroundAudioSource.volume = newValue;
    }
    public void MainLightChanged(float newValue) {
        MainLight.intensity = newValue;
    }
}
