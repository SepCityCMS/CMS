// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="Games.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class Games.
    /// </summary>
    public class Games
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the game identifier.
        /// </summary>
        /// <value>The game identifier.</value>
        public long GameID { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>The name of the game.</value>
        public string GameName { get; set; }

        /// <summary>
        /// Gets or sets the image file.
        /// </summary>
        /// <value>The image file.</value>
        public string ImageFile { get; set; }

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName { get; set; }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        /// <value>The height of the window.</value>
        public string WindowHeight { get; set; }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        /// <value>The width of the window.</value>
        public string WindowWidth { get; set; }
    }
}