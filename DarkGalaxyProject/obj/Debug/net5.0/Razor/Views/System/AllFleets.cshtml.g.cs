#pragma checksum "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6dcdbd152f982845a470c855071be67a75b89909"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_System_AllFleets), @"mvc.1.0.view", @"/Views/System/AllFleets.cshtml")]
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
#line 1 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
using DarkGalaxyProject.Services.SystemServices;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6dcdbd152f982845a470c855071be67a75b89909", @"/Views/System/AllFleets.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c87d1cb92b99a860fa765e763e61c97a7eab4136", @"/Views/_ViewImports.cshtml")]
    public class Views_System_AllFleets : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<FleetServiceModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n<ul>\r\n");
#nullable restore
#line 7 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
     foreach (var fleet in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <li>\r\n        <span>Ships count: ");
#nullable restore
#line 10 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
                      Write(fleet.Ships.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n        <span>ArrivalTime: ");
#nullable restore
#line 11 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
                      Write(fleet.ArrivalTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n        <span>Destination system position: ");
#nullable restore
#line 12 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
                                      Write(fleet.DestinationSystemPosition);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n");
#nullable restore
#line 13 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
         if (fleet.ArrivalTime != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span>");
#nullable restore
#line 15 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
              Write(fleet.Outgoing ? "Outgoing" : "Incoming");

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n");
#nullable restore
#line 16 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </li>\r\n");
#nullable restore
#line 18 "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\System\AllFleets.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<FleetServiceModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591