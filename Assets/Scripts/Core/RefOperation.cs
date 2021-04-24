using System;
using System.Xml.Linq;

namespace Core
{
    public class RefOperation
    {
        public string Name;
        public string Description;
        public int MinDepth;
        public int MaxDepth;
        
        public string Dig;
        
        public string CapsReward;
        public string RootsReward;
        
        public static RefOperation FromXml(XElement xElement)
        {
            RefOperation op = new RefOperation();
            
            op.Name = LoadText(xElement.Element("name"));
            op.Description = LoadText(xElement.Element("description"));
            op.MinDepth = Convert.ToInt32(xElement.Attribute("minDepth")?.Value);
            op.MaxDepth = Convert.ToInt32(xElement.Attribute("maxDepth")?.Value);
            op.MaxDepth = op.MaxDepth == 0 ? 999999999 : op.MaxDepth;
            op.Dig = xElement.Element("dig").Value;
            op.CapsReward = xElement.Element("reward").Attribute("caps")?.Value; 
            op.RootsReward = xElement.Element("reward").Attribute("roots")?.Value; 

            return op;
        }
        
        private static string LoadText(XElement xmlText)
        {
            return xmlText.Value.Trim().Replace("    ", "");
        }
    }
}