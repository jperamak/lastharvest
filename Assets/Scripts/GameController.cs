using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public int food;
        private Player _player;
        public int currenLevel = 1;
        public List<FamilyMember> family;
        private int harvestables;

        public void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _player.Harvested += OnHarvested;
            DontDestroyOnLoad(this);
            StartFamily();
            harvestables = GameObject.FindGameObjectsWithTag("Harvestable").Length;
        }

        private void OnHarvested(object sender, HarvestEventArgs args)
        {
            Debug.Log("Harvested: " + args.Harvestable);
            Destroy(args.Harvestable.gameObject);
            harvestables--;
            if (harvestables <= 0)
                Debug.Log("done");
            //StartCoroutine(NextLevel ());
        }

        private IEnumerator NextLevel()
        {
            //loading screen
            Feed();
            Application.LoadLevel("LoadingScreen");
            yield return new WaitForSeconds(5);
            //feed family
            Application.LoadLevel(++currenLevel);
            harvestables = GameObject.FindGameObjectsWithTag("Harvestable").Length;
        }

        private void StartFamily()
        {
            family = new List<FamilyMember>();
            family.Add(new FamilyMember("Billy-Bob", 23));
            family.Add(new FamilyMember("Wifey", 19));
            family.Add(new FamilyMember("Babby1", 3));
            family.Add(new FamilyMember("Babby2", 1));
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