#pragma checksum "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2b4c655cc5b09b2aa155296c688181bc23876062"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Planet_ViewPlanet), @"mvc.1.0.view", @"/Views/Planet/ViewPlanet.cshtml")]
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
#nullable restore
#line 1 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
using DarkGalaxyProject.Services.PlanetServices;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2b4c655cc5b09b2aa155296c688181bc23876062", @"/Views/Planet/ViewPlanet.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c87d1cb92b99a860fa765e763e61c97a7eab4136", @"/Views/_ViewImports.cshtml")]
    public class Views_Planet_ViewPlanet : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PlanetServiceModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 6 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
  
    ViewData["Title"] = "Planet View";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Planet ");
#nullable restore
#line 10 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
      Write(Model.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 10 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
                        Write(Model.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<hr />\r\n<div class=\"row\">\r\n    <div class=\"col-md-4\">\r\n        <span>Planet name: ");
#nullable restore
#line 15 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
                      Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n        <span>Position: ");
#nullable restore
#line 16 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
                   Write(Model.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n        <span>Type ");
#nullable restore
#line 17 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
              Write(Model.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n        <span>Factories---> Level: ");
#nullable restore
#line 18 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
                              Write(Model.Factories.Level);

#line default
#line hidden
#nullable disable
            WriteLiteral(", Income -----> ");
#nullable restore
#line 18 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
                                                                    Write(Model.Factories.Income);

#line default
#line hidden
#nullable disable
            WriteLiteral(", Upgrade Cost -----> ");
#nullable restore
#line 18 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
                                                                                                                 Write(Model.Factories.UpgradeCost);

#line default
#line hidden
#nullable disable
            WriteLiteral(", Upgrade Time ------> ");
#nullable restore
#line 18 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
                                                                                                                                                                    Write(Model.Factories.UpgradeTimeLength);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n");
#nullable restore
#line 19 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
         if (Model.PlayerId == UserManager.GetUserId(User))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2b4c655cc5b09b2aa155296c688181bc238760627603", async() => {
                WriteLiteral("\r\n                <input id=\"buildingId\" type=\"hidden\" name=\"buildingId\"");
                BeginWriteAttribute("value", " value=", 843, "", 869, 1);
#nullable restore
#line 22 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
WriteAttributeValue("", 850, Model.Factories.Id, 850, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                <input id=\"planetId\" type=\"hidden\" name=\"planetId\"");
                BeginWriteAttribute("value", " value=", 938, "", 954, 1);
#nullable restore
#line 23 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
WriteAttributeValue("", 945, Model.Id, 945, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                <div class=\"form-group\">\r\n                    <input id=\"input\" onclick=\"initialUpgrade()\" value=\"Level Up\" class=\"btn btn-primary\" />\r\n                </div>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral("            <p id=\"timer\"></p>\r\n");
#nullable restore
#line 30 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2b4c655cc5b09b2aa155296c688181bc2387606210254", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n        window.onload = function () {\r\n            if (\'");
#nullable restore
#line 41 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
            Write(Model.Factories.UpgradeFinishTime);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"' != '') {
                upgrade();
            }
            else {
            }
        }

        function SetUpgradeTime() {
            $(document).ready(function () {
                var buildingId = $(""#buildingId"").val();
                var planetId = $(""#planetId"").val();
                var datastring = ""buildingId="" + buildingId + ""&planetId="" + planetId;
                $.ajax({
                    type: ""post"",
                    url: ""SetUpgradeTime"",
                    data: datastring
                })
            })
        }

        function NullifyUpgradeTime() {
            $(document).ready(function () {
                console.log('NullifyUpgradeTime');
                var buildingId = $(""#buildingId"").val();
                var planetId = $(""#planetId"").val();
                var datastring = ""buildingId="" + buildingId + ""&planetId="" + planetId;
                $.ajax({
                    type: ""post"",
                    url: ""NullifyUpgradeTime"",
");
                WriteLiteral(@"                    data: datastring
                })
            })
        }

        var counter = 0;
        //If i refresh the page though the time would still disappear and nothing will happen when the UpgradeTime == Date.now()
        //I need another function activating on page load checking if the property UpgradeTime is not null
        function convertSeconds(s) {
            var min = Math.floor(s / 60);
            var sec = Math.floor(s % 60);
            if (min < 10) {
                min = '0' + min;
            }
            if (sec < 10) {
                sec = '0' + sec;
            }
            return min + ':' + sec;
        }
        var dateString = '");
#nullable restore
#line 89 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Planet\ViewPlanet.cshtml"
                      Write(Model.Factories.UpgradeFinishTime.HasValue ? Model.Factories.UpgradeFinishTime.Value : null);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';

        function initialUpgrade() {
            if (dateString == '') {
                $(document).ready(function () {
                    var buildingId = $(""#buildingId"").val();
                    var planetId = $(""#planetId"").val();
                    var datastring = ""buildingId="" + buildingId + ""&planetId="" + planetId;
                    $.ajax({
                        type: ""post"",
                        url: ""StartUpgrade"",
                        data: datastring
                    });
                    SetUpgradeTime();
                    upgrade();
                });
                setTimeout(function () { location.reload(); }, 200); //If I don't set timeout sometimes the timer still isn't out
            }
        }

        function upgrade() {
            var timeleft = 0;
            if (dateString != '') {
                var oneSplit = dateString.split(' ');
                var year = oneSplit[0].split('/')[2];
                var month = oneSplit[0].sp");
                WriteLiteral(@"lit('/')[1];
                var day = oneSplit[0].split('/')[0];
                var hour = oneSplit[1].split(':')[0];
                var minutes = oneSplit[1].split(':')[1];
                var seconds = oneSplit[1].split(':')[2];

                var upgradeDate = new Date(year, month - 1, day, hour, minutes, seconds);

                timeleft = (upgradeDate.getTime() - new Date().getTime()) / 1000;
            }

            if (timeleft > 0) {
                document.getElementById('timer').innerText = convertSeconds(timeleft);
                var _tick = setInterval(function () {
                    counter++;
                    document.getElementById('timer').innerText = convertSeconds(timeleft - counter);
                    if (counter >= timeleft) {
                        clearInterval(_tick);
                        console.log('-_-');
                        levelUp();
                    }
                }, 1000);
            }

            if (timeleft <= 0 && date");
                WriteLiteral(@"String != '') {
                levelUp();
            }

            function levelUp() {
                $(document).ready(function () {
                    var buildingId = $(""#buildingId"").val();
                    var planetId = $(""#planetId"").val();
                    var datastring = ""buildingId="" + buildingId + ""&planetId="" + planetId;
                    $.ajax({
                        type: ""post"",
                        url: ""LevelUp"",
                        data: datastring
                    });
                    NullifyUpgradeTime();
                    setTimeout(function () { location.reload(); }, 200); //If I don't set timeout sometimes the level still shows the same
                });
            }
        }
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PlanetServiceModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
