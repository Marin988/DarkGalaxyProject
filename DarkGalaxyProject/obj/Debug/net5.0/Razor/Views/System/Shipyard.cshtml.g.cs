#pragma checksum "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7bda30f6adcbeb12cc8a4a4dde37d30046db20bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_System_Shipyard), @"mvc.1.0.view", @"/Views/System/Shipyard.cshtml")]
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
#line 1 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
using DarkGalaxyProject.Models.System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
using DarkGalaxyProject.Data.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
using System.Text;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7bda30f6adcbeb12cc8a4a4dde37d30046db20bc", @"/Views/System/Shipyard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c87d1cb92b99a860fa765e763e61c97a7eab4136", @"/Views/_ViewImports.cshtml")]
    public class Views_System_Shipyard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<BuildShipFormViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("shipType"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
  
    ViewData["Title"] = "Shipyard";
    string shipType = "s";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>\r\n    Build Ships\r\n</h1>\r\n\r\n<span>Build ship</span>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7bda30f6adcbeb12cc8a4a4dde37d30046db20bc5194", async() => {
                WriteLiteral("\r\n    <div class=\"form-group\">\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7bda30f6adcbeb12cc8a4a4dde37d30046db20bc5494", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 18 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Html.GetEnumSelectList<ShipType>();

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <input id=\"count\" class=\"form-control\" />\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <input id=\"systemId\" hidden");
                BeginWriteAttribute("value", " value=", 584, "", 614, 1);
#nullable restore
#line 24 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
WriteAttributeValue("", 591, Model.First().systemId, 591, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <input onclick=\"startBuilding()\" value=\"Build\" class=\"btn btn-primary\" />\r\n    </div>\r\n");
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
            WriteLiteral("\r\n\r\n<p id=\"timer\"></p>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n        ");
#nullable restore
#line 35 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
   Write(shipType);

#line default
#line hidden
#nullable disable
                WriteLiteral(" = $(\"#shipType\").val().toString();\r\n\r\n");
#nullable restore
#line 37 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
         if(shipType == "s" || shipType == "")
        {
            if(Model.Any(p => p.FinishedBuildingTime.HasValue))
            {
                shipType = Model.FirstOrDefault(p => p.FinishedBuildingTime.HasValue).ShipType.ToString();
            }
            else
            {
                shipType = "Goliath";
            }
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n");
#nullable restore
#line 50 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
          
            var Ship = Model.FirstOrDefault(p => p.ShipType.ToString() == shipType);
        

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        //var count = Model.First(s => s.ShipType.ToString() == shipType).j;\r\n\r\n        window.onload = function () {\r\n            if (\'");
#nullable restore
#line 57 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
            Write(Ship.FinishedBuildingTime.HasValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"' != 'False') {
                 buildShip();
            }
            else {
            }
        }

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
#line 76 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\Shipyard.cshtml"
                      Write(Ship.FinishedBuildingTime.HasValue ? Ship.FinishedBuildingTime.Value : null);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';

        function startBuilding() {
            if (dateString == '') {
                    $(document).ready(function () {
                        var shipType = $(""#shipType"").val().toString();
                        var systemId = $(""#systemId"").val();
                        var count = $(""#count"").val();
                        var datastring = ""shipType="" + shipType + ""&systemId="" + systemId + ""&count="" + count;
                        $.ajax({
                            type: ""post"",
                            url: ""StartBuilding"", // set the count, set the building finish time for the whole count (for the corresponding ship type)
                            data: datastring
                        });
                        setTimeout(function () { location.reload(); }, 200);
                    });
                    //buildShip();//is that ever happening? it is right after the location.reload()... Oh yeah it is, even before the new datetime sets...

            }
        }");
                WriteLiteral(@"

        function buildShip() {
            var counter = 0;
            var timeleft = 0;

                var oneSplit = dateString.split(' ');
                var year = oneSplit[0].split('/')[2];
                var month = oneSplit[0].split('/')[1];
                var day = oneSplit[0].split('/')[0];
                var hour = oneSplit[1].split(':')[0];
                var minutes = oneSplit[1].split(':')[1];
                var seconds = oneSplit[1].split(':')[2];

                var buildingFinishTime = new Date(year, month - 1, day, hour, minutes, seconds);

            timeleft = (buildingFinishTime.getTime() - new Date().getTime()) / 1000;

            if (timeleft - counter > 0) {

                document.getElementById('timer').innerText = convertSeconds(timeleft);

                var _tick = setInterval(function () {
                    counter++;
                    document.getElementById('timer').innerText = convertSeconds(timeleft - counter);
                    ");
                WriteLiteral(@"if (timeleft - counter <= 0) {
                        clearInterval(_tick);
                        build();
                    }
                }, 1000);
            } else {
                build();
            }
        }

        function build() {
            $(document).ready(function () {
                var shipType = $(""#shipType"").val().toString();
                var systemId = $(""#systemId"").val();
                var datastring = ""shipType="" + shipType + ""&systemId="" + systemId;
                $.ajax({
                    type: ""post"",
                    url: ""BuildShip"", //set count to 0, set buildingfinishtime to null
                    data: datastring
                });
                setTimeout(function () { location.reload(); }, 200);
            });
        }

    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<BuildShipFormViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
