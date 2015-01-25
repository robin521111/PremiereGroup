using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeduInfo.Helper
{
    public class SilverlightHelper
    {
        /// <summary>
        /// add a silverlight controller
        /// </summary>
        /// <param name="silverlightXapFileLocation">Location for silverlight you adding</param>
        /// <param name="minimumRuntimeVersion">specific vertion for silverlight</param>
        /// <param name="objectContainerWidth">width</param>
        /// <param name="objectContainerHeight">height</param>
        /// <param name="onErrorJavaScriptFunctionName">error handller with javascript</param>
        /// <param name="divObjectTagId"></param>
        /// <param name="iFrameStyle">style of iframe</param>
        /// <returns>HtmlString for Silverlight</returns>
        public static HtmlString SilverlightHost(
            string silverlightXapFileLocation, // URI Location of the Silverlight XAP file
            string minimumRuntimeVersion = "4.0.50826.0",
            string objectContainerWidth = "100%",
            string objectContainerHeight = "100%",
            string onErrorJavaScriptFunctionName = "onSilverlightError",
            string divObjectTagId = "silverlightControlHost",
            string iFrameStyle = "visibility:hidden;height:0px;width:0px;border:0px"
            )
        {
            // build the string
            string silverlightFrame =
                                    string.Format(
                                        "<div id=\"{0}\">" +
                                                "<object data=\"data:application/x-silverlight-2,\" type=\"application/x-silverlight-2\" width=\"{1}\" height=\"{2}\">" +
                                                  "<param name=\"source\" value=\"{3}\"/>" +
                                                  "<param name=\"minRuntimeVersion\" value=\"{4}\" />" +
                                                  "<param name=\"onError\" value=\"{5}\" />" +
                                                  "<param name=\"background\" value=\"white\" />" +
                                                  "<param name=\"autoUpgrade\" value=\"true\" />" +
                                                  "<a href=\"http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0\" style=\"text-decoration:none\">" +
                                                      "<img src=\"http://go.microsoft.com/fwlink/?LinkId=161376\" alt=\"Get Microsoft Silverlight\" style=\"border-style:none\"/>" +
                                                  "</a>" +
                                                "</object><iframe id=\"_sl_historyFrame\" style=\"{6}\"></iframe>" +
                                          @"</div>",
                                      divObjectTagId,
                                      objectContainerWidth,
                                      objectContainerHeight,
                                      silverlightXapFileLocation,
                                      minimumRuntimeVersion,
                                      onErrorJavaScriptFunctionName,
                                      iFrameStyle
                                      );

            // Rendering alternative

            //XElement silverlightHtml = new XElement("div",
            //        new XAttribute("id", "silverlightControlHost"),
            //            new XElement("object",
            //                new XAttribute("data", "data:application/x-silverlight-2,"),
            //                new XAttribute("type", "application/x-silverlight-2"),
            //                new XAttribute("width", objectContainerWidth),
            //                new XAttribute("height", objectContainerHeight),
            //                    new XElement("param",
            //                        new XAttribute("name", "source"),
            //                        new XAttribute("value", silverlightXapFileLocation)),
            //                    new XElement("param",
            //                        new XAttribute("name", "onError"),
            //                        new XAttribute("value", onErrorJavaScriptFunctionName)),
            //                    new XElement("param",
            //                        new XAttribute("name", "background"),
            //                        new XAttribute("value", "white")),
            //                    new XElement("param",
            //                        new XAttribute("name", "minRuntimeVersion"),
            //                        new XAttribute("value", minimumRuntimeVersion)),
            //                    new XElement("param",
            //                        new XAttribute("name", "autoUpgrade"),
            //                        new XAttribute("value", "true")),
            //                    new XElement("a",
            //                        new XAttribute("href", "http://go.microsoft.com/fwlink/?LinkID=149156" + minimumRuntimeVersion),
            //                        new XAttribute("style", "text-decoration:none"),
            //                            new XElement("img",
            //                                new XAttribute("src", "http://go.microsoft.com/fwlink/?LinkId=161376"),
            //                                new XAttribute("alt", "Get Microsoft Silverlight"),
            //                                new XAttribute("style", "border-style:none"))
            //                                )
            //            ),
            //            new XElement("iframe",
            //                        new XAttribute("id", "_sl_historyFrame"),
            //                        new XAttribute("style", "visibility:hidden;height:0px;width:0px;border:0px"))
            //);

            string silverlightHtmlString = silverlightFrame.ToString();
            return new HtmlString(silverlightHtmlString);
        }
    
    }
}