# Blackbird.io Smartsheet

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Smartsheet is a software as a service (SaaS) offering for collaboration and work management, developed and marketed 
by Smartsheet Inc.

## Before setting up

Before you can connect, please make sure you have an active Smartsheet account. 
You will need to select your corresponding base URL when setting up the connection:
- Standard/US (`app.smartsheet.com`): Smartsheet
- Gov (`app.smartsheetgov.com`): Smartsheet Gov
- Europe (`app.smartsheet.eu`): Smartsheet Regions Europe
- Australia (`app.smartsheet.au`): Smartsheet Regions Australia

### API Key

Follow the instructions at [this link](https://help.smartsheet.com/articles/2482389-generate-API-key) to create
a new API key. 

After the key is created, copy it and store it in a safe place. You won't be able to access it again.

### OAuth2

You need to register your own OAuth application to be able to connect. For this, follow 
[this documentation](https://developers.smartsheet.com/api/smartsheet/guides/advanced-topics/oauth#register-for-developer-tools).

Fill in the `App redirect URL` value with this link: `https://bridge.blackbird.io/api/AuthorizationCode`.
The rest of the fields do not matter for Blackbird, so you can specify any values you wish.

After the app is created, copy the `client ID` and `client secret` values. You'll need them later to create a connection.

## Connecting

Navigate to apps and search for Smartsheet. Click _Add connection_ and name your connection for future 
reference e.g. 'My Smartsheet connection'. Choose the base URL of your instance.

### API Key

1. Select the _API Key_ connection type from the dropdown.
2. Fill in the API key you copied earlier.

![API Key connection](image/README/api_key.png)

### OAuth2

1. Select the _OAuth2_ connection type from the dropdown.
2. Fill in the client ID and client secret you copied earlier.
3. Click _Authorize connection_ and follow Smartsheet's instructions to connect to your custom OAuth app.
4. When the process is complete, return to Blackbird and confirm the connection was created successfully

![OAuth connection](image/README/oauth.png)

## Additional notes

- Please note that newly created or recently updated data may not be immediately discoverable via search. 
This is a limitation of the Smartsheet API.
- When updating cell values, make sure to wrap multiselect items in brackets and separate them with a comma. 
Example: `[test1, test2]`.

## Actions

### Attachments

- **Search attachments**. Search attachments on a sheet, discussion or row.
- **Upload attachment**. Upload an attachment to a sheet, comment or a row.
- **Download attachment**. Download a specific attachment.
- **Get attachment**. Get information about a specific attachment.
- **Delete attachment**. Delete a specific attachment.

### Cells

- **Get cell value**. Get value of a specific cell.
- **Update cell value**. Update value of an existing cell.

### Columns

- **Search columns**. Search columns for a specific sheet.
- **Get column**. Get definition for a specific column.
- **Add column**. Insert one column into a specific sheet.
- **Update column**. Update properties of a specific column.
- **Delete column**. Delete a specific column.

### Comments

- **Get comment**. Get a specific comment.
- **Create comment**. Add a comment to a discussion.
- **Update comment**. Update the text of a specific comment. Only the author can edit it.
- **Delete comment**. Delete a specific comment.

### Contacts

- **Search contacts**. Search through the user's contacts.
- **Get contact**. Get a specific contact.

### Discussions

- **Search discussions**. Search through all discussions for a specified sheet, optionally filtered by row.
- **Get discussion**. Get a specific discussion.
- **Create discussion**. Create a new discussion on a sheet or a row.
- **Delete discussion**. Delete a specific discussion.

### Folders

- **Get folder**. Get metadata for a specific folder.
- **Get folder path**. Get path for a specific folder.
- **Create folder**. Create a new folder.
- **Update folder**. Update an existing folder.
- **Delete folder**. Delete an existing folder.

### Rows

- **Get row**. Get row values.
- **Add row**. Insert a new row into a specific sheet.
- **Update row**. Update values of an existing row.
- **Delete row**. Delete a specific row.

### Sheets

- **Search sheets**. Search sheets that the user has access to.
- **Search within all sheets**. Searches all sheets for the specified text.
- **Search within sheet**. Searches a specific sheet for the specified text.
- **Get sheet**. Get a specific sheet.
- **Create sheet**. Create a sheet from scratch.
- **Update sheet**. Update a specific sheet.
- **Download sheet**. Download a specific sheet.
- **Upload sheet**. Upload a new sheet.

### Users

- **Search users**. Search users in the organization account.
- **Get user**. Get a specific user.

### Workspaces

- **Search workspaces**. Search workspaces that the user has access to.
- **Get workspace**. Get a specific workspace.

## Events

### Attachments

- **On attachments created**. Triggers when an attachment is added to the sheet.
- **On attachments updated**. Triggers when an attachment is updated.
- **On attachments deleted**. Triggers when an attachment is deleted.

### Folders

- **On folders created**. Triggers when new folders are created.

### Sheets

- **On sheets created**. Triggers when new sheets are created.
- **On sheet updated**. Triggers when a specific sheet is updated.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
