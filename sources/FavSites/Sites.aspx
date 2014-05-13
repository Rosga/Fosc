<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Sites.aspx.cs" Inherits="FavSites.Sites" EnableEventValidation="False" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<script src="Scripts\jquery-1.8.2.js"></script>
<script src="Scripts\jquery-ui-1.8.24.js"></script>
<script src="Scripts\_references.js"></script>
    
    <script>
        //виклик jQuery-функції sortable для усіх елементів класу .sitesContent
        $(function() {
            $(".sitesContent").sortable({
                //після зміни положення хоча б одного елемента виконуватимуться наступні дії:
                update: function (event, ui) {
                    //зобразити імена всіх елементів в одну трічку. 
                    var sitesOrder = $(this).sortable('toArray').toString();
                    //розщепити стрічку по комам, утворивши масив з із імен елементів
                    var rows = sitesOrder.split(",");
                    //створити новий масив
                    var arrID = new Array();
                    //цикл
                    //пройтись по кожному елементу в масиві імен елементів
                    //розчепити кожне ім'я по символу нижнього підкреслення
                    //витягти з цього розчеплення ID запису та записати його в масив.
                    for (var i = 0; i < rows.length; i++) {
                        var fields = rows[i].split("_");
                        arrID[arrID.length] = fields[1];
                    }
                    //записати новостворений масив стрічок в одну JSON-строку
                    var jsonText = JSON.stringify(arrID);

                    //ініціювати AJAX-подію та викликати Веб-службу SetNewOrderOfSites
                    //для збереження порядку змінених закладок
                    //передати щойностворену JSON-строку як параметр
                    $.ajax({
                        type: "POST",
                        url: "SitesReorderService.asmx/SetNewOrderOfSites",
                        data: {
                            order: jsonText
                        },
                    });
                }
            });
        });
    </script>

    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Your Favourite Sites</h2>
    </hgroup>

    <div id="divSitesContent" class="sitesContent" >    
        <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div id="divSites_<%#: DataBinder.Eval(Container.DataItem, "SiteId") %>" class="sites">
                        <asp:ImageButton CssClass="imgbtnDelSite" ImageUrl="~\Images\delete.png" runat="server"
                                Height="12" Width="12"
                                ImageAlign="Right"
                                ID="imgbtnDeleteSite"
                                CommandArgument='<%#: DataBinder.Eval(Container.DataItem, "SiteID") %>'
                                OnCommand="imgbtnDeleteSite_Command" />
                        <a class="newtab-link" href="<%# DataBinder.Eval(Container.DataItem, "SiteLink") %>" target="_blank">
                            <div class="newtab-image" style="background-image: url('<%# DataBinder.Eval(Container.DataItem, "imgSiteUrl")%>')">
                            </div>
                            <label class="sitesName" ><%# DataBinder.Eval(Container.DataItem, "SiteName") %></>
                        </a>
                    </div> 
                 </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
