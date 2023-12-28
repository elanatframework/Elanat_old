![logo](https://github.com/elanatframework/Elanat/assets/111444759/fb48c66f-2c43-43bd-b877-a17e0022a781)
# Elanat CMS Old

This archived repository is the first version of Elanat based on ASP.NET Standard; To access the repository of the second version of Elanat based on ASP.NET Core, refer to the link below.

https://github.com/elanatframework/Elanat

We maintain this repository to make a comparison between Elanat in the ASP.NET Standard version and the ASP.NET Core version. We will clearly tell the story of Elanat's migration from ASP .NET Standard version to ASP.NET Core version in this README.md file.

## The difference between Model and View and Controller 
Email page

View in ASP.NET Standard
[https://github.com/elanatframework/Elanat_old/blob/elanat_framework/page/email/Default.aspx](https://github.com/elanatframework/Elanat_old/blob/elanat_framework/page/email/Default.aspx)

View in ASP.NET Core
[https://github.com/elanatframework/Elanat/blob/elanat_framework/wwwroot/page/email/Default.aspx](https://github.com/elanatframework/Elanat/blob/elanat_framework/wwwroot/page/email/Default.aspx)

Model in ASP.NET Standard
[https://github.com/elanatframework/Elanat_old/blob/elanat_framework/page/email/Default.aspx](https://github.com/elanatframework/Elanat_old/blob/elanat_framework/page/email/SiteEmailModel.cs)

Model in ASP.NET Core
[https://github.com/elanatframework/Elanat/blob/elanat_framework/class/controller_and_model/page/email/SiteEmailModel.cs](https://github.com/elanatframework/Elanat/blob/elanat_framework/class/controller_and_model/page/email/SiteEmailModel.cs)

Controller in ASP.NET Standard
[https://github.com/elanatframework/Elanat_old/blob/elanat_framework/page/email/Default.aspx.cs](https://github.com/elanatframework/Elanat_old/blob/elanat_framework/page/email/Default.aspx.cs)

Controller in ASP.NET Core
[https://github.com/elanatframework/Elanat/blob/elanat_framework/class/controller_and_model/page/email/SiteEmailController.cs](https://github.com/elanatframework/Elanat/blob/elanat_framework/class/controller_and_model/page/email/SiteEmailController.cs)

As you can see, the View page has remained unchanged and only the first line has changed.


Elanat 1.0.8.4
--------------

Elanat is Add-on oriented framework on the web.

Elanat Copyright (C) 2022-2023 Mohammad Rabie.

The main features of Elanat: 

  - Supports all programming languages
  - Include eight various add-on :
     Component, Module, Patch, Plugin, Page, Fetch, Extra helper, Editor template
  - Inner MVC structer
  - Dynamic add-ons

Elanat is free software under the GNU GPLV3.
Read App_Data/elanat_system_data/license/en/license.txt for more information about license.

Website :
  elanat.net

Tools used in creating Elanat:

	- ASP.NET Standard 4.5
	- SQLServer
	- Tinymce
	- Codemirror
	- Ionic.Zip.dll
	- MySqlConnector.dll
	- File Icons Vs. 3 Icons by Jordan Michael

--------------
