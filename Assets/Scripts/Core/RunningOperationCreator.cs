using UnityEngine;

namespace Core
{
    public class RunningOperationCreator
    {
        public readonly RefOperationLoader RefOperationLoader;

        public RunningOperationCreator()
        {
            RefOperationLoader = new RefOperationLoader();
            RefOperationLoader.LoadData();
        }

        public RunningOperation Create(int depth)
        {
            RefOperation refOp = RefOperationLoader.RandomRefOperation(depth);

            RunningOperation runningOp = new RunningOperation();
            runningOp.Name = refOp.Name;
            runningOp.Description = refOp.Description;
            runningOp.Depth = depth;

            runningOp.Dig = 0;
            runningOp.DigTotal = GetDig(depth, refOp.Dig);

            runningOp.AtqChallenge = new AtqChallenge();
            runningOp.DefChallenge = new DefChallenge();
            
            runningOp.CapsReward = GetCapsReward(depth, refOp.CapsReward);
            runningOp.RootsReward = GetRootsReward(depth, refOp.RootsReward);

            return runningOp;
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