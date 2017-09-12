<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReportViewerWebForm.aspx.cs" Inherits="ReportViewerForMvc.ReportViewerWebForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
        <div>
            <%--call printdiv() function on button click--%>

            <input id="PrintButton" title="Print" style="width: 16px; height: 16px;" type="image" alt="Print" runat="server" src="~/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=11.0.3442.2&amp;Name=Microsoft.Reporting.WebForms.Icons.Print.gif" />
            <div class="pdf">
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="ReportViewerForMvc" Name="ReportViewerForMvc.Scripts.PostMessage.js" />
                </Scripts>
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>


<script>
    $(document).ready(function () {
        
        // Check if the current browser is IE (MSIE is not used since IE 11)
        var isIE = /MSIE/i.test(navigator.userAgent) || /rv:11.0/i.test(navigator.userAgent);

        function printReport() {
            

            var reportViewerName = 'ReportViewer1'; //Name attribute of report viewer control.
            var src_url = $find(reportViewerName)._getInternalViewer().ExportUrlBase + 'PDF';

            var contentDisposition = 'AlwaysInline'; //Content Disposition instructs the server to either return the PDF being requested as an attachment or a viewable report.
            var src_new = src_url.replace(/(ContentDisposition=).*?(&)/, '$1' + contentDisposition + '$2');

            var iframe = $('<iframe>', {
                src: src_new,
                id: 'pdfDocument',
                frameborder: 0,
                scrolling: 'no'
            }).hide().load(function () {
                var PDF = document.getElementById('pdfDocument');
                PDF.focus();
                try {
                    PDF.contentWindow.print();
                }
                catch (ex) {
                    alert(ex)
                    //If all else fails, we want to inform the user that it is impossible to directly print the document with the current browser.
                    //Instead, let's give them the option to export the pdf so that they can print it themselves with a 3rd party PDF reader application.

                    if (confirm("ActiveX and PDF Native Print support is not supported in your browser. The system is unable to print your document directly. Would you like to download the PDF version instead? You may print the document by opening the PDF using your PDF reader application.")) {
                        window.open($find(reportViewerName)._getInternalViewer().ExportUrlBase + 'PDF');
                    }
                }

            })

            //Bind the iframe we created to an invisible div.
            $('.pdf').html(iframe);


        }

        // 2. Add Print button for non-IE browsers
        if (!isIE) {

            $('#PrintButton').click(function (e) {
                e.preventDefault();
                printReport();
            })
        }


        //function PrintReport() {
        //    var reportViewerName = 'ReportViewer1';
        //    var src_url = $find(reportViewerName)._getInternalViewer().ExportUrlBase + 'PDF';

        //    var contentDisposition = 'AlwaysInline';
        //    var src_new = src_url.replace(/(ContentDisposition=).*?(&)/, '$1' + contentDisposition + '$2');

        //    var iframe = $('<iframe>', {
        //        src: src_new,
        //        id: 'iframePDF',
        //        frameborder: 0,
        //        scrolling: 'no'
        //    });

        //    $('#pdfPrint').html(iframe);    //There should be a div named "pdfPrint"

        //    if (iframe != undefined && iframe.length > 0) {
        //        var frame = iframe[0];

        //        if (frame != null || frame != undefined) {
        //            var contentView = iframe[0].contentWindow;

        //            contentView.focus();
        //            contentView.print();
        //        }
        //    }
        //    }

    });


    </script>