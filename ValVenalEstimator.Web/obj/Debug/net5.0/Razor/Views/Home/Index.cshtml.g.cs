#pragma checksum "F:\ASP.NET Core projects\ValVenalEstimator\ValVenalEstimator.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "274286b9fe4752564f5b4f8231d598999690a6c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "F:\ASP.NET Core projects\ValVenalEstimator\ValVenalEstimator.Web\Views\_ViewImports.cshtml"
using ValVenalEstimator.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\ASP.NET Core projects\ValVenalEstimator\ValVenalEstimator.Web\Views\_ViewImports.cshtml"
using ValVenalEstimator.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"274286b9fe4752564f5b4f8231d598999690a6c7", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46a00e0f99e897bb77a0f1105c6bf28621309a99", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Prefecture>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GetValues", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "F:\ASP.NET Core projects\ValVenalEstimator\ValVenalEstimator.Web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "274286b9fe4752564f5b4f8231d598999690a6c75178", async() => {
                WriteLiteral(@"
    <div class=""form-group row"">
        <label for=""Prefecture"" class=""col-sm-2 col-form-label"">Préfecture : </label>
        <div class=""col-sm-10"">
            <select name=""prefect"" id=""prefect"" required=""required"" class=""custom-select my-1 mr-sm-2"">
                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "274286b9fe4752564f5b4f8231d598999690a6c75726", async() => {
                    WriteLiteral("Choisisez la préfecture");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 12 "F:\ASP.NET Core projects\ValVenalEstimator\ValVenalEstimator.Web\Views\Home\Index.cshtml"
                 foreach (Prefecture p in Model)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "274286b9fe4752564f5b4f8231d598999690a6c77260", async() => {
#nullable restore
#line 14 "F:\ASP.NET Core projects\ValVenalEstimator\ValVenalEstimator.Web\Views\Home\Index.cshtml"
                                     Write(p.Name);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 14 "F:\ASP.NET Core projects\ValVenalEstimator\ValVenalEstimator.Web\Views\Home\Index.cshtml"
                       WriteLiteral(p.Id);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 15 "F:\ASP.NET Core projects\ValVenalEstimator\ValVenalEstimator.Web\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"            </select>
        </div>
    </div>
    <div class=""form-group row"">
        <label for=""Place"" class=""col-sm-2 col-form-label"">Quartier : </label>
        <div class=""col-sm-10"">
            <select name=""idPlace"" id=""idPlace"" required=""required"" class=""custom-select"">
            </select>
        </div>
    </div>
    <div class=""form-group row"">
        <label for=""area"" class=""col-sm-2 col-form-label"">Superficie (en m2) : </label>
        <div class=""col-sm-10"">
            <input name=""area"" type=""number"" class=""form-control"" min=""1"" required=""required"" />
        </div>
    </div>
    <div class=""form-group row"">
        <label for=""area"" class=""col-sm-2 col-form-label"">Valeur d'achat du terrain : </label>
        <div class=""col-sm-10"">
            <input name=""valAchat"" type=""number"" class=""form-control"" min=""1"" required=""required"" />
        </div>
    </div>
    <div class=""form-group row"">
        <label for=""area"" class=""col-sm-2 col-form-label"">Nombres de pages");
                WriteLiteral(@" de l'acte notarié : </label>
        <div class=""col-sm-10"">
            <input name=""nbrePge"" type=""number"" class=""form-control"" min=""1"" required=""required"" />
        </div>
    </div>
    <div class=""form-group"">
        <input type=""submit"" value=""Envoyer"" class=""btn btn-primary mb-2"" />
    </div>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
<script type=""text/javascript"">
    $(document).ready(function () {
        var PrefectSelected = $(""#prefect"");
        var PlaceSelected = $(""#idPlace"");
        PlaceSelected.prop('disabled', true);
        PrefectSelected.change(function () {
            if ($(this).val() == """") {
                PlaceSelected.prop('disabled', true);
                PlaceSelected.val("""");
            }
            else {
                $.ajax({
                    url: ""https://localhost:5004/api/Places/prefecture/"" + $(this).val(),
                    method: ""get"",
                    success: function (data) {
                        PlaceSelected.prop('disabled', false);
                        PlaceSelected.empty();
                        PlaceSelected.append($('<option/>', { value: '', text: '-- Selectionner le quartier --' }));
                        $(data).each(function (index, item) {
                            PlaceSelected.append($('<option/>', { value: item.id, text: item.name }));
 ");
                WriteLiteral(@"                       });
                    }
                });
            }
        });

    });
</script>

<script type=""text/javascript"">
    (function ($, undefined) {
        ""use strict"";
        // When ready.
        $(function () {
            var $form = $(""#form"");
            var $input = $form.find(""input"");
            $input.on(""keyup"", function (event) {
                // When user select text in the document, also abort.
                var selection = window.getSelection().toString();
                if (selection !== '') {
                    return;
                }
                // When the arrow keys are pressed, abort.
                if ($.inArray(event.keyCode, [38, 40, 37, 39]) !== -1) {
                    return;
                }
                var $this = $(this);
                // Get the value.
                var input = $this.val();
                var input = input.replace(/[\D\s\._\-]+/g, """");
                input = input ? parseIn");
                WriteLiteral(@"t(input, 10) : 0;
                $this.val(function () {
                    return (input === 0) ? """" : input.toLocaleString(""en-US"");
                });
            });
            /**
             * ==================================
             * When Form Submitted
             * ==================================
             */
            $form.on(""submit"", function (event) {
                var $this = $(this);
                var arr = $this.serializeArray();
                for (var i = 0; i < arr.length; i++) {
                    arr[i].value = arr[i].value.replace(/[($)\s\._\-]+/g, ''); // Sanitize the values.
                };
                console.log(arr);
                event.preventDefault();
            });
        });
    })(jQuery);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Prefecture>> Html { get; private set; }
    }
}
#pragma warning restore 1591
