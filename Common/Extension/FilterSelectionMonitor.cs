using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
namespace Common
{
    public class FilterSelectionMonitor : ISelectionFilter
    {
        private ElementId _categoryId;
        public FilterSelectionMonitor(Category category)
        {
            _categoryId = category.Id;
        }
        public bool AllowElement(Element elem)
        {
            bool isElemet = elem.Category.Id.Equals(_categoryId) && elem.Category != null;
            return isElemet;
        }
        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
}
