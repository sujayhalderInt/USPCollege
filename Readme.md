# Umbraco Base Project Developers Notes

## Introduction

This is a website based on UmbracoCMS 7.11.1

### Umbraco 

1. Clone this repository to a local folder of your choice (Your Location). 
https://stash.precedent.com/projects/UVS

2. Create a webroot folder for the precedentbase project e.g. C:\inetpub\wwwroot\UmbracoBase
   and manually create the following folders underneath: 
   e.g.
   C:\inetpub\wwwroot\UmbracoBase\Data
   C:\inetpub\wwwroot\UmbracoBase\Website
   
3. Switch to the "develop" branch and publish to the C:\inetpub\wwwroot\UmbracoBase\Website

4. Set up a new website in IIS using the following host name: precedent.umbraco.base

5. Test the IIS site and log into Umbraco CMS using the username and password you set for your own user, usually your email.

6. Take a copy of the the Web.Debug.config.disabled, rename Web.Debug.config and update it to  with a connection string to your local copy of the IPA SQLServer database.

7. Run the Git update assume unchanged command to avoid checking in your local debug connectionstring
   e.g. $ git update-index --assume-unchanged 'C:\Projects\precedent.umbraco.base\src\Umbraco.Base.Website\Web.Debug.config'
   
8. Make a link to the project USync Folder from the IIS Usync site folder: from the command prompt run the following command:
   mklink /J C:\(Your Webroot Location)\Data\uSync C:\(Your Project Location)\precedent.umbraco.base\Data\uSync (changing the path accordingly)
   e.g. mklink /J C:\inetpub\wwwroot\UmbracoBase\Data\uSync C:\Projects\precedent.umbraco.base\Data\uSync

9. Make a link to the Website media Folder, so all the developers gets the imagesfrom the repository:
	mklink /J C:\(Your Webroot Location)\data\Media C:\(Your Project Location)\precedent.umbraco.base\Data\Media (changing the path accordingly)
	e.g. mklink /J C:\inetpub\wwwroot\UmbracoBase\Data\Media C:\Projects\precedent.umbraco.base\Data\Media

10. Take a copy of the the uSyncBackOffice.Debug.Config.disabled, rename uSyncBackOffice.Debug.Config and update it to  with you correct path  for the usync data folder
   (e.g. C:\inetpub\wwwroot\UmbracoBase\data\uSync\data\)
   
11. Run the Git update assume unchanged command to avoid checking in your local uSyncBackOffice.Debug.Config
   e.g. $ git update-index --assume-unchanged 'C:\Projects\precedent.umbraco.base\src\Umbraco.Base.Website\config\uSyncBackOffice.Debug.Config'

12. In IIS Create the following 2 virtual directories:
	- Media -> To point to C:\inetpub\wwwroot\IPA\data\Media
	- uSync -> To point to C:\inetpub\wwwroot\IPA\data\uSync
	
13. Launch the IIS site, USync should automatically syncronise the database with the latest content.