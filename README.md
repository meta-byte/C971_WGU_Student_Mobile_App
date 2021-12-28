# WGU_Student_Mobile_App


## Overview
This Xamarin Forms application is designed to enable students of Western Governors University to build a framework for their education by enabling them to create terms, courses and assessments and associate them with one another for easy organization. The application will notify users of upcoming start dates for these terms, courses and assessments. It also provides a location for saving quick notes for a given course and saving instructor contact information. Additionally students can generate a report that contains a hierarchical overview of their education plan. The core logic of this application was developed using C# for the backend and Xamarin Forms (Xaml) for the front end. A SQLite database was used to save items for students. Saved items are login specific meaning multiple users can safely use the application. 


## Tech and Features Used
| Feature       | Summary                                                                                                  | 
| ------------- | -------------------------------------------------------------------------------------------------------- |
| C# | The core logic was created using the C# programming language.
| Xamarin Forms | Xamarin Forms was used for the front end and all visual elements.                               |
| .NET Standard 2.0 | The class library the Xamarin Forms application was built on. |
| Visual Studio Testing Tools | An imported package used with an adapter for unit testing .NET Standard 2.0 |
SQLite| The application uses a SQLite database. |


## Requirements
* Windows PC operating on Windows 10
* Visual Studio 2019 or greater.
* Android mobile device operating on Android 11 or greater with USB debugging enabled. (Optional but recommended. Visual Studio has a built in Android device emulator.)


## Setup
* Clone or Download the source code.
* If downloaded, extract the .zip file into your downloads folder or desktop.
* Navigate into the extracted project folder and open the WGU_Student_Mobile_App.sln file.
* In Visual Studio, Click "Build" in the navigation menu at the top of the screen.
* Click "Build Solution". 
    * You may encounter an error stating `Assembly 'Xamarin.Android.Support.v4' is using '[assembly: Android.IncludeAndroidResourcesFromAttribute]', which is no longer supported. Use a newer version of this NuGet package or notify the library author.`
    * If this error is present please click "Build Solution" again. You may need to do this more than once. This is an intermittent error caused by dependencies that are no longer maintained.
* Once you see "Build Succeeded" at the bottom of the window you may begin debugging.
* To debug plug in your USB Debugging enabled Android Device or select an Android device to emulate. If no devices are present select the drop down next to the green play button and select "Android Device Manager" then create a mobile device to emulate.
* Press the green play button with your device selected to begin debugging.


## How to use
* The application comes pre-loaded with dummy data. To use the pre-loaded data enter "test" as the username and "test" as the password and click login. If you wish to use your own credentials select "Register". A dummy account by the name of "test2" exists that does not contain any pre-loaded data. Use this if you do not wish to register.
    * Register your own email, username and password. *Note all passwords and emails are stored in plain text. For security purposes please do not use your personal email account or passwords. Your email address is only used to ensure uniqueness of the user account*
    * Once you have registered you may login with your chosen username and password.
* Tapping the hamburger icon in the top left corner of the application will allow you to navigate to the various sections of the application. 
* To add an item, select "Add" from the context menu at the top of the screen. Fill out the required form and tap "Save". To abort select "Cancel".
* To view details for a given item, tap the item you wish to view and then select "Details" from the context menu.
* To edit a given item, tap the item you wish to edit and then tap "Edit" on the context menu. Press "Save" to save your changes.
* To delete an item, select the item you wish to remove and then select "Delete" on the context menu. Confirm the deletion.
* To refresh the view, pull down.
* Course searching is available. To search for a course enter a title or substring present in a course title and select "Search"
* To generate a report, navigate to Reports via the navigation menu. Select the report type and press "Generate". Open the .txt file in your desired application. Android's built in HTML Viewer is recommended if available.
* To logout, close the application. Your items will be saved for use the next time you login.


## Known Bugs
* On build you may see a "Xamarin.Android.Support.v4" error. This occurs intermittently due to one of the NuGet packages used having dependencies that are no longer maintained. Rebuilding the solution should clear the error.

