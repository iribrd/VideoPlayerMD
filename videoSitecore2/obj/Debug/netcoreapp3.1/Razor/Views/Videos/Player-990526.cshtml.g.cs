#pragma checksum "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85d05e9fb439d48a0efda20cc28b94f709ad2b63"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Videos_Player_990526), @"mvc.1.0.view", @"/Views/Videos/Player-990526.cshtml")]
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
#line 1 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\_ViewImports.cshtml"
using videoSitecore2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\_ViewImports.cshtml"
using videoSitecore2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85d05e9fb439d48a0efda20cc28b94f709ad2b63", @"/Views/Videos/Player-990526.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ae90efb981cdeb45e846e794aff95ca85de3796", @"/Views/_ViewImports.cshtml")]
    public class Views_Videos_Player_990526 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<videoSitecore2.Models.Videoview>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
  
    ViewData["Title"] = "Play";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>نمایش ویدئو</h1>\r\n<hr />\r\n\r\n\r\n<!--<div>\r\n\r\n");
#nullable restore
#line 12 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
        foreach (var item in Model)
        {



#line default
#line hidden
#nullable disable
            WriteLiteral("    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 18 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayNameFor(model => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 21 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayFor(model => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 24 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayNameFor(model => item.Location));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 27 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayFor(model => item.Location));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 30 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayNameFor(model => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 33 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayFor(model => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n\r\n\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 38 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayNameFor(model => item.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 41 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayFor(model => item.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 45 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayNameFor(model => item.metadataTitleId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 48 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Html.DisplayFor(model => item.metadataTitleId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n\r\n\r\n    </dl>\r\n");
#nullable restore
#line 53 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>-->\r\n\r\n\r\n<div>\r\n\r\n\r\n\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            نام:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 66 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Model.First().Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n\r\n        \r\n        <dt class=\"col-sm-2\">\r\n            توضیحات:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 74 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
       Write(Model.First().Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n\r\n\r\n    </dl>\r\n</div>\r\n\r\n\r\n\r\n<div style=\"height: 450px;\">\r\n    <div id=\"defaultPlayer\"></div>\r\n</div>\r\n<div id=\"timeline\"></div>\r\n\r\n<script>\r\n\r\n\r\n    var zsmlistline = [\r\n");
#nullable restore
#line 92 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
          foreach (var item in Model)
         {


#line default
#line hidden
#nullable disable
            WriteLiteral("             ");
            WriteLiteral("{ title: \"");
#nullable restore
#line 95 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
                    Write(Html.Raw(item.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", type: \'cuepoint\', metadataId: ");
#nullable restore
#line 95 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
                                                                          Write(item.metadataTitleId);

#line default
#line hidden
#nullable disable
            WriteLiteral(", color: \'#3CF\', pointNav: true },\r\n");
#nullable restore
#line 96 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    ];\r\n\r\n    var location1 = \"");
#nullable restore
#line 99 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
                Write(Html.Raw((Model.First().Location).Replace("\\", "/")));

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n    var zsmpath = \"./Hoshno/\" + location1;\r\n\r\n     //var location2 = \"");
#nullable restore
#line 102 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
                   Write(Html.Raw((Model.First().Location).Replace("\\", "\\\\")));

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n    //var zsmpathtest = \"C: \\\\Documents\\\\Users\\\\Administrator\\\\source\\\\Hoshno\\\\\" + location2;\r\n   // alert(zsmpathtest);\r\n\r\n    var url1 = window.location.host;\r\n    var id1 = \"");
#nullable restore
#line 107 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
          Write(Html.Raw(Model.First().Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n\r\n\r\n    var zsmjsonfile = [\r\n");
#nullable restore
#line 111 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
          foreach (var item in Model)
         {

#line default
#line hidden
#nullable disable
            WriteLiteral("           ");
            WriteLiteral("\'https://\' + url1 + \'/Videos/startTimeEvent2/\' + ");
#nullable restore
#line 113 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
                                                         Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(" + \'?metadataTitleId=\'+ ");
#nullable restore
#line 113 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"
                                                                                         Write(item.metadataTitleId);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n");
#nullable restore
#line 114 "C:\Users\Administrator\source\repos\videoSitecore2\videoSitecore2\Views\Videos\Player-990526.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    ];


    $(function () {

                $(""#defaultPlayer"").mediaPlayer({
                autoplay: false,
                 src: zsmpath,
                controlBar:
                {
                    sticky: true
                },
                plugins: {
                    dataServices: zsmjsonfile,
                    list: [
                        {
                            'className': 'fr.ina.amalia.player.plugins.TimelinePlugin',
                            'container': '#timeline',
                            'parameters': {
                                listOfLines: zsmlistline
                            }
                        }
                    ]
                }

            });

    } );
</script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<videoSitecore2.Models.Videoview>> Html { get; private set; }
    }
}
#pragma warning restore 1591
