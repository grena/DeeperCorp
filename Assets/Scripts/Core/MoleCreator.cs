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
            int percentMasterOne = 15; // 15% chance to be master in one field
            int percentMasterTwo = 5; // 5% chance to be master in two fields
            int percentMasterThree = 1; // 1% chance to be master in three fields

            List<string> fields = new List<string> {"dig", "atq", "def"};
            int masterLevel = 0;
            int roll = Random.Range(0, 101);
            if (roll < percentMasterOne) masterLevel = 1;
            if (roll < percentMasterTwo) masterLevel = 2;
            if (roll < percentMasterThree) masterLevel = 3;

            List<string> masterFields = fields.Shuffle().Shuffle().Shuffle().Take(masterLevel).ToList();

            Mole mole = new Mole
            {
                Dig = Random.Range(2, 6 + 1),
                Atq = Random.Range(0, 2 + 1),
                Def = Random.Range(0, 2 + 1),
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