using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TumbuhanLabelManager : MonoBehaviour {

	public Button AortaBtn;
    public Button VenaCavaAtasBtn;
    public Button VenaCavaBawahBtn;
    public Button ArteriParuKiriBtn;
    public Button VenaParuBtn;
    public Canvas AortaPanel;
    public Canvas VenaCavaAtasPanel;
    public Canvas VenaCavaBawahPanel;
    public Canvas ArteriParuKiriPanel;
    public Canvas VenaParuPanel;
    public AudioSource SFXPanelIn;
    public AudioSource SFXPanelOut;



    void Start()
    {
        VenaCavaAtasPanel = VenaCavaAtasPanel.GetComponent<Canvas>();
        VenaCavaBawahPanel = VenaCavaBawahPanel.GetComponent<Canvas>();
        ArteriParuKiriPanel = ArteriParuKiriPanel.GetComponent<Canvas>();
        VenaParuPanel = VenaParuPanel.GetComponent<Canvas>();
        AortaBtn = AortaBtn.GetComponent<Button>();
        VenaCavaAtasBtn = VenaCavaAtasBtn.GetComponent<Button>();
        VenaCavaBawahBtn = VenaCavaBawahBtn.GetComponent<Button>();
        ArteriParuKiriBtn = ArteriParuKiriBtn.GetComponent<Button>();
        VenaParuBtn = VenaParuBtn.GetComponent<Button>();


    }

    public void AortaDisplayOn()
    {
        AortaBtn.enabled = false;
        VenaCavaAtasBtn.enabled = false;
        VenaCavaBawahBtn.enabled = false;
        ArteriParuKiriBtn.enabled = false;
        VenaParuBtn.enabled = false;
        SFXPanelIn.Play();
    }
    public void AortaDisplayOff()
    {
        AortaBtn.enabled = true;
        VenaCavaAtasBtn.enabled = true;
        VenaCavaBawahBtn.enabled = true;
        ArteriParuKiriBtn.enabled = true;
        VenaParuBtn.enabled = true;
        SFXPanelOut.Play();
    }
    public void VenaCavaAtasDisOn()
    {
        AortaBtn.enabled = false;
        VenaCavaAtasBtn.enabled = false;
        VenaCavaBawahBtn.enabled = false;
        ArteriParuKiriBtn.enabled = false;
        VenaParuBtn.enabled = false;
        SFXPanelIn.Play();

    }

    public void VenaCavaAtasDisOff()
    {
        AortaBtn.enabled = true;
        VenaCavaAtasBtn.enabled = true;
        VenaCavaBawahBtn.enabled = true;
        ArteriParuKiriBtn.enabled = true;
        VenaParuBtn.enabled = true;
        SFXPanelOut.Play();

    }

    public void VenaCavaBwhDisOn()
    {
        AortaBtn.enabled = false;
        VenaCavaAtasBtn.enabled = false;
        VenaCavaBawahBtn.enabled = false;
        ArteriParuKiriBtn.enabled = false;
        VenaParuBtn.enabled = false;
        SFXPanelIn.Play();
    }

    public void VenaCavaBhwDisOff()
    {
        AortaBtn.enabled = true;
        VenaCavaAtasBtn.enabled = true;
        VenaCavaBawahBtn.enabled = true;
        ArteriParuKiriBtn.enabled = true;
        VenaParuBtn.enabled = true;
        SFXPanelOut.Play();

    }

    public void ArteriParuKiriDisOn()
    {
        AortaBtn.enabled = false;
        VenaCavaAtasBtn.enabled = false;
        VenaCavaBawahBtn.enabled = false;
        ArteriParuKiriBtn.enabled = false;
        VenaParuBtn.enabled = false;
        SFXPanelIn.Play();

    }

    public void ArteriParuKiriDisOff()
    {
        AortaBtn.enabled = true;
        VenaCavaAtasBtn.enabled = true;
        VenaCavaBawahBtn.enabled = true;
        ArteriParuKiriBtn.enabled = true;
        VenaParuBtn.enabled = true;
        SFXPanelOut.Play();

    }

    public void VenaParuDisOn()
    {
        AortaBtn.enabled = false;
        VenaCavaAtasBtn.enabled = false;
        VenaCavaBawahBtn.enabled = false;
        ArteriParuKiriBtn.enabled = false;
        VenaParuBtn.enabled = false;
        SFXPanelIn.Play();
    }

    public void VenaParuDisOff()
    {
        AortaBtn.enabled = true;
        VenaCavaAtasBtn.enabled = true;
        VenaCavaBawahBtn.enabled = true;
        ArteriParuKiriBtn.enabled = true;
        VenaParuBtn.enabled = true;
        SFXPanelOut.Play();
    }
}
