using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneMenu : MonoBehaviour
{
    private GameObject _phoneMenu;
    private Button exitButton;

    public void Start()
    {
        _phoneMenu = GameObject.FindGameObjectWithTag("Phone_Menu");
        exitButton = GameObject.FindGameObjectWithTag("Button_Exit").GetComponent<Button>();
        exitButton.onClick.AddListener(ExitButtonClicked);
        _phoneMenu.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return)) {
            _phoneMenu.SetActive(true);
        }
    }

    public void ExitButtonClicked() {
        _phoneMenu.SetActive(false);
    }   
}
