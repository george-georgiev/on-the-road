# On The Road 
###### by Team We Want Angular

[![Build status](https://ci.appveyor.com/api/projects/status/wgdu1qmvutix6gu3?svg=true)](https://ci.appveyor.com/project/GalinStoychev/on-the-road)
[![Coverage Status](https://coveralls.io/repos/github/WeWantAngular/on-the-road/badge.svg?branch=master)](https://coveralls.io/github/WeWantAngular/on-the-road?branch=master)

# Web Forms course teamwork project 

## Team Members
* George Georgiev
* Galin Stoychev

## App Description
On The Road is a place where people can find likeminded people ready to dive into new adventures. Here, you can publish your plans about some trips(hiking in the mountain, going to the beach, visiting some cultural place, etc.) and find other who would like to join your idea. Or you can be the one to join others ideas and have wonderful time together.

## Project Description
The application have:
* public part (accessible without authentication)
* private part (available for registered users)
* administrative part (available for administrators only)

## General Requirements
Your Web application should use the following technologies, frameworks and development techniques:
* Use **ASP.NET Web Forms** and **Visual Studio 2015**
* Your UI should use **server-side Web Forms** UI rendering (ASPX pages and ASCX controls)
	* ASP.NET MVC and JavaScript UI controls are **not** allowed!
* Use **MS SQL Server** as database back-end
	* Use Entity Framework to access your database
* Use **data-binding** technique by choice
	* You are free to use data-source controls (like `EntityDataSource` and `ObjectDataSource`), model binding or manual binding in the C# code behind pages.
* Use at least **four data grids** (table-like data UI components) with **server-side paging and sorting**
* Create **beautiful and responsive UI**
	* You may use **Bootstrap** or **Materialize**
	* You may change the standard theme and modify it to apply own web design and visual styles
* Use a **Master page** to define the common UI for the public, private and administrative parts
* Use **Sitemap** and navigational UI controls to implement site navigation
* Use the standard **ASP.NET Identity System** for managing **users** and **roles**
	* Your registered users should have are least two roles: **user** and **administrator**
* Use the standard ASP.NET Web Forms controls (from `System.Web.UI`)
	* External UI controls from Telerik / Infragistics / DevExpress / etc. are **not** allowed!
* Use `UpdatePanel`s and **AJAX** where applicable to avoid full postbacks
* Use at least **three ASCX user controls** that encapsulate some functionality
* Use at least one **file upload** form to send files at the server side (e.g. profile photos for your users)
* Use **caching** of data where it makes sense (e.g. starting page)
* Apply **error handling** and data validation to avoid crashes when invalid data is entered
* Prevent yourself from **security** holes (XSS, XSRF, Parameter Tampering, etc.)
	* Handle correctly the **special HTML characters** and tags like `<script>`, `<br />`, etc.
* Create **unit tests** for your "business" functionality following the best practices for writing unit tests (**at least 70% code coverage**) - **~30% of the points for the project**
* Use **MVP pattern** in collaboration with the **Dependency Inversion** principle and **Dependency Injection** technique - **~20% of the points for the project**
