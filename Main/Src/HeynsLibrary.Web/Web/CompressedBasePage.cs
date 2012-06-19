using System;
using System.IO;
using System.Linq;
using System.Web.UI;

using HeynsLibrary.Web.ViewState;
using HeynsLibrary.Compression;

namespace HeynsLibrary.Web
{
    public class CompressedBasePage : System.Web.UI.Page
    {
        private const string _compressedViewState = "__COMPRESSEDVIEWSTATE";

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            var stringWriter = new StringWriter();
            var htmlWriter = new HtmlTextWriter(stringWriter);
            base.Render(htmlWriter);
            string html = stringWriter.ToString();
            html = ViewStateOrganizer.Organize(html);
            writer.Write(html);
        }

        protected override void SavePageStateToPersistenceMedium(object pageViewState)
        {
            var compressed = true;
            var viewState = string.Empty;

            var losformatter = new LosFormatter();
            var stringWriter = new StringWriter();
            losformatter.Serialize(stringWriter, pageViewState);
            var viewStateString = stringWriter.ToString();
            var bytes = Convert.FromBase64String(viewStateString);            
            var compressedBytes = Compressor.Compress<byte[]>(bytes);
            if (compressedBytes.Length > bytes.Length)
            {
                compressed = false;
                viewState = Convert.ToInt32(compressed) + Convert.ToBase64String(bytes);
            }
            else
            {
                viewState = Convert.ToInt32(compressed) + Convert.ToBase64String(compressedBytes);
            }
            ClientScript.RegisterHiddenField(_compressedViewState, viewState);
        }

        // De-serialize view state
        protected override object LoadPageStateFromPersistenceMedium()
        {
            var isCompressed = true;

            var compressedViewState = Request.Form[_compressedViewState];
            isCompressed = Convert.ToBoolean(Convert.ToInt32(compressedViewState.Substring(0, 1)));
            compressedViewState = compressedViewState.Remove(0, 1);
            var bytes = Convert.FromBase64String(compressedViewState);            

            if(isCompressed)
                bytes = Compressor.Decompress<byte[]>(bytes);
            var losformatter = new LosFormatter();
            return losformatter.Deserialize(Convert.ToBase64String(bytes));
        }
    }
}