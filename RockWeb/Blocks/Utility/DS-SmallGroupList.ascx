<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DS-SmallGroupList.ascx.cs" Inherits="RockWeb.Blocks.Utility.SmallGroupList" %>

<asp:UpdatePanel ID="upnlSmallGroupsList" runat="server">
    <ContentTemplate>

        <asp:Panel ID="pnlView" runat="server" CssClass="panel panel-block">
        
            <div class="panel-heading">
                <h1 class="panel-title">
                    <i class="fa fa-star"></i> 
                    Small Groups
                </h1>
            </div>
            <div class="panel-body">

                <div class="grid grid-panel">
                    <Rock:Grid ID="gList" runat="server" AllowSorting="true" OnRowSelected="gGroup_RowSelected" DataKeyNames="Id">
                        <Columns>
                            <Rock:RockBoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            <Rock:RockBoundField DataField="Description" HeaderText="Description" />
                        </Columns>
                    </Rock:Grid>
                </div>
            </div>
        
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
