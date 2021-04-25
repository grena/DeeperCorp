using System;
using System.Xml.Linq;

namespace Core
{
    public class RefDef
    {
        public string Name;
        public string Level;
        public int MinDepth;
        public int MaxDepth;
        
        public static RefDef FromXml(XElement xElement)
        {
            RefDef refDef = new RefDef();

            refDef.Name = LoadText(xElement);
            refDef.Level = xElement.Attribute("level")?.Value;
            refDef.MinDepth = Convert.ToInt32(xElement.Attribute("minDepth")?.Value);
            refDef.MaxDepth = Convert.ToInt32(xElement.Attribute("maxDepth")?.Value);
            refDef.MaxDepth = refDef.MaxDepth == 0 ? 999999999 : refDef.MaxDepth;

            return refDef;
        }
        
        private static string LoadText(XElement xmlText)
        {
            return xmlText.Value.Trim().Replace("    ", "");
        }
    }
}