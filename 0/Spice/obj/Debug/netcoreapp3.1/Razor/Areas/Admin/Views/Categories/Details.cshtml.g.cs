#pragma checksum "C:\Users\Farah\source\repos\Spice\Spice\Areas\Admin\Views\Categories\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b4a1fc8bdb8b37b81589b237fa451df4e8b50d9b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Categories_Details), @"mvc.1.0.view", @"/Areas/Admin/Views/Categories/Details.cshtml")]
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
#line 1 "C:\Users\Farah\source\repos\Spice\Spice\Areas\Admin\Views\_ViewImports.cshtml"
using Spice;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Farah\source\repos\Spice\Spice\Areas\Admin\Views\_ViewImports.cshtml"
using Spice.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Farah\source\repos\Spice\Spice\Areas\Admin\Views\_ViewImports.cshtml"
using Spice.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b4a1fc8bdb8b37b81589b237fa451df4e8b50d9b", @"/Areas/Admin/Views/Categories/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7e612b14dcdb5fa77db93eee90ddf641c329fac", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Categories_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Spice.Models.Category>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Farah\source\repos\Spice\Spice\Areas\Admin\Views\Categories\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""border"" style=""background-color:white;border:solid 1px #ddd;border-radius:10px;padding:30px;margin-bottom:30px;"">
    <h1>Details</h1>

    <div>
        <h4>Category</h4>
        <hr />
        <dl class=""row"">
            <dt class=""col-sm-2"">
                ");
#nullable restore
#line 15 "C:\Users\Farah\source\repos\Spice\Spice\Areas\Admin\Views\Categories\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 18 "C:\Users\Farah\source\repos\Spice\Spice\Areas\Admin\Views\Categories\Details.cshtml"
           Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n        </dl>\r\n    </div>\r\n    <d     </div>\r\n;[;P[iv class=\"text-center\">\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Spice.Models.Category> Html { get; private set; }
    }
}
#pragma warning restore 1591