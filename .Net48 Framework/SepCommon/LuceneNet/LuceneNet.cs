// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="LuceneNet.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Store;
    using Lucene.Net.Util;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text.RegularExpressions;
    using Directory = System.IO.Directory;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Adds a text.
        /// </summary>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="recordId">Identifier for the record.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="html">The HTML.</param>
        /// <param name="url">URL of the resource.</param>
        /// <param name="isHTML">True if is html, false if not.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="userId">Identifier for the user.</param>
        public static void AddText(int ModuleID, long recordId, string title, string description, string keywords, string html, string url, bool isHTML, string postalCode, string userId)
        {
            if (ModuleID > 0)
            {
                AddToFeed(ModuleID, recordId, title, description, url, userId);
                AddTextToIndex(ModuleID, recordId, title, description, keywords, html, url, isHTML, postalCode, userId);
            }

            AddTextToIndex(0, recordId, title, description, keywords, html, url, isHTML, postalCode, userId);
        }

        /// <summary>
        /// Adds a text to index.
        /// </summary>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="recordId">Identifier for the record.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="html">The HTML.</param>
        /// <param name="url">URL of the resource.</param>
        /// <param name="isHTML">True if is html, false if not.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="userId">Identifier for the user.</param>
        private static void AddTextToIndex(int ModuleID, long recordId, string title, string description, string keywords, string html, string url, bool isHTML, string postalCode, string userId)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            var doc = new Document();

            var indexDirectory = GetDirValue("app_data") + "lucene\\";

            if (!Directory.Exists(indexDirectory))
            {
                Directory.CreateDirectory(indexDirectory);
            }

            if (ModuleID > 0)
            {
                indexDirectory = GetDirValue("app_data") + "lucene\\" + ModuleID + "\\";

                if (!Directory.Exists(indexDirectory))
                {
                    Directory.CreateDirectory(indexDirectory);
                }
            }

            var DIindexDirectory = new DirectoryInfo(indexDirectory);
            var iConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, new StandardAnalyzer(LuceneVersion.LUCENE_48));

            FSDirectory FSD = FSDirectory.Open(DIindexDirectory);

            IndexWriter writer = null;

            try
            {
                writer = new IndexWriter(FSD, iConfig);
            }
            catch
            {
                try
                {
                    DirectoryInfo indexDirInfo = new DirectoryInfo(indexDirectory);
                    FSDirectory indexFSDir = FSDirectory.Open(indexDirInfo, new SimpleFSLockFactory(indexDirInfo));
                    IndexWriter.Unlock(indexFSDir);
                    indexFSDir.Dispose();
                    writer = new IndexWriter(FSD, iConfig);
                }
                catch (Exception ex)
                {
                    Debug_Log("AddTextToIndex: " + ex.Message);
                }
            }

            if (writer != null)
            {
                doc.Add(new TextField("recordid", Strings.ToString(recordId), Field.Store.YES));

                doc.Add(new TextField("title", title, Field.Store.YES));
                if (!string.IsNullOrWhiteSpace(description))
                {
                    doc.Add(new TextField("description", description, Field.Store.YES));
                }

                if (!string.IsNullOrWhiteSpace(keywords))
                {
                    doc.Add(new TextField("keywords", keywords, Field.Store.YES));
                }

                if (!string.IsNullOrWhiteSpace(html))
                {
                    if (isHTML)
                    {
                        doc.Add(new TextField("text", ParseHtml(html), Field.Store.YES));
                    }
                    else
                    {
                        doc.Add(new TextField("text", html, Field.Store.YES));
                    }
                }

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    doc.Add(new TextField("userid", userId, Field.Store.YES));
                }

                doc.Add(new TextField("url", url, Field.Store.YES));
                if (!string.IsNullOrWhiteSpace(postalCode))
                {
                    doc.Add(new TextField("postalcode", postalCode, Field.Store.YES));
                }

                try
                {
                    writer.AddDocument(doc);
                }
                catch
                {
                }
            }

            if (writer != null)
            {
                writer.Dispose();
            }

            FSD.Dispose();

        }

        /// <summary>
        /// Adds to feed.
        /// </summary>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="recordId">Identifier for the record.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="url">URL of the resource.</param>
        /// <param name="userId">Identifier for the user.</param>
        private static void AddToFeed(int ModuleID, long recordId, string title, string description, string url, string userId)
        {
            var sTitle = string.Empty;

            var SqlStr = string.Empty;
            var bUpdate = false;

            var DatePosted = DateTime.Now;

            switch (ModuleID)
            {
                case 5:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added a new discount.") + " (" + title + ")";
                    SqlStr = "SELECT DatePosted FROM DiscountSystem WHERE DiscountID=@UniqueID";
                    break;

                case 10:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has uploaded a new file.") + " (" + title + ")";
                    SqlStr = "SELECT DatePosted FROM LibrariesFiles WHERE FileID=@UniqueID";
                    break;

                case 12:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added a new topic to the forums.") + " (" + title + ")";
                    SqlStr = "SELECT DatePosted FROM ForumsMessages WHERE TopicID=@UniqueID";
                    break;

                case 19:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added a link to the link directory.") + " (" + title + ")";
                    SqlStr = "SELECT DatePosted FROM LinksWebsites WHERE LinkID=@UniqueID";
                    break;

                case 18:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has updated their profile.");
                    SqlStr = "SELECT DatePosted FROM MatchMaker WHERE ProfileID=@UniqueID";
                    break;

                case 20:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added a business.") + " (" + title + ")";
                    SqlStr = "SELECT DatePosted FROM BusinessListings WHERE BusinessID=@UniqueID";
                    break;

                case 31:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added an item for auction.") + " (" + title + ")";
                    SqlStr = "SELECT DatePosted FROM AuctionAds WHERE AdID=@UniqueID";
                    break;

                case 35:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added an article.") + " (" + title + ")";
                    SqlStr = "SELECT Start_Date FROM Articles WHERE ArticleID=@UniqueID";
                    break;

                case 44:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added a new classified ad.") + " (" + title + ")";
                    SqlStr = "SELECT DatePosted FROM ClassifiedsAds WHERE AdID=@UniqueID";
                    break;

                case 46:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added a event.") + " (" + title + ")";
                    SqlStr = "SELECT EventDate FROM EventCalendar WHERE EventID=@UniqueID";
                    break;

                case 61:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has added a new blog.") + " (" + title + ")";
                    SqlStr = "SELECT DatePosted FROM Blog WHERE BlogID=@UniqueID";
                    break;

                case 63:
                    sTitle = LangText("~~" + GetUserInformation("UserName", userId) + "~~ has updated their profile.");
                    SqlStr = "SELECT DatePosted FROM Profiles WHERE ProfileID=@UniqueID";
                    break;
            }

            if (!string.IsNullOrWhiteSpace(sTitle))
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", recordId);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (ModuleID == 46)
                                {
                                    DatePosted = toDate(openNull(RS["EventDate"]));
                                }
                                else if (ModuleID == 35)
                                {
                                    DatePosted = toDate(openNull(RS["Start_Date"]));
                                }
                                else
                                {
                                    DatePosted = toDate(openNull(RS["DatePosted"]));
                                }
                            }
                        }
                    }

                    using (var cmd = new SqlCommand("SELECT FeedID FROM Feeds WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", recordId);
                        cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }

                    if (bUpdate)
                    {
                        SqlStr = "UPDATE Feeds SET Title=@Title, Description=@Description, UserID=@UserID, MoreLink=@MoreLink WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID";
                    }
                    else
                    {
                        SqlStr = "INSERT INTO Feeds (FeedID, UniqueID, ModuleID, Title, Description, UserID, MoreLink, DatePosted) VALUES (@FeedID, @UniqueID, @ModuleID, @Title, @Description, @UserID, @MoreLink, @DatePosted)";
                    }

                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        using (var cmd = new SqlCommand(SqlStr, conn))
                        {
                            cmd.Parameters.AddWithValue("@FeedID", GetIdentity());
                            cmd.Parameters.AddWithValue("@UniqueID", recordId);
                            cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                            cmd.Parameters.AddWithValue("@Title", sTitle);
                            cmd.Parameters.AddWithValue("@Description", !string.IsNullOrWhiteSpace(description) ? description : string.Empty);
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.Parameters.AddWithValue("@MoreLink", url);
                            cmd.Parameters.AddWithValue("@DatePosted", DatePosted);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>A string.</returns>
        private static string ParseHtml(string html)
        {
            var temp = Regex.Replace(html, "<[^>]*>", string.Empty);
            return temp.Replace("&nbsp;", " ");
        }
    }
}

namespace SepCommon
{
    using Lucene.Net.Index;
    using Lucene.Net.Store;
    using Lucene.Net.Util;
    using SepCommon.SepCore;
    using System.Data.SqlClient;
    using System.IO;
    using Directory = System.IO.Directory;

    /// <summary>
    /// A lucene delete.
    /// </summary>
    public class LuceneDelete
    {
        /// <summary>
        /// Deletes the text.
        /// </summary>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="recordId">Identifier for the record.</param>
        public void DeleteText(int ModuleID, long recordId)
        {
            if (ModuleID > 0)
            {
                DeleteFromFeed(ModuleID, recordId);
                DeleteTextToIndex(ModuleID, recordId);
            }

            DeleteTextToIndex(0, recordId);
        }

        /// <summary>
        /// Deletes from feed.
        /// </summary>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="recordId">Identifier for the record.</param>
        private static void DeleteFromFeed(int ModuleID, long recordId)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM Feeds WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID", conn))
                {
                    cmd.Parameters.AddWithValue("@UniqueID", recordId);
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes the text to index.
        /// </summary>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="recordId">Identifier for the record.</param>
        private static void DeleteTextToIndex(int ModuleID, long recordId)
        {
            var indexDirectory = SepFunctions.GetDirValue("app_data") + "lucene\\";

            if (!Directory.Exists(indexDirectory))
            {
                Directory.CreateDirectory(indexDirectory);
            }

            if (ModuleID > 0)
            {
                indexDirectory = SepFunctions.GetDirValue("app_data") + "lucene\\" + ModuleID + "\\";

                if (!Directory.Exists(indexDirectory))
                {
                    Directory.CreateDirectory(indexDirectory);
                }
            }

            var iConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, null);
            var DIindexDirectory = new DirectoryInfo(indexDirectory);

            // Check if index exists
            try
            {
                FSDirectory FSD = FSDirectory.Open(DIindexDirectory);
                IndexReader reader = DirectoryReader.Open(FSD, 1);

                IndexWriter writer = new IndexWriter(FSD, iConfig);
                if (reader.NumDocs > 0)
                {
                    var recid = new BytesRef
                    {
                        Bytes = SepFunctions.StringToBytes(Strings.ToString(recordId))
                    };
                    writer.DeleteDocuments(new Term("recordid", recid));
                }

                writer.Dispose();
                reader.Dispose();
                FSD.Dispose();
            }
            catch
            {
            }
        }
    }
}

namespace SepCommon
{
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers.Classic;
    using Lucene.Net.Search;
    using Lucene.Net.Store;
    using Lucene.Net.Util;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.IO;
    using Directory = System.IO.Directory;

    /// <summary>
    /// A lucene search.
    /// </summary>
    public class LuceneSearch
    {
        /// <summary>
        /// The duration.
        /// </summary>
        private static TimeSpan duration;

        /// <summary>
        /// from item.
        /// </summary>
        private static int fromItem;

        /// <summary>
        /// The initialize start at.
        /// </summary>
        private static int m_InitStartAt;

        /// <summary>
        /// The keywords.
        /// </summary>
        private static string m_Keywords = string.Empty;

        /// <summary>
        /// The maximum results.
        /// </summary>
        private static int m_maxResults = 20;

        /// <summary>
        /// Identifier for the user.
        /// </summary>
        private static string m_UserID = string.Empty;

        /// <summary>
        /// The start at.
        /// </summary>
        private static int startAt;

        /// <summary>
        /// to item.
        /// </summary>
        private static int toItem;

        /// <summary>
        /// Number of.
        /// </summary>
        private static int total;

        /// <summary>
        /// Gets or sets the initialize start at.
        /// </summary>
        /// <value>The initialize start at.</value>
        public int InitStartAt
        {
            get
            {
                if (m_InitStartAt < 0)
                {
                    return 0;
                }

                // too big starting item, return last page
                if (m_InitStartAt >= total - 1)
                {
                    return LastPageStartsAt;
                }

                return m_InitStartAt;
            }

            set => m_InitStartAt = value;
        }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>The keywords.</value>
        public string Keywords
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_Keywords))
                {
                    return null;
                }

                return m_Keywords;
            }

            set => m_Keywords = value;
        }

        /// <summary>
        /// Gets or sets the maximum results.
        /// </summary>
        /// <value>The maximum results.</value>
        public int maxResults
        {
            get
            {
                if (m_maxResults == 0)
                {
                    return 20;
                }

                return m_maxResults;
            }

            set => m_maxResults = value;
        }

        /// <summary>
        /// Gets or sets the identifier of the module.
        /// </summary>
        /// <value>The identifier of the module.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user.
        /// </summary>
        /// <value>The identifier of the user.</value>
        public string UserID
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_UserID))
                {
                    return null;
                }

                return m_UserID;
            }

            set => m_UserID = value;
        }

        /// <summary>
        /// Gets the last page starts at.
        /// </summary>
        /// <value>The last page starts at.</value>
        private int LastPageStartsAt => PageCount * maxResults;

        /// <summary>
        /// Gets the number of pages.
        /// </summary>
        /// <value>The number of pages.</value>
        private int PageCount => (total - 1) / maxResults;

        /// <summary>
        /// Gets the paging.
        /// </summary>
        /// <returns>A DataTable.</returns>
        public DataTable Paging()
        {
            // pageNumber starts at 1
            var pageNumber = (startAt + maxResults - 1) / maxResults;

            var dt = new DataTable();
            dt.Columns.Add("html", typeof(string));

            var ar = dt.NewRow();
            ar["html"] = PagingItemHtml(startAt, pageNumber + 1, false);
            dt.Rows.Add(ar);

            var previousPagesCount = 4;
            var i = pageNumber - 1;
            while (i >= 0 && i >= pageNumber - previousPagesCount)
            {
                var step = i - pageNumber;
                var r = dt.NewRow();
                r["html"] = PagingItemHtml(startAt + maxResults * step, i + 1, true);

                dt.Rows.InsertAt(r, 0);
                i -= 1;
            }

            var nextPagesCount = 4;
            i = pageNumber + 1;
            while (i <= PageCount && i <= pageNumber + nextPagesCount)
            {
                var step = i - pageNumber;
                var r = dt.NewRow();
                r["html"] = PagingItemHtml(startAt + maxResults * step, i + 1, true);

                dt.Rows.Add(r);
                i += 1;
            }

            return dt;
        }

        /// <summary>
        /// Searches for the first match.
        /// </summary>
        /// <param name="Distance">(Optional) The distance.</param>
        /// <param name="PostalCode">(Optional) The postal code.</param>
        /// <param name="UserID">(Optional) Identifier for the user.</param>
        /// <returns>A DataTable.</returns>
        public DataTable search(string Distance = "ANY", string PostalCode = "", string UserID = "")
        {
            var Results = new DataTable();
            var start = DateTime.Now;

            // create the searcher index is placed in "index" subdirectory
            var indexDirectory = SepFunctions.GetDirValue("app_data") + "lucene\\";

            if (!Directory.Exists(indexDirectory))
            {
                Directory.CreateDirectory(indexDirectory);
            }

            if (ModuleID > 0)
            {
                indexDirectory = SepFunctions.GetDirValue("app_data") + "lucene\\" + ModuleID + "\\";

                if (!Directory.Exists(indexDirectory))
                {
                    Directory.CreateDirectory(indexDirectory);
                }
            }

            if (!Directory.Exists(indexDirectory))
            {
                return Results;
            }

            IndexReader reader = null;


            IndexSearcher searcher;
            try
            {
                var DIindexDirectory = new DirectoryInfo(indexDirectory);
                FSDirectory FSD = FSDirectory.Open(DIindexDirectory);
                reader = DirectoryReader.Open(FSD, 1);
                searcher = new IndexSearcher(reader);
                FSD.Dispose();
            }
            catch
            {
                if (reader != null)
                {
                    reader.Dispose();
                }

                return Results;
            }

            // parse the query, "text" is the default field to search
            string[] fields = { "title", "description", "keywords", "text" };
            if (!string.IsNullOrWhiteSpace(UserID))
            {
                // "userid";
            }

            var parser = new MultiFieldQueryParser(
                LuceneVersion.LUCENE_48,
                fields,
                new StandardAnalyzer(LuceneVersion.LUCENE_48));

            Query query;
            if (!string.IsNullOrWhiteSpace(UserID))
            {
                query = parser.Parse(UserID);
            }
            else
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Keywords))
                    {
                        query = parser.Parse(Keywords);
                    }
                    else
                    {
                        reader.Dispose();
                        return Results;
                    }
                }
                catch
                {
                    reader.Dispose();
                    return Results;
                }
            }

            // create the result DataTable
            Results.Columns.Add("title", typeof(string));
            Results.Columns.Add("description", typeof(string));
            Results.Columns.Add("keywords", typeof(string));
            Results.Columns.Add("text", typeof(string));
            Results.Columns.Add("url", typeof(string));
            Results.Columns.Add("postalcode", typeof(string));

            // search
            var hits = searcher.Search(query, 1000);

            total = hits.TotalHits;

            // initialize startAt
            startAt = InitStartAt;

            // how many items we should show - less than defined at the end of the results
            var resultsCount = Math.Min(total, maxResults + startAt);

            for (var i = startAt; i <= resultsCount - 1; i++)
            {
                // get the document from index
                var doc = searcher.Doc(hits.ScoreDocs[i].Doc);

                if (!string.IsNullOrWhiteSpace(doc.Get("text")))
                {
                    bool ShowRecord;
                    if (Distance != "ANY" && SepFunctions.toDouble(SepFunctions.PostalCodeDistance(PostalCode, doc.Get("postalcode"))) < SepFunctions.toDouble(Distance))
                    {
                        ShowRecord = true;
                    }
                    else if (Distance == "ANY" && string.IsNullOrWhiteSpace(PostalCode))
                    {
                        ShowRecord = true;
                    }
                    else
                    {
                        ShowRecord = false;
                    }

                    if (ShowRecord)
                    {
                        // create a new row with the result data
                        var row = Results.NewRow();
                        row["title"] = doc.Get("title");
                        row["description"] = doc.Get("description");
                        row["keywords"] = doc.Get("keywords");
                        row["url"] = Strings.Replace(Strings.Replace(Strings.Replace(SepFunctions.GetDomainName() + doc.Get("url"), "http://", string.Empty), "https://", string.Empty), "//", "/");
                        row["postalcode"] = doc.Get("postalcode");

                        Results.Rows.Add(row);
                    }
                }
            }

            // result information
            duration = DateTime.Now - start;
            fromItem = startAt + 1;
            toItem = Math.Min(startAt + maxResults, total);

            reader.Dispose();

            return Results;
        }

        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <returns>A string.</returns>
        public string Summary()
        {
            if (total > 0)
            {
                return "Results <b>" + fromItem + " - " + toItem + "</b> of <b>" + total + "</b> for <b>" + Keywords + "</b>. (" + duration.TotalSeconds + " seconds)";
            }

            return "No results found";
        }

        /// <summary>
        /// Paging item HTML.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="number">Number of.</param>
        /// <param name="active">True to active.</param>
        /// <returns>A string.</returns>
        private string PagingItemHtml(int start, int number, bool active)
        {
            if (active)
            {
                var sPageName = "search.aspx";
                switch (ModuleID)
                {
                    case 5:
                        sPageName = "discounts_search.aspx";
                        break;

                    case 9:
                        sPageName = "faq_search.aspx";
                        break;

                    case 10:
                        sPageName = "downloads_search.aspx";
                        break;

                    case 12:
                        sPageName = "forums_search.aspx";
                        break;

                    case 19:
                        sPageName = "links_search.aspx";
                        break;

                    case 20:
                        sPageName = "businesses_search.aspx";
                        break;

                    case 31:
                        sPageName = "auction_search.aspx";
                        break;

                    case 35:
                        sPageName = "articles_search.aspx";
                        break;

                    case 37:
                        sPageName = "elearning_search.aspx";
                        break;

                    case 44:
                        sPageName = "classifieds_search.aspx";
                        break;

                    case 46:
                        sPageName = "events_search.aspx";
                        break;

                    case 61:
                        sPageName = "blogs_search.aspx";
                        break;

                    case 63:
                        sPageName = "profiles_search.aspx";
                        break;
                }

                var sUserId = !string.IsNullOrWhiteSpace(UserID) ? "&UserID=" + UserID : string.Empty;

                return "<a href=\"" + sPageName + "?q=" + Keywords + "&start=" + start + sUserId + "\">" + number + "</a>";
            }

            return "<b>" + number + "</b>";
        }
    }
}