using System;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services.WidgetIndexers
{
    public class RichTextWidgetIndexer : IWidgetIndexer
    {
        public string IndexComponent(dynamic control)
        {
            var text = string.Empty;

            try
            {
                var v = control.value.value;
                if (v != null)
                {
                    text = (string)v.text.ToString();
                }
            }
            catch (Exception e)
            {

            }

            return text;
        }
    }
}
