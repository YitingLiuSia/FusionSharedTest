using UnityEngine;
using System.Collections;
using TMPro;
using System.Text.RegularExpressions;
using Fusion;
namespace SiaX
{
    public class TextManager : MonoBehaviour
    {
        public GameObject parentName;
        public bool lookAtPlayer;
        public bool defaultSelectText = true;
        public bool selectNumberOnly = false;
        public float offsetY = 0;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject textHolder;// text's parent 
        [SerializeField] private Transform playerTransform;
        NetworkRunner runner;
        NetworkObject localPlayer;

        void Start()
        {
            runner = GameObject.FindObjectOfType<NetworkRunner>();
            StartCoroutine(GetLocalPlayer());
        }

        IEnumerator GetLocalPlayer()
        {
            yield return new WaitForSeconds(0.2f);
            if (lookAtPlayer)
            {
                if (runner.LocalPlayer != null)
                {
                    if (runner.LocalPlayer.IsMasterClient)
                    {
                        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                        Debug.Log("players:" + players.Length);
                        for (int i = 0; i < players.Length; i++)
                        {
                            if (players[i].GetComponent<NetworkObject>().HasStateAuthority)
                            {
                                localPlayer = players[i].GetComponent<NetworkObject>();
                            }
                        }
                    }
                }
            }
        }
        private void OnValidate()
        {
            SetNameToParent();
        }
        private void SetNameToParent()
        {
            if (parentName == null)
                return;
            if (defaultSelectText)
            {
                text = this.GetComponent<TextMeshProUGUI>();
            }
            if (selectNumberOnly)
            {
                string number = GetNumberFromString(parentName.name).ToString();
                text.text = number;
            }
            else
            {
                text.text = parentName.name;
            }
        }
        private int GetNumberFromString(string inputString)
        {
            string pattern = @"\((\d+)\)";
            Match match = Regex.Match(inputString, pattern);

            if (match.Success)
            {
                string numberString = match.Groups[1].Value;
                if (int.TryParse(numberString, out int number))
                {
                    return number;
                }
            }

            return -1;
        }
        void Update()
        {
            if (localPlayer == null)
                return;

            playerTransform = localPlayer.transform;

            if (lookAtPlayer)
            {
                if (textHolder)
                {
                    LookAtPlayer(textHolder.transform);
                }
                if (text)
                {
                    LookAtPlayer(text.rectTransform);
                }
            }
        }
        private void LookAtPlayer<T>(T t) where T : Transform
        {
            t.LookAt(playerTransform);
            t.rotation = Quaternion.Euler(0, t.eulerAngles.y + offsetY, 0);
        }
    }
}
