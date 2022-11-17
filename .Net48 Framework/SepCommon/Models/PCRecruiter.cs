// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="PCRecruiter.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Class PCRecruiterJobResults.
    /// </summary>
    public class PCRecruiterJobResults
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public List<PCRecruiterJobs> Results { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterCandidateResults.
    /// </summary>
    public class PCRecruiterCandidateResults
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public List<PCRecruiterCandidateFields> Results { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterTotalRecords.
    /// </summary>
    public class PCRecruiterTotalRecords
    {
        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        /// <value>The total records.</value>
        public int TotalRecords { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterCompanyResults.
    /// </summary>
    public class PCRecruiterCompanyResults
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public List<PCRecruiterCompanies> Results { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterCandidates.
    /// </summary>
    public class PCRecruiterCandidates
    {
        /// <summary>
        /// Gets or sets the candidate.
        /// </summary>
        /// <value>The candidate.</value>
        public List<PCRecruiterCandidateFields> Candidate { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterCandidateFields.
    /// </summary>
    public class PCRecruiterCandidateFields
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>The address2.</value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the available.
        /// </summary>
        /// <value>The available.</value>
        public DateTime? Available { get; set; }

        /// <summary>
        /// Gets or sets the bill rate.
        /// </summary>
        /// <value>The bill rate.</value>
        public PCRSalaryField BillRate { get; set; }

        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>The candidate identifier.</value>
        public string CandidateId { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the company user.
        /// </summary>
        /// <value>The name of the company user.</value>
        public string CompanyUserName { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        /// <value>The county.</value>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the current salary.
        /// </summary>
        /// <value>The current salary.</value>
        public PCRSalaryField CurrentSalary { get; set; }

        /// <summary>
        /// Gets or sets the date entered.
        /// </summary>
        /// <value>The date entered.</value>
        public DateTime? DateEntered { get; set; }

        /// <summary>
        /// Gets or sets the default currency.
        /// </summary>
        /// <value>The default currency.</value>
        public string DefaultCurrency { get; set; }

        /// <summary>
        /// Gets or sets the type of the degree.
        /// </summary>
        /// <value>The type of the degree.</value>
        public string DegreeType { get; set; }

        /// <summary>
        /// Gets or sets the desired salary.
        /// </summary>
        /// <value>The desired salary.</value>
        public PCRSalaryField DesiredSalary { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the fax phone.
        /// </summary>
        /// <value>The fax phone.</value>
        public string FaxPhone { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the grad year.
        /// </summary>
        /// <value>The grad year.</value>
        public string GradYear { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has resume.
        /// </summary>
        /// <value><c>true</c> if this instance has resume; otherwise, <c>false</c>.</value>
        public bool HasResume { get; set; }

        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        /// <value>The home phone.</value>
        public string HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>The identification.</value>
        public string Identification { get; set; }

        /// <summary>
        /// Gets or sets the industry.
        /// </summary>
        /// <value>The industry.</value>
        public string Industry { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>The keywords.</value>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the last activity.
        /// </summary>
        /// <value>The last activity.</value>
        public DateTime? LastActivity { get; set; }

        /// <summary>
        /// Gets or sets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle initial.
        /// </summary>
        /// <value>The middle initial.</value>
        public string MiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone.
        /// </summary>
        /// <value>The mobile phone.</value>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Gets or sets the note flag.
        /// </summary>
        /// <value>The note flag.</value>
        public string NoteFlag { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the opt out.
        /// </summary>
        /// <value>The opt out.</value>
        public string OptOut { get; set; }

        /// <summary>
        /// Gets or sets the pager.
        /// </summary>
        /// <value>The pager.</value>
        public string Pager { get; set; }

        /// <summary>
        /// Gets or sets the pay rate.
        /// </summary>
        /// <value>The pay rate.</value>
        public PCRSalaryField PayRate { get; set; }

        /// <summary>
        /// Gets or sets the photograph.
        /// </summary>
        /// <value>The photograph.</value>
        public string Photograph { get; set; }

        /// <summary>
        /// Gets or sets the photograph thumb.
        /// </summary>
        /// <value>The photograph thumb.</value>
        public string PhotographThumb { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the postal code extension.
        /// </summary>
        /// <value>The postal code extension.</value>
        public string PostalCodeExtension { get; set; }

        /// <summary>
        /// Gets or sets the profile line1.
        /// </summary>
        /// <value>The profile line1.</value>
        public string ProfileLine1 { get; set; }

        /// <summary>
        /// Gets or sets the profile line2.
        /// </summary>
        /// <value>The profile line2.</value>
        public string ProfileLine2 { get; set; }

        /// <summary>
        /// Gets or sets the profile line3.
        /// </summary>
        /// <value>The profile line3.</value>
        public string ProfileLine3 { get; set; }

        /// <summary>
        /// Gets or sets the profile line4.
        /// </summary>
        /// <value>The profile line4.</value>
        public string ProfileLine4 { get; set; }

        /// <summary>
        /// Gets or sets the profile line5.
        /// </summary>
        /// <value>The profile line5.</value>
        public string ProfileLine5 { get; set; }

        /// <summary>
        /// Gets or sets the profile line6.
        /// </summary>
        /// <value>The profile line6.</value>
        public string ProfileLine6 { get; set; }

        /// <summary>
        /// Gets or sets the profile line7.
        /// </summary>
        /// <value>The profile line7.</value>
        public string ProfileLine7 { get; set; }

        /// <summary>
        /// Gets or sets the profile line8.
        /// </summary>
        /// <value>The profile line8.</value>
        public string ProfileLine8 { get; set; }

        /// <summary>
        /// Gets or sets the profile line9.
        /// </summary>
        /// <value>The profile line9.</value>
        public string ProfileLine9 { get; set; }

        /// <summary>
        /// Gets or sets the relocate.
        /// </summary>
        /// <value>The relocate.</value>
        public string Relocate { get; set; }

        /// <summary>
        /// Gets or sets the resume text.
        /// </summary>
        /// <value>The resume text.</value>
        public string ResumeText { get; set; }

        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>The salutation.</value>
        public string Salutation { get; set; }

        /// <summary>
        /// Gets or sets the school.
        /// </summary>
        /// <value>The school.</value>
        public string School { get; set; }

        /// <summary>
        /// Gets or sets the show on web rollup.
        /// </summary>
        /// <value>The show on web rollup.</value>
        public string ShowOnWebRollup { get; set; }

        /// <summary>
        /// Gets or sets the specialty.
        /// </summary>
        /// <value>The specialty.</value>
        public string Specialty { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the subjective.
        /// </summary>
        /// <value>The subjective.</value>
        public string Subjective { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the work phone.
        /// </summary>
        /// <value>The work phone.</value>
        public string WorkPhone { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterCandidateResume.
    /// </summary>
    public class PCRecruiterCandidateResume
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the resume.
        /// </summary>
        /// <value>The resume.</value>
        public string Resume { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterInterviewFields.
    /// </summary>
    public class PCRecruiterInterviewFields
    {
        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>The candidate identifier.</value>
        public string CandidateId { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the type of the current interview.
        /// </summary>
        /// <value>The type of the current interview.</value>
        public string CurrentInterviewType { get; set; }

        /// <summary>
        /// Gets or sets the current status.
        /// </summary>
        /// <value>The current status.</value>
        public string CurrentStatus { get; set; }

        /// <summary>
        /// Gets or sets the interview status.
        /// </summary>
        /// <value>The interview status.</value>
        public string InterviewStatus { get; set; }

        /// <summary>
        /// Gets or sets the type of the interview.
        /// </summary>
        /// <value>The type of the interview.</value>
        public string InterviewType { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>The job identifier.</value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the written by.
        /// </summary>
        /// <value>The written by.</value>
        public string WrittenBy { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterCompanies.
    /// </summary>
    public class PCRecruiterCompanies
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>The address2.</value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the annual revenue.
        /// </summary>
        /// <value>The annual revenue.</value>
        public PCRSalaryField AnnualRevenue { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the type of the company.
        /// </summary>
        /// <value>The type of the company.</value>
        public string CompanyType { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        /// <value>The county.</value>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the default currency.
        /// </summary>
        /// <value>The default currency.</value>
        public string DefaultCurrency { get; set; }

        /// <summary>
        /// Gets or sets the division of.
        /// </summary>
        /// <value>The division of.</value>
        public string DivisionOf { get; set; }

        /// <summary>
        /// Gets or sets the email WWW address.
        /// </summary>
        /// <value>The email WWW address.</value>
        public string EmailWWWAddress { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>The fax.</value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the industry.
        /// </summary>
        /// <value>The industry.</value>
        public string Industry { get; set; }

        /// <summary>
        /// Gets or sets the industry code.
        /// </summary>
        /// <value>The industry code.</value>
        public string IndustryCode { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>The keywords.</value>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the last activity.
        /// </summary>
        /// <value>The last activity.</value>
        public string LastActivity { get; set; }

        /// <summary>
        /// Gets or sets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        public string LastModified { get; set; }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        /// <value>The logo.</value>
        public string Logo { get; set; }

        /// <summary>
        /// Gets or sets the note flag.
        /// </summary>
        /// <value>The note flag.</value>
        public string NoteFlag { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the number of employees.
        /// </summary>
        /// <value>The number of employees.</value>
        public string NumberOfEmployees { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the postal code extension.
        /// </summary>
        /// <value>The postal code extension.</value>
        public string PostalCodeExtension { get; set; }

        /// <summary>
        /// Gets or sets the specialty.
        /// </summary>
        /// <value>The specialty.</value>
        public string Specialty { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the subjective.
        /// </summary>
        /// <value>The subjective.</value>
        public string Subjective { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }

    /// <summary>
    /// Class PCRecruiterJobs.
    /// </summary>
    public class PCRecruiterJobs
    {
        /// <summary>
        /// Gets or sets the begin date.
        /// </summary>
        /// <value>The begin date.</value>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// Gets or sets the benefits.
        /// </summary>
        /// <value>The benefits.</value>
        public string Benefits { get; set; }

        /// <summary>
        /// Gets or sets the bill rate.
        /// </summary>
        /// <value>The bill rate.</value>
        public PCRSalaryField BillRate { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        /// <value>The contact email.</value>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        /// <value>The name of the contact.</value>
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the contact phone.
        /// </summary>
        /// <value>The contact phone.</value>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        /// <value>The county.</value>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the custom fields.
        /// </summary>
        /// <value>The custom fields.</value>
        public Collection<PCRPositionCustom> CustomFields { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime? DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the default currency.
        /// </summary>
        /// <value>The default currency.</value>
        public string DefaultCurrency { get; set; }

        /// <summary>
        /// Gets or sets the degree required.
        /// </summary>
        /// <value>The degree required.</value>
        public string DegreeRequired { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>The department.</value>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the direct apply link.
        /// </summary>
        /// <value>The direct apply link.</value>
        public string DirectApplyLink { get; set; }

        /// <summary>
        /// Gets or sets the direct job link.
        /// </summary>
        /// <value>The direct job link.</value>
        public string DirectJobLink { get; set; }

        /// <summary>
        /// Gets or sets the education aid.
        /// </summary>
        /// <value>The education aid.</value>
        public string EducationAid { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the end hold.
        /// </summary>
        /// <value>The end hold.</value>
        public string EndHold { get; set; }

        /// <summary>
        /// Gets or sets the external expiration.
        /// </summary>
        /// <value>The external expiration.</value>
        public string ExternalExpiration { get; set; }

        /// <summary>
        /// Gets or sets the fee percentage.
        /// </summary>
        /// <value>The fee percentage.</value>
        public string FeePercentage { get; set; }

        /// <summary>
        /// Gets or sets the guarantee.
        /// </summary>
        /// <value>The guarantee.</value>
        public string Guarantee { get; set; }

        /// <summary>
        /// Gets or sets the health coverage.
        /// </summary>
        /// <value>The health coverage.</value>
        public string HealthCoverage { get; set; }

        /// <summary>
        /// Gets or sets the industry.
        /// </summary>
        /// <value>The industry.</value>
        public string Industry { get; set; }

        /// <summary>
        /// Gets or sets the internal expiration.
        /// </summary>
        /// <value>The internal expiration.</value>
        public string InternalExpiration { get; set; }

        /// <summary>
        /// Gets or sets the internal job description.
        /// </summary>
        /// <value>The internal job description.</value>
        public string InternalJobDescription { get; set; }

        /// <summary>
        /// Gets or sets the job description.
        /// </summary>
        /// <value>The job description.</value>
        public string JobDescription { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>The job identifier.</value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the job title.
        /// </summary>
        /// <value>The job title.</value>
        public string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the job.
        /// </summary>
        /// <value>The type of the job.</value>
        public string JobType { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>The keywords.</value>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the last activity.
        /// </summary>
        /// <value>The last activity.</value>
        public string LastActivity { get; set; }

        /// <summary>
        /// Gets or sets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        public string LastModified { get; set; }

        /// <summary>
        /// Gets or sets the maximum salary.
        /// </summary>
        /// <value>The maximum salary.</value>
        public PCRSalaryField MaxSalary { get; set; }

        /// <summary>
        /// Gets or sets the maximum years exp.
        /// </summary>
        /// <value>The maximum years exp.</value>
        public string MaxYearsExp { get; set; }

        /// <summary>
        /// Gets or sets the minimum salary.
        /// </summary>
        /// <value>The minimum salary.</value>
        public PCRSalaryField MinSalary { get; set; }

        /// <summary>
        /// Gets or sets the minimum years exp.
        /// </summary>
        /// <value>The minimum years exp.</value>
        public string MinYearsExp { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the number of openings.
        /// </summary>
        /// <value>The number of openings.</value>
        public string NumberOfOpenings { get; set; }

        /// <summary>
        /// Gets or sets the pay rate.
        /// </summary>
        /// <value>The pay rate.</value>
        public PCRSalaryField PayRate { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>The position identifier.</value>
        public string PositionId { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the postal code extension.
        /// </summary>
        /// <value>The postal code extension.</value>
        public string PostalCodeExtension { get; set; }

        /// <summary>
        /// Gets or sets the show on web.
        /// </summary>
        /// <value>The show on web.</value>
        public string ShowOnWeb { get; set; }

        /// <summary>
        /// Gets or sets the specialty.
        /// </summary>
        /// <value>The specialty.</value>
        public string Specialty { get; set; }

        /// <summary>
        /// Gets or sets the start hold.
        /// </summary>
        /// <value>The start hold.</value>
        public string StartHold { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the vacation.
        /// </summary>
        /// <value>The vacation.</value>
        public string Vacation { get; set; }

        /// <summary>
        /// Gets or sets the web notes.
        /// </summary>
        /// <value>The web notes.</value>
        public string WebNotes { get; set; }

        /// <summary>
        /// Gets or sets the why open.
        /// </summary>
        /// <value>The why open.</value>
        public string WhyOpen { get; set; }
    }

    /// <summary>
    /// Class PCRSalaryField.
    /// </summary>
    public class PCRSalaryField
    {
        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>The currency code.</value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
    }

    /// <summary>
    /// Class PCRCompanyCustom.
    /// </summary>
    public class PCRCompanyCustom
    {
        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        /// <value>The type of the field.</value>
        public string FieldType { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public string[] Values { get; set; }
    }

    /// <summary>
    /// Class PCRPositionCustom.
    /// </summary>
    public class PCRPositionCustom
    {
        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        /// <value>The type of the field.</value>
        public string FieldType { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public string[] Values { get; set; }
    }
}