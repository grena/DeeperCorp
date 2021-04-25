using System;
using System.Xml.Linq;

namespace Core
{
    public class RefAtq
    {
        public string Name;
        public string Level;
        public int MinDepth;
        public int MaxDepth;
        
        public static RefAtq FromXml(XElement xElement)
        {
            RefAtq refAtq = new RefAtq();

            refAtq.Name = LoadText(xElement);
            refAtq.Level = xElement.Attribute("level")?.Value;
            refAtq.MinDepth = Convert.ToInt32(xElement.Attribute("minDepth")?.Value);
            refAtq.MaxDepth = Convert.ToInt32(xElement.Attribute("maxDepth")?.Value);
            refAtq.MaxDepth = refAtq.MaxDepth == 0 ? 999999999 : refAtq.MaxDepth;

            return refAtq;
        }
        
        private static string LoadText(XElement xmlText)
        {
            return xmlText.Value.Trim().Replace("    ", "");
        }
    }
}