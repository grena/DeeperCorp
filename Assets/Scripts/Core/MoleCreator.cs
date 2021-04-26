using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Core
{
    public class MoleCreator
    {
        public List<Sprite> sprites = new List<Sprite>();
        
        public Mole Create()
        {
            int percentMasterOne = Mathf.Min(100, 10 + GameManager.Singleton.UpgradeMasterTotal); // 15% chance to be master in one field
            int percentMasterTwo = Mathf.Min(100, 5 + GameManager.Singleton.UpgradeMasterTotal); // 5% chance to be master in two fields
            int percentMasterThree = Mathf.Min(100, 0 + GameManager.Singleton.UpgradeMasterTotal); // 0% chance to be master in three fields

            List<string> fields = new List<string> {"dig", "atq", "def"};
            int masterLevel = 0;
            
            int roll = Random.Range(0, 101);
            if (roll < percentMasterOne) masterLevel = 1;
            if (roll < percentMasterTwo) masterLevel = 2;
            if (roll < percentMasterThree) masterLevel = 3;

            List<string> masterFields = fields.Shuffle().Shuffle().Shuffle().Take(masterLevel).ToList();

            float digBonus = 1 + (GameManager.Singleton.UpgradeDigTotal / 100);
            float atqBonus = 1 + (GameManager.Singleton.UpgradeAtqTotal / 100);
            float defBonus = 1 + (GameManager.Singleton.UpgradeDefTotal / 100);
            
            float digRoll = Mathf.CeilToInt(Random.Range(3f, 8f));
            float atqRoll = Mathf.CeilToInt(Random.Range(0f, 2f));
            float defRoll = Mathf.CeilToInt(Random.Range(0f, 2f));
            
            Mole mole = new Mole
            {
                Dig = Mathf.CeilToInt(digRoll * digBonus),
                Atq = Mathf.CeilToInt(atqRoll * atqBonus),
                Def = Mathf.CeilToInt(defRoll * defBonus),
                masterLevel = masterLevel,
                Sprite = sprites.Shuffle().Shuffle().Shuffle().First()
            };
            
            if (masterFields.Contains("dig")) mole.Dig = mole.Dig * 3;
            if (masterFields.Contains("atq")) mole.Atq = mole.Atq * 3;
            if (masterFields.Contains("def")) mole.Def = mole.Def * 3;

            return mole;
        }
    }
}