using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject CharacterPrefab;
    public GameObject[] _aidKits;
    public GameObject[] _cartriges;

    void Start()
    {
        // Генерируем айди для персонажа
        AuthenticationValues authValues = new AuthenticationValues();
        authValues.AuthType = CustomAuthenticationType.Custom;

        string uniqueId = PhotonNetwork.LocalPlayer.UserId;
        authValues.SetAuthPostData(System.Convert.ToString(uniqueId));
        object[] characterData = new object[] { uniqueId };

        // Создаем персонажа и начальные данные для него
        GameObject prefab = PhotonNetwork.Instantiate(CharacterPrefab.name, new Vector3(64.7f, 2.62f, 96.7f), Quaternion.identity, 0, characterData);
        prefab.transform.position = new Vector3(64.7f, 2.62f, 96.7f);
        prefab.AddComponent<PlayerMovement>();
        prefab.tag = "Player";

        // Создаем аптечки и патроны
        if (PhotonNetwork.IsMasterClient)
        {
            int i = 0;
            foreach (GameObject obj in _aidKits)
            {
                object[] data = new object[] { i };
                GameObject aidKit = PhotonNetwork.Instantiate("SpawnObjects/AidKit", obj.transform.position, obj.transform.rotation, 0, data);
                ++i;
            }

            foreach (GameObject obj in _cartriges)
            {
                object[] data = new object[] { i };
                GameObject cartrige = PhotonNetwork.Instantiate("SpawnObjects/Cartrige", obj.transform.position, obj.transform.rotation, 0, data);
                ++i;
            }
        }
    }

    
}
