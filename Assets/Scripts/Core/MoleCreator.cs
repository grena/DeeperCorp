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
            float percentMasterOne = 15 + GameManager.Singleton.UpgradeMasterTotal; // 15% chance to be master in one field
            float percentMasterTwo = 5 + GameManager.Singleton.UpgradeMasterTotal; // 5% chance to be master in two fields
            float percentMasterThree = 0 + GameManager.Singleton.UpgradeMasterTotal; // 0% chance to be master in three fields

            List<string> fields = new List<string> {"dig", "atq", "def"};
            int masterLevel = 0;
            
            float roll = Random.Range(0, 100);
            if (roll < percentMasterOne) masterLevel = 1;
            if (roll < percentMasterTwo) masterLevel = 2;
            if (roll < percentMasterThree) masterLevel = 3;

            List<string> masterFields = fields.Shuffle().Shuffle().Shuffle().Take(masterLevel).ToList();

            float digBonus = 1 + (GameManager.Singleton.UpgradeDigTotal / 100);
            float atqBonus = 1 + (GameManager.Singleton.UpgradeAtqTotal / 100);
            float defBonus = 1 + (GameManager.Singleton.UpgradeDefTotal / 100);
            
            Mole mole = new Mole
            {
                Dig = Mathf.CeilToInt(Random.Range(4, 7) * digBonus),
                Atq = Mathf.CeilToInt(Random.Range(0, 3) * atqBonus),
                Def = Mathf.CeilToInt(Random.Range(0, 3) * defBonus),
                masterLevel = masterLevel,
                Sprite = sprites.Shuffle().Shuffle().Shuffle().First()
            };
            
            if (masterFields.Contains("dig")) mole.Dig = mole.Dig * 3;
            if (masterFields.Contains("atq")) mole.Atq = (mole.Atq + 1) * 3;
            if (masterFields.Contains("def")) mole.Def = (mole.Def + 1) * 3;

            return mole;
        }
    }
}