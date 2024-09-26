# CaseRevisionManagementSystem
Case Revision Management System

Lawyer/Client can login to the system -> See Calender on dashboard - https://razor.radzen.com/scheduler
Lawyer can add new case by just clicking on date from Calender
Lawyer has menu as below
->Cases
->->Create, Edit, Display List
->->Upload documents will be stored in local system for now(in project format should be UploadDoc/CaseNo/)
-> Image and pdf open thay and biji koi type ni file hoy to download thay

->Clients
->->Create, Edit, Display List
->Collaborative Lawyers
->->Create, Edit, Display List
->Collaborative Case
->->Create, Edit, Display List
->Logout

Client has menu as below (Client only upload documents from their side)
->Cases
->Collaborative Lawyers
->Logout

===Table -> CaseDocument (DocumentId(Pk),CaseId(FK),DocumentUrl,FIle Name, BaseEntity) - done
Confonet kari ne site chhe & mobile app pan chhe ene inherit kari ne aakhu software banavanu chhe

-- Case start thay tyare advocate 1 chhe e case per pan client want to change lawyer so e in that case we I'll have details of last lawyer details 

Pdf viewer
Image and pdf open thay and biji koi type ni file hoy to download thay


//style="width:750px;height:750px;"
            //<RadzenButton Text="Cancel" Click="() => ds.Close(true)" ButtonStyle="ButtonStyle.Info" Style="margin-bottom: 10px;"/>


    //IList<AppointmentFM> appointments = new List<AppointmentFM>
    //{
    //    new AppointmentFM { Start = DateTime.Today.AddDays(-2), End = DateTime.Today.AddDays(-2), Text = "Birthday" },
    //    new AppointmentFM { Start = DateTime.Today.AddDays(-11), End = DateTime.Today.AddDays(-10), Text = "Day off" },
    //    new AppointmentFM { Start = DateTime.Today.AddDays(-10), End = DateTime.Today.AddDays(-8), Text = "Work from home" },
    //    new AppointmentFM { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(12), Text = "Online meeting" },
    //    new AppointmentFM { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(13), Text = "Skype call" },
    //    new AppointmentFM { Start = DateTime.Today.AddHours(14), End = DateTime.Today.AddHours(14).AddMinutes(30), Text = "Dentist appointment" },
    //    new AppointmentFM { Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(12), Text = "Vacation" },
    //};
	
CASE ADD:
CASE-1 ---> 17/2/2024 ---> CC/1221/2024 (FIRST CASE)  - [CASEPARENTID-OPTIONAL]

CASE-2 ADD:
CASE-2 ---> 22/2/2024 ---> CC/151/2024 (SECOND CASE)  - [CASEPARENTID-OPTIONAL]

CASE-3 ADD:


confonet ni website ke api hoy to case wise details levi hov to made khari e pan jara jojo ni


19-01-2024
===============

1. NEED TO ADD DROPDOWNS FOR SELECTING STATE,CITY ETC..
2. NEED TO CHECK CASE EDIT MODULE FROM DASHBOARD CALENDAR DATE SELECTION
3. MANAGE DASHBOARD CASE ADD/EDIT FORM UI

19-01-2024
==============

- ADD SEARCHABLE DROPDOWN FOR CLIENT AND CASENUMBER - DONE

- DISPLAY ADDED CASE FOR WEEK AND DAY - RESOLVED

- SEARCHABLE DROPDOWN NOT SHOWING VALUE IN EDIT FORM - WORKING

- NEED TO ADD DROPDOWNS FOR SELECTING STATE,CITY ETC..  

- MANAGE DASHBOARD CASE ADD/EDIT FORM UI

- NEED TO ADD DROPDOWN FOR COURT LOCATION 