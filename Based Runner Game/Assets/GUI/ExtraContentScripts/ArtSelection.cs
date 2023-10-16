using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ArtSelection : MonoBehaviour
{
    //public static int SelectedArt = -1;
    public Extras extras_Reference;

    public GameObject SelectedArea;

    public GameObject ArtArea;

    public GameObject[] Arts;

    private Image ArtSelected;

    public TMP_Text ArtName;
    public TMP_Text ArtDescription;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableArtSelectionArea()
    {
        SelectedArea.SetActive(false);
    }


    public void Art_00()
    {
        if (extras_Reference.extras[0].isUnlocked == true)
        {
            ArtSelected = extras_Reference.CheckExtraOut(0);

            SelectedArea.SetActive(true);
            ArtName.text = extras_Reference.extras[0].name;
            ArtDescription.text = extras_Reference.extras[0].description;
            ArtArea.GetComponent<Image>().sprite = ArtSelected.sprite;
        }

      //  extras_Reference.extras[0].isUnlocked = true;
    }

    public void Art_01()
    {
        if (extras_Reference.extras[1].isUnlocked == true)
        {
            ArtSelected = extras_Reference.CheckExtraOut(1);

            SelectedArea.SetActive(true);
            ArtName.text = extras_Reference.extras[1].name;
            ArtDescription.text = extras_Reference.extras[1].description;
            ArtArea.GetComponent<Image>().sprite = ArtSelected.sprite;
        }

        extras_Reference.extras[1].isUnlocked = true;
    }

    public void Art_02()
    {
        if (extras_Reference.extras[2].isUnlocked == true)
        {
            ArtSelected = extras_Reference.CheckExtraOut(2);

            SelectedArea.SetActive(true);
            ArtName.text = extras_Reference.extras[2].name;
            ArtDescription.text = extras_Reference.extras[2].description;
            ArtArea.GetComponent<Image>().sprite = ArtSelected.sprite;
        }
           

    }

    public void Art_03()
    {
        if (extras_Reference.extras[3].isUnlocked == true)
        {
            ArtSelected = extras_Reference.CheckExtraOut(3);

            SelectedArea.SetActive(true);
            ArtName.text = extras_Reference.extras[3].name;
            ArtDescription.text = extras_Reference.extras[3].description;
            ArtArea.GetComponent<Image>().sprite = ArtSelected.sprite;
        }
           

    }

    public void Art_04()
    {
        if (extras_Reference.extras[4].isUnlocked == true)
        {
            ArtSelected = extras_Reference.CheckExtraOut(4);

            SelectedArea.SetActive(true);
            ArtName.text = extras_Reference.extras[4].name;
            ArtDescription.text = extras_Reference.extras[4].description;
            ArtArea.GetComponent<Image>().sprite = ArtSelected.sprite;
        }
    }

    public void Art_05()
    {
        if (extras_Reference.extras[5].isUnlocked == true)
        {
            ArtSelected = extras_Reference.CheckExtraOut(5);

            SelectedArea.SetActive(true);
            ArtName.text = extras_Reference.extras[5].name;
            ArtDescription.text = extras_Reference.extras[5].description;
            ArtArea.GetComponent<Image>().sprite = ArtSelected.sprite;
        }
    }



}
