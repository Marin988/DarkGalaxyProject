#pragma checksum "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "82ab56d57644c473294f1b9748f9966089ae732d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_System_PlayerSystems), @"mvc.1.0.view", @"/Views/System/PlayerSystems.cshtml")]
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
#line 3 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\_ViewImports.cshtml"
using DarkGalaxyProject.Data.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82ab56d57644c473294f1b9748f9966089ae732d", @"/Views/System/PlayerSystems.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c87d1cb92b99a860fa765e763e61c97a7eab4136", @"/Views/_ViewImports.cshtml")]
    public class Views_System_PlayerSystems : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SystemViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
  
    ViewData["Title"] = "My systems";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>\r\n    My systems\r\n</h1>\r\n\r\n<ul>\r\n");
#nullable restore
#line 12 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
     foreach (var system in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li>\r\n            <span>Position: ");
#nullable restore
#line 15 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
                       Write(system.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n            <span>Type: ");
#nullable restore
#line 16 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
                   Write(system.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n");
#nullable restore
#line 17 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
             foreach (var ship in system.Ships)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span>HP: ");
#nullable restore
#line 19 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
                     Write(ship.HP);

#line default
#line hidden
#nullable disable
            WriteLiteral("---------</span>\r\n                <span>Damage: ");
#nullable restore
#line 20 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
                         Write(ship.Damage);

#line default
#line hidden
#nullable disable
            WriteLiteral("---------</span>\r\n                <span>Speed: ");
#nullable restore
#line 21 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
                        Write(ship.Speed);

#line default
#line hidden
#nullable disable
            WriteLiteral("---------</span>\r\n                <span>Storage: ");
#nullable restore
#line 22 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
                          Write(ship.Storage);

#line default
#line hidden
#nullable disable
            WriteLiteral("---------</span>\r\n                <span>Type: ");
#nullable restore
#line 23 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
                       Write(ship.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n");
#nullable restore
#line 24 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "82ab56d57644c473294f1b9748f9966089ae732d6807", async() => {
                WriteLiteral("\r\n                <input hidden name=\"systemId\"");
                BeginWriteAttribute("value", " value=", 768, "", 785, 1);
#nullable restore
#line 26 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
WriteAttributeValue("", 775, system.Id, 775, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                <input type=\"submit\" value=\"View system\" />\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 25 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
AddHtmlAttributeValue("", 682, Url.Action("SwitchSystem", "System"), 682, 37, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </li>\r\n");
#nullable restore
#line 30 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\PlayerSystems.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SystemViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
