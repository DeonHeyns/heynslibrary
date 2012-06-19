namespace HeynsLibrary.Web.ViewState
{
    public static class ViewStateOrganizer
    {
        public static string Organize(string html)
        {
            return AspNetHiddenMove(html);
        }

        private static string AspNetHiddenMove(string html)
        {
            var viewStateStartPoint = html.IndexOf("<div class=\"aspNetHidden\"");
            if (viewStateStartPoint >= 0)
            {
                int endPoint = html.IndexOf("</div>", viewStateStartPoint) + 6;
                string viewstateInput = html.Substring(viewStateStartPoint, endPoint - viewStateStartPoint);
                html = html.Remove(viewStateStartPoint, endPoint - viewStateStartPoint);
                int formEndStart = html.LastIndexOf("</form>");
                if (formEndStart >= 0)
                    html = html.Insert(formEndStart, viewstateInput);
            }
            return html;
        }
    }

    //Usage:
    // Base Page / Page 
    //protected override void Render(System.Web.UI.HtmlTextWriter writer)
    //    {
    //        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
    //        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
    //        base.Render(htmlWriter);
    //        string html = stringWriter.ToString();
    //        html = this.Organize(html);
    //        writer.Write(html);
    //    }
}