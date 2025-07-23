<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DS-SmallGroupDetail.ascx.cs" Inherits="RockWeb.Blocks.Utility.SmallGroupDetail" %>

<asp:UpdatePanel ID="upnlSmallGroupDetail" runat="server">
    <ContentTemplate>

        <asp:Panel ID="pnlDetails" runat="server" CssClass="panel panel-block">

            <div class="panel-heading panel-follow">
                <h1 class="panel-title pull-left">
                    <asp:Literal ID="lGroupIconHtml" runat="server" />
                    <asp:Literal ID="lReadOnlyTitle" runat="server" />
                </h1>

                <div class="panel-labels">
                    <Rock:HighlightLabel ID="hlblGroupType" runat="server" LabelType="Type"/>
                </div>
            </div>

            <Rock:PanelDrawer ID="pdAuditDetails" runat="server"></Rock:PanelDrawer>

            <div class="panel-body">
                <div class="form-group">
	                <label class="control-label">Description:</label>
	                <div class="control-wrapper">
                        <asp:Label ID="lDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
	                </div>
	                <label class="control-label">Date Created:</label>
	                <div class="control-wrapper">
                        <asp:Label ID="lDateCreated" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
	                </div>
	                <label class="control-label">Date Modified:</label>
	                <div class="control-wrapper">
                        <asp:Label ID="lDateModified" runat="server" Text='<%# Bind("DateModified") %>'></asp:Label>
	                </div>
	                <label class="control-label">Group Capacity:</label>
	                <div class="control-wrapper">
                        <asp:Label ID="lGroupCapacity" runat="server" Text='<%# Bind("GroupCapacity") %>'></asp:Label>
	                </div>
                </div>
            </div>
        
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>