#pragma checksum "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b1b0193abb04051c0c6f94a6fbafab212b84f08"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Planet_PopulatedPlanet), @"mvc.1.0.view", @"/Views/Planet/PopulatedPlanet.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b1b0193abb04051c0c6f94a6fbafab212b84f08", @"/Views/Planet/PopulatedPlanet.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c3dc50ee04f6b71ac87ad788b1384ab4f3a7461", @"/Views/_ViewImports.cshtml")]
    public class Views_Planet_PopulatedPlanet : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DarkGalaxyProject.Models.PopulatedPlanetViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("LevelUp"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
  
    ViewData["Title"] = "BuildShip";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>System ");
#nullable restore
#line 7 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
      Write(Model.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 7 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                        Write(Model.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<h4>Populated planet</h4>\r\n<hr />\r\n<div class=\"row\">\r\n    <div class=\"col-md-4\">\r\n        <span>Planet name: ");
#nullable restore
#line 13 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                      Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n        <span>Position: ");
#nullable restore
#line 14 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                   Write(Model.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n        <span>Type: ");
#nullable restore
#line 15 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
               Write(Model.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n        <span>Amenities ------> Level: ");
#nullable restore
#line 16 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                                  Write(Model.Amenity.Level);

#line default
#line hidden
#nullable disable
            WriteLiteral(", CulturalIncrement -----> ");
#nullable restore
#line 16 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                                                                                 Write(Model.Amenity.CulturalIncrement);

#line default
#line hidden
#nullable disable
            WriteLiteral(" , Energy Cost -----> ");
#nullable restore
#line 16 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                                                                                                                                       Write(Model.Amenity.EnergyCost);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b1b0193abb04051c0c6f94a6fbafab212b84f087131", async() => {
                WriteLiteral("\r\n            <input type=\"hidden\" name=\"buildingId\"");
                BeginWriteAttribute("value", " value=", 668, "", 692, 1);
#nullable restore
#line 18 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
WriteAttributeValue("", 675, Model.Amenity.Id, 675, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n            <input type=\"hidden\" name=\"systemId\"");
                BeginWriteAttribute("value", " value=", 743, "", 765, 1);
#nullable restore
#line 19 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
WriteAttributeValue("", 750, Model.SystemId, 750, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n            <input type=\"hidden\" name=\"planetType\"");
                BeginWriteAttribute("value", " value=", 818, "", 842, 1);
#nullable restore
#line 20 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
WriteAttributeValue("", 825, Model.PlanetType, 825, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n            <div class=\"form-group\">\r\n                <input type=\"submit\" value=\"Level Up\" class=\"btn btn-primary\" />\r\n            </div>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        <span>LivingQuarters -----> Level: ");
#nullable restore
#line 25 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                                      Write(Model.LivingQuarters.Level);

#line default
#line hidden
#nullable disable
            WriteLiteral(", PopulationCapacity -----> ");
#nullable restore
#line 25 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                                                                                             Write(Model.LivingQuarters.PopulationCapacity);

#line default
#line hidden
#nullable disable
            WriteLiteral(" , Upgrade Cost -----> ");
#nullable restore
#line 25 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
                                                                                                                                                            Write(Model.LivingQuarters.UpgradeCost);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b1b0193abb04051c0c6f94a6fbafab212b84f0811253", async() => {
                WriteLiteral("\r\n            <input type=\"hidden\" name=\"buildingId\"");
                BeginWriteAttribute("value", " value=", 1308, "", 1339, 1);
#nullable restore
#line 27 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
WriteAttributeValue("", 1315, Model.LivingQuarters.Id, 1315, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n            <input type=\"hidden\" name=\"systemId\"");
                BeginWriteAttribute("value", " value=", 1390, "", 1412, 1);
#nullable restore
#line 28 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
WriteAttributeValue("", 1397, Model.SystemId, 1397, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n            <input type=\"hidden\" name=\"planetType\"");
                BeginWriteAttribute("value", " value=", 1465, "", 1489, 1);
#nullable restore
#line 29 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
WriteAttributeValue("", 1472, Model.PlanetType, 1472, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n            <div class=\"form-group\">\r\n                <input type=\"submit\" value=\"Level Up\" class=\"btn btn-primary\" />\r\n            </div>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b1b0193abb04051c0c6f94a6fbafab212b84f0814306", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 42 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\PopulatedPlanet.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DarkGalaxyProject.Models.PopulatedPlanetViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
