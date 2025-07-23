// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using Rock;
using Rock.Attribute;
using Rock.Data;
using Rock.Model;
using Rock.Web.Cache;
using Rock.Web.UI;
using Slingshot.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.UI;
using Group = Rock.Model.Group;

namespace RockWeb.Blocks.Utility
{
    [RockObsolete( "1.16.7" )]
    [Obsolete( "This block type has been deprecated." )]
    [DisplayName( "Small Group Detail (Legacy)" )]
    [Category( "Groups" )]
    [Description( "Detail block to display specifics of a small group." )]
    [ContextAware( typeof( Group ) )]

    #region Block Attributes

    // Used to store block attributes, such as fields

    #endregion Block Attributes

    [Rock.SystemGuid.BlockTypeGuid( "D6B14847-B652-49E2-9D4B-658D502F0AEC" )]
    public partial class SmallGroupDetail : Rock.Web.UI.RockBlock
    {

        #region Attribute Keys

        private static class AttributeKey
        {
            // Used to store AttributeKeys for Block Attributes
        }

        #endregion Attribute Keys

        #region PageParameterKeys

        private static class PageParameterKey
        {
            public const string GroupId = "GroupId";
        }

        #endregion PageParameterKeys

        #region Fields

        // Used for private variables.

        #endregion

        #region Properties

        // Used for public / protected properties.

        #endregion

        #region Base Control Methods

        // Overrides of the base RockBlock methods (i.e. OnInit, OnLoad)

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );

            // This event gets fired after block settings are updated. It's nice to repaint the screen if these settings would alter it.
            this.BlockUpdated += Block_BlockUpdated;
            this.AddConfigurationUpdateTrigger( upnlSmallGroupDetail );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            int? groupId = 0;
            if ( !string.IsNullOrWhiteSpace( PageParameter( PageParameterKey.GroupId ) ) )
            {
                groupId = PageParameter( PageParameterKey.GroupId ).AsIntegerOrNull();
            }

            if ( !Page.IsPostBack )
            {
                if ( groupId.HasValue )
                {
                    ShowDetail( groupId.Value );
                }
                else
                {
                    pnlDetails.Visible = false;
                }
            }

            base.OnLoad( e );
        }

        #endregion

        #region Events

        // Handlers called by the controls on your block.

        /// <summary>
        /// Handles the BlockUpdated event of the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Block_BlockUpdated( object sender, EventArgs e )
        {

        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the group.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        private Group GetGroup( int groupId, RockContext rockContext = null )
        {
            string key = string.Format( "Group:{0}", groupId );
            Group group = RockPage.GetSharedItem( key ) as Group;
            if ( group == null )
            {
                rockContext = rockContext ?? new RockContext();
                group = new GroupService( rockContext )
                    .Queryable()
                    .Include( g => g.GroupType )
                    .Include( g => g.GroupLocations.Select( s => s.Schedules ) )
                    .Include( g => g.GroupSyncs )
                    .Where( g => g.Id == groupId )
                    .FirstOrDefault();
                RockPage.SaveSharedItem( key, group );
            }

            return group;
        }

        /// <summary>
        /// Shows the detail.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        public void ShowDetail( int groupId )
        {
            RockContext rockContext = new RockContext();

            Group group = null;

            if ( groupId > 0 )
            {
                group = GetGroup( groupId, rockContext );
                ShowReadonlyDetails( group );
            }
        }

        /// <summary>
        /// Shows the readonly details.
        /// </summary>
        /// <param name="group">The group.</param>
        private void ShowReadonlyDetails( Group group )
        {
            GroupTypeCache groupType = GroupTypeCache.Get( group.GroupTypeId );

            string groupIconHtml = string.Empty;
            if ( groupType != null )
            {
                groupIconHtml = !string.IsNullOrWhiteSpace( groupType.IconCssClass ) ?
                    string.Format( "<i class='{0}' ></i>", groupType.IconCssClass ) : string.Empty;
            }

            lGroupIconHtml.Text = groupIconHtml;

            if (groupType != null)
            {
                lReadOnlyTitle.Text = group.Name.FormatAsHtmlTitle();

                hlblGroupType.Text = groupType.Name;
                lDescription.Text = group.Description;
                lDateCreated.Text = group.CreatedDateTime.Value.ToString("d");
                lDateModified.Text = group.ModifiedDateTime.Value.ToString("d");

                if (group.GroupCapacity.HasValue)
                {
                    lGroupCapacity.Text = group.GroupCapacity.ToString();
                }
                else
                {
                    lGroupCapacity.Text = "No capacity specified.";
                }
            }
        }

        #endregion
    }
}