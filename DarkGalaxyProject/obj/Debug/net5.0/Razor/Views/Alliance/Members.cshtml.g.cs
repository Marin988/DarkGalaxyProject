#pragma checksum "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36ff0f2baf73695ba3725781b43229a8bef029c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Alliance_Members), @"mvc.1.0.view", @"/Views/Alliance/Members.cshtml")]
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
#line 1 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
using DarkGalaxyProject.Data.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
using DarkGalaxyProject.Models.Alliance;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36ff0f2baf73695ba3725781b43229a8bef029c0", @"/Views/Alliance/Members.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c87d1cb92b99a860fa765e763e61c97a7eab4136", @"/Views/_ViewImports.cshtml")]
    public class Views_Alliance_Members : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MembersCandidatesViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Leave", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "PromoteToLeader", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AcceptCandidate", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RejectCandidate", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 7 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
  
    ViewData["Title"] = "Members";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 11 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
 if (TempData["Message"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p> ");
#nullable restore
#line 13 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
   Write(TempData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n");
#nullable restore
#line 14 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>Members:</h2>\r\n\r\n<ul>\r\n");
#nullable restore
#line 19 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
     foreach (var member in Model.Members)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <li>\r\n        <span>Player ");
#nullable restore
#line 22 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
                Write(member.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" with ");
#nullable restore
#line 22 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
                                      Write(member.Systems);

#line default
#line hidden
#nullable disable
            WriteLiteral(" systems</span>\r\n");
#nullable restore
#line 23 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
         if (member.Id == UserManager.GetUserId(User))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36ff0f2baf73695ba3725781b43229a8bef029c07606", async() => {
                WriteLiteral("\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36ff0f2baf73695ba3725781b43229a8bef029c07880", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 26 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                <div class=\"form-group\">\r\n                    <input type=\"text\" id=\"playerId\" name=\"playerId\" hidden");
                BeginWriteAttribute("value", " value=", 789, "", 806, 1);
#nullable restore
#line 28 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
WriteAttributeValue("", 796, member.Id, 796, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n                </div>\r\n                <div class=\"form-group\">\r\n                    <input type=\"submit\" value=\"Leave\" class=\"btn btn-primary\" />\r\n                </div>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 34 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
         if (UserManager.GetUserId(User) == Model.AllianceLeaderId && member.Id != UserManager.GetUserId(User))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36ff0f2baf73695ba3725781b43229a8bef029c011949", async() => {
                WriteLiteral("\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36ff0f2baf73695ba3725781b43229a8bef029c012224", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 38 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                <div class=\"form-group\">\r\n                    <input type=\"text\" id=\"playerId\" name=\"playerId\" hidden");
                BeginWriteAttribute("value", " value=", 1411, "", 1428, 1);
#nullable restore
#line 40 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
WriteAttributeValue("", 1418, member.Id, 1418, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n                    <input type=\"text\" id=\"allianceId\" name=\"allianceId\" hidden");
                BeginWriteAttribute("value", " value=", 1533, "", 1557, 1);
#nullable restore
#line 41 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
WriteAttributeValue("", 1540, Model.AllianceId, 1540, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n                </div>\r\n                <div class=\"form-group\">\r\n                    <input type=\"submit\" value=\"Promote to leader\" class=\"btn btn-primary\" />\r\n                </div>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 47 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </li>\r\n");
#nullable restore
#line 49 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\r\n        --------------------------------Candidates------------------------------------------ -\r\n<ul>\r\n");
#nullable restore
#line 53 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
     foreach (var candidate in Model.Candidates)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <li>\r\n        <span>Player ");
#nullable restore
#line 56 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
                Write(candidate.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" with ");
#nullable restore
#line 56 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
                                         Write(candidate.Systems);

#line default
#line hidden
#nullable disable
            WriteLiteral(" systems</span>\r\n");
#nullable restore
#line 57 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
         if (UserManager.GetUserId(User) == Model.AllianceLeaderId)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36ff0f2baf73695ba3725781b43229a8bef029c017925", async() => {
                WriteLiteral("\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36ff0f2baf73695ba3725781b43229a8bef029c018200", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 60 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                <div class=\"form-group\">\r\n                    <input type=\"text\" id=\"allianceId\" name=\"allianceId\" hidden");
                BeginWriteAttribute("value", " value=", 2409, "", 2433, 1);
#nullable restore
#line 62 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
WriteAttributeValue("", 2416, Model.AllianceId, 2416, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n                    <input type=\"text\" id=\"candidateId\" name=\"candidateId\" hidden");
                BeginWriteAttribute("value", " value=", 2540, "", 2560, 1);
#nullable restore
#line 63 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
WriteAttributeValue("", 2547, candidate.Id, 2547, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n                </div>\r\n                <div class=\"form-group\">\r\n                    <input type=\"submit\" value=\"Accept\" class=\"btn btn-primary\" />\r\n                </div>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36ff0f2baf73695ba3725781b43229a8bef029c022288", async() => {
                WriteLiteral("\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36ff0f2baf73695ba3725781b43229a8bef029c022563", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 70 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                <div class=\"form-group\">\r\n                    <input type=\"text\" id=\"allianceId\" name=\"allianceId\" hidden");
                BeginWriteAttribute("value", " value=", 3035, "", 3059, 1);
#nullable restore
#line 72 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
WriteAttributeValue("", 3042, Model.AllianceId, 3042, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n                    <input type=\"text\" id=\"candidateId\" name=\"candidateId\" hidden");
                BeginWriteAttribute("value", " value=", 3166, "", 3186, 1);
#nullable restore
#line 73 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
WriteAttributeValue("", 3173, candidate.Id, 3173, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n                </div>\r\n                <div class=\"form-group\">\r\n                    <input type=\"submit\" value=\"Reject\" class=\"btn btn-primary\" />\r\n                </div>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 79 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </li>\r\n");
#nullable restore
#line 81 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Alliance\Members.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<Player> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MembersCandidatesViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
