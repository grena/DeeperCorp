using UnityEngine;

namespace Core
{
    public class RunningOperationCreator
    {
        public readonly RefLoader RefLoader;

        public RunningOperationCreator()
        {
            RefLoader = new RefLoader();
            RefLoader.LoadData();
        }

        public RunningOperation Create(int depth)
        {
            RefOperation refOp = RefLoader.RandomRefOperation(depth);

            RunningOperation runningOp = new RunningOperation();
            runningOp.Name = refOp.Name;
            runningOp.Description = refOp.Description;
            runningOp.Depth = depth;

            runningOp.Dig = 0;
            runningOp.DigTotal = GetDig(depth, refOp.Dig);

            runningOp.AtqChallenge = CreateAtq(depth);
            runningOp.DefChallenge = CreateDef(depth);
            
            runningOp.CapsReward = GetCapsReward(depth, refOp.CapsReward);
            runningOp.RootsReward = GetRootsReward(depth, refOp.RootsReward);

            return runningOp;
        }

        public RunningOperation CreateDeeper(int depth)
        {
            RunningOperation runningOp = new RunningOperation();
            runningOp.Name = "GO DEEPER.";
            runningOp.Description = "By going DEEPER, we'll find new expeditions with more resources. Go DEEPER, go BETTER, Deeper CORP.\n\nCAUTION !!! This will replace all available expeditions.";
            runningOp.Depth = depth;

            runningOp.Dig = 0;
            runningOp.DigTotal = GetDig(depth, "HIGH");
            
            runningOp.AtqChallenge = new AtqChallenge();
            runningOp.DefChallenge = new DefChallenge();
            
            runningOp.CapsReward = 0;
            runningOp.RootsReward = 0;

            return runningOp;
        }

        private AtqChallenge CreateAtq(int depth)
        {
            RefAtq refAtq = RefLoader.RandomRefAtq(depth);

            AtqChallenge atqChallenge = new AtqChallenge
            {
                Name = refAtq.Name, 
                RequiredValue = GetAtq(depth, refAtq.Level)
            };

            return atqChallenge;
        }

        private int GetAtq(int depth, string level)
        {
            if (level == "NONE") return 0;

            int atq = 0 + depth;
            
            if (level == "LOW") atq = Mathf.FloorToInt( Random.Range(1.05f, 1.15f) * atq); // +5% ~ +15%
            if (level == "MED") atq = Mathf.FloorToInt(Random.Range(1.20f, 1.45f) * atq); // +20% ~ +45%
            if (level == "HIGH") atq = Mathf.FloorToInt(Random.Range(1.60f, 1.90f) * atq); // +60% ~ +90%

            return atq;
        }
        
        private DefChallenge CreateDef(int depth)
        {
            RefDef refDef = RefLoader.RandomRefDef(depth);

            DefChallenge defChallenge = new DefChallenge
            {
                Name = refDef.Name, 
                RequiredValue = GetDef(depth, refDef.Level)
            };

            return defChallenge;
        }

        private int GetDef(int depth, string level)
        {
            if (level == "NONE") return 0;

            int def = 0 + depth;
            
            if (level == "LOW") def = Mathf.FloorToInt( Random.Range(1.05f, 1.15f) * def); // +5% ~ +15%
            if (level == "MED") def = Mathf.FloorToInt(Random.Range(1.20f, 1.45f) * def); // +20% ~ +45%
            if (level == "HIGH") def = Mathf.FloorToInt(Random.Range(1.60f, 1.90f) * def); // +60% ~ +90%

            return def;
        }

        private int GetDig(int depth, string level)
        {
            int dig = 6 + (2 * depth);

            if (level == "LOW") dig = Mathf.FloorToInt( Random.Range(1.05f, 1.15f) * dig); // +5% ~ +15%
            if (level == "MED") dig = Mathf.FloorToInt(Random.Range(1.20f, 1.45f) * dig); // +20% ~ +45%
            if (level == "HIGH") dig = Mathf.FloorToInt(Random.Range(1.60f, 1.90f) * dig); // +60% ~ +90% 

            return dig;
        }

        private int GetCapsReward(int depth, string level)
        {
            if (string.IsNullOrEmpty(level)) return 0;
            
            int caps = 10 + (2 * depth);

            if (level == "LOW") caps = Mathf.FloorToInt( Random.Range(1.05f, 1.15f) * caps); // +5% ~ +15%
            if (level == "MED") caps = Mathf.FloorToInt(Random.Range(1.20f, 1.45f) * caps); // +20% ~ +45%
            if (level == "HIGH") caps = Mathf.FloorToInt(Random.Range(1.60f, 1.90f) * caps); // +60% ~ +90% 

            return caps;
        }
        
        private int GetRootsReward(int depth, string level)
        {
            if (string.IsNullOrEmpty(level)) return 0;
            
            int roots = 100 + (2 * depth);

            if (level == "LOW") roots = Mathf.FloorToInt( Random.Range(1.05f, 1.15f) * roots); // +5% ~ +15%
            if (level == "MED") roots = Mathf.FloorToInt(Random.Range(1.20f, 1.45f) * roots); // +20% ~ +45%
            if (level == "HIGH") roots = Mathf.FloorToInt(Random.Range(1.60f, 1.90f) * roots); // +60% ~ +90% 

            return roots;
        }
    }
}