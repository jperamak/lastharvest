using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public int food;
        public int currenLevel = 1;
        public List<FamilyMember> family;

        public void Start()
        {
            DontDestroyOnLoad(this);
            StartFamily();
        }

        public void GoToNextLevel()
        {
            StartCoroutine(NextLevel());
        }

        public void FailLevel()
        {
            Debug.Log("Level failed :(");
        }

        private IEnumerator NextLevel()
        {
            //loading screen
            Feed();
            Application.LoadLevel("LoadingScreen");
            yield return new WaitForSeconds(5);
            //feed family
            Application.LoadLevel(++currenLevel);
        }

        private void StartFamily()
        {
            family = new List<FamilyMember>
            {
                new FamilyMember("Billy-Bob", 23),
                new FamilyMember("Wifey", 19),
                new FamilyMember("Babby1", 3),
                new FamilyMember("Babby2", 1)
            };
        }

        private void Feed()
        {
            foreach (FamilyMember fm in family.ToList())
            {
                food = fm.Feed(food);
                if (!fm.DidSurvive())
                    family.Remove(fm);
            }
        }

    }
}