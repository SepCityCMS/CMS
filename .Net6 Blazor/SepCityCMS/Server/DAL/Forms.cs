// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Forms.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using Models;
    using SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Forms.
    /// </summary>
    public static class Forms
    {
        /// <summary>
        /// Answers the get.
        /// </summary>
        /// <param name="SubmissionID">The submission identifier.</param>
        /// <param name="FormID">The form identifier.</param>
        /// <param name="QuestionID">The question identifier.</param>
        /// <returns>Models.FormsAnswers.</returns>
        public static FormsAnswers Answer_Get(long SubmissionID, long FormID, long QuestionID)
        {
            var returnXML = new Models.FormsAnswers();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM FormAnswers WHERE SubmissionID=@SubmissionID AND FormID=@FormID AND QuestionID=@QuestionID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@SubmissionID", SubmissionID);
                    cmd.Parameters.AddWithValue("@FormID", FormID);
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.SubmissionID = SepFunctions.toLong(SepFunctions.openNull(RS["SubmissionID"]));
                            returnXML.AnswerID = SepFunctions.toLong(SepFunctions.openNull(RS["AnswerID"]));
                            returnXML.FormID = SepFunctions.toLong(SepFunctions.openNull(RS["FormID"]));
                            returnXML.QuestionID = SepFunctions.toLong(SepFunctions.openNull(RS["QuestionID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.Answer = SepFunctions.openNull(RS["Answer"]);
                            returnXML.SubmitDate = SepFunctions.toDate(SepFunctions.openNull(RS["SubmitDate"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Forms the delete.
        /// </summary>
        /// <param name="FormIDs">The form i ds.</param>
        /// <returns>System.String.</returns>
        public static string Form_Delete(string FormIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrFormIDs = Strings.Split(FormIDs, ",");

                if (arrFormIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFormIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Forms SET Status='-1', DateDeleted=@DateDeleted WHERE FormID=@FormID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FormID", arrFormIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE FormSections SET Status='-1', DateDeleted=@DateDeleted WHERE FormID=@FormID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FormID", arrFormIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE FormQuestions SET Status='-1', DateDeleted=@DateDeleted WHERE FormID=@FormID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FormID", arrFormIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE FormAnswers SET Status='-1', DateDeleted=@DateDeleted WHERE FormID=@FormID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FormID", arrFormIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(13, arrFormIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Form(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Forms the get.
        /// </summary>
        /// <param name="FormID">The form identifier.</param>
        /// <returns>Models.Forms.</returns>
        public static Models.Forms Form_Get(long FormID)
        {
            var returnXML = new Models.Forms();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Forms WHERE FormID=@FormID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@FormID", FormID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.FormID = SepFunctions.toLong(SepFunctions.openNull(RS["FormID"]));
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.Available = SepFunctions.toBoolean(SepFunctions.openNull(RS["Available"]));
                            returnXML.Title = SepFunctions.openNull(RS["Title"]);
                            returnXML.Email = SepFunctions.openNull(RS["Email"]);
                            returnXML.CompletionURL = SepFunctions.openNull(RS["CompletionURL"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Forms the mark available.
        /// </summary>
        /// <param name="FormIDs">The form i ds.</param>
        /// <returns>System.String.</returns>
        public static string Form_Mark_Available(string FormIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrFormIDs = Strings.Split(FormIDs, ",");

                if (arrFormIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFormIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Forms SET Available='1' WHERE FormID=@FormID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FormID", arrFormIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error marking a form as available:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Form(s) has been marked as available.");

            return sReturn;
        }

        /// <summary>
        /// Forms the mark unavailable.
        /// </summary>
        /// <param name="FormIDs">The form i ds.</param>
        /// <returns>System.String.</returns>
        public static string Form_Mark_Unavailable(string FormIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrFormIDs = Strings.Split(FormIDs, ",");

                if (arrFormIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFormIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Forms SET Available='0' WHERE FormID=@FormID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FormID", arrFormIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error marking a form as unavailable:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Form(s) has been marked as unavailable.");

            return sReturn;
        }

        /// <summary>
        /// Forms the save.
        /// </summary>
        /// <param name="FormID">The form identifier.</param>
        /// <param name="Title">The title.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Description">The description.</param>
        /// <param name="Email">The email.</param>
        /// <param name="CompletionURL">The completion URL.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Form_Save(long FormID, string Title, string UserID, string Description, string Email, string CompletionURL, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (FormID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT FormID FROM Forms WHERE FormID=@FormID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FormID", FormID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    FormID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Forms SET Title=@Title, Email=@Email, CompletionURL=@CompletionURL, Description=@Description, PortalID=@PortalID WHERE FormID=@FormID";
                }
                else
                {
                    SqlStr = "INSERT INTO Forms (FormID, Title, Email, CompletionURL, Description, Available, PortalID, DatePosted, Status) VALUES (@FormID, @Title, @Email, @CompletionURL, @Description, @Available, @PortalID, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@FormID", FormID);
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@CompletionURL", CompletionURL);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Available", "0");
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 13, Strings.ToString(FormID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Form", "Form", string.Empty);

            if (intReturn == 0)
            {
                if (bUpdate)
                {
                    return 2;
                }

                return 3;
            }
            else
            {
                return intReturn;
            }
        }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Forms&gt;.</returns>
        public static List<Models.Forms> GetForms(string SortExpression = "Title", string SortDirection = "ASC", string searchWords = "")
        {
            var lForms = new List<Models.Forms>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Title";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                if (searchWords == "Available")
                {
                    wClause = " AND Available='1'";
                }
                else
                {
                    wClause = " AND Title LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
                }
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT FormID,Title,Available,DatePosted FROM Forms WHERE Status <> -1 AND (PortalID=@PortalID OR PortalID ='-1')" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dForms = new Models.Forms { FormID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FormID"])) };
                    dForms.Title = SepFunctions.openNull(ds.Tables[0].Rows[i]["Title"]);
                    dForms.Available = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["Available"]));
                    dForms.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lForms.Add(dForms);
                }
            }

            return lForms;
        }

        /// <summary>
        /// Gets the forms questions.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="sectionId">The section identifier.</param>
        /// <param name="FormID">The form identifier.</param>
        /// <returns>List&lt;Models.FormsQuestions&gt;.</returns>
        public static List<FormsQuestions> GetFormsQuestions(string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "", long sectionId = 0, long FormID = 0)
        {
            var lFormsQuestions = new List<FormsQuestions>();

            var rowCount = 0;

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Weight";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Question LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (sectionId > -1)
            {
                wClause += " AND SectionID='" + SepFunctions.FixWord(Strings.ToString(sectionId)) + "'";
            }

            if (FormID > 0)
            {
                wClause += " AND FormID='" + SepFunctions.toLong(Strings.ToString(FormID)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT QuestionID,SectionID,FormID,TypeID,Weight,Mandatory,Question FROM FormQuestions WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    rowCount += 1;
                    var dFormsQuestions = new Models.FormsQuestions { QuestionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["QuestionID"])) };
                    dFormsQuestions.SectionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SectionID"]));
                    dFormsQuestions.FormID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FormID"]));
                    dFormsQuestions.TypeID = SepFunctions.openNull(ds.Tables[0].Rows[i]["TypeID"]);
                    dFormsQuestions.Weight = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Weight"]));
                    dFormsQuestions.Mandatory = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Mandatory"]);
                    dFormsQuestions.Question = SepFunctions.openNull(ds.Tables[0].Rows[i]["Question"]);
                    dFormsQuestions.RowNumber = rowCount;
                    lFormsQuestions.Add(dFormsQuestions);
                }
            }

            return lFormsQuestions;
        }

        /// <summary>
        /// Gets the forms questions options.
        /// </summary>
        /// <param name="QuestionID">The question identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.FormsQuestionsOptions&gt;.</returns>
        public static List<FormsQuestionsOptions> GetFormsQuestionsOptions(long QuestionID, string SortExpression = "OptionValue", string SortDirection = "ASC", string searchWords = "")
        {
            var lFormsQuestionsOptions = new List<FormsQuestionsOptions>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "OptionValue";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND OptionValue LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT OptionID,QuestionID,OptionValue FROM FormOptions WHERE QuestionID='" + SepFunctions.FixWord(Strings.ToString(QuestionID)) + "'" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dFormsQuestionsOptions = new Models.FormsQuestionsOptions { OptionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["OptionID"])) };
                    dFormsQuestionsOptions.QuestionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["QuestionID"]));
                    dFormsQuestionsOptions.OptionValue = SepFunctions.openNull(ds.Tables[0].Rows[i]["OptionValue"]);
                    lFormsQuestionsOptions.Add(dFormsQuestionsOptions);
                }
            }

            return lFormsQuestionsOptions;
        }

        /// <summary>
        /// Gets the forms sections.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="FormId">The form identifier.</param>
        /// <returns>List&lt;Models.FormsSections&gt;.</returns>
        public static List<FormsSections> GetFormsSections(string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "", long FormId = 0)
        {
            var lFormsSections = new List<FormsSections>();

            var rowCount = 0;

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Weight";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND SectionName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (FormId > 0)
            {
                wClause += " AND FormID='" + SepFunctions.toLong(Strings.ToString(FormId)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT SectionID,FormID,Weight,SectionName FROM FormSections WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    rowCount += 1;
                    var dFormsSections = new Models.FormsSections { SectionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SectionID"])) };
                    dFormsSections.FormID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FormID"]));
                    dFormsSections.Weight = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Weight"]));
                    dFormsSections.SectionName = SepFunctions.openNull(ds.Tables[0].Rows[i]["SectionName"]);
                    dFormsSections.RowNumber = rowCount;
                    lFormsSections.Add(dFormsSections);
                }
            }

            return lFormsSections;
        }

        /// <summary>
        /// Gets the forms submissions.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.FormsSubmissions&gt;.</returns>
        public static List<FormsSubmissions> GetFormsSubmissions(string SortExpression = "SubmitDate", string SortDirection = "DESC", string searchWords = "")
        {
            var lFormsSubmissions = new List<FormsSubmissions>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "SubmitDate";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND Username LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT DISTINCT SubmissionID,FormID,(SELECT TOP 1 Username FROM Members WHERE UserID=FormAnswers.UserID) AS Username,(SELECT Title FROM Forms WHERE FormID=FormAnswers.FormID) AS FormName,(SELECT TOP 1 CAST(Answer AS varchar(100)) FROM FormAnswers WHERE QuestionID='0' AND SubmissionID=FormAnswers.SubmissionID) AS EmailAddress,(SELECT TOP 1 SubmitDate FROM FormAnswers WHERE QuestionID='0' AND SubmissionID=FormAnswers.SubmissionID) AS SubmitDate FROM FormAnswers WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dFormsSubmissions = new Models.FormsSubmissions { SubmissionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SubmissionID"])) };
                    dFormsSubmissions.FormID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FormID"]));
                    dFormsSubmissions.FormName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FormName"]);
                    dFormsSubmissions.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dFormsSubmissions.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]);
                    dFormsSubmissions.SubmitDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["SubmitDate"]));
                    lFormsSubmissions.Add(dFormsSubmissions);
                }
            }

            return lFormsSubmissions;
        }

        /// <summary>
        /// Questions the delete.
        /// </summary>
        /// <param name="QuestionIDs">The question i ds.</param>
        /// <returns>System.String.</returns>
        public static string Question_Delete(string QuestionIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrQuestionIDs = Strings.Split(QuestionIDs, ",");

                if (arrQuestionIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrQuestionIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE FormQuestions SET Status='-1', DateDeleted=@DateDeleted WHERE QuestionID=@QuestionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@QuestionID", arrQuestionIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE FormAnswers SET Status='-1', DateDeleted=@DateDeleted WHERE QuestionID=@QuestionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@QuestionID", arrQuestionIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Question(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Questions the get.
        /// </summary>
        /// <param name="QuestionID">The question identifier.</param>
        /// <returns>Models.FormsQuestions.</returns>
        public static FormsQuestions Question_Get(long QuestionID)
        {
            var returnXML = new Models.FormsQuestions();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM FormQuestions WHERE QuestionID=@QuestionID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.QuestionID = SepFunctions.toLong(SepFunctions.openNull(RS["QuestionID"]));
                            returnXML.FormID = SepFunctions.toLong(SepFunctions.openNull(RS["FormID"]));
                            returnXML.SectionID = SepFunctions.toLong(SepFunctions.openNull(RS["SectionID"]));
                            returnXML.TypeID = SepFunctions.openNull(RS["TypeID"]);
                            returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
                            returnXML.Mandatory = SepFunctions.toBoolean(SepFunctions.openNull(RS["Mandatory"]));
                            returnXML.Question = SepFunctions.openNull(RS["Question"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Questions the option save.
        /// </summary>
        /// <param name="OptionID">The option identifier.</param>
        /// <param name="QuestionID">The question identifier.</param>
        /// <param name="OptionValue">The option value.</param>
        /// <returns>System.String.</returns>
        public static string Question_Option_Save(long OptionID, long QuestionID, string OptionValue)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (OptionID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT OptionID FROM FormOptions WHERE OptionID=@OptionID", conn))
                    {
                        cmd.Parameters.AddWithValue("@OptionID", OptionID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    OptionID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE FormOptions SET OptionValue=@OptionValue WHERE OptionID=@OptionID";
                }
                else
                {
                    SqlStr = "INSERT INTO FormOptions (OptionID, QuestionID, OptionValue) VALUES (@OptionID, @QuestionID, @OptionValue)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@OptionID", OptionID);
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                    cmd.Parameters.AddWithValue("@OptionValue", OptionValue);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Question option has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Question option has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Questions the save.
        /// </summary>
        /// <param name="QuestionID">The question identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="FormID">The form identifier.</param>
        /// <param name="SectionID">The section identifier.</param>
        /// <param name="QuestionType">Type of the question.</param>
        /// <param name="Weight">The weight.</param>
        /// <param name="Required">if set to <c>true</c> [required].</param>
        /// <param name="Question">The question.</param>
        /// <returns>System.Int32.</returns>
        public static int Question_Save(long QuestionID, string UserID, long FormID, long SectionID, string QuestionType, string Weight, bool Required, string Question)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (QuestionID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT QuestionID FROM FormQuestions WHERE QuestionID=@QuestionID", conn))
                    {
                        cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    QuestionID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE FormQuestions SET FormID=@FormID, SectionID=@SectionID, TypeID=@TypeID, Weight=@Weight, Mandatory=@Mandatory, Question=@Question WHERE QuestionID=@QuestionID";
                }
                else
                {
                    SqlStr = "INSERT INTO FormQuestions (QuestionID, FormID, SectionID, TypeID, Weight, Mandatory, Question, Status) VALUES (@QuestionID, @FormID, @SectionID, @TypeID, @Weight, @Mandatory, @Question, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                    cmd.Parameters.AddWithValue("@FormID", FormID);
                    cmd.Parameters.AddWithValue("@SectionID", SectionID);
                    cmd.Parameters.AddWithValue("@TypeID", QuestionType);
                    cmd.Parameters.AddWithValue("@Weight", Weight);
                    cmd.Parameters.AddWithValue("@Mandatory", Required);
                    cmd.Parameters.AddWithValue("@Question", Question);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 13, Strings.ToString(FormID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "FormQuestion", "Form Question", string.Empty);

            if (intReturn == 0)
            {
                if (bUpdate)
                {
                    return 2;
                }

                return 3;
            }
            else
            {
                return intReturn;
            }
        }

        /// <summary>
        /// Sections the delete.
        /// </summary>
        /// <param name="SectionIDs">The section i ds.</param>
        /// <returns>System.String.</returns>
        public static string Section_Delete(string SectionIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrSectionIDs = Strings.Split(SectionIDs, ",");

                if (arrSectionIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrSectionIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE FormSections SET Status='-1', DateDeleted=@DateDeleted WHERE SectionID=@SectionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SectionID", arrSectionIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Section(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Sections the get.
        /// </summary>
        /// <param name="SectionID">The section identifier.</param>
        /// <returns>Models.FormsSections.</returns>
        public static FormsSections Section_Get(long SectionID)
        {
            var returnXML = new Models.FormsSections();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM FormSections WHERE SectionID=@SectionID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@SectionID", SectionID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.SectionID = SepFunctions.toLong(SepFunctions.openNull(RS["SectionID"]));
                            returnXML.FormID = SepFunctions.toLong(SepFunctions.openNull(RS["FormID"]));
                            returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
                            returnXML.SectionName = SepFunctions.openNull(RS["SectionName"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Sections the save.
        /// </summary>
        /// <param name="SectionID">The section identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="FormID">The form identifier.</param>
        /// <param name="SectionOrder">The section order.</param>
        /// <param name="SectionName">Name of the section.</param>
        /// <returns>System.Int32.</returns>
        public static int Section_Save(long SectionID, string UserID, long FormID, long SectionOrder, string SectionName)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SectionID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT SectionID FROM FormSections WHERE SectionID=@SectionID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SectionID", SectionID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    SectionID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE FormSections SET FormID=@FormID, SectionName=@SectionName, Weight=@Weight WHERE SectionID=@SectionID";
                }
                else
                {
                    SqlStr = "INSERT INTO FormSections (SectionID, FormID, SectionName, Weight, Status) VALUES (@SectionID, @FormID, @SectionName, @Weight, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@SectionID", SectionID);
                    cmd.Parameters.AddWithValue("@SectionName", SectionName);
                    cmd.Parameters.AddWithValue("@Weight", SectionOrder);
                    cmd.Parameters.AddWithValue("@FormID", FormID);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 13, Strings.ToString(SectionID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "FormSection", "Form Section", string.Empty);

            if (intReturn == 0)
            {
                if (bUpdate)
                {
                    return 2;
                }

                return 3;
            }
            else
            {
                return intReturn;
            }
        }

        /// <summary>
        /// Submissionses the delete.
        /// </summary>
        /// <param name="SubmissionIDs">The submission i ds.</param>
        /// <returns>System.String.</returns>
        public static string Submissions_Delete(string SubmissionIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrSubmissionIDs = Strings.Split(SubmissionIDs, ",");

                if (arrSubmissionIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrSubmissionIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE FormAnswers SET Status='-1', DateDeleted=@DateDeleted WHERE SubmissionID=@SubmissionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SubmissionID", arrSubmissionIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Submissions(s) has been successfully deleted.");

            return sReturn;
        }
    }
}