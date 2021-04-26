using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using Utils;

namespace Core
{
    public class RefLoader
    {
        public List<RefOperation> RefOperations = new List<RefOperation>();
        public List<RefAtq> RefAtqs = new List<RefAtq>();
        public List<RefDef> RefDefs = new List<RefDef>();
        public List<DefChallenge> RefDefChallenges = new List<DefChallenge>();

        public void LoadData()
        {
            // LOAD OPERATIONS
            // if your original XML file is located at
            // "Ressources/MyXMLFile.xml"
            TextAsset textAsset = (TextAsset) Resources.Load("Data/operations");  
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(textAsset.text);
            XElement xFileContent = XElement.Load(xmldoc.DocumentElement.CreateNavigator().ReadSubtree());
            
            var operations = xFileContent.Elements("operation").ToList();
            
            foreach (XElement e in operations)
            {
                RefOperation refOp = RefOperation.FromXml(e);
                RefOperations.Add(refOp);
            }
            
            // LOAD ATQ CHALLENGES
            textAsset = (TextAsset) Resources.Load("Data/creatures");  
            xmldoc = new XmlDocument();
            xmldoc.LoadXml(textAsset.text);
            xFileContent = XElement.Load(xmldoc.DocumentElement.CreateNavigator().ReadSubtree());
            
            var creatures = xFileContent.Elements("creature").ToList();
            
            foreach (XElement e in creatures)
            {
                RefAtq refAtq = RefAtq.FromXml(e);
                RefAtqs.Add(refAtq);
            }
            
            // LOAD DEF CHALLENGES
            textAsset = (TextAsset) Resources.Load("Data/dangers");  
            xmldoc = new XmlDocument();
            xmldoc.LoadXml(textAsset.text);
            xFileContent = XElement.Load(xmldoc.DocumentElement.CreateNavigator().ReadSubtree());
            
            var dangers = xFileContent.Elements("danger").ToList();
            
            foreach (XElement e in dangers)
            {
                RefDef refAtq = RefDef.FromXml(e);
                RefDefs.Add(refAtq);
            }
        }

        public RefOperation RandomRefOperation(int atDepth)
        {
            return RefOperations
                .Shuffle().Shuffle().Shuffle()
                .First(o => o.MinDepth <= atDepth && o.MaxDepth >= atDepth);
        }

        public RefAtq RandomRefAtq(int atDepth)
        {
            return RefAtqs
                .Shuffle().Shuffle().Shuffle()
                .First(o => o.MinDepth <= atDepth && o.MaxDepth >= atDepth);
        }
        
        public RefDef RandomRefDef(int atDepth)
        {
            return RefDefs
                .Shuffle().Shuffle().Shuffle()
                .First(o => o.MinDepth <= atDepth && o.MaxDepth >= atDepth);
        }
    }
}