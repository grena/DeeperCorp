using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using Utils;

namespace Core
{
    public class RefOperationLoader
    {
        public List<RefOperation> RefOperations = new List<RefOperation>();

        public void LoadData()
        {
            string roomSchemasPath = Path.Combine(Application.streamingAssetsPath, "Data");
            var files = Directory.GetFiles(roomSchemasPath, "operations.xml");
            
            foreach (string filePath in files)
            {
                XElement xFileContent = XElement.Load(filePath);
                var operations = xFileContent.Elements("operation").ToList();
                
                foreach (XElement e in operations)
                {
                    RefOperation refOp = RefOperation.FromXml(e);
                    RefOperations.Add(refOp);
                }
            }
        }

        public RefOperation RandomRefOperation(int atDepth)
        {
            return RefOperations
                .Shuffle().Shuffle().Shuffle()
                .First(o => o.MinDepth <= atDepth && o.MaxDepth >= atDepth);
        }
    }
}