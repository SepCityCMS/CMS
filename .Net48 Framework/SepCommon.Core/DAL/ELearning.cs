// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ELearning.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.Models;
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class ELearning.
    /// </summary>
    public static class ELearning
    {
        /// <summary>
        /// Enum AssignmentType
        /// </summary>
        public enum AssignmentType
        {
            /// <summary>
            /// All assignments
            /// </summary>
            AllAssignments = 1,

            /// <summary>
            /// The available assignments
            /// </summary>
            AvailableAssignments = 2,

            /// <summary>
            /// The submitted assignments
            /// </summary>
            SubmittedAssignments = 3
        }

        /// <summary>
        /// Assignments the delete.
        /// </summary>
        /// <param name="AssignmentIDs">The assignment i ds.</param>
        /// <returns>System.String.</returns>
        public static string Assignment_Delete(string AssignmentIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrAssignmentIDs = Strings.Split(AssignmentIDs, ",");

                if (arrAssignmentIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAssignmentIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ELearnHomework SET Status='-1', DateDeleted=@DateDeleted WHERE HomeID=@AssignmentID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AssignmentID", arrAssignmentIDs[i]);
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

            sReturn = SepFunctions.LangText("Assignment(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Assignments the get.
        /// </summary>
        /// <param name="AssignmentID">The assignment identifier.</param>
        /// <returns>Models.ElearningAssignments.</returns>
        public static ElearningAssignments Assignment_Get(long AssignmentID)
        {
            var returnXML = new Models.ElearningAssignments();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ELearnHomework WHERE HomeID=@HomeID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@HomeID", AssignmentID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.AssignmentID = SepFunctions.toLong(SepFunctions.openNull(RS["HomeID"]));
                            returnXML.CourseID = SepFunctions.toLong(SepFunctions.openNull(RS["CourseID"]));
                            returnXML.Title = SepFunctions.openNull(RS["HWTitle"]);
                            returnXML.DueDate = SepFunctions.toDate(SepFunctions.openNull(RS["DueDate"]));
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Assignments the save.
        /// </summary>
        /// <param name="AssignmentID">The assignment identifier.</param>
        /// <param name="CourseID">The course identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Title">The title.</param>
        /// <param name="DueDate">The due date.</param>
        /// <param name="Description">The description.</param>
        /// <returns>System.Int32.</returns>
        public static int Assignment_Save(long AssignmentID, long CourseID, string UserID, string Title, DateTime DueDate, string Description)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (AssignmentID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT HomeID FROM ELearnHomework WHERE HomeID=@HomeID", conn))
                    {
                        cmd.Parameters.AddWithValue("@HomeID", AssignmentID);
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
                    AssignmentID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ELearnHomework SET HWTitle=@HWTitle, Description=@Description, DueDate=@DueDate WHERE HomeID=@HomeID";
                }
                else
                {
                    SqlStr = "INSERT INTO ELearnHomework (HomeID, CourseID, HWTitle, Description, DueDate, Status) VALUES (@HomeID, @CourseID, @HWTitle, @Description, @DueDate, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@HomeID", AssignmentID);
                    cmd.Parameters.AddWithValue("@CourseID", CourseID);
                    cmd.Parameters.AddWithValue("@HWTitle", Title);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@DueDate", DueDate);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 37, Strings.ToString(AssignmentID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Assignment", "Assignment", string.Empty);

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
        /// Assignments the submit.
        /// </summary>
        /// <param name="SubmitID">The submit identifier.</param>
        /// <param name="AssignmentID">The assignment identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Grade">The grade.</param>
        /// <param name="Notes">The notes.</param>
        /// <returns>System.Int32.</returns>
        public static int Assignment_Submit(long SubmitID, string AssignmentID, string UserID, string Grade, string Notes)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SubmitID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT HUserID FROM ELearnHomeUsers WHERE HUserID=@HUserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@HUserID", SubmitID);
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
                    SubmitID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ELearnHomeUsers SET Grade=@Grade, HomeNotes=@HomeNotes WHERE HUserID=@HUserID";
                }
                else
                {
                    SqlStr = "INSERT INTO ELearnHomeUsers (HUserID, UserID, HomeID, Grade, HomeNotes, DatePosted, Approved) VALUES (@HUserID, @UserID, @HomeID, @Grade, @HomeNotes, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@HUserID", SubmitID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@HomeID", AssignmentID);
                    cmd.Parameters.AddWithValue("@Grade", Grade);
                    cmd.Parameters.AddWithValue("@HomeNotes", Notes);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 37, Strings.ToString(SubmitID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "SubmitAssignment", "Assignment Submitted", string.Empty);

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
        /// Assignments the submit get.
        /// </summary>
        /// <param name="AssignmentID">The assignment identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>Models.ElearningAssignments.</returns>
        public static ElearningAssignments Assignment_Submit_Get(long AssignmentID, string UserID)
        {
            var returnXML = new Models.ElearningAssignments();
            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT EH.Grade,EH.HomeNotes,EH.DatePosted,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='37' AND UniqueID=EH.HomeID AND isTemp='0' AND Approved='1' AND ControlID='Attachment') AS DownloadLink FROM ELearnHomeUsers AS EH WHERE EH.HomeID=@HomeID AND EH.UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@HomeID", AssignmentID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.Grade = SepFunctions.openNull(RS["Grade"]);
                            returnXML.Notes = SepFunctions.openNull(RS["HomeNotes"]);
                            returnXML.DateSubmitted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["DownloadLink"])))
                            {
                                returnXML.DownloadLink = "<a href=\"" + sInstallFolder + "elearning_download.aspx?UploadID=" + SepFunctions.openNull(RS["DownloadLink"]) + "\" target=\"_blank\">" + SepFunctions.LangText("Download Assignment") + "</a>";
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Courses the change status.
        /// </summary>
        /// <param name="CourseIDs">The course i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Course_Change_Status(string CourseIDs, int Status)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrCourseIDs = Strings.Split(CourseIDs, ",");

                if (arrCourseIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrCourseIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ELearnCourses SET Status=@Status WHERE CourseID=@CourseID", conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseID", arrCourseIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
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

            sReturn = SepFunctions.LangText("Course(s) status has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Courses the delete.
        /// </summary>
        /// <param name="CourseIDs">The course i ds.</param>
        /// <returns>System.String.</returns>
        public static string Course_Delete(string CourseIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrCourseIDs = Strings.Split(CourseIDs, ",");

                if (arrCourseIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrCourseIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ELearnCourses SET Status='-1', DateDeleted=@DateDeleted WHERE CourseID=@CourseID", conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseID", arrCourseIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ELearnExams SET Status='-1', DateDeleted=@DateDeleted WHERE CourseID=@CourseID", conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseID", arrCourseIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ELearnHomework SET Status='-1', DateDeleted=@DateDeleted WHERE CourseID=@CourseID", conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseID", arrCourseIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ELearnStudents SET Status='-1', DateDeleted=@DateDeleted WHERE CourseID=@CourseID", conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseID", arrCourseIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(37, arrCourseIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Course(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Courses the get.
        /// </summary>
        /// <param name="CourseID">The course identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.ElearningCourses.</returns>
        public static ElearningCourses Course_Get(long CourseID, long ChangeID = 0)
        {
            var returnXML = new Models.ElearningCourses();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ELearnCourses WHERE CourseID=@CourseID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", CourseID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var logData = string.Empty;
                            if (ChangeID > 0)
                            {
                                logData = SepFunctions.Get_Change_Log(ChangeID);
                            }

                            if (ChangeID > 0 && !string.IsNullOrWhiteSpace(logData))
                            {
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    var fieldName = RS.GetName(i);
                                    var fieldType = RS.GetFieldType(i);
                                    var fieldValue = SepFunctions.openNull(RS[i]);
                                    if (Strings.InStr(logData, "<" + fieldName + ">") > 0)
                                    {
                                        var xmlNode = SepFunctions.ParseXML(fieldName, logData);
                                        if (!string.IsNullOrWhiteSpace(xmlNode))
                                        {
                                            fieldValue = xmlNode;
                                        }
                                    }

                                    var prop = returnXML.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        switch (fieldType.Name)
                                        {
                                            case "Double":
                                                prop.SetValue(returnXML, SepFunctions.toLong(fieldValue), null);
                                                break;

                                            case "DateTime":
                                                prop.SetValue(returnXML, SepFunctions.toDate(fieldValue), null);
                                                break;

                                            case "Boolean":
                                                prop.SetValue(returnXML, SepFunctions.toBoolean(fieldValue), null);
                                                break;

                                            case "Int32":
                                                prop.SetValue(returnXML, SepFunctions.toInt(fieldValue), null);
                                                break;

                                            case "Decimal":
                                                prop.SetValue(returnXML, SepFunctions.toDecimal(fieldValue), null);
                                                break;

                                            default:
                                                prop.SetValue(returnXML, fieldValue, null);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                returnXML.CourseID = SepFunctions.toLong(SepFunctions.openNull(RS["CourseID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.CourseName = SepFunctions.openNull(RS["CourseName"]);
                                returnXML.Instructor = SepFunctions.openNull(RS["Instructor"]);
                                returnXML.StartDate = SepFunctions.toDate(SepFunctions.openNull(RS["StartDate"]));
                                returnXML.EndDate = SepFunctions.toDate(SepFunctions.openNull(RS["EndDate"]));
                                returnXML.Credits = SepFunctions.toLong(SepFunctions.openNull(RS["Credits"]));
                                returnXML.CreateDate = SepFunctions.toDate(SepFunctions.openNull(RS["CreateDate"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                                using (var cmd2 = new SqlCommand("SELECT UnitPrice,RecurringPrice,RecurringCycle,Description FROM ShopProducts WHERE ModelNumber=@CourseID AND ModuleID='37'", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@CourseID", CourseID);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            RS2.Read();
                                            returnXML.Price = SepFunctions.toDecimal(SepFunctions.openNull(RS2["UnitPrice"]));
                                            returnXML.RecurringPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS2["RecurringPrice"]));
                                            returnXML.RecurringCycle = SepFunctions.openNull(RS2["RecurringCycle"]);
                                            returnXML.Description = SepFunctions.openNull(RS2["Description"]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT UploadID,FileData,ContentType,PortalID FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID=37", conn))
                {
                    cmd.Parameters.AddWithValue("@UniqueID", CourseID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ThumbnailImage = Strings.ToString(Information.IsDBNull(RS["FileData"]) ? string.Empty : SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["FileData"])));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Courses the save.
        /// </summary>
        /// <param name="CourseID">The course identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CourseName">Name of the course.</param>
        /// <param name="Instructor">The instructor.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="Credits">The credits.</param>
        /// <param name="UnitPrice">The unit price.</param>
        /// <param name="RecurringPrice">The recurring price.</param>
        /// <param name="RecurringCycle">The recurring cycle.</param>
        /// <param name="Description">The description.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="ThumbnailFileName">Name of the thumbnail file.</param>
        /// <param name="ThumbnailImageType">Type of the thumbnail image.</param>
        /// <param name="ThumbnailImageData">The thumbnail image data.</param>
        /// <returns>System.Int32.</returns>
        public static int Course_Save(long CourseID, long CatID, string UserID, string CourseName, string Instructor, DateTime StartDate, DateTime EndDate, int Credits, string UnitPrice, string RecurringPrice, string RecurringCycle, string Description, long PortalID, string ThumbnailFileName, string ThumbnailImageType, string ThumbnailImageData)
        {
            var bUpdate = false;
            var bUpdateProduct = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (CourseID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM ELearnCourses WHERE CourseID=@CourseID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    oldValues.Add(RS.GetName(i), SepFunctions.openNull(RS[i]));
                                }

                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    CourseID = SepFunctions.GetIdentity();
                }

                if (CourseID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ProductID FROM ShopProducts WHERE ModelNumber=@ModelNumber", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModelNumber", CourseID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdateProduct = true;
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(ThumbnailFileName) && !string.IsNullOrWhiteSpace(ThumbnailImageType) && !string.IsNullOrWhiteSpace(ThumbnailImageData))
                {
                    using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", CourseID);
                        cmd.Parameters.AddWithValue("@ModuleID", 37);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID, UniqueID, UserID, ModuleID, FileName, FileSize, ContentType, isTemp, Approved, DatePosted, PortalID, FileData, ControlID, UserRates, TotalRates, Weight) VALUES(@UploadID, @UniqueID, @UserID, @ModuleID, @FileName, @FileSize, @ContentType, @isTemp, @Approved, @DatePosted, @PortalID, @FileData, @ControlID, '0', '0', '99')", conn))
                    {
                        cmd.Parameters.AddWithValue("@UploadID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@UniqueID", CourseID);
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.Parameters.AddWithValue("@ModuleID", 37);
                        cmd.Parameters.AddWithValue("@FileName", ThumbnailFileName);
                        cmd.Parameters.AddWithValue("@FileSize", ThumbnailImageData.Length);
                        cmd.Parameters.AddWithValue("@ContentType", ThumbnailImageType);
                        cmd.Parameters.AddWithValue("@isTemp", true);
                        cmd.Parameters.AddWithValue("@Approved", true);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.Parameters.AddWithValue("@FileData", SepFunctions.StringToBytes(SepFunctions.Base64Decode(ThumbnailImageData)));
                        cmd.Parameters.AddWithValue("@ControlID", string.Empty);
                        cmd.ExecuteNonQuery();
                    }
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ELearnCourses SET CatID=@CatID, CourseName=@CourseName, StartDate=@StartDate, EndDate=@EndDate, Instructor=@Instructor, Credits=@Credits WHERE CourseID=@CourseID";
                }
                else
                {
                    SqlStr = "INSERT INTO ELearnCourses (CourseID, CatID, CourseName, StartDate, EndDate, Instructor, Credits, PortalID, Status) VALUES (@CourseID, @CatID, @CourseName, @StartDate, @EndDate, @Instructor, @Credits, @PortalID, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", CourseID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@CourseName", CourseName);
                    cmd.Parameters.AddWithValue("@Instructor", Instructor);
                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);
                    cmd.Parameters.AddWithValue("@Credits", Credits);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }

                if (bUpdateProduct)
                {
                    SqlStr = "UPDATE ShopProducts SET CatID=@CatID, RecurringPrice=@RecurringPrice, RecurringCycle=@RecurringCycle, ProductName=@ProductName, UnitPrice=@UnitPrice, Description=@Description WHERE ModelNumber=@ModelNumber";
                }
                else
                {
                    SqlStr = "INSERT INTO ShopProducts (ProductID, CatID, RecurringPrice, RecurringCycle, ModelNumber, ModuleID, ProductName, UnitPrice, Description, PortalID, AffiliateUnitPrice, AffiliateRecurringPrice, ExcludeAffiliate) VALUES (@ProductID, @CatID, @RecurringPrice, @RecurringCycle, @ModelNumber, '37', @ProductName, @UnitPrice, @Description, @PortalID, '0', '0', '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", SepFunctions.GetIdentity());
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@UnitPrice", SepFunctions.toDecimal(UnitPrice));
                    cmd.Parameters.AddWithValue("@RecurringPrice", SepFunctions.toDecimal(RecurringPrice));
                    cmd.Parameters.AddWithValue("@RecurringCycle", RecurringCycle);
                    cmd.Parameters.AddWithValue("@ModelNumber", CourseID.ToString());
                    cmd.Parameters.AddWithValue("@ProductName", CourseName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 37, Strings.ToString(CourseID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Course", "Course", string.Empty);

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM ELearnCourses WHERE CourseID=@CourseID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    if (oldValues.ContainsKey(RS.GetName(i)))
                                    {
                                        if (SepFunctions.openNull(RS[i]) != SepFunctions.openNull(oldValues[RS.GetName(i)]))
                                        {
                                            changedValues.Add(RS.GetName(i), SepFunctions.openNull(oldValues[RS.GetName(i)]));
                                        }
                                    }
                                }

                                bUpdate = true;
                            }
                        }
                    }

                    if (changedValues.Count > 0)
                    {
                        var writeLog = new StringBuilder();
                        writeLog.AppendLine("<?xml version=\"1.0\" encoding=\"utf - 8\" ?>");
                        writeLog.AppendLine("<root>");
                        for (var e = changedValues.GetEnumerator(); e.MoveNext();)
                        {
                            writeLog.AppendLine("<" + e.Key + ">" + SepFunctions.HTMLEncode(Strings.ToString(e.Value)) + "</" + e.Key + ">");
                        }

                        writeLog.AppendLine("</root>");
                        SepFunctions.Update_Change_Log(37, Strings.ToString(CourseID), CourseName, Strings.ToString(writeLog));
                    }
                }
            }

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
        /// Exams the delete.
        /// </summary>
        /// <param name="ExamIDs">The exam i ds.</param>
        /// <returns>System.String.</returns>
        public static string Exam_Delete(string ExamIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrExamIDs = Strings.Split(ExamIDs, ",");

                if (arrExamIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrExamIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ELearnExams SET Status='-1', DateDeleted=@DateDeleted WHERE ExamID=@ExamID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ExamID", arrExamIDs[i]);
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

            sReturn = SepFunctions.LangText("Exam(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Exams the get.
        /// </summary>
        /// <param name="ExamID">The exam identifier.</param>
        /// <returns>Models.ElearningExams.</returns>
        public static ElearningExams Exam_Get(long ExamID)
        {
            var returnXML = new Models.ElearningExams();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ELearnExams WHERE ExamID=@ExamID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ExamID", ExamID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ExamID = SepFunctions.toLong(SepFunctions.openNull(RS["ExamID"]));
                            returnXML.CourseID = SepFunctions.toLong(SepFunctions.openNull(RS["CourseID"]));
                            returnXML.ExamName = SepFunctions.openNull(RS["ExamName"]);
                            returnXML.StartDate = SepFunctions.toDate(SepFunctions.openNull(RS["StartDate"]));
                            returnXML.EndDate = SepFunctions.toDate(SepFunctions.openNull(RS["EndDate"]));
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Exams the get HTML.
        /// </summary>
        /// <param name="ExamID">The exam identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.String.</returns>
        public static string Exam_Get_HTML(long ExamID, string UserID)
        {
            var Str = new StringBuilder();
            long aCount = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ELearnExamUsers WHERE ExamID=@ExamID AND UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@ExamID", ExamID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            return SepFunctions.LangText("You have already took this exam.");
                        }

                        using (var cmd2 = new SqlCommand("select UploadID from uploads where UniqueID=@ExamID AND ModuleID='37'", conn))
                        {
                            cmd2.Parameters.AddWithValue("@ExamID", ExamID);
                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                            {
                                if (RS2.HasRows)
                                {
                                    RS2.Read();
                                }
                            }
                        }

                        using (var cmd2 = new SqlCommand("SELECT * FROM ELearnExamQuestions WHERE ExamID=@ExamID ORDER BY QuestionNo,QuestionID", conn))
                        {
                            cmd2.Parameters.AddWithValue("@ExamID", ExamID);
                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                            {
                                if (RS2.HasRows)
                                {
                                    while (RS2.Read())
                                    {
                                        aCount += 1;
                                        Str.Append("<p>");
                                        if (SepFunctions.openNull(RS2["QuestionType"]) == "MS")
                                        {
                                            Str.Append("<label>" + aCount + "). " + SepFunctions.openNull(RS2["QuestionHeader"]) + " ");
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer1"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer1"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer1"]) + " ");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer2"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer2"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer2"]) + " ");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer3"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer3"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer3"]) + " ");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer4"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer4"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer4"]) + " ");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer5"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer5"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer5"]) + " ");
                                            }

                                            Str.Append(" " + SepFunctions.openNull(RS2["QuestionFooter"]) + "</label>");
                                        }
                                        else if (SepFunctions.openNull(RS2["QuestionType"]) == "MC")
                                        {
                                            Str.Append("<label>" + aCount + "). " + SepFunctions.openNull(RS2["QuestionHeader"]) + "</label>");
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer1"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer1"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer1"]) + "<br/>");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer2"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer2"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer2"]) + "<br/>");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer3"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer3"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer3"]) + "<br/>");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer4"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer4"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer4"]) + "<br/>");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer5"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer5"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer5"]) + "<br/>");
                                            }
                                        }
                                        else if (SepFunctions.openNull(RS2["QuestionType"]) == "SP")
                                        {
                                            Str.Append("<label>" + aCount + "). " + SepFunctions.openNull(RS2["QuestionHeader"]) + " (<i>" + SepFunctions.LangText("Type the mispelled word in the text field, if any.") + "</i>)</label>");
                                            Str.Append("<input type=\"text\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" class=\"textEntry\" />");
                                        }
                                        else if (SepFunctions.openNull(RS2["QuestionType"]) == "ABV1")
                                        {
                                            Str.Append("<label>" + aCount + "). " + SepFunctions.openNull(RS2["QuestionHeader"]) + "</label>");
                                            Str.Append("<input type=\"text\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" class=\"textEntry\" />");
                                        }
                                        else if (SepFunctions.openNull(RS2["QuestionType"]) == "ABV2")
                                        {
                                            Str.Append("<label>" + aCount + "). " + SepFunctions.openNull(RS2["QuestionHeader"]) + "</label>");
                                            Str.Append("<input type=\"text\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" class=\"textEntry\" />");
                                        }
                                        else if (SepFunctions.openNull(RS2["QuestionType"]) == "FB")
                                        {
                                            Str.Append("<label>" + aCount + "). " + SepFunctions.openNull(RS2["QuestionHeader"]) + " <input type=\"text\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" class=\"textEntry ignore\" /> " + SepFunctions.openNull(RS2["QuestionFooter"]) + "</td>");
                                        }
                                        else if (SepFunctions.openNull(RS2["QuestionType"]) == "MCB")
                                        {
                                            Str.Append("<label>" + aCount + "). " + SepFunctions.openNull(RS2["QuestionHeader"]) + " _____________ " + SepFunctions.openNull(RS2["QuestionFooter"]) + "</label>");
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer1"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer1"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer1"]) + "<br/>");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer2"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer2"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer2"]) + "<br/>");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer3"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer3"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer3"]) + "<br/>");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer4"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer4"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer4"]) + "<br/>");
                                            }

                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["Answer5"])))
                                            {
                                                Str.Append("<input type=\"radio\" id=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" name=\"" + SepFunctions.openNull(RS2["QuestionID"]) + "\" value=\"" + SepFunctions.openNull(RS2["Answer5"]) + "\" />" + " " + SepFunctions.openNull(RS2["Answer5"]) + "<br/>");
                                            }
                                        }

                                        Str.Append("</p>");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Strings.ToString(Str);
        }

        /// <summary>
        /// Exams the save.
        /// </summary>
        /// <param name="ExamID">The exam identifier.</param>
        /// <param name="CourseID">The course identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ExamName">Name of the exam.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="Description">The description.</param>
        /// <returns>System.Int32.</returns>
        public static int Exam_Save(long ExamID, long CourseID, string UserID, string ExamName, DateTime StartDate, DateTime EndDate, string Description)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ExamID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ExamID FROM ELearnExams WHERE ExamID=@ExamID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamID", ExamID);
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
                    ExamID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ELearnExams SET ExamName=@ExamName, StartDate=@StartDate, EndDate=@EndDate, Description=@Description WHERE ExamID=@ExamID";
                }
                else
                {
                    SqlStr = "INSERT INTO ELearnExams (ExamID, ExamName, StartDate, EndDate, Description, CourseID, Status) VALUES (@ExamID, @ExamName, @StartDate, @EndDate, @Description, @CourseID, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ExamID", ExamID);
                    cmd.Parameters.AddWithValue("@ExamName", ExamName);
                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@CourseID", CourseID);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 37, Strings.ToString(ExamID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Exam", "Exam", string.Empty);

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
        /// Exams the save answers.
        /// </summary>
        /// <param name="ExamID">The exam identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.String.</returns>
        public static string Exam_Save_Answers(long ExamID, string UserID)
        {
            var QuestionID = string.Empty;
            long CorrectAnswer = 0;
            long NotGraded = 0;
            long ExamGrade = 0;
            long CorrectAnswers = 0;
            long WrongAnswers = 0;
            long TotalAnswers = 0;
            long ApproveExam = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ELearnExamUsers WHERE ExamID=@ExamID AND UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@ExamID", ExamID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            return SepFunctions.LangText("You have already took this exam.");
                        }

                        using (var cmd2 = new SqlCommand("SELECT * FROM ELearnExamQuestions WHERE ExamID=@ExamID ORDER BY QuestionNo,QuestionID", conn))
                        {
                            cmd2.Parameters.AddWithValue("@ExamID", ExamID);
                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                            {
                                if (RS2.HasRows)
                                {
                                    while (RS2.Read())
                                    {
                                        QuestionID = SepFunctions.openNull(RS2["QuestionID"]);
                                        TotalAnswers = TotalAnswers + 1;
                                        if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["RightAnswer"])))
                                        {
                                            if (Strings.LCase(Request.Item(SepFunctions.openNull(RS2["QuestionID"]))) == Strings.LCase(SepFunctions.openNull(RS2["RightAnswer"])))
                                            {
                                                CorrectAnswers = CorrectAnswers + 1;
                                                CorrectAnswer = 1;
                                            }
                                            else
                                            {
                                                WrongAnswers = WrongAnswers + 1;
                                                CorrectAnswer = 2;
                                            }
                                        }
                                        else
                                        {
                                            NotGraded = NotGraded + 1;
                                            CorrectAnswer = 3;
                                        }

                                        using (var cmd3 = new SqlCommand("INSERT INTO ELearnExamUsers (EUserID, UserID, ExamID, QuestionID, Answer, CorrectAnswer) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.Session_User_ID() + "', '" + SepFunctions.FixWord(Strings.ToString(ExamID)) + "', '" + QuestionID + "', '" + SepFunctions.FixWord(Request.Item(Strings.ToString(QuestionID))) + "', '" + CorrectAnswer + "')", conn))
                                        {
                                            cmd3.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }

                        ExamGrade = Convert.ToInt64(CorrectAnswers / TotalAnswers * 100);
                        if (NotGraded == 0)
                        {
                            ApproveExam = 1;
                        }
                        else
                        {
                            ApproveExam = 0;
                        }

                        using (var cmd2 = new SqlCommand("INSERT INTO ELearnExamGrades (GradeID, ExamID, UserID, Grade, Approved) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.FixWord(Strings.ToString(ExamID)) + "', '" + SepFunctions.Session_User_ID() + "', '" + ExamGrade + "', " + ApproveExam + ")", conn))
                        {
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Exam has been successfully saved.");
        }

        /// <summary>
        /// Gets the e learning assignments.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="ShowAssignments">The show assignments.</param>
        /// <param name="StudentID">The student identifier.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <returns>List&lt;Models.ElearningAssignments&gt;.</returns>
        public static List<ElearningAssignments> GetELearningAssignments(string SortExpression = "DueDate", string SortDirection = "DESC", string searchWords = "", AssignmentType ShowAssignments = AssignmentType.AllAssignments, long StudentID = 0, long CategoryId = -1)
        {
            var lElearningAssignments = new List<ElearningAssignments>();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DueDate";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            wClause = "EH.CourseID=Mod.CourseID AND EH.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND HWTitle LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            switch (ShowAssignments)
            {
                case AssignmentType.AvailableAssignments:
                    wClause += " AND Mod.CourseID IN (SELECT CourseID FROM ELearnStudents WHERE ELearnStudents.UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "') AND EH.HomeID NOT IN (SELECT HomeID FROM ELearnHomeUsers WHERE ELearnHomeUsers.UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "')";
                    break;

                case AssignmentType.SubmittedAssignments:
                    wClause += " AND Mod.CourseID IN (SELECT CourseID FROM ELearnStudents WHERE ELearnStudents.UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "') AND EH.HomeID IN (SELECT HomeID FROM ELearnHomeUsers WHERE ELearnHomeUsers.UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "')";
                    break;

                default:
                    wClause += string.Empty;
                    break;
            }

            if (StudentID > 0)
            {
                wClause += " AND Mod.CourseID IN (SELECT CourseID FROM ELearnStudents WHERE ELearnStudents.UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "') AND EH.HomeID IN (SELECT HomeID FROM ELearnHomeUsers WHERE ELearnHomeUsers.UserID='" + SepFunctions.FixWord(Strings.ToString(StudentID)) + "')";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT EH.HomeID,EH.HWTitle,EH.Description,(SELECT Grade FROM ELearnHomeUsers WHERE HomeID=EH.HomeID AND UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "') AS Grade,(SELECT HomeNotes FROM ELearnHomeUsers WHERE HomeID=EH.HomeID AND UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "') AS HomeNotes,(SELECT HUserID FROM ELearnHomeUsers WHERE HomeID=EH.HomeID AND UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "') AS SubmitID,(SELECT DatePosted FROM ELearnHomeUsers WHERE HomeID=EH.HomeID AND UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "') AS DateSubmitted,EH.DueDate,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='37' AND UniqueID=EH.HomeID AND isTemp='0' AND Approved='1' AND ControlID='Attachment') AS DownloadLink,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='37' AND UniqueID=EH.HomeID AND isTemp='0' AND Approved='1' AND ControlID='VideoFile') AS PresentationLink FROM ELearnHomework AS EH,ELearnCourses AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dElearningAssignments = new Models.ElearningAssignments { AssignmentID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["HomeID"])) };
                    dElearningAssignments.SubmitID = SepFunctions.openNull(ds.Tables[0].Rows[i]["SubmitID"]);
                    dElearningAssignments.Title = SepFunctions.openNull(ds.Tables[0].Rows[i]["HWTitle"]);
                    dElearningAssignments.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    dElearningAssignments.DueDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DueDate"]));
                    dElearningAssignments.Grade = SepFunctions.openNull(ds.Tables[0].Rows[i]["Grade"]);
                    dElearningAssignments.Notes = SepFunctions.openNull(ds.Tables[0].Rows[i]["HomeNotes"]);
                    dElearningAssignments.DateSubmitted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DateSubmitted"]));
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["DownloadLink"])))
                    {
                        dElearningAssignments.DownloadLink = "<a href=\"" + sInstallFolder + "elearning_download.aspx?UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["DownloadLink"]) + "\" target=\"_blank\">" + SepFunctions.LangText("Download") + "</a>";
                    }

                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["PresentationLink"])))
                    {
                        dElearningAssignments.PresentationLink = "<a href=\"" + sInstallFolder + "elearning_presentation.aspx?UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["PresentationLink"]) + "\" target=\"_blank\">" + SepFunctions.LangText("Open Presentation") + "</a>";
                    }

                    lElearningAssignments.Add(dElearningAssignments);
                }
            }

            return lElearningAssignments;
        }

        /// <summary>
        /// Gets the e learning courses.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="ShowUserCourse">if set to <c>true</c> [show user course].</param>
        /// <param name="showAvailable">if set to <c>true</c> [show available].</param>
        /// <returns>List&lt;Models.ElearningCourses&gt;.</returns>
        public static List<ElearningCourses> GetELearningCourses(string SortExpression = "CourseName", string SortDirection = "ASC", string searchWords = "", long CategoryId = -1, bool ShowUserCourse = false, bool showAvailable = false)
        {
            var lELearningCourses = new List<ElearningCourses>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "CourseName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            wClause = "Mod.Status=1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Mod.CourseName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (ShowUserCourse)
            {
                wClause += " AND Mod.CourseID IN (SELECT CourseID FROM ELearnStudents WHERE ELearnStudents.UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "')";
            }

            if (showAvailable)
            {
                wClause += " AND Mod.StartDate > getDate()";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.CourseID,Mod.CatID,Mod.CourseName,Mod.StartDate,Mod.EndDate,Mod.Status,Mod.Credits,Mod.Instructor FROM ELearnCourses AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dELearningCourses = new Models.ElearningCourses { CourseID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CourseID"])) };
                    dELearningCourses.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dELearningCourses.CourseName = SepFunctions.openNull(ds.Tables[0].Rows[i]["CourseName"]);
                    dELearningCourses.Instructor = SepFunctions.openNull(ds.Tables[0].Rows[i]["Instructor"]);
                    dELearningCourses.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dELearningCourses.StartDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["StartDate"]));
                    dELearningCourses.EndDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["EndDate"]));
                    dELearningCourses.Credits = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Credits"]));
                    lELearningCourses.Add(dELearningCourses);
                }
            }

            return lELearningCourses;
        }

        /// <summary>
        /// Gets the e learning exam questions.
        /// </summary>
        /// <param name="ExamID">The exam identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.ElearningExamQuestions&gt;.</returns>
        public static List<ElearningExamQuestions> GetELearningExamQuestions(long ExamID, string SortExpression = "QuestionNo", string SortDirection = "ASC", string searchWords = "")
        {
            var lElearningExamQuestions = new List<ElearningExamQuestions>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "QuestionNo";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (QuestionHeader LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR QuestionFooter LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT QuestionID,QuestionNo,QuestionHeader,QuestionFooter,QuestionType FROM ELearnExamQuestions WHERE ExamID='" + SepFunctions.FixWord(Strings.ToString(ExamID)) + "'" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dElearningExamQuestions = new Models.ElearningExamQuestions { QuestionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["QuestionID"])) };
                    dElearningExamQuestions.QuestionNo = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["QuestionNo"]));
                    dElearningExamQuestions.QuestionHeader = SepFunctions.openNull(ds.Tables[0].Rows[i]["QuestionHeader"]);
                    dElearningExamQuestions.QuestionFooter = SepFunctions.openNull(ds.Tables[0].Rows[i]["QuestionFooter"]);
                    dElearningExamQuestions.QuestionType = SepFunctions.openNull(ds.Tables[0].Rows[i]["QuestionType"]);
                    lElearningExamQuestions.Add(dElearningExamQuestions);
                }
            }

            return lElearningExamQuestions;
        }

        /// <summary>
        /// Gets the e learning exams.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="ShowUserExams">if set to <c>true</c> [show user exams].</param>
        /// <param name="StudentID">The student identifier.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <returns>List&lt;Models.ElearningExams&gt;.</returns>
        public static List<ElearningExams> GetELearningExams(string SortExpression = "StartDate", string SortDirection = "ASC", string searchWords = "", bool ShowUserExams = false, long StudentID = 0, long CategoryId = -1)
        {
            var lElearningExams = new List<ElearningExams>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "StartDate";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            wClause = "EE.CourseID=Mod.CourseID AND EE.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND ExamName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (ShowUserExams)
            {
                wClause += " AND Mod.CourseID IN (SELECT CourseID FROM ELearnStudents WHERE ELearnStudents.UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "')";
            }

            if (StudentID > 0)
            {
                wClause += " AND Mod.CourseID IN (SELECT CourseID FROM ELearnStudents WHERE ELearnStudents.StudentID='" + SepFunctions.FixWord(Strings.ToString(StudentID)) + "')";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT EE.ExamID,EE.ExamName,EE.StartDate,EE.EndDate FROM ELearnExams AS EE,ELearnCourses AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dElearningExams = new Models.ElearningExams { ExamID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ExamID"])) };
                    dElearningExams.ExamName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ExamName"]);
                    dElearningExams.StartDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["StartDate"]));
                    dElearningExams.EndDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["EndDate"]));
                    lElearningExams.Add(dElearningExams);
                }
            }

            return lElearningExams;
        }

        /// <summary>
        /// Gets the e learning students.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <returns>List&lt;Models.ElearningStudents&gt;.</returns>
        public static List<ElearningStudents> GetELearningStudents(string SortExpression = "Username", string SortDirection = "ASC", string searchWords = "", long CategoryId = -1)
        {
            var lElearningStudents = new List<ElearningStudents>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Username";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            wClause = "E.CourseID=Mod.CourseID AND E.UserID=M.UserID AND E.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (FirstName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR LastName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT E.StudentID,E.DateEnrolled,M.Username,M.FirstName,M.LastName FROM ELearnStudents AS E,Members AS M,ELearnCourses AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dElearningStudents = new Models.ElearningStudents { StudentID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["StudentID"])) };
                    dElearningStudents.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dElearningStudents.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    dElearningStudents.DateEnrolled = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DateEnrolled"]));
                    lElearningStudents.Add(dElearningStudents);
                }
            }

            return lElearningStudents;
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
                        using (var cmd = new SqlCommand("DELETE FROM ELearnExamQuestions WHERE QuestionID=@QuestionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@QuestionID", arrQuestionIDs[i]);
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
        /// <returns>Models.ElearningExamQuestions.</returns>
        public static ElearningExamQuestions Question_Get(long QuestionID)
        {
            var returnXML = new Models.ElearningExamQuestions();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ELearnExamQuestions WHERE QuestionID=@QuestionID", conn))
                {
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.QuestionID = SepFunctions.toLong(SepFunctions.openNull(RS["QuestionID"]));
                            returnXML.ExamID = SepFunctions.toLong(SepFunctions.openNull(RS["ExamID"]));
                            returnXML.QuestionType = SepFunctions.openNull(RS["QuestionType"]);
                            returnXML.QuestionNo = SepFunctions.toLong(SepFunctions.openNull(RS["QuestionNo"]));
                            returnXML.QuestionHeader = SepFunctions.openNull(RS["QuestionHeader"]);
                            returnXML.QuestionFooter = SepFunctions.openNull(RS["QuestionFooter"]);
                            returnXML.Answer1 = SepFunctions.openNull(RS["Answer1"]);
                            returnXML.Answer2 = SepFunctions.openNull(RS["Answer2"]);
                            returnXML.Answer3 = SepFunctions.openNull(RS["Answer3"]);
                            returnXML.Answer4 = SepFunctions.openNull(RS["Answer4"]);
                            returnXML.Answer5 = SepFunctions.openNull(RS["Answer5"]);
                            returnXML.RightAnswer = SepFunctions.openNull(RS["RightAnswer"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Questions the save.
        /// </summary>
        /// <param name="QuestionID">The question identifier.</param>
        /// <param name="ExamID">The exam identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="QuestionNo">The question no.</param>
        /// <param name="QuestionHeader">The question header.</param>
        /// <param name="QuestionFooter">The question footer.</param>
        /// <param name="QuestionType">Type of the question.</param>
        /// <param name="RightAnswer">The right answer.</param>
        /// <param name="Answer1">The answer1.</param>
        /// <param name="Answer2">The answer2.</param>
        /// <param name="Answer3">The answer3.</param>
        /// <param name="Answer4">The answer4.</param>
        /// <param name="Answer5">The answer5.</param>
        /// <returns>System.Int32.</returns>
        public static int Question_Save(long QuestionID, long ExamID, string UserID, long QuestionNo, string QuestionHeader, string QuestionFooter, string QuestionType, string RightAnswer, string Answer1, string Answer2, string Answer3, string Answer4, string Answer5)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (QuestionID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT QuestionID FROM ELearnExamQuestions WHERE QuestionID=@QuestionID", conn))
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
                    SqlStr = "UPDATE ELearnExamQuestions SET QuestionNo=@QuestionNo, QuestionHeader=@QuestionHeader, QuestionFooter=@QuestionFooter, QuestionType=@QuestionType, RightAnswer=@RightAnswer, Answer1=@Answer1, Answer2=@Answer2, Answer3=@Answer3, Answer4=@Answer4, Answer5=@Answer5 WHERE QuestionID=@QuestionID";
                }
                else
                {
                    SqlStr = "INSERT INTO ELearnExamQuestions (QuestionID, ExamID, QuestionNo, QuestionHeader, QuestionFooter, QuestionType, RightAnswer, Answer1, Answer2, Answer3, Answer4, Answer5) VALUES (@QuestionID, @ExamID, @QuestionNo, @QuestionHeader, @QuestionFooter, @QuestionType, @RightAnswer, @Answer1, @Answer2, @Answer3, @Answer4, @Answer5)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                    cmd.Parameters.AddWithValue("@ExamID", ExamID);
                    cmd.Parameters.AddWithValue("@QuestionNo", QuestionNo);
                    cmd.Parameters.AddWithValue("@QuestionHeader", QuestionHeader);
                    cmd.Parameters.AddWithValue("@QuestionFooter", QuestionFooter);
                    cmd.Parameters.AddWithValue("@QuestionType", QuestionType);
                    cmd.Parameters.AddWithValue("@RightAnswer", RightAnswer);
                    cmd.Parameters.AddWithValue("@Answer1", Answer1);
                    cmd.Parameters.AddWithValue("@Answer2", Answer2);
                    cmd.Parameters.AddWithValue("@Answer3", Answer3);
                    cmd.Parameters.AddWithValue("@Answer4", Answer4);
                    cmd.Parameters.AddWithValue("@Answer5", Answer5);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 37, Strings.ToString(QuestionID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "ExamQuestion", "Exam Question", string.Empty);

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
        /// Students the delete.
        /// </summary>
        /// <param name="StudentIDs">The student i ds.</param>
        /// <returns>System.String.</returns>
        public static string Student_Delete(string StudentIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrStudentIDs = Strings.Split(StudentIDs, ",");

                if (arrStudentIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrStudentIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ELearnStudents SET Status='-1', DateDeleted=@DateDeleted WHERE StudentID=@StudentID", conn))
                        {
                            cmd.Parameters.AddWithValue("@StudentID", arrStudentIDs[i]);
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

            sReturn = SepFunctions.LangText("Student(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Students the get.
        /// </summary>
        /// <param name="StudentID">The student identifier.</param>
        /// <returns>Models.ElearningStudents.</returns>
        public static ElearningStudents Student_Get(long StudentID)
        {
            var returnXML = new Models.ElearningStudents();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ELearnStudents WHERE StudentID=@StudentID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", StudentID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.StudentID = SepFunctions.toLong(SepFunctions.openNull(RS["StudentID"]));
                            returnXML.CourseID = SepFunctions.toLong(SepFunctions.openNull(RS["CourseID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.UserName = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.FirstName = SepFunctions.GetUserInformation("FirstName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.LastName = SepFunctions.GetUserInformation("LastName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.Active = SepFunctions.openBoolean(RS["Active"]);
                            returnXML.DateEnrolled = SepFunctions.toDate(SepFunctions.openNull(RS["DateEnrolled"]));
                            returnXML.UserNotes = SepFunctions.openNull(RS["UserNotes"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Students the save.
        /// </summary>
        /// <param name="StudentID">The student identifier.</param>
        /// <param name="CourseID">The course identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Active">if set to <c>true</c> [active].</param>
        /// <param name="DateEnrolled">The date enrolled.</param>
        /// <param name="UserNotes">The user notes.</param>
        /// <returns>System.Int32.</returns>
        public static int Student_Save(long StudentID, long CourseID, string UserID, bool Active, DateTime DateEnrolled, string UserNotes)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (StudentID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT StudentID FROM ELearnStudents WHERE UserID=@UserID AND CourseID=@CourseID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
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
                    StudentID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ELearnStudents SET Active=@Active, DateEnrolled=@DateEnrolled, UserNotes=@UserNotes WHERE StudentID=@StudentID";
                }
                else
                {
                    SqlStr = "INSERT INTO ELearnStudents (StudentID, CourseID, UserID, Active, DateEnrolled, UserNotes, Status) VALUES (@StudentID, @CourseID, @UserID, @Active, @DateEnrolled, @UserNotes, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", StudentID);
                    cmd.Parameters.AddWithValue("@CourseID", CourseID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Active", Active);
                    cmd.Parameters.AddWithValue("@DateEnrolled", DateEnrolled);
                    cmd.Parameters.AddWithValue("@UserNotes", UserNotes);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 37, Strings.ToString(StudentID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Student", "Student", string.Empty);

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
    }
}