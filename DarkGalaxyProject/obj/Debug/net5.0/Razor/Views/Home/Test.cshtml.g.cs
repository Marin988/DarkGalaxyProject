#pragma checksum "D:\softuni-WEB\DarkGalaxyProject\DarkGalaxyProject\Views\Home\Test.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c6313b737a3542d1da5d62848c386335fe9270db"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Test), @"mvc.1.0.view", @"/Views/Home/Test.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6313b737a3542d1da5d62848c386335fe9270db", @"/Views/Home/Test.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c87d1cb92b99a860fa765e763e61c97a7eab4136", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Test : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div id=\"left\">\r\n    <div id=\"mainmenucomponent\"></div>\r\n    <div id=\"tutorialiconcomponent\"");
            BeginWriteAttribute("class", " class=\"", 92, "\"", 100, 0);
            EndWriteAttribute();
            WriteLiteral(@">
        <div id=""helper"">
            <a class=""tooltip tooltipClose"" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=tutorial"" title=""Tutorial Overview<br/><a href=&quot;https://s168-us.ogame.gameforge.com/game/index.php?page=rewards&quot;></a>"">?</a>
        </div>
    </div>
    <div id=""toolbarcomponent""");
            BeginWriteAttribute("class", " class=\"", 452, "\"", 460, 0);
            EndWriteAttribute();
            WriteLiteral(@">
        <div id=""links"">
            <ul id=""menuTable"" class=""leftmenu"">

                <li>
                    <span class=""menu_icon"">
                        <a href=""https://s168-us.ogame.gameforge.com/game/index.php?page=rewards"" class=""tooltipRight js_hideTipOnMobile "" target=""_self""");
            BeginWriteAttribute("title", " title=\"", 763, "\"", 771, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                            <div class=""menuImage overview
                                "">
                            </div>
                        </a>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=overview""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 1101, "\"", 1113, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Overview</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <a href=""https://s168-us.ogame.gameforge.com/game/index.php?page=resourceSettings"" class=""tooltipRight js_hideTipOnMobile "" target=""_self"" title=""Resource settings"">
                            <div class=""menuImage resources active
                                "">
                            </div>
                        </a>
                    </span>
                    <a class=""menubutton  selected"" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=supplies""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 1850, "\"", 1862, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Resources</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <a href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=facilities"" class=""tooltipRight js_hideTipOnMobile "" target=""_self"" title=""Jump Gate"">
                            <div class=""menuImage station
                                "">
                            </div>
                        </a>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=facilities""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 2591, "\"", 2603, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Facilities</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <a href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=traderOverview#page=traderResources&amp;animation=false"" class=""trader tooltipRight js_hideTipOnMobile  tpd-hideOnClickOutside"" target=""_self""");
            BeginWriteAttribute("title", " title=\"", 3060, "\"", 3068, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                            <div class=""menuImage traderOverview
                                "">
                            </div>
                        </a>
                    </span>
                    <a class=""menubutton premiumHighligt"" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=traderOverview""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 3425, "\"", 3437, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Trader</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <a href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ajax&amp;component=technologytree&amp;tab=3&amp;open=all"" class=""overlay tooltipRight js_hideTipOnMobile "" target=""_blank"" title=""Technology"">
                            <div class=""menuImage research
                                "">
                            </div>
                        </a>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=research""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 4197, "\"", 4209, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Research</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <span class=""menuImage shipyard  ""></span>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=shipyard""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 4644, "\"", 4656, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Shipyard</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <span class=""menuImage defense  ""></span>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=defenses""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 5090, "\"", 5102, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Defense</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <a href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=movement"" class=""tooltipRight js_hideTipOnMobile "" target=""_self"" title=""fleet movement"">
                            <div class=""menuImage fleet1
                                "">
                            </div>
                        </a>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=fleetdispatch""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 5834, "\"", 5846, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Fleet</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <span class=""menuImage galaxy  ""></span>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=galaxy""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 6274, "\"", 6286, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Galaxy</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <span class=""menuImage empire  ""></span>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=standalone&amp;component=empire""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 6719, "\"", 6731, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_blank"">
                        <span class=""textlabel"">Empire</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <span class=""menuImage alliance  ""></span>
                    </span>
                    <a class=""menubutton "" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=ingame&amp;component=alliance""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 7165, "\"", 7177, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Alliance</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <span class=""menuImage premium  ""></span>
                    </span>
                    <a class=""menubutton premiumHighligt officers"" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=premium""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 7613, "\"", 7625, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Recruit Officers</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <a href=""https://s168-us.ogame.gameforge.com/game/index.php?page=shop#page=inventory&amp;category=d8d49c315fa620d9c7f1f19963970dea59a0e3be"" class=""tooltipRight js_hideTipOnMobile "" target=""_self"" title=""Inventory"">
                            <div class=""menuImage shop
                                "">
                            </div>
                        </a>
                    </span>
                    <a class=""menubutton premiumHighligt"" href=""https://s168-us.ogame.gameforge.com/game/index.php?page=shop""");
            BeginWriteAttribute("accesskey", " accesskey=\"", 8388, "\"", 8400, 0);
            EndWriteAttribute();
            WriteLiteral(@" target=""_self"">
                        <span class=""textlabel"">Shop</span>
                    </a>
                </li>

                <li>
                    <span class=""menu_icon"">
                        <span class=""menuImage   ""></span>
                    </span>
                    <a class=""menubutton "" href=""https://www.stomt.com/ogame"" target=""_blank"">
                        <span class=""textlabel"">Stomt</span>
                    </a>
                </li>
            </ul>

            <div id=""toolLinksWrapper"">
                <ul id=""menuTableTools"" class=""leftmenu""></ul>
            </div>
            <br class=""clearfloat"">
        </div>
    </div>
    <div id=""advicebarcomponent""");
            BeginWriteAttribute("class", " class=\"", 9138, "\"", 9146, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n        <div class=\"adviceWrapper\">\r\n\r\n            <div id=\"advice-bar\">\r\n\r\n\r\n\r\n\r\n\r\n\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
