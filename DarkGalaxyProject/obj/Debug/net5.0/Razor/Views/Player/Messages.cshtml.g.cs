#pragma checksum "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "284623ba0448f9015d5861faaf34615e329cadd5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Player_Messages), @"mvc.1.0.view", @"/Views/Player/Messages.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\_ViewImports.cshtml"
using DarkGalaxyProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\_ViewImports.cshtml"
using DarkGalaxyProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml"
using DarkGalaxyProject.Models.Player;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"284623ba0448f9015d5861faaf34615e329cadd5", @"/Views/Player/Messages.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c3dc50ee04f6b71ac87ad788b1384ab4f3a7461", @"/Views/_ViewImports.cshtml")]
    public class Views_Player_Messages : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<MessageListingViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nav-link text-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Player", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SendMessage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml"
  
    ViewData["Title"] = "Messages";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>\r\n    Messages\r\n</h1>\r\n\r\n<ul>\r\n");
#nullable restore
#line 13 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml"
     foreach (var message in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li>\r\n            <span>");
#nullable restore
#line 16 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml"
             Write(message.ReceiverName);

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 16 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml"
                                    Write(message.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n            <span>Time: ");
#nullable restore
#line 17 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml"
                   Write(message.Time);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n            <a");
            BeginWriteAttribute("href", " href=\"", 351, "\"", 387, 2);
            WriteAttributeValue("", 358, "Message?messageId=", 358, 18, true);
#nullable restore
#line 18 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml"
WriteAttributeValue("", 376, message.Id, 376, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Read</a>\r\n        </li>\r\n");
#nullable restore
#line 20 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Player\Messages.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "284623ba0448f9015d5861faaf34615e329cadd56595", async() => {
                WriteLiteral("Write a new message");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<MessageListingViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
