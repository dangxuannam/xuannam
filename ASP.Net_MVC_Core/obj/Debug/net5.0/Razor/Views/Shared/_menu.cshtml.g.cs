#pragma checksum "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/Shared/_menu.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8bc67ddb5024cf0303fc33cbcaabf5e73f8e92c598c68029a5ea19a64bc635fc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__menu), @"mvc.1.0.view", @"/Views/Shared/_menu.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/_ViewImports.cshtml"
using ASP.Net_MVC_Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/_ViewImports.cshtml"
using ASP.Net_MVC_Core.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"8bc67ddb5024cf0303fc33cbcaabf5e73f8e92c598c68029a5ea19a64bc635fc", @"/Views/Shared/_menu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"41dbceeb7bc0b61b46dbbf67eb3d04a52c0fcde3cabdb859787a0bf712147625", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__menu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"menu_container container\">\n    <div class=\"menu_title\">\n        Quản lý sinh\n    </div>\n    <div class=\"menu_item\">\n        <div class=\"row item\">\n            ");
#nullable restore
#line 7 "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/Shared/_menu.cshtml"
       Write(Html.ActionLink("Lớp học", "class_student", "student"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </div>\n        <div class=\"row item\">\n            ");
#nullable restore
#line 10 "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/Shared/_menu.cshtml"
       Write(Html.ActionLink("Sinh viên", "index", "student"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </div>\n        <div class=\"row item\">\n            ");
#nullable restore
#line 13 "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/Shared/_menu.cshtml"
       Write(Html.ActionLink("Lý lịch sinh viên", "info", "student"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </div>\n        <div class=\"row item\">\n            ");
#nullable restore
#line 16 "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/Shared/_menu.cshtml"
       Write(Html.ActionLink("Môn học", "course", "student"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </div>\n        <div class=\"row item\">\n            ");
#nullable restore
#line 19 "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/Shared/_menu.cshtml"
       Write(Html.ActionLink("Điểm", "point", "student"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </div>\n        <div class=\"row item\">\n            ");
#nullable restore
#line 22 "/Users/hoangdoan/Library/Mobile Documents/com~apple~CloudDocs/EDU/BaiTapLapTrinhWeb_ASP.Net MVC Core/SourceCode/ASP.Net_MVC_Core/Views/Shared/_menu.cshtml"
       Write(Html.ActionLink("Kết quả học tập", "edu_result", "student"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </div>\n    </div>\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
