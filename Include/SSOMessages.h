//
//  ssomessages.mc
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  Description: EventLog Messages used by Enterprise SSO
//
//
// Event Viewer categories
//
//
//  Values are 32 bit values layed out as follows:
//
//   3 3 2 2 2 2 2 2 2 2 2 2 1 1 1 1 1 1 1 1 1 1
//   1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0
//  +---+-+-+-----------------------+-------------------------------+
//  |Sev|C|R|     Facility          |               Code            |
//  +---+-+-+-----------------------+-------------------------------+
//
//  where
//
//      Sev - is the severity code
//
//          00 - Success
//          01 - Informational
//          10 - Warning
//          11 - Error
//
//      C - is the Customer code flag
//
//      R - is a reserved bit
//
//      Facility - is the facility code
//
//      Code - is the facility's status code
//
//
// Define the facility codes
//


//
// Define the severity codes
//
#define STATUS_SEVERITY_WARNING          0x2
#define STATUS_SEVERITY_SUCCESS          0x0
#define STATUS_SEVERITY_INFORMATIONAL    0x1
#define STATUS_SEVERITY_ERROR            0x3


//
// MessageId: CATEGORY_ENTSSO
//
// MessageText:
//
//  Enterprise Single Sign-On
//
#define CATEGORY_ENTSSO                  0x00000001L

// Update this next value if we add categories
//
// MessageId: CATEGORY_SSO_LAST
//
// MessageText:
//
//  SSOLast
//
#define CATEGORY_SSO_LAST                0x00000002L

//
// Event Log messages
//
// FIX: bug # 56593: change event log message ids and error codes
//
// Allocated ranges are -
//
// 0x2900 (10,496) thru 0x29FF (10,751) for ENTSSO event log message ids (1)
// 0x2A00 (10,752) thru 0x2AFF (11,007) for ENTSSO error codes
// 0x2B00 (11,008) thru 0x2BFF (11,263) for ENTSSO event log message ids (2)
//
// Examples
//
// MessageId: SSO_INFO_EXAMPLE
//
// MessageText:
//
//  This is example info text
//
#define SSO_INFO_EXAMPLE                 0x40002900L

//
// MessageId: SSO_INFO_EXAMPLE_WITH_STRINGS
//
// MessageText:
//
//  This is example info text, string 1 = %1,  string 2 = %2
//
#define SSO_INFO_EXAMPLE_WITH_STRINGS    0x40002901L

//
// MessageId: SSO_INFO_EXAMPLE_WITH_STRINGS_AND_ERROR
//
// MessageText:
//
//  This is example info text, string 1 = %1,  string 2 = %2, error code = %3
//
#define SSO_INFO_EXAMPLE_WITH_STRINGS_AND_ERROR 0x40002902L

//
// MessageId: SSO_INFO_EXAMPLE_MAX_STRINGS_AND_ERROR
//
// MessageText:
//
//  This is example info text, %1, %2, %3, %4, %5, error code = %6
//
#define SSO_INFO_EXAMPLE_MAX_STRINGS_AND_ERROR 0x40002903L

//
// MessageId: SSO_ERROR_EVENT_LOG_FAILED
//
// MessageText:
//
//  The event logging system encountered an error. The event log messages may not be correct for this application.
//
#define SSO_ERROR_EVENT_LOG_FAILED       0xC0002904L

// ENTSSO V1
//
// MessageId: SSO_INFO_SERVICE_STARTING
//
// MessageText:
//
//  The SSO service is starting.%r
//  Computer Name: %1%r
//  SQL Server Name: %2%r
//  SSO Database Name: %3%r
//  Using SSL. Protocol encryption enforced for SQL Server connections.
//
#define SSO_INFO_SERVICE_STARTING        0x40002905L

//
// MessageId: SSO_INFO_SERVICE_STARTED
//
// MessageText:
//
//  The SSO service has started.
//
#define SSO_INFO_SERVICE_STARTED         0x40002906L

//
// MessageId: SSO_ERROR_SERVICE_START_FAILED
//
// MessageText:
//
//  The SSO service failed to start.%r
//  Error Code: %1
//
#define SSO_ERROR_SERVICE_START_FAILED   0xC0002907L

//
// MessageId: SSO_INFO_SERVICE_STOPPING
//
// MessageText:
//
//  The SSO service is stopping.
//
#define SSO_INFO_SERVICE_STOPPING        0x40002908L

//
// MessageId: SSO_INFO_SERVICE_STOPPED
//
// MessageText:
//
//  The SSO service has stopped.
//
#define SSO_INFO_SERVICE_STOPPED         0x40002909L

//
// MessageId: SSO_INFO_SERVICE_INSTALL_OK
//
// MessageText:
//
//  The SSO service was installed.
//
#define SSO_INFO_SERVICE_INSTALL_OK      0x4000290AL

//
// MessageId: SSO_INFO_SERVICE_INSTALL_FAILED
//
// MessageText:
//
//  Failed to install the SSO service.%r
//  Error Code: %1
//
#define SSO_INFO_SERVICE_INSTALL_FAILED  0xC000290BL

//
// MessageId: SSO_INFO_SERVICE_REMOVE_OK
//
// MessageText:
//
//  The SSO service was removed.
//
#define SSO_INFO_SERVICE_REMOVE_OK       0x4000290CL

//
// MessageId: SSO_INFO_SERVICE_REMOVE_FAILED
//
// MessageText:
//
//  Failed to remove the SSO service.%r
//  Error Code: %1
//
#define SSO_INFO_SERVICE_REMOVE_FAILED   0xC000290DL

// not used
//MessageId=10510
//Severity=Error
//SymbolicName=SSO_INFO_DLL_LOAD_FAILED
//Language=English
//Failed to load %1. Check that this file is available.
//.
//
// MessageId: SSO_ERROR_NO_DSN
//
// MessageText:
//
//  The SQL Server and SSO database names were not found in the registry. Use the SSO admin utilities to configure these values.
//
#define SSO_ERROR_NO_DSN                 0xC000290FL

//
// MessageId: SSO_ERROR_LOADLIBRARY
//
// MessageText:
//
//  Failed to load %1%r
//  Error code: %2. 
//
#define SSO_ERROR_LOADLIBRARY            0xC0002910L

//
// MessageId: SSO_ERROR_DB_FIRST_ACCESS
//
// MessageText:
//
//  Failed to contact the SSO database: %1%r
//  %2%r
//  Error code: %3
//
#define SSO_ERROR_DB_FIRST_ACCESS        0xC0002911L

//
// MessageId: SSO_ERROR_DB_ACCESS
//
// MessageText:
//
//  An error occurred while attempting to access the SSO database.%r
//  Function: %1%r
//  File: %2%r
//  %3.%r
//  SQL Error code: %4%r
//  Error code: %5
//
#define SSO_ERROR_DB_ACCESS              0xC0002912L

//
// MessageId: SSO_ERROR_POLL_DATABASE
//
// MessageText:
//
//  Lost contact with the SSO database. Check that the SSO database is available.
//
#define SSO_ERROR_POLL_DATABASE          0xC0002913L

//
// MessageId: SSO_ERROR_BAD_SSO_APP_ADMIN
//
// MessageText:
//
//  The SSO Affiliate Admin account is not valid. If it is a local account check that this account exists on the server. If it is a domain account contact your domain administrator. Enable SSO when the account is valid.%r
//  SSO Affiliate Admin account: %1%r
//  Invalid accounts: %2%r
//  Error Code: %3
//
#define SSO_ERROR_BAD_SSO_APP_ADMIN      0xC0002914L

//
// MessageId: SSO_ERROR_BAD_SSO_ADMIN
//
// MessageText:
//
//  The SSO Admin account is not valid. If it is a local account check that this account exists on the server. If it is a domain account contact your domain administrator. Enable SSO when the account is valid.%r
//  SSO Admin account: %1%r
//  Invalid accounts: %2%r
//  Error Code: %3
//
#define SSO_ERROR_BAD_SSO_ADMIN          0xC0002915L

//
// MessageId: SSO_WARN_BAD_USER_GROUP
//
// MessageText:
//
//  The Application Users account for this application is not valid. If it is a local account check that this account exists on the server. If it is a domain account contact your domain administrator. Enable the application when the account is valid.%r
//  Application Name: %1%r
//  Application Users account: %2%r
//  Invalid accounts: %3%r
//  Error Code: %4
//
#define SSO_WARN_BAD_USER_GROUP          0x80002916L

//
// MessageId: SSO_WARN_BAD_APP_ADMIN_GROUP
//
// MessageText:
//
//  The Application Admin account for this application is not valid. If it is a local account check that this account exists on the server. If it is a domain account contact your domain administrator. Enable the application when the account is valid.%r
//  Application Name: %1%r
//  Application Admin account: %2%r
//  Invalid accounts: %3%r
//  Error Code: %4
//
#define SSO_WARN_BAD_APP_ADMIN_GROUP     0x80002917L

//
// MessageId: SSO_WARN_NO_SECRETS
//
// MessageText:
//
//  No secrets were found in the registry of the master secret server. Use the configuration tools to generate or restore a master secret.
//
#define SSO_WARN_NO_SECRETS              0x80002918L

//
// MessageId: SSO_ERROR_SECRETS_NOT_LOADED
//
// MessageText:
//
//  Could not load secrets from the registry of the master secret server.
//
#define SSO_ERROR_SECRETS_NOT_LOADED     0xC0002919L

//
// MessageId: SSO_ERROR_REENCRYPT
//
// MessageText:
//
//  SSO database re-encryption failed. Will try again in %1 minutes.%r
//  Error Code: %2
//
#define SSO_ERROR_REENCRYPT              0xC000291AL

//
// MessageId: SSO_INFO_RESTORE_SECRET_OK
//
// MessageText:
//
//  The master secrets were successfully restored.%r
//  File Name: %1%r
//  Current MSID: %2%r
//  Previous MSID: %3%r
//  Client User: %4%r
//  Client Computer: %5
//
#define SSO_INFO_RESTORE_SECRET_OK       0x4000291BL

//
// MessageId: SSO_ERROR_RESTORE_SECRET_FAILED
//
// MessageText:
//
//  Failed to restore the master secrets.%r
//  File Name: %1%r
//  Current MSID: %2%r
//  Previous MSID: %3%r
//  Client User: %4%r
//  Error Code: %5
//
#define SSO_ERROR_RESTORE_SECRET_FAILED  0xC000291CL

//
// MessageId: SSO_INFO_GENERATE_SECRET_OK
//
// MessageText:
//
//  A new master secret was successfully generated.%r
//  MSID: %1%r
//  Client User: %2%r
//  Client Computer: %3%r
//  Tracking ID: %4
//
#define SSO_INFO_GENERATE_SECRET_OK      0x4000291DL

//
// MessageId: SSO_ERROR_GENERATE_SECRET_FAILED
//
// MessageText:
//
//  Failed to generate a new master secret.%r
//  Client User: %1%r
//  Client Computer:%2%r
//  Tracking ID: %3%r
//  Error Code: %4
//
#define SSO_ERROR_GENERATE_SECRET_FAILED 0xC000291EL

//
// MessageId: SSO_ERROR_GET_SECRET_FAILED
//
// MessageText:
//
//  A request to get a master secret failed.%r
//  Secret Number: %1%r
//  Client User: %2%r
//  Client Computer: %3%r
//  Tracking ID: %4%r
//  Error Code: %5
//
#define SSO_ERROR_GET_SECRET_FAILED      0xC000291FL

//
// MessageId: SSO_INFO_REENCRYPT_OK
//
// MessageText:
//
//  SSO database re-encryption has completed successfully.%r
//  Number of Credentials re-encrypted: %1%r
//  Current MSID: %2%r
//  Previous MSID: %3
//
#define SSO_INFO_REENCRYPT_OK            0x40002920L

//
// MessageId: SSO_INFO_GOT_CURRENT_SECRET
//
// MessageText:
//
//  Got the current secret from the master secret server.%r
//  Secret Server Name: %1%r
//  MSID: %2
//
#define SSO_INFO_GOT_CURRENT_SECRET      0x40002921L

//
// MessageId: SSO_INFO_GOT_PREVIOUS_SECRET
//
// MessageText:
//
//  Got the previous secret from the master secret server.%r
//  Secret Server Name: %1%r
//  MSID: %2
//
#define SSO_INFO_GOT_PREVIOUS_SECRET     0x40002922L

//
// MessageId: SSO_ERROR_GET_SECRET_OK
//
// MessageText:
//
//  A request to get a master secret succeeded.%r
//  Secret Number: %1%r
//  MSID: %2%r
//  Client User: %3%r
//  Client Computer: %4%r
//  Tracking ID: %5
//
#define SSO_ERROR_GET_SECRET_OK          0x40002923L

//
// MessageId: SSO_WARN_LOST_SECRET_SERVER
//
// MessageText:
//
//  Failed to retrieve master secrets. Verify that the master secret server name is correct and that it is available.%r
//  Secret Server Name: %1%r
//  Error Code: %2
//
#define SSO_WARN_LOST_SECRET_SERVER      0x80002924L

//
// MessageId: SSO_INFO_FOUND_SECRET_SERVER
//
// MessageText:
//
//  Recovered from failure to get master secrets.%r
//  Secret Server Name: %1
//
#define SSO_INFO_FOUND_SECRET_SERVER     0x40002925L

//
// MessageId: SSO_INFO_SECRET_SERVER_STARTING
//
// MessageText:
//
//  This computer is the master secret server.
//
#define SSO_INFO_SECRET_SERVER_STARTING  0x40002926L

//
// MessageId: SSO_INFO_API_AUDIT
//
// MessageText:
//
//  SSO AUDIT%r
//  Function: %1%r
//  Tracking ID: %2%r
//  Client Computer: %3%r
//  Client User: %4%r
//  Application Name: %5
//
#define SSO_INFO_API_AUDIT               0x40002927L

//
// MessageId: SSO_WARN_API_AUDIT
//
// MessageText:
//
//  SSO AUDIT%r
//  Function: %1%r
//  Tracking ID: %2%r
//  Client Computer: %3%r
//  Client User: %4%r
//  Application Name: %5%r
//  Error Code: %6
//
#define SSO_WARN_API_AUDIT               0x80002928L

//
// MessageId: SSO_WARN_SSO_DISABLED
//
// MessageText:
//
//  The SSO system is currently disabled.
//
#define SSO_WARN_SSO_DISABLED            0x80002929L

//
// MessageId: SSO_WARN_APP_DISABLED
//
// MessageText:
//
//  The application is currently disabled.%r
//  Application Name: %1
//
#define SSO_WARN_APP_DISABLED            0x8000292AL

//
// MessageId: SSO_WARN_SSO_TICKETS_NOT_ALLOWED
//
// MessageText:
//
//  Tickets are not enabled for the SSO system.
//
#define SSO_WARN_SSO_TICKETS_NOT_ALLOWED 0x8000292BL

//
// MessageId: SSO_WARN_APP_TICKETS_NOT_ALLOWED
//
// MessageText:
//
//  Tickets are not enabled for this application.%r
//  Application Name: %1
//
#define SSO_WARN_APP_TICKETS_NOT_ALLOWED 0x8000292CL

//
// MessageId: SSO_WARN_SSO_TICKETS_VALIDATED
//
// MessageText:
//
//  Tickets cannot be redeemed without being validated.
//
#define SSO_WARN_SSO_TICKETS_VALIDATED   0x8000292DL

//
// MessageId: SSO_WARN_APP_TICKETS_VALIDATED
//
// MessageText:
//
//  Tickets cannot be redeemed for this application without being validated.%r
//  Application Name: %1
//
#define SSO_WARN_APP_TICKETS_VALIDATED   0x8000292EL

//
// MessageId: SSO_WARN_ORIGINAL_TICKET_VALIDATED
//
// MessageText:
//
//  The ticket was issued with validation required. It cannot be redeemed for this application without being validated.%r
//  Application Name: %1
//
#define SSO_WARN_ORIGINAL_TICKET_VALIDATED 0x8000292FL

//
// MessageId: SSO_WARN_TICKET_EXPIRED
//
// MessageText:
//
//  The ticket has expired.%r
//  Application Name: %1
//
#define SSO_WARN_TICKET_EXPIRED          0x80002930L

//
// MessageId: SSO_WARN_ISSUE_TICKETS_NOT_ALLOWED
//
// MessageText:
//
//  Tickets are not enabled for the SSO system.
//
#define SSO_WARN_ISSUE_TICKETS_NOT_ALLOWED 0x80002931L

// V3 - no longer needed
//MessageId=10546
//Severity=Warning
//SymbolicName=SSO_WARN_MAPPING_DISABLED
//Language=English
//The external credentials cannot be returned because the mapping is disabled.
//.
//
// MessageId: SSO_WARN_UPDATE_FAILED
//
// MessageText:
//
//  Failed to update the credentials in the SSO database during re-encryption.
//
#define SSO_WARN_UPDATE_FAILED           0x80002933L

//
// MessageId: SSO_INFO_CREATE_DATABASE_OK
//
// MessageText:
//
//  The SSO database was created and initialized .%r
//  SQL Server Name: %1%r
//  SSO Database Name: %2%r
//  Client User: %3
//
#define SSO_INFO_CREATE_DATABASE_OK      0x40002934L

//
// MessageId: SSO_ERROR_CREATE_DATABASE_FAILED
//
// MessageText:
//
//  Creation and initialization of the SSO database failed.%r
//  SQL Server Name: %1%r
//  SSO Database Name: %2%r
//  Client User: %3%r
//  Error Code: %4
//
#define SSO_ERROR_CREATE_DATABASE_FAILED 0xC0002935L

//
// MessageId: SSO_ERROR_SERVICE_NOT_SSO_ADMIN
//
// MessageText:
//
//  The SSO service could not start because the service account it is running under is not a member of the SSO Admin account.%r
//  SSO Admin account: %1%r
//  SSO service account: %2
//
#define SSO_ERROR_SERVICE_NOT_SSO_ADMIN  0xC0002936L

//
// MessageId: SSO_WARN_INVALID_USER
//
// MessageText:
//
//  A mapping could not be created because the specified user is not valid for this application.%r
//  Domain Name: %1%r
//  User Name: %2%r
//  Application Name: %3%r
//  Application Users account: %4
//
#define SSO_WARN_INVALID_USER            0x80002937L

//
// MessageId: SSO_ERROR_MAPPING_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Mapping server access denied.%r
//
#define SSO_ERROR_MAPPING_CALLBACK_ACCESS_DENIED 0xC0002938L

//
// MessageId: SSO_ERROR_ADMIN_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Admin server access denied.%r
//
#define SSO_ERROR_ADMIN_CALLBACK_ACCESS_DENIED 0xC0002939L

//
// MessageId: SSO_ERROR_LOOKUP_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Lookup server access denied.%r
//
#define SSO_ERROR_LOOKUP_CALLBACK_ACCESS_DENIED 0xC000293AL

//
// MessageId: SSO_ERROR_SECRET_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Secret server access denied.%r
//  Client User: %1
//
#define SSO_ERROR_SECRET_CALLBACK_ACCESS_DENIED 0xC000293BL

//
// MessageId: SSO_WARN_INVALID_GROUP_USER
//
// MessageText:
//
//  The mapping specified for a group application is not valid. The mapping must specify one of the Application Users accounts for this application.%r
//  Domain Name: %1%r
//  User Name: %2%r
//  Application Name: %3%r
//  Application Users account: %4
//
#define SSO_WARN_INVALID_GROUP_USER      0x8000293CL

//
// MessageId: SSO_WARN_USER_NOT_ALLOWED_FOR_GROUPS
//
// MessageText:
//
//  Application Users are not allowed to control mappings for group applications.%r
//  Domain Name: %1%r
//  User Name: %2%r
//  Application Name: %3%r
//  Client User: %4
//
#define SSO_WARN_USER_NOT_ALLOWED_FOR_GROUPS 0x8000293DL

//
// MessageId: SSO_WARN_USER_OWN_MAPPINGS
//
// MessageText:
//
//  Application Users are only allowed to control their own mappings.%r
//  Domain Name: %1%r
//  User Name: %2%r
//  Application Name: %3%r
//  Client User: %4
//
#define SSO_WARN_USER_OWN_MAPPINGS       0x8000293EL

//
// MessageId: SSO_INFO_BACKUP_SECRET_OK
//
// MessageText:
//
//  The master secrets were successfully backed up.%r
//  File Name: %1%r
//  Current MSID: %2%r
//  Previous MSID: %3%r
//  Client User: %4%r
//  Client Computer: %5
//
#define SSO_INFO_BACKUP_SECRET_OK        0x4000293FL

//
// MessageId: SSO_ERROR_BACKUP_SECRET_FAILED
//
// MessageText:
//
//  Failed to back up the master secrets.%r
//  File Name: %1%r
//  Current MSID: %2%r
//  Previous MSID: %3%r
//  Client User: %4%r
//  Error Code: %5
//
#define SSO_ERROR_BACKUP_SECRET_FAILED   0xC0002940L

//
// MessageId: SSO_ERROR_BACKUP_FAILED_MEDIA
//
// MessageText:
//
//  The file specified for backup of the master secrets must be on an NTFS file system or removable media.%r
//  File Name: %1%r
//  Client User: %2%r
//  Client Computer: %3%r
//  Error Code: %4
//
#define SSO_ERROR_BACKUP_FAILED_MEDIA    0xC0002941L

//
// MessageId: SSO_INFO_REENCRYPT_STARTING
//
// MessageText:
//
//  SSO database re-encryption is in progress.
//
#define SSO_INFO_REENCRYPT_STARTING      0x40002942L

//
// MessageId: SSO_WARN_PERFMON_FAILED
//
// MessageText:
//
//  Performance monitoring failed to start.%r
//  Error Code: %1
//
#define SSO_WARN_PERFMON_FAILED          0x80002943L

//
// MessageId: SSO_ERROR_BACKUP_FILE_INCORRECT_FORMAT
//
// MessageText:
//
//  The backup file does not have the correct format.
//
#define SSO_ERROR_BACKUP_FILE_INCORRECT_FORMAT 0xC0002944L

//
// MessageId: SSO_ERROR_SECRET_VALIDATE_FAILED
//
// MessageText:
//
//  The secret could not be loaded from the registry. The service account for the SSO service may have been changed or the secret may be corrupted. Restore the secret from a backup file.%r
//  MSID: %1 
//
#define SSO_ERROR_SECRET_VALIDATE_FAILED 0xC0002945L

//
// MessageId: SSO_WARN_CANNOT_UPDATE_APP_FLAGS
//
// MessageText:
//
//  You cannot update some of the specified flags for this application. They will be ignored.%r
//  Application Name: %1%r
//  Specified Flag Mask: %2
//
#define SSO_WARN_CANNOT_UPDATE_APP_FLAGS 0x80002946L

//
// MessageId: SSO_WARN_INVALID_APP_USER_GROUP
//
// MessageText:
//
//  The Application Users account is not valid for application update.%r
//  Application Name: %1%r
//  Application Users account: %2%r
//  Invalid accounts: %3%r
//  Error Code: %4
//
#define SSO_WARN_INVALID_APP_USER_GROUP  0x80002947L

//
// MessageId: SSO_WARN_INVALID_APP_ADMIN_GROUP
//
// MessageText:
//
//  The Application Admin account is not valid for application update.%r
//  Application Name: %1%r
//  Application Admin account: %2%r
//  Invalid accounts: %3%r
//  Error Code: %4
//
#define SSO_WARN_INVALID_APP_ADMIN_GROUP 0x80002948L

//MessageId=10569
//Severity=Warning
//SymbolicName=SSO_WARN_SSO_DISABLED_FOR_CHANGE_SS
//Language=English
//To update the secret server name you must first disable SSO.%r
//.
//
// MessageId: SSO_WARN_NO_UPDATE_BY_APP_ADMIN
//
// MessageText:
//
//  The Application Admin account cannot be changed by an Application Administrator.%r
//  Application Name: %1%r
//  Application Admin account: %2%r
//  Error Code: %3
//
#define SSO_WARN_NO_UPDATE_BY_APP_ADMIN  0x80002949L

//
// MessageId: SSO_WARN_SSO_DISABLED_FOR_CHANGE_SSO_ADMIN
//
// MessageText:
//
//  To update the SSO Admin account name you must first disable SSO.%r
//
#define SSO_WARN_SSO_DISABLED_FOR_CHANGE_SSO_ADMIN 0x8000294AL

//
// MessageId: SSO_WARN_SSO_NOT_IN_NEW_SSO_ADMIN
//
// MessageText:
//
//  To change the SSO Admin account name the SSO service account must be a member of the new SSO Admin account.%r
//  SSO service account: %1%r
//  New SSO Admin account: %2
//
#define SSO_WARN_SSO_NOT_IN_NEW_SSO_ADMIN 0x8000294BL

//
// MessageId: SSO_WARN_SSO_DISABLED_FOR_CHANGE_SSO_AFF_ADMIN
//
// MessageText:
//
//  To update the SSO Affiliate Admin account name you must first disable SSO.%r
//
#define SSO_WARN_SSO_DISABLED_FOR_CHANGE_SSO_AFF_ADMIN 0x8000294CL

//
// MessageId: SSO_WARN_INVALID_SSO_APP_ADMIN_GROUP
//
// MessageText:
//
//  The SSO Affiliate Admin account is not valid for global info update.%r
//  SSO Affiliate Admin account: %1%r
//  Invalid accounts: %2%r
//  Error Code: %3
//
#define SSO_WARN_INVALID_SSO_APP_ADMIN_GROUP 0x8000294DL

//
// MessageId: SSO_WARN_INVALID_SSO_ADMIN_GROUP
//
// MessageText:
//
//  The SSO Admin account is not valid for global info update.%r
//  SSO Admin account: %1%r
//  Invalid accounts: %2%r
//  Error Code: %3
//
#define SSO_WARN_INVALID_SSO_ADMIN_GROUP 0x8000294EL

//
// MessageId: SSO_INFO_CHANGED_SECRET_SERVER
//
// MessageText:
//
//  Updated secret server name.%r
//  New Secret Server: %1%r
//  Old Secret Server: %2%r
//  Tracking ID: %3%r
//  Client Computer: %4%r
//  Client User: %5
//
#define SSO_INFO_CHANGED_SECRET_SERVER   0x4000294FL

//
// MessageId: SSO_INFO_CHANGED_SSO_ADMIN
//
// MessageText:
//
//  Updated SSO Admin account.%r
//  New SSO Admin account: %1%r
//  Old SSO Admin account: %2%r
//  Tracking ID: %3%r
//  Client Computer: %4%r
//  Client User: %5
//
#define SSO_INFO_CHANGED_SSO_ADMIN       0x40002950L

//
// MessageId: SSO_INFO_CHANGED_SSO_AFF_ADMIN
//
// MessageText:
//
//  Updated SSO Affiliate Admin account.%r
//  New SSO Affiliate Admin account: %1%r
//  Old SSO Affiliate Admin account: %2%r
//  Tracking ID: %3%r
//  Client Computer: %4%r
//  Client User: %5
//
#define SSO_INFO_CHANGED_SSO_AFF_ADMIN   0x40002951L

//
// MessageId: SSO_INFO_CHANGED_APP_USER_GROUP
//
// MessageText:
//
//  Updated Application Users account.%r
//  New Application Users account: %1%r
//  Old Application Users account: %2%r
//  Tracking ID: %3%r
//  Client User: %4%r
//  Application Name: %5
//
#define SSO_INFO_CHANGED_APP_USER_GROUP  0x40002952L

//
// MessageId: SSO_INFO_CHANGED_APP_ADMIN_GROUP
//
// MessageText:
//
//  Updated Application Admin account.%r
//  New Application Admin account: %1%r
//  Old Application Admin account: %2%r
//  Tracking ID: %3%r
//  Client User: %4%r
//  Application Name: %5
//
#define SSO_INFO_CHANGED_APP_ADMIN_GROUP 0x40002953L

//
// MessageId: SSO_INFO_APP_ENABLED
//
// MessageText:
//
//  The application was enabled.%r
//  Tracking ID: %1%r
//  Client Computer: %2%r
//  Client User: %3%r
//  Application Name: %4
//
#define SSO_INFO_APP_ENABLED             0x40002954L

//
// MessageId: SSO_INFO_APP_DISABLED
//
// MessageText:
//
//  The application was disabled.%r
//  Tracking ID: %1%r
//  Client Computer: %2%r
//  Client User: %3%r
//  Application Name: %4
//
#define SSO_INFO_APP_DISABLED            0x40002955L

//
// MessageId: SSO_INFO_SSO_ENABLED
//
// MessageText:
//
//  The SSO system was enabled.%r
//  Tracking ID: %1%r
//  Client Computer: %2%r
//  Client User: %3
//
#define SSO_INFO_SSO_ENABLED             0x40002956L

//
// MessageId: SSO_INFO_SSO_DISABLED
//
// MessageText:
//
//  The SSO system was disabled.%r
//  Tracking ID: %1%r
//  Client Computer: %2%r
//  Client User: %3
//
#define SSO_INFO_SSO_DISABLED            0x40002957L

//
// MessageId: SSO_WARN_NO_IMPERSONATION
//
// MessageText:
//
//  Could not impersonate the client.%r
//  Error Code: %1
//
#define SSO_WARN_NO_IMPERSONATION        0x80002958L

//
// MessageId: SSO_WARN_EXPIRED_TICKET_REDEEMED
//
// MessageText:
//
//  A ticket was redeemed after the ticket time-out period. This is allowed because the ticket time-out is disabled for this application.%r
//  Application Name: %1
//
#define SSO_WARN_EXPIRED_TICKET_REDEEMED 0x80002959L

//
// MessageId: SSO_WARN_CRED_CACHE_OFF
//
// MessageText:
//
//  The credential cache has been disabled for this SSO server.
//
#define SSO_WARN_CRED_CACHE_OFF          0x8000295AL

//
// MessageId: SSO_INFO_CRED_CACHE_SIZE_CHANGED
//
// MessageText:
//
//  The credential cache size has been changed for this SSO server.%r
//  Credential Cache Size: %1
//
#define SSO_INFO_CRED_CACHE_SIZE_CHANGED 0x4000295BL

//
// MessageId: SSO_WARN_SQL_NOT_FOUND
//
// MessageText:
//
//  The specified SQL Server was not found. Check that the SQL Server name is correct and that SQL Server is running on that computer.%r
//  SQL Server Name: %1
//
#define SSO_WARN_SQL_NOT_FOUND           0x8000295CL

//
// MessageId: SSO_ERROR_BACKUP_SECRET_REQUIRED
//
// MessageText:
//
//  The master secret has not been backed up. If you lose the master secret all the information stored in the SSO system will be lost permanently and your systems may fail to work correctly. Please use the SSO admin tools to back up your master secret.
//
#define SSO_ERROR_BACKUP_SECRET_REQUIRED 0xC000295DL

//
// MessageId: SSO_ERROR_OUT_OF_SERVICE
//
// MessageText:
//
//  Enterprise Single Sign-On is going offline.
//
#define SSO_ERROR_OUT_OF_SERVICE         0xC000295EL

//
// MessageId: SSO_INFO_IN_SERVICE
//
// MessageText:
//
//  Enterprise Single Sign-On is back online.
//
#define SSO_INFO_IN_SERVICE              0x4000295FL

//
// MessageId: SSO_WARN_TICKET_DECRYPT_FAILED
//
// MessageText:
//
//  The ticket could not be decrypted. The ticket is not valid or it may have expired.%r
//  Application Name: %1%r
//  Error Code: %2
//
#define SSO_WARN_TICKET_DECRYPT_FAILED   0x80002960L

//
// MessageId: SSO_WARN_TICKET_BAD_FORMAT
//
// MessageText:
//
//  The ticket does not have the correct format.%r
//  Application Name: %1
//
#define SSO_WARN_TICKET_BAD_FORMAT       0x80002961L

//
// MessageId: SSO_WARN_TICKET_VALIDATE_FAILED
//
// MessageText:
//
//  Validation of the ticket failed. The sender name must match that of the ticket issuer.%r
//  Application Name: %1%r
//  Ticket Issued By: %2%r
//  Sender Name: %3
//
#define SSO_WARN_TICKET_VALIDATE_FAILED  0x80002962L

//
// MessageId: SSO_WARN_APP_INCOMPLETE_FIELDS
//
// MessageText:
//
//  The application cannot be enabled because the fields are incomplete for this application.%r
//  Application Name: %1%r
//  Number of Fields Defined: %2%r
//  Number of Fields Created: %3
//
#define SSO_WARN_APP_INCOMPLETE_FIELDS   0x80002963L

//
// MessageId: SSO_WARN_TICKET_USER_NOT_IN_GROUP
//
// MessageText:
//
//  The ticket cannot be redeemed because the user for whom the ticket was issued is not a member of the Application Users group.%r
//  Application Name: %1%r
//  Ticket Issued For: %2%r
//  Application Users group: %3
//
#define SSO_WARN_TICKET_USER_NOT_IN_GROUP 0x80002964L

//
// MessageId: SSO_WARN_CALLER_NOT_IN_NEW_SSO_ADMIN
//
// MessageText:
//
//  The client user must be a member of the new SSO Admin account to change the SSO Admin account name.%r
//  Client User: %1%r
//  New SSO Admin account: %2
//
#define SSO_WARN_CALLER_NOT_IN_NEW_SSO_ADMIN 0x80002965L

//
// MessageId: SSO_WARN_BACKUP_RESTORE_FILE_VERSION_1
//
// MessageText:
//
//  The master secrets were restored from an older format backup file.
//
#define SSO_WARN_BACKUP_RESTORE_FILE_VERSION_1 0x80002966L

//
// MessageId: SSO_WARN_ENTSSO_IS_ADMIN
//
// MessageText:
//
//  The SSO service is running under a local administrator account. This is not recommended for security reasons. See documentation for details.%r
//  SSO service account: %1
//
#define SSO_WARN_ENTSSO_IS_ADMIN         0x80002967L

//
// MessageId: SSO_ERROR_CS_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Config Store server access denied.%r
//
#define SSO_ERROR_CS_CALLBACK_ACCESS_DENIED 0xC0002968L

//
// MessageId: SSO_WARN_INVALID_FLAGS
//
// MessageText:
//
//  The specified flags are not valid for creating this type of application.
//  Note that an application cannot be created as enabled.
//  See documentation for details.%r
//  Application Name: %1%r
//  Specified Flags: %2%r
//  Allowed Flags: %3
//
#define SSO_WARN_INVALID_FLAGS           0x80002969L

//
// MessageId: SSO_ERROR_RPC_FAILURE
//
// MessageText:
//
//  RPC failure. Please check your network configuration.%r
//  Error Code: %1
//
#define SSO_ERROR_RPC_FAILURE            0xC000296AL

//
// MessageId: SSO_WARN_DB_ACCESS
//
// MessageText:
//
//  Could not access the SSO database. If this condition persists, the SSO service will go offline.%r
//  %1.%r
//  SQL Error code: %2
//
#define SSO_WARN_DB_ACCESS               0x8000296BL

//
// MessageId: SSO_ERROR_SSOCSTX_OUT_OF_PROC
//
// MessageText:
//
//  The 'ENTSSO Server' COM+ application is not configured correctly. It must be a COM+ library application.
//
#define SSO_ERROR_SSOCSTX_OUT_OF_PROC    0xC000296CL

//
// MessageId: SSO_ERROR_DTC_IMPORT
//
// MessageText:
//
//  Could not import a DTC transaction. Please check that MSDTC is configured correctly for remote operation. See documentation for details.%r
//  Error Code: %1
//
#define SSO_ERROR_DTC_IMPORT             0xC000296DL

//
// MessageId: SSO_INFO_TICKETS_ENABLED
//
// MessageText:
//
//  Tickets have been enabled for the SSO system.%r
//  Tracking ID: %1%r
//  Client Computer: %2%r
//  Client User: %3
//
#define SSO_INFO_TICKETS_ENABLED         0x4000296EL

//
// MessageId: SSO_INFO_TICKETS_DISABLED
//
// MessageText:
//
//  Tickets have been disabled for the SSO system.%r
//  Tracking ID: %1%r
//  Client Computer: %2%r
//  Client User: %3
//
#define SSO_INFO_TICKETS_DISABLED        0x4000296FL

//
// MessageId: SSO_INFO_TICKETS_ENABLED_FOR_APP
//
// MessageText:
//
//  Tickets have been enabled for the application.%r
//  Tracking ID: %1%r
//  Client Computer: %2%r
//  Client User: %3%r
//  Application Name: %4
//
#define SSO_INFO_TICKETS_ENABLED_FOR_APP 0x40002970L

//
// MessageId: SSO_INFO_TICKETS_DISABLED_FOR_APP
//
// MessageText:
//
//  Tickets have been disabled for the application.%r
//  Tracking ID: %1%r
//  Client Computer: %2%r
//  Client User: %3%r
//  Application Name: %4
//
#define SSO_INFO_TICKETS_DISABLED_FOR_APP 0x40002971L

//
// MessageId: SSO_WARN_CRED_CACHE_FAILED
//
// MessageText:
//
//  The credential cache has encountered an unexpected error and has shut down. This may affect performance.%r
//  Error Code: %1
//
#define SSO_WARN_CRED_CACHE_FAILED       0x80002972L

//
// MessageId: SSO_INFO_SERVICE_STARTING_NO_SSL
//
// MessageText:
//
//  The SSO service is starting.%r
//  Computer Name: %1%r
//  SQL Server Name: %2%r
//  SSO Database Name: %3%r
//  Not using SSL. See documentation for details on how to secure the SQL Server connection.
//
#define SSO_INFO_SERVICE_STARTING_NO_SSL 0x40002973L

//
// MessageId: SSO_WARN_TRANSACTION_TIMEOUT
//
// MessageText:
//
//  The transaction time-out exceeds the recommended maximum for this operation. See documentation for details.%r
//  Transaction ID: %1%r
//  Transaction time-out: %2 minutes (zero indicates an infinite time-out)%r
//  Recommended maximum: %3 minutes
//
#define SSO_WARN_TRANSACTION_TIMEOUT     0x80002974L

//
// MessageId: SSO_ERROR_RPC_CALLBACK
//
// MessageText:
//
//  SSO server access denied.%r
//  Client User: %1%r
//  RPC call information: %2:%3%r
//  Error Code: %4
//
#define SSO_ERROR_RPC_CALLBACK           0xC0002975L

//
// MessageId: SSO_WARN_INVALID_TICKET_TIMEOUT
//
// MessageText:
//
//  The ticket time-out value is not valid for global info update.%r
//  Ticket time-out: %1 minutes%r
//  Minimum ticket time-out: 1 minute%r
//  Maximum ticket time-out: %2 minutes%r
//  Error Code: %3
//
#define SSO_WARN_INVALID_TICKET_TIMEOUT  0x80002976L

//
// MessageId: SSO_WARN_NO_UPDATE_TICKET_TIMEOUT
//
// MessageText:
//
//  The client user must be a member of the SSO Admin account to change the ticket time-out for an application.%r
//  Client User: %1%r
//  SSO Admin account: %2%r
//  Application Name: %3%r
//  Error Code: %4
//
#define SSO_WARN_NO_UPDATE_TICKET_TIMEOUT 0x80002977L

// NOTE - gap in message ids here
// PASSWORD SYNC (V2)
//
// MessageId: SSO_INFO_PASSWORD_SYNC_STARTING
//
// MessageText:
//
//  Password sync services are available for this computer.%r
//
#define SSO_INFO_PASSWORD_SYNC_STARTING  0x4000299AL

//
// MessageId: SSO_INFO_PASSWORD_SYNC_STARTED
//
// MessageText:
//
//  Password sync services were initialized OK.%r
//
#define SSO_INFO_PASSWORD_SYNC_STARTED   0x4000299BL

//
// MessageId: SSO_ERROR_PASSWORD_SYNC_START_FAILED
//
// MessageText:
//
//  Password sync services failed to initialize.%r
//  Error Code: %1
//
#define SSO_ERROR_PASSWORD_SYNC_START_FAILED 0xC000299CL

//
// MessageId: SSO_ERROR_PS_ADAPTER_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Password sync server (for adapters) access denied.%r
//
#define SSO_ERROR_PS_ADAPTER_CALLBACK_ACCESS_DENIED 0xC000299DL

//
// MessageId: SSO_ERROR_PS_WINDOWS_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Password sync server (for Windows) access denied.%r
//
#define SSO_ERROR_PS_WINDOWS_CALLBACK_ACCESS_DENIED 0xC000299EL

//
// MessageId: SSO_ERROR_PS_PING_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Password sync server (for ping) access denied.%r
//  Client User: %1
//
#define SSO_ERROR_PS_PING_CALLBACK_ACCESS_DENIED 0xC000299FL

//
// MessageId: SSO_INFO_PASSWORD_SYNC_ADAPTERS_STARTING
//
// MessageText:
//
//  Password sync for external adapters is starting.%r
//
#define SSO_INFO_PASSWORD_SYNC_ADAPTERS_STARTING 0x400029A0L

//
// MessageId: SSO_INFO_PASSWORD_SYNC_ADAPTERS_STARTED
//
// MessageText:
//
//  Password sync for external adapters has started.%r
//
#define SSO_INFO_PASSWORD_SYNC_ADAPTERS_STARTED 0x400029A1L

//
// MessageId: SSO_ERROR_PASSWORD_SYNC_ADAPTERS_START_FAILED
//
// MessageText:
//
//  Password sync for external adapters failed to start.%r
//  Error Code: %1
//
#define SSO_ERROR_PASSWORD_SYNC_ADAPTERS_START_FAILED 0xC00029A2L

//
// MessageId: SSO_INFO_PASSWORD_SYNC_WINDOWS_STARTING
//
// MessageText:
//
//  Password sync for Windows is starting.%r
//
#define SSO_INFO_PASSWORD_SYNC_WINDOWS_STARTING 0x400029A3L

//
// MessageId: SSO_INFO_PASSWORD_SYNC_WINDOWS_STARTED
//
// MessageText:
//
//  Password sync for Windows has started.%r
//
#define SSO_INFO_PASSWORD_SYNC_WINDOWS_STARTED 0x400029A4L

//
// MessageId: SSO_ERROR_PASSWORD_SYNC_WINDOWS_START_FAILED
//
// MessageText:
//
//  Password sync for Windows failed to start.%r
//  Error Code: %1
//
#define SSO_ERROR_PASSWORD_SYNC_WINDOWS_START_FAILED 0xC00029A5L

//
// MessageId: SSO_INFO_WINDOWS_PASSWORD_CHANGE_RECEIVED
//
// MessageText:
//
//  A password change was received from Windows.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Additional Data: %3%r
//  Client User: %4
//
#define SSO_INFO_WINDOWS_PASSWORD_CHANGE_RECEIVED 0x400029A6L

//
// MessageId: SSO_INFO_EXTERNAL_PASSWORD_CHANGE_RECEIVED
//
// MessageText:
//
//  A password change was received from an adapter.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3%r
//  Client User: %4%r
//  Client Computer: %5
//
#define SSO_INFO_EXTERNAL_PASSWORD_CHANGE_RECEIVED 0x400029A7L

//
// MessageId: SSO_INFO_TIMESTAMP
//
// MessageText:
//
//  PASSWORD SYNC TIMESTAMP%r
//  Tracking ID: %1%r
//  Function: %2%r
//  File Time: %3%r
//  System Time: %4%r
//  Local Time: %5
//
#define SSO_INFO_TIMESTAMP               0x400029A8L

//
// MessageId: SSO_WARN_NO_PROPERTIES
//
// MessageText:
//
//  Error reading adapter properties. Using defaults.%r
//  Adapter: %1%r
//  Error Code: %2
//
#define SSO_WARN_NO_PROPERTIES           0x800029A9L

//
// MessageId: SSO_INFO_DAMPED_EXTERNAL_PASSWORD_CHANGE
//
// MessageText:
//
//  An external password change was damped (detected as a duplicate and discarded).%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3
//
#define SSO_INFO_DAMPED_EXTERNAL_PASSWORD_CHANGE 0x400029AAL

//
// MessageId: SSO_INFO_SUPPRESSED_DUPLICATE_WINDOWS_PASSWORD_CHANGE
//
// MessageText:
//
//  Suppressed duplicate Windows password change.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3
//
#define SSO_INFO_SUPPRESSED_DUPLICATE_WINDOWS_PASSWORD_CHANGE 0x400029ABL

//
// MessageId: SSO_WARN_NO_OLD_WINDOWS_PASSWORD
//
// MessageText:
//
//  Cannot change the Windows password because the old Windows password for this account is not available in the SSO database.%r
//  Use the SSO admin tools to set the external credentials for this Windows account.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3
//
#define SSO_WARN_NO_OLD_WINDOWS_PASSWORD 0x800029ACL

//
// MessageId: SSO_WARN_CHANGE_WINDOWS_PASSWORD_FAILED
//
// MessageText:
//
//  Failed to change the Windows password.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3%r
//  Error Code: %4
//
#define SSO_WARN_CHANGE_WINDOWS_PASSWORD_FAILED 0x800029ADL

//
// MessageId: SSO_INFO_CHANGED_WINDOWS_PASSWORD
//
// MessageText:
//
//  Successfully changed the Windows password.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3
//
#define SSO_INFO_CHANGED_WINDOWS_PASSWORD 0x400029AEL

//
// MessageId: SSO_INFO_EXTERNAL_MAPPING_CONFLICT_ALLOWED
//
// MessageText:
//
//  A Windows password change will cause changes to more than one account on the same external system.%r
//  This is allowed because the adapter for this external system is configured to allow mapping conflicts.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3%r
//  External Account 1: %4%r
//  External Account 2: %5
//
#define SSO_INFO_EXTERNAL_MAPPING_CONFLICT_ALLOWED 0x400029AFL

//
// MessageId: SSO_WARN_EXTERNAL_MAPPING_CONFLICT_NOT_ALLOWED
//
// MessageText:
//
//  A Windows password change would have caused changes to more than one account on the same external system.%r
//  This has been prevented because the adapter for this external system is configured to not allow mapping conflicts.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3%r
//  External Account 1: %4%r
//  External Account 2: %5
//
#define SSO_WARN_EXTERNAL_MAPPING_CONFLICT_NOT_ALLOWED 0x800029B0L

//
// MessageId: SSO_INFO_WINDOWS_MAPPING_CONFLICT_ALLOWED
//
// MessageText:
//
//  An external password change will cause changes to more than one Windows account.%r
//  This is allowed because the adapter for this external system is configured to allow mapping conflicts.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3%r
//  Windows Account 1: %4%r
//  Windows Account 2: %5
//
#define SSO_INFO_WINDOWS_MAPPING_CONFLICT_ALLOWED 0x400029B1L

//
// MessageId: SSO_WARN_WINDOWS_MAPPING_CONFLICT_NOT_ALLOWED
//
// MessageText:
//
//  An external password change would have caused changes to more than one Windows account.%r
//  This has been prevented because the adapter for this external system is configured to not allow mapping conflicts.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3%r
//  Windows Account 1: %4%r
//  Windows Account 2: %5
//
#define SSO_WARN_WINDOWS_MAPPING_CONFLICT_NOT_ALLOWED 0x800029B2L

//
// MessageId: SSO_INFO_WINDOWS_PASSWORD_SET
//
// MessageText:
//
//  The Windows password was successfully updated in the SSO database.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application Name: %3%r
//  External Account: %4
//
#define SSO_INFO_WINDOWS_PASSWORD_SET    0x400029B3L

//
// MessageId: SSO_ERROR_REQUIRE_OLD_PASSWORD
//
// MessageText:
//
//  External password change. When changing the password for an external account the adapter must supply the old password.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3
//
#define SSO_ERROR_REQUIRE_OLD_PASSWORD   0xC00029B4L

//
// MessageId: SSO_WARN_NO_EXISTING_PASSWORD
//
// MessageText:
//
//  External password change. The old password cannot be verified because there are no existing credentials for this external account.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4
//
#define SSO_WARN_NO_EXISTING_PASSWORD    0x800029B5L

//
// MessageId: SSO_WARN_PASSWORD_MISMATCH
//
// MessageText:
//
//  External password change. The old password supplied by the adapter does not match the existing password for the external account.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4
//
#define SSO_WARN_PASSWORD_MISMATCH       0x800029B6L

//
// MessageId: SSO_WARN_PS_MAPPING_DISABLED
//
// MessageText:
//
//  External password change. The password was not changed for the external account because the mapping is disabled.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4
//
#define SSO_WARN_PS_MAPPING_DISABLED     0x800029B7L

//
// MessageId: SSO_WARN_PS_GET_CREDS_FAILED
//
// MessageText:
//
//  The password was not changed for the external account because the existing external credentials could not be obtained from the SSO database.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4%r
//  Error Code: %5
//
#define SSO_WARN_PS_GET_CREDS_FAILED     0x800029B8L

//
// MessageId: SSO_WARN_NO_WINDOWS_PASSWORD_CHANGE
//
// MessageText:
//
//  External password change. The Windows password was not changed because one or more of the external account changes failed.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3
//
#define SSO_WARN_NO_WINDOWS_PASSWORD_CHANGE 0x800029B9L

//
// MessageId: SSO_WARN_REPLAY_INVALID_DIR_FOUND
//
// MessageText:
//
//  A directory was found in the replay files directory - it will be ignored.%r
//  Directory: %1
//
#define SSO_WARN_REPLAY_INVALID_DIR_FOUND 0x800029BAL

//
// MessageId: SSO_WARN_REPLAY_FILE_DELETED_TOO_OLD
//
// MessageText:
//
//  A replay file was deleted because it was too old.%r
//  Replay File: %1
//
#define SSO_WARN_REPLAY_FILE_DELETED_TOO_OLD 0x800029BBL

// AVAILABLE!!!
//MessageId=10684
//Severity=Warning
//SymbolicName=SSO_WARN_REPLAY_FILE_NAME_BAD_FORMAT
//Language=English
//Incorrect format for the replay file name. It has been deleted.%r
//Replay File: %1
//.
//
// MessageId: SSO_WARN_REPLAY_CANNOT_OPEN
//
// MessageText:
//
//  Could not open the replay or progress file.%r
//  File Name: %1%r
//  Error Code: %2
//
#define SSO_WARN_REPLAY_CANNOT_OPEN      0x800029BDL

//
// MessageId: SSO_INFO_REPLAY_STARTED
//
// MessageText:
//
//  Replaying stored password changes.%r
//  Replay File: %1
//
#define SSO_INFO_REPLAY_STARTED          0x400029BEL

//
// MessageId: SSO_INFO_REPLAY_COMPLETE
//
// MessageText:
//
//  Replay of stored password changes completed.%r
//  Replay File: %1%r
//  Additional Data: %2
//
#define SSO_INFO_REPLAY_COMPLETE         0x400029BFL

//
// MessageId: SSO_ERROR_REPLAY_FILE_UNEXPECTED_DATA
//
// MessageText:
//
//  Corruption was detected in the replay file.%r
//  Replay File: %1
//
#define SSO_ERROR_REPLAY_FILE_UNEXPECTED_DATA 0xC00029C0L

//
// MessageId: SSO_ERROR_REPLAY_INCORRECT_RECORD_VERSION
//
// MessageText:
//
//  Corruption was detected in the replay file.%r
//  Replay File: %1
//
#define SSO_ERROR_REPLAY_INCORRECT_RECORD_VERSION 0xC00029C1L

//
// MessageId: SSO_ERROR_REPLAY_INCORRECT_RECORD_NUMBER
//
// MessageText:
//
//  Corruption was detected in the replay file.%r
//  Replay File: %1
//
#define SSO_ERROR_REPLAY_INCORRECT_RECORD_NUMBER 0xC00029C2L

//
// MessageId: SSO_ERROR_REPLAY_FILE_MISMATCH
//
// MessageText:
//
//  Corruption was detected in the replay file.%r
//  Replay File: %1%r
//  Additional Data: %2
//
#define SSO_ERROR_REPLAY_FILE_MISMATCH   0xC00029C3L

//
// MessageId: SSO_WARN_REPLAY_ACCESS_DENIED
//
// MessageText:
//
//  The password change cannot be replayed because the original caller is no longer a member of the access account for the adapter.%r
//  Replay File: %1%r
//  Adapter: %2%r
//  Original Caller: %3%r
//  Access Account: %4
//
#define SSO_WARN_REPLAY_ACCESS_DENIED    0x800029C4L

//
// MessageId: SSO_WARNING_REPLAY_CANNOT_CREATE
//
// MessageText:
//
//  Could not create the replay or progress file.%r
//  File Name: %1%r
//  Error Code: %2
//
#define SSO_WARNING_REPLAY_CANNOT_CREATE 0x800029C5L

//
// MessageId: SSO_ERROR_REPLAY_INCORRECT_VERSION
//
// MessageText:
//
//  Corruption was detected in the replay or progress file.%r
//  File Name: %1
//
#define SSO_ERROR_REPLAY_INCORRECT_VERSION 0xC00029C6L

//
// MessageId: SSO_ERROR_REPLAY_INCORRECT_FILE_NAME
//
// MessageText:
//
//  Corruption was detected in the replay or progress file.%r
//  File Name: %1%r
//  Decrypted File Name: %2
//
#define SSO_ERROR_REPLAY_INCORRECT_FILE_NAME 0xC00029C7L

//
// MessageId: SSO_ERROR_PROGRESS_INCORRECT_RECORD_VERSION
//
// MessageText:
//
//  Corruption was detected in the progress file.%r
//  File Name: %1
//
#define SSO_ERROR_PROGRESS_INCORRECT_RECORD_VERSION 0xC00029C8L

//
// MessageId: SSO_ERROR_PROGRESS_FILE_MISMATCH
//
// MessageText:
//
//  Corruption was detected in the progress file.%r
//  Replay File: %1%r
//  Additional Data: %2
//
#define SSO_ERROR_PROGRESS_FILE_MISMATCH 0xC00029C9L

//
// MessageId: SSO_INFO_REPLAY_FILE_DELETED
//
// MessageText:
//
//  The replay or progress file was deleted.%r
//  File Name: %1
//
#define SSO_INFO_REPLAY_FILE_DELETED     0x400029CAL

//
// MessageId: SSO_INFO_EXTERNAL_PASSWORD_CHANGE_RECEIVED_REPLAY
//
// MessageText:
//
//  A password change was received from an adapter (from replay file).%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3%r
//  Client User: %4%r
//  Record Number: %5
//
#define SSO_INFO_EXTERNAL_PASSWORD_CHANGE_RECEIVED_REPLAY 0x400029CBL

// HISSO event log messages
//
// MessageId: ENTSSO_LOG_HISSO_SYSTEM_NOT_ENABLED
//
// MessageText:
//
//  A call to LogonExternalUser failed because the SSO system is not enabled for Host Initiated Single Sign-On.%r
//  Application Name: %1%r
//  External Account: %2
//
#define ENTSSO_LOG_HISSO_SYSTEM_NOT_ENABLED 0x800029CCL

//
// MessageId: ENTSSO_LOG_HISSO_APP_NOT_ENABLED
//
// MessageText:
//
//  A call to LogonExternalUser failed because the application is not enabled for Host Initiated Single Sign-On.%r
//  Application Name: %1%r
//  External Account: %2
//
#define ENTSSO_LOG_HISSO_APP_NOT_ENABLED 0x800029CDL

//
// MessageId: ENTSSO_LOG_HISSO_INVALID_CREDENTIALS
//
// MessageText:
//
//  A call to LogonExternalUser failed because the specified credentials are invalid or do not match those in the SSO database.%r
//  Application Name: %1%r
//  External Account: %2
//
#define ENTSSO_LOG_HISSO_INVALID_CREDENTIALS 0x800029CEL

//
// MessageId: ENTSSO_LOG_HISSO_LSA_INITIALIZATION_FAILED
//
// MessageText:
//
//  A call to LogonExternalUser failed because the service failed to connect to the LSA server.%r
//  Error Code: %1
//
#define ENTSSO_LOG_HISSO_LSA_INITIALIZATION_FAILED 0x800029CFL

//
// MessageId: ENTSSO_LOG_HISSO_LSA_S4U_FAILED
//
// MessageText:
//
//  A call to LogonExternalUser failed because the LSA server returned an error.%r
//  Application Name: %1%r
//  External Account: %2%r
//  Error Code: %3
//
#define ENTSSO_LOG_HISSO_LSA_S4U_FAILED  0x800029D0L

//
// MessageId: SSO_WARN_PS_NOT_ADAPTER
//
// MessageText:
//
//  The specified adapter is not an adapter application. Check the application type.%r
//  Adapter: %1
//
#define SSO_WARN_PS_NOT_ADAPTER          0x800029D1L

//
// MessageId: SSO_WARN_PS_NO_GLOBAL_SYNC
//
// MessageText:
//
//  Group adapters require password sync to be allowed by the global flags. Check the global flags.%r
//  Adapter: %1
//
#define SSO_WARN_PS_NO_GLOBAL_SYNC       0x800029D2L

//
// MessageId: SSO_WARN_PS_NO_APP_SYNC
//
// MessageText:
//
//  Password sync flags for this adapter must allow password sync and must match the global flags. Check the adapter flags and the global flags.%r
//  Adapter: %1
//
#define SSO_WARN_PS_NO_APP_SYNC          0x800029D3L

//
// MessageId: SSO_WARN_PS_NO_WIN_SYNC
//
// MessageText:
//
//  Password sync from Windows is not allowed. Check the global flags.%r
//  Tracking ID: %1%r
//  Client User: %2
//
#define SSO_WARN_PS_NO_WIN_SYNC          0x800029D4L

//
// MessageId: SSO_WARN_PS_WIN_ACCESS_DENIED
//
// MessageText:
//
//  Windows password changes will only be accepted from Domain Controllers in the access domain.%r
//  Tracking ID: %1%r
//  Client User: %2%r
//  Access Domain: %3%r
//  Access Sid: %4
//
#define SSO_WARN_PS_WIN_ACCESS_DENIED    0x800029D5L

//
// MessageId: SSO_INFO_PS_WIN_CHANGE_DAMPED
//
// MessageText:
//
//  A Windows password change was damped (detected as a duplicate and discarded).%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Client User: %3
//
#define SSO_INFO_PS_WIN_CHANGE_DAMPED    0x400029D6L

//
// MessageId: SSO_WARN_PS_MISSING_INITIAL_CREDS
//
// MessageText:
//
//  External password change. Some missing external credential fields for this mapping have been set to default values.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4
//
#define SSO_WARN_PS_MISSING_INITIAL_CREDS 0x800029D7L

//
// MessageId: SSO_WARN_PS_NOTIFICATION_TOO_OLD
//
// MessageText:
//
//  A notification was discarded because it was too old.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Notification Type: %3%r
//  Additional Data: %4
//
#define SSO_WARN_PS_NOTIFICATION_TOO_OLD 0x800029D8L

//
// MessageId: SSO_INFO_PS_DUPLICATE_EXTERNAL_CHANGE
//
// MessageText:
//
//  Suppressed duplicate external password change.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3
//
#define SSO_INFO_PS_DUPLICATE_EXTERNAL_CHANGE 0x400029D9L

//
// MessageId: SSO_WARN_PS_NOTIFICATION_RETRIES_EXPIRED
//
// MessageText:
//
//  Retries expired, discarding notification.%r
//  Tracking ID: %1%r
//  Adapter: %2
//
#define SSO_WARN_PS_NOTIFICATION_RETRIES_EXPIRED 0x800029DAL

//
// MessageId: SSO_WARN_NO_CREATE_ADAPTER
//
// MessageText:
//
//  The client user must be a member of the SSO Admin account to create an adapter.%r
//  Client User: %1%r
//  SSO Admin account: %2%r
//  Adapter: %3%r
//  Error Code: %4
//
#define SSO_WARN_NO_CREATE_ADAPTER       0x800029DBL

//
// MessageId: SSO_INFO_NEW_REPLAY_DIR
//
// MessageText:
//
//  Successfully created a new replay files directory.%r
//  Client User: %1%r
//  Replay Files Directory: %2
//
#define SSO_INFO_NEW_REPLAY_DIR          0x400029DCL

//
// MessageId: SSO_ERROR_NEW_REPLAY_DIR_FAILED
//
// MessageText:
//
//  Failed to create the replay files directory.%r
//  Client User: %1%r
//  Replay Files Directory: %2%r
//  Error Code: %3
//
#define SSO_ERROR_NEW_REPLAY_DIR_FAILED  0xC00029DDL

//
// MessageId: SSO_ERROR_REPLAY_INCORRECT_ADAPTER
//
// MessageText:
//
//  Corruption was detected in the replay or progress file.%r
//  File Name: %1%r
//  Clear Adapter Name: %2%r
//  Decrypted Adapter Name: %3
//
#define SSO_ERROR_REPLAY_INCORRECT_ADAPTER 0xC00029DEL

//
// MessageId: SSO_INFO_REPLAY_FILE_CREATED
//
// MessageText:
//
//  A new replay file was sucessfully created.%r
//  Adapter: %1%r
//  File Name: %2
//
#define SSO_INFO_REPLAY_FILE_CREATED     0x400029DFL

//
// MessageId: SSO_INFO_REPLAY_RECORD_WRITTEN
//
// MessageText:
//
//  A password change was successfully stored in the replay file.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3%r
//  File Name: %4%r
//  Record Number: %5
//
#define SSO_INFO_REPLAY_RECORD_WRITTEN   0x400029E0L

//
// MessageId: SSO_INFO_REPLAY_FILE_CLOSED_FULL
//
// MessageText:
//
//  The replay file has been closed because it is full.%r
//  File Name: %1
//
#define SSO_INFO_REPLAY_FILE_CLOSED_FULL 0x400029E1L

//
// MessageId: SSO_INFO_REPLAY_FILE_CLOSED_IDLE
//
// MessageText:
//
//  The replay file has been closed because it is not currently in use.%r
//  File Name: %1
//
#define SSO_INFO_REPLAY_FILE_CLOSED_IDLE 0x400029E2L

//
// MessageId: SSO_ERROR_REPLAY_SECURITY_1
//
// MessageText:
//
//  The security on the replay files directory does not match the security on the replay or progress file.%r
//  File Name: %1%r
//  File Security: %2%r
//  Directory Security: %3
//
#define SSO_ERROR_REPLAY_SECURITY_1      0xC00029E3L

//
// MessageId: SSO_ERROR_REPLAY_SECURITY_2
//
// MessageText:
//
//  The security on the replay or progress file has been changed from its original value.%r
//  File Name: %1%r
//  Current File Security: %2%r
//  Original File Security: %3
//
#define SSO_ERROR_REPLAY_SECURITY_2      0xC00029E4L

//
// MessageId: SSO_ERROR_REPLAY_SECURITY_3
//
// MessageText:
//
//  The security on the replay files directory does not match the security on the replay or progress file.%r
//  File Name: %1%r
//  File Security: %2%r
//  Directory Security: %3
//
#define SSO_ERROR_REPLAY_SECURITY_3      0xC00029E5L

//
// MessageId: SSO_ERROR_REPLAY_CORRUPTION
//
// MessageText:
//
//  Corruption was detected in the replay or progress file.%r
//  File Name: %1%r
//  Additional Data: %2%r
//  Error Code: %3
//
#define SSO_ERROR_REPLAY_CORRUPTION      0xC00029E6L

//
// MessageId: SSO_WARN_REPLAY_RECORD_FAILED
//
// MessageText:
//
//  Replay of the stored password change failed.%r
//  Adapter: %1%r
//  File Name: %2%r
//  Error Code: %3
//
#define SSO_WARN_REPLAY_RECORD_FAILED    0x800029E7L

//
// MessageId: SSO_ERROR_VERSION
//
// MessageText:
//
//  This version of the SSO server is not compatible with the SSO database. Please upgrade your master secret server.%r
//  SQL Server Name: %1%r
//  SSO Database Name: %2%r
//  SSO Database Version: %3%r
//  Required Version: %4
//
#define SSO_ERROR_VERSION                0xC00029E8L

//
// MessageId: SSO_INFO_PROGRESS_RECORD_WRITTEN
//
// MessageText:
//
//  A new progress file was sucessfully created.%r
//  Adapter: %1%r
//  File Name: %2%r
//  Record Number: %3
//
#define SSO_INFO_PROGRESS_RECORD_WRITTEN 0x400029E9L

//
// MessageId: SSO_INFO_PROGRESS_RECORD_READ
//
// MessageText:
//
//  The record number was successfully read from the progress file.%r
//  Adapter: %1%r
//  File Name: %2%r
//  Record Number: %3
//
#define SSO_INFO_PROGRESS_RECORD_READ    0x400029EAL

//
// MessageId: SSO_WARN_INVALID_USER_NOT_IN_GROUP
//
// MessageText:
//
//  A mapping could not be created because the specified user is not a member of the Application Users group.%r
//  Domain Name: %1%r
//  User Name: %2%r
//  Application Name: %3%r
//  Application Users group: %4%r
//  Error Code: %5
//
#define SSO_WARN_INVALID_USER_NOT_IN_GROUP 0x800029EBL

//
// MessageId: SSO_INFO_PS_SET_WINDOWS_PASSWORD
//
// MessageText:
//
//  The Windows password was successfully updated in the SSO database.%r
//  Tracking ID: %1%r
//  Windows Account: %2\%3
//
#define SSO_INFO_PS_SET_WINDOWS_PASSWORD 0x400029ECL

//
// MessageId: SSO_WARN_PS_SET_WINDOWS_PASSWORD
//
// MessageText:
//
//  Failed to update the Windows password in the SSO database.%r
//  Tracking ID: %1%r
//  Windows Account: %2\%3%r
//  Error Code: %4
//
#define SSO_WARN_PS_SET_WINDOWS_PASSWORD 0x800029EDL

//
// MessageId: SSO_INFO_PS_NOTIFICATION_QUEUED
//
// MessageText:
//
//  A notification was queued for an adapter.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3%r
//  Windows Account: %4\%5
//
#define SSO_INFO_PS_NOTIFICATION_QUEUED  0x400029EEL

//
// MessageId: SSO_WARN_PS_APP_DISABLED
//
// MessageText:
//
//  External password change. The password was not changed for the external account because the application is disabled.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4
//
#define SSO_WARN_PS_APP_DISABLED         0x800029EFL

//
// MessageId: SSO_INFO_PS_SET_EXTERNAL_PASSWORD
//
// MessageText:
//
//  The external password was successfully updated in the SSO database.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4
//
#define SSO_INFO_PS_SET_EXTERNAL_PASSWORD 0x400029F0L

//
// MessageId: SSO_WARN_PS_SET_EXTERNAL_PASSWORD
//
// MessageText:
//
//  Failed to update the external password in the SSO database.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4%r
//  Error Code: %5
//
#define SSO_WARN_PS_SET_EXTERNAL_PASSWORD 0x800029F1L

//
// MessageId: SSO_INFO_PS_SET_WINDOWS_PASSWORD_ADAPTER
//
// MessageText:
//
//  The Windows password was successfully updated in the SSO database.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3\%4
//
#define SSO_INFO_PS_SET_WINDOWS_PASSWORD_ADAPTER 0x400029F2L

//
// MessageId: SSO_WARN_PS_SET_WINDOWS_PASSWORD_ADAPTER
//
// MessageText:
//
//  Failed to update the Windows password in the SSO database.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3\%4%r
//  Error Code: %5
//
#define SSO_WARN_PS_SET_WINDOWS_PASSWORD_ADAPTER 0x800029F3L

//
// MessageId: SSO_WARN_INVALID_WINDOWS_USER
//
// MessageText:
//
//  The Windows Account is not valid for application update.%r
//  Application Name: %1%r
//  Windows Account: %2%r
//  Error Code: %3
//
#define SSO_WARN_INVALID_WINDOWS_USER    0x800029F4L

//
// MessageId: SSO_WARN_SAVING_REPLAY_FILE
//
// MessageText:
//
//  Saving replay or progress file.%r
//  Saved File: %1%r
//  Error Code: %2
//
#define SSO_WARN_SAVING_REPLAY_FILE      0x800029F5L

//
// MessageId: SSO_WARN_NO_UPDATE_ADAPTER
//
// MessageText:
//
//  The client user must be a member of the SSO Admin account or the Application Admin account to update the adapter.%r
//  Client User: %1%r
//  SSO Admin account: %2%r
//  Application Admin account: %3%r
//  Adapter: %4%r
//  Error Code: %5
//
#define SSO_WARN_NO_UPDATE_ADAPTER       0x800029F6L

//
// MessageId: SSO_ERROR_REPLAY_STOPPED
//
// MessageText:
//
//  Replay of stored password changes stopped due to errors.%r
//  Replay File: %1%r
//  Additional Data: %2%r
//  Error Code: %3
//
#define SSO_ERROR_REPLAY_STOPPED         0xC00029F7L

//
// MessageId: SSO_INFO_ADAPTER_ONLINE
//
// MessageText:
//
//  The adapter is online.%r
//  Adapter: %1
//
#define SSO_INFO_ADAPTER_ONLINE          0x400029F8L

//
// MessageId: SSO_ERROR_ADAPTER_OFFLINE
//
// MessageText:
//
//  The adapter is offline.%r
//  Adapter: %1
//
#define SSO_ERROR_ADAPTER_OFFLINE        0xC00029F9L

//
// MessageId: SSO_WARN_PASSWORD_EXPIRED
//
// MessageText:
//
//  The password for the external account has expired.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3
//
#define SSO_WARN_PASSWORD_EXPIRED        0x800029FAL

//
// MessageId: SSO_ERROR_UNKNOWN_NOTIFICATION
//
// MessageText:
//
//  An unknown notification type was received.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Notification Type: %3
//
#define SSO_ERROR_UNKNOWN_NOTIFICATION   0xC00029FBL

//
// MessageId: SSO_WARN_PS_ADAPTER_NOT_RUNNING
//
// MessageText:
//
//  Could not contact the destination adapter.
//  It may not be running or initialized.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  SSO Server Name: %3%r
//  Error Code: %4
//
#define SSO_WARN_PS_ADAPTER_NOT_RUNNING  0x800029FCL

//
// MessageId: SSO_WARN_PS_CLIENT_PING
//
// MessageText:
//
//  Could not contact the destination SSO server.
//  Check that the SSO service is running on that server and that it can be contacted.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  SSO Server Name: %3%r
//  Error Code: %4
//
#define SSO_WARN_PS_CLIENT_PING          0x800029FDL

//
// MessageId: SSO_ERROR_PS_WRONG_USER_NAME_TYPE
//
// MessageText:
//
//  PasswordChangeNotify: Incorrect User Name Type. The only accepted value is USER_NAME_TYPE_NT4.
//  The target is incorrectly configured in Active Directory.%r
//  User Name Type: %1%r
//  Client User: %2%r
//  Error Code: %3
//
#define SSO_ERROR_PS_WRONG_USER_NAME_TYPE 0xC00029FEL

// *** WARNING: 10,751 is last
// *** REMINDER *** UPDATE common\EventsToAuditLevels.cpp WITH AUDIT LEVELS FOR NEW EVENTS
// *** NEW EVENT RANGE (2) ***
// NOTE - this new range is not yet supported by EventsToAuditLevels.cpp stevejam 4/30/2004
//
// MessageId: SSO_ERROR_CHECK_GROUP
//
// MessageText:
//
//  Check group membership failed.%r
//  Group Name: %1%r
//  Account Name: %2%r
//  Additional Data: %3%r
//  Error Code: %4
//
#define SSO_ERROR_CHECK_GROUP            0xC0002B00L

//
// MessageId: SSO_WARN_NOT_SSO_ADMIN
//
// MessageText:
//
//  Client user is not a member of the SSO Administrators account.%r
//  Tracking ID: %1%r
//  Client User: %2\%3%r
//  SSO Administrators Account: %4
//
#define SSO_WARN_NOT_SSO_ADMIN           0x80002B01L

//
// MessageId: SSO_WARN_NOT_SSO_AFF_ADMIN
//
// MessageText:
//
//  Client user is not a member of the SSO Affiliate Administrators account.%r
//  Tracking ID: %1%r
//  Client User: %2\%3%r
//  SSO Affiliate Administrator Account: %4
//
#define SSO_WARN_NOT_SSO_AFF_ADMIN       0x80002B02L

//
// MessageId: SSO_WARN_NOT_APP_ADMIN
//
// MessageText:
//
//  Client user is not a member of the Application Administrators account.%r
//  Tracking ID: %1%r
//  Client User: %2\%3%r
//  Application Name: %4%r
//  Application Administrators Account: %5
//
#define SSO_WARN_NOT_APP_ADMIN           0x80002B03L

//
// MessageId: SSO_WARN_NOT_APP_ADMIN_ADMIN_SAME
//
// MessageText:
//
//  Client user is not a member of the Application Administrators account.%r
//  Tracking ID: %1%r
//  Client User: %2\%3%r
//  Application Name: %4%r
//  Application Administrators Account: %5
//
#define SSO_WARN_NOT_APP_ADMIN_ADMIN_SAME 0x80002B04L

//
// MessageId: SSO_WARN_NOT_APP_USER
//
// MessageText:
//
//  Client user is not a member of the Application Users account.%r
//  Tracking ID: %1%r
//  Client User: %2\%3%r
//  Application Name: %4%r
//  Application Users Account: %5
//
#define SSO_WARN_NOT_APP_USER            0x80002B05L

//
// MessageId: SSO_ERROR_ACCESS_CHECK
//
// MessageText:
//
//  Access check failed.%r
//  Client User: %1\%2%r
//  Application Name: %3%r
//  Additional Data: %4%r
//  Error Code: %5
//
#define SSO_ERROR_ACCESS_CHECK           0xC0002B06L

//
// MessageId: SSO_ERROR_EXTENDED_RPC_INFO
//
// MessageText:
//
//  RPC EXTENDED ERROR INFORMATION%r
//  %1%r
//  Error Code: %2
//
#define SSO_ERROR_EXTENDED_RPC_INFO      0xC0002B07L

//
// MessageId: SSO_ERROR_AUTHZ
//
// MessageText:
//
//  The AuthzInitializeContextFromSid function failed with ERROR_ACCESS_DENIED.
//  This means that the service account that the SSO server is running under does not 
//  have sufficient permissions to check group membership in Active Directory.
//  Please check your documentation for details on how to fix this problem.%r
//
#define SSO_ERROR_AUTHZ                  0xC0002B08L

//
// MessageId: SSO_PS_WARN_NOT_IN_GROUP_DELETE_OK
//
// MessageText:
//
//  The mapping is not valid because the Windows account is not in the Application Users group for the application.
//  The mapping has been deleted.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application Name: %3%r
//  Application Users group: %4
//
#define SSO_PS_WARN_NOT_IN_GROUP_DELETE_OK 0x80002B09L

//
// MessageId: SSO_PS_WARN_GROUP_CHECK_FAILED
//
// MessageText:
//
//  Failed to check whether the mapping was a member of the Application Users group.
//  The mapping will be ignored.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application Name: %3%r
//  Application Users group: %4%r
//  Error Code: %5
//
#define SSO_PS_WARN_GROUP_CHECK_FAILED   0x80002B0AL

//
// MessageId: SSO_PS_WARN_NOT_IN_GROUP_DELETE_FAILED
//
// MessageText:
//
//  The mapping is not valid because the Windows account is not in the Application Users group for the application.
//  Failed to delete the mapping. The mapping will be ignored.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application Name: %3%r
//  Application Users group: %4%r
//  Error Code: %5
//
#define SSO_PS_WARN_NOT_IN_GROUP_DELETE_FAILED 0x80002B0BL

//
// MessageId: SSO_INFO_WINDOWS_TO_WINDOWS_MAPPING_CONFLICT_ALLOWED
//
// MessageText:
//
//  A Windows password change will cause a different Windows account to be changed.%r
//  This is allowed because the adapter for this external system is configured to allow mapping conflicts.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  External Account: %3%r
//  Windows Account 1: %4%r
//  Windows Account 2: %5
//
#define SSO_INFO_WINDOWS_TO_WINDOWS_MAPPING_CONFLICT_ALLOWED 0x40002B0CL

//
// MessageId: SSO_INFO_EXTERNAL_TO_EXTERNAL_MAPPING_CONFLICT_ALLOWED
//
// MessageText:
//
//  An external password change will cause a different external account to be changed.%r
//  This is allowed because the adapter for this external system is configured to allow mapping conflicts.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3%r
//  External Account 1: %4%r
//  External Account 2: %5
//
#define SSO_INFO_EXTERNAL_TO_EXTERNAL_MAPPING_CONFLICT_ALLOWED 0x40002B0DL

//
// MessageId: SSO_WARN_EXTERNAL_TO_EXTERNAL_MAPPING_CONFLICT_NOT_ALLOWED
//
// MessageText:
//
//  An external password change would have caused a different external account to be changed.%r
//  This has been prevented because the adapter for this external system is configured to not allow mapping conflicts.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3%r
//  External Account 1: %4%r
//  External Account 2: %5
//
#define SSO_WARN_EXTERNAL_TO_EXTERNAL_MAPPING_CONFLICT_NOT_ALLOWED 0x80002B0EL

//
// MessageId: SSO_WARN_WINDOWS_PASSWORD_DELETED
//
// MessageText:
//
//  An invalid Windows password in the SSO database was deleted to prevent account lockout.%r
//  Use the SSO admin tools to set the external credentials for this Windows account.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Windows Account: %3
//
#define SSO_WARN_WINDOWS_PASSWORD_DELETED 0x80002B0FL

// NEW EVENT LOG MESSAGES FOR ENTSSO VERSION 3.0
//
// MessageId: SSO_ERROR_PS_ADMIN_CALLBACK_ACCESS_DENIED
//
// MessageText:
//
//  Password sync server (for admin) access denied.%r
//  Client User: %1
//
#define SSO_ERROR_PS_ADMIN_CALLBACK_ACCESS_DENIED 0xC0002B10L

//
// MessageId: SSO_WARN_INVALID_APP_TICKET_TIMEOUT
//
// MessageText:
//
//  The ticket time-out value is not valid for application update.%r
//  Application Name: %1%r
//  Ticket time-out: %2 minutes%r
//  Maximum ticket time-out: %3 minutes%r
//  Error Code: %4
//
#define SSO_WARN_INVALID_APP_TICKET_TIMEOUT 0x80002B11L

//
// MessageId: SSO_WARN_EXISTING_GROUP_MAPPING
//
// MessageText:
//
//  A new group mapping could not be created because there is a conflict with an existing mapping. Please delete the existing mapping and try again.%r
//  Existing Mapping: %1%r
//  New Mapping: %2%r
//  External Account: %3%r
//  Application Name: %4
//
#define SSO_WARN_EXISTING_GROUP_MAPPING  0x80002B12L

//
// MessageId: SSO_WARN_GROUP_GET_CREDS_FAILED
//
// MessageText:
//
//  GetCredentials for group application failed.%r
//  Application: %1%r
//  Application Users account: %2%r
//  Index: %3%r
//  Error Code: %4
//
#define SSO_WARN_GROUP_GET_CREDS_FAILED  0x80002B13L

//
// MessageId: SSO_INFO_GROUP_GET_CREDS_OK
//
// MessageText:
//
//  GetCredentials for group application succeeded.%r
//  Application: %1%r
//  Application Users account: %2%r
//  Index: %3
//
#define SSO_INFO_GROUP_GET_CREDS_OK      0x40002B14L

//
// MessageId: SSO_INFO_CASE_SENSITIVE
//
// MessageText:
//
//  The SSO database is case sensitive. The SSO server will run in case sensitive mode. See documentation for details.%r
//
#define SSO_INFO_CASE_SENSITIVE          0x40002B15L

//
// MessageId: SSO_INFO_PS_WIN_CHANGE_DISCARDED_NO_MAPPINGS
//
// MessageText:
//
//  A Windows password change was discarded because there are no mappings for this Windows account.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Client User: %3
//
#define SSO_INFO_PS_WIN_CHANGE_DISCARDED_NO_MAPPINGS 0x40002B16L

//
// MessageId: SSO_INFO_PS_WIN_CHANGE_INVALID_MAPPING
//
// MessageText:
//
//  Windows password change. A mapping for this Windows account has been detected but ignored because it is no longer valid.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application: %3%r
//  External Account: %4%r
//  Client User: %5
//
#define SSO_INFO_PS_WIN_CHANGE_INVALID_MAPPING 0x40002B17L

//
// MessageId: SSO_INFO_PS_WIN_CHANGE_MAPPING_DISABLED
//
// MessageText:
//
//  Windows password change. A mapping for this Windows account has been detected but ignored because it is disabled.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application: %3%r
//  External Account: %4%r
//  Client User: %5
//
#define SSO_INFO_PS_WIN_CHANGE_MAPPING_DISABLED 0x40002B18L

//
// MessageId: SSO_INFO_PS_WIN_CHANGE_APP_DISABLED
//
// MessageText:
//
//  Windows password change. A mapping for this Windows account has been detected but ignored because the application is disabled.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application: %3%r
//  External Account: %4%r
//  Client User: %5
//
#define SSO_INFO_PS_WIN_CHANGE_APP_DISABLED 0x40002B19L

//
// MessageId: SSO_INFO_PS_WIN_CHANGE_APP_INCORRECT_TYPE
//
// MessageText:
//
//  Windows password change. A mapping for this Windows account has been detected but ignored because the application is not the correct type.
//  Only 'Individual' type applications support Windows password sync.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application: %3%r
//  External Account: %4%r
//  Client User: %5
//
#define SSO_INFO_PS_WIN_CHANGE_APP_INCORRECT_TYPE 0x40002B1AL

//
// MessageId: SSO_INFO_PS_WIN_CHANGE_NO_ADAPTER
//
// MessageText:
//
//  Windows password change. A mapping for this Windows account has been detected but ignored because no adapter is 
//  configured for this application.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application: %3%r
//  External Account: %4%r
//  Client User: %5
//
#define SSO_INFO_PS_WIN_CHANGE_NO_ADAPTER 0x40002B1BL

//
// MessageId: SSO_INFO_PS_WIN_CHANGE_ADAPTER_NO_SYNC
//
// MessageText:
//
//  Windows password change. A mapping for this Windows account has been detected but ignored because the 
//  adapter configured for this application does not support password sync to external systems.%r
//  Tracking ID: %1%r
//  Windows Account: %2%r
//  Application: %3%r
//  Adapter: %4%r
//  Client User: %5
//
#define SSO_INFO_PS_WIN_CHANGE_ADAPTER_NO_SYNC 0x40002B1CL

//
// MessageId: SSO_INFO_PS_MAPPING_INVALID
//
// MessageText:
//
//  External password change. The password was not changed for the external account because the mapping is 
//  no longer valid.%r
//  Tracking ID: %1%r
//  Adapter: %2%r
//  Application Name: %3%r
//  External Account: %4
//
#define SSO_INFO_PS_MAPPING_INVALID      0x40002B1DL

//
// MessageId: SSO_ERROR_NO_LOCAL_SYSTEM
//
// MessageText:
//
//  The SSO service cannot run under the Local System account.%r
//
#define SSO_ERROR_NO_LOCAL_SYSTEM        0xC0002B1EL

//
// MessageId: SSO_ERROR_NO_LOCAL_SERVICE
//
// MessageText:
//
//  The SSO service cannot run under the Local Service account.%r
//
#define SSO_ERROR_NO_LOCAL_SERVICE       0xC0002B1FL

//
// MessageId: SSO_WARN_NETWORK_SERVICE
//
// MessageText:
//
//  The SSO service is running under the Network Service account. 
//  There are some special considerations for this account.
//  See your documentation for details.%r
//
#define SSO_WARN_NETWORK_SERVICE         0x80002B20L

//
// MessageId: SSO_WARN_LOCAL_ACCOUNT
//
// MessageText:
//
//  The SSO service is running under a local account.
//  This is not recommended and will limit the functionality of SSO.
//  See your documentation for details.%r
//  SSO service account: %1
//
#define SSO_WARN_LOCAL_ACCOUNT           0x80002B21L

//
// MessageId: SSO_WARN_ACCESS_DENIED_ACCOUNTS
//
// MessageText:
//
//  Access denied.
//  The client user must be a member of one of the following accounts to perform this function.%r
//  SSO Administrators: %1%r
//  SSO Affiliate Administrators: %2%r
//  Application Administrators: %3%r
//  Application Users: %4%r
//  Additional Data: %5
//
#define SSO_WARN_ACCESS_DENIED_ACCOUNTS  0x80002B22L

// 11,263 is last
// *** NEXT EVENT (NOT ERROR CODE!!!) GOES IMMEDIATELY ABOVE HERE ***
// *** FOR NEXT ERROR CODE SEE END OF FILE ***
// *** REMINDER *** UPDATE common\EventsToAuditLevels.cpp WITH AUDIT LEVELS FOR NEW EVENTS
// NOTE - this new range is not yet supported by EventsToAuditLevels.cpp stevejam 4/30/2004
// Error codes that are returned by ENTSSO methods
// FIX: bug # 56593: change event log message ids and error codes
//
// Allocated ranges are -
//
// 0x2900 (10,496) thru 0x29FF (10,751) for ENTSSO event log message ids (1)
// 0x2A00 (10,752) thru 0x2AFF (11,007) for ENTSSO error codes
// 0x2B00 (11,008) thru 0x2BFF (11,263) for ENTSSO event log message ids (2)
//
//
// MessageId: ENTSSO_E_APP_EXISTS
//
// MessageText:
//
//  The application already exists.
//
#define ENTSSO_E_APP_EXISTS              0xC0002A00L

//
// MessageId: ENTSSO_E_MAPPING_EXISTS
//
// MessageText:
//
//  The mapping already exists.
//
#define ENTSSO_E_MAPPING_EXISTS          0xC0002A01L

//
// MessageId: ENTSSO_E_SSO_DISABLED
//
// MessageText:
//
//  The SSO system is currently disabled.
//
#define ENTSSO_E_SSO_DISABLED            0xC0002A02L

//
// MessageId: ENTSSO_E_APP_DISABLED
//
// MessageText:
//
//  The application is currently disabled.
//
#define ENTSSO_E_APP_DISABLED            0xC0002A03L

//
// MessageId: ENTSSO_E_NO_APP
//
// MessageText:
//
//  The application does not exist.
//
#define ENTSSO_E_NO_APP                  0xC0002A04L

//
// MessageId: ENTSSO_E_NO_MAPPING
//
// MessageText:
//
//  The mapping does not exist. For Config Store applications, the config info has not been set.
//
#define ENTSSO_E_NO_MAPPING              0xC0002A05L

//
// MessageId: ENTSSO_E_FIELD_EXISTS
//
// MessageText:
//
//  The field already exists.
//
#define ENTSSO_E_FIELD_EXISTS            0xC0002A06L

//
// MessageId: ENTSSO_E_BACKUP_RESTORE_FAILED_MEDIA
//
// MessageText:
//
//  The file specified for backup or restore of the master secrets must be on an NTFS file system or removable media.
//
#define ENTSSO_E_BACKUP_RESTORE_FAILED_MEDIA 0xC0002A07L

//
// MessageId: ENTSSO_E_NO_EXISTING_VALUE
//
// MessageText:
//
//  The property bag contained a VT_NULL value to indicate that the existing property value should be retained, but there is no existing value.
//
#define ENTSSO_E_NO_EXISTING_VALUE       0xC0002A08L

//
// MessageId: ENTSSO_E_EMPTY_EXISTING_VALUE
//
// MessageText:
//
//  The property bag contained a VT_NULL value to indicate that the existing property value should be retained, but the existing value is empty.
//
#define ENTSSO_E_EMPTY_EXISTING_VALUE    0xC0002A09L

//
// MessageId: ENTSSO_E_WRONG_THREAD
//
// MessageText:
//
//  The SSO client component has been called on the wrong thread. It is currently locked to a thread because it has a transaction.
//
#define ENTSSO_E_WRONG_THREAD            0xC0002A0AL

//
// MessageId: ENTSSO_E_TRACKING_ID_MISMATCH
//
// MessageText:
//
//  The tracking ID does not match the transaction ID.
//
#define ENTSSO_E_TRACKING_ID_MISMATCH    0xC0002A0BL

//
// MessageId: ENTSSO_E_MAPPING_DISABLED
//
// MessageText:
//
//  The mapping is disabled.
//
#define ENTSSO_E_MAPPING_DISABLED        0xC0002A0CL

//
// MessageId: ENTSSO_E_NO_CREDENTIALS
//
// MessageText:
//
//  No credentials have been set for the mapping.
//
#define ENTSSO_E_NO_CREDENTIALS          0xC0002A0DL

//
// MessageId: ENTSSO_E_NOT_SECRET_SERVER
//
// MessageText:
//
//  This function can only be performed on the master secret server.
//
#define ENTSSO_E_NOT_SECRET_SERVER       0xC0002A0EL

//
// MessageId: ENTSSO_E_NO_SSO_SERVER
//
// MessageText:
//
//  Could not contact the SSO server '%1'. Check that the SSO service is running on that server.
//
#define ENTSSO_E_NO_SSO_SERVER           0xC0002A0FL

//
// MessageId: ENTSSO_E_OUT_OF_SERVICE
//
// MessageText:
//
//  Enterprise Single Sign-On is offline.
//
#define ENTSSO_E_OUT_OF_SERVICE          0xC0002A10L

//
// MessageId: ENTSSO_E_APP_INCOMPLETE_FIELDS
//
// MessageText:
//
//  The application cannot be enabled because the fields are incomplete for this application.
//
#define ENTSSO_E_APP_INCOMPLETE_FIELDS   0xC0002A11L

//
// MessageId: ENTSSO_E_MAPPING_DISABLED_ADMIN
//
// MessageText:
//
//  The mapping has been disabled by an administrator.
//
#define ENTSSO_E_MAPPING_DISABLED_ADMIN  0xC0002A12L

//
// MessageId: ENTSSO_E_NOT_ADMIN
//
// MessageText:
//
//  This function can only be performed by a local administrator.
//
#define ENTSSO_E_NOT_ADMIN               0xC0002A13L

//
// MessageId: ENTSSO_E_NOT_SSO_ADMIN
//
// MessageText:
//
//  The user is not a member of the specified SSO Admin account.
//
#define ENTSSO_E_NOT_SSO_ADMIN           0xC0002A14L

//
// MessageId: ENTSSO_E_SSO_MUST_BE_DISABLED
//
// MessageText:
//
//  Enterprise Single Sign-On must be disabled before performing this function.
//
#define ENTSSO_E_SSO_MUST_BE_DISABLED    0xC0002A15L

//
// MessageId: ENTSSO_E_SSO_NOT_IN_NEW_SSO_ADMIN
//
// MessageText:
//
//  The SSO service account is not a member of the specified SSO Admin account.
//
#define ENTSSO_E_SSO_NOT_IN_NEW_SSO_ADMIN 0xC0002A16L

//
// MessageId: ENTSSO_E_APP_ADMIN_SAME
//
// MessageText:
//
//  The Application Admin account for this application is specified to be the SSO Affiliate Admin account.
//
#define ENTSSO_E_APP_ADMIN_SAME          0xC0002A17L

//
// MessageId: ENTSSO_E_INVALID_ACCOUNT_FORMAT
//
// MessageText:
//
//  The format of the account name is not valid. Domain accounts must include the domain name. Local accounts must not include a domain or computer name.
//
#define ENTSSO_E_INVALID_ACCOUNT_FORMAT  0xC0002A18L

//
// MessageId: ENTSSO_E_NO_BUILTIN_ACCOUNTS
//
// MessageText:
//
//  The account name is not valid. Built-in accounts are not allowed.
//
#define ENTSSO_E_NO_BUILTIN_ACCOUNTS     0xC0002A19L

//
// MessageId: ENTSSO_E_DECRYPT_FAILED
//
// MessageText:
//
//  Decryption failed.
//
#define ENTSSO_E_DECRYPT_FAILED          0xC0002A1AL

//
// MessageId: ENTSSO_E_BACKUP_FILE_WRONG_PASSWORD
//
// MessageText:
//
//  An incorrect password was specified for the backup file.
//
#define ENTSSO_E_BACKUP_FILE_WRONG_PASSWORD 0xC0002A1BL

//
// MessageId: ENTSSO_E_INVALID_ACCOUNT
//
// MessageText:
//
//  The account name is not valid or does not exist.
//
#define ENTSSO_E_INVALID_ACCOUNT         0xC0002A1CL

//
// MessageId: ENTSSO_E_NO_LOCAL_ACCOUNTS
//
// MessageText:
//
//  Local accounts are not allowed for this application. Check your application flags.
//
#define ENTSSO_E_NO_LOCAL_ACCOUNTS       0xC0002A1DL

//
// MessageId: ENTSSO_E_INVALID_FLAGS
//
// MessageText:
//
//  The specified flags are not valid or are incompatible with each other.
//
#define ENTSSO_E_INVALID_FLAGS           0xC0002A1EL

//
// MessageId: ENTSSO_E_NO_SECRET
//
// MessageText:
//
//  Cannot perform encryption because the secret is not available from the master secret server. See the event log for related errors.
//
#define ENTSSO_E_NO_SECRET               0xC0002A1FL

//
// MessageId: ENTSSO_E_FILE_OPEN_FAILED
//
// MessageText:
//
//  Could not create or open the specified file. Check the file name and access permissions.
//
#define ENTSSO_E_FILE_OPEN_FAILED        0xC0002A20L

//
// MessageId: ENTSSO_E_DB_ACCESS
//
// MessageText:
//
//  An error occurred while attempting to access the SSO database.
//
#define ENTSSO_E_DB_ACCESS               0xC0002A21L

//
// MessageId: ENTSSO_E_ACCOUNT_NOT_VALID
//
// MessageText:
//
//  The account name is not valid or does not exist. See the event log (on computer '%1') for more details.
//
#define ENTSSO_E_ACCOUNT_NOT_VALID       0xC0002A22L

//
// MessageId: ENTSSO_E_FLAGS_NOT_VALID
//
// MessageText:
//
//  The specified flags are not valid or are incompatible with each other. See the event log (on computer '%1') for more details.
//
#define ENTSSO_E_FLAGS_NOT_VALID         0xC0002A23L

//
// MessageId: ENTSSO_E_DTC_IMPORT_FAILED1
//
// MessageText:
//
//  Could not import a DTC transaction. Please check that MSDTC is configured correctly for remote operation. See documentation for details.
//
#define ENTSSO_E_DTC_IMPORT_FAILED1      0xC0002A24L

//
// MessageId: ENTSSO_E_DTC_IMPORT_FAILED2
//
// MessageText:
//
//  Could not import a DTC transaction. Please check that MSDTC is configured correctly for remote operation. See the event log (on computer '%1') for more details.
//
#define ENTSSO_E_DTC_IMPORT_FAILED2      0xC0002A25L

//
// MessageId: ENTSSO_E_TICKET_EXPIRED
//
// MessageText:
//
//  The ticket has timed out.
//
#define ENTSSO_E_TICKET_EXPIRED          0xC0002A26L

// PASSWORD SYNC
//
// MessageId: ENTSSO_E_WRONG_STATE
//
// MessageText:
//
//  This function has been called in the wrong state.
//
#define ENTSSO_E_WRONG_STATE             0xC0002A30L

//
// MessageId: ENTSSO_E_INVALID_NOTIFICATION_TYPE
//
// MessageText:
//
//  The notification type is not valid.
//
#define ENTSSO_E_INVALID_NOTIFICATION_TYPE 0xC0002A31L

//
// MessageId: ENTSSO_E_WRONG_SESSION
//
// MessageText:
//
//  A request was received for the wrong session. It may have been disconnected.
//
#define ENTSSO_E_WRONG_SESSION           0xC0002A32L

//
// MessageId: ENTSSO_E_TIMESTAMP_TOO_OLD
//
// MessageText:
//
//  The request was rejected because the timestamp was too old.
//
#define ENTSSO_E_TIMESTAMP_TOO_OLD       0xC0002A33L

//
// MessageId: ENTSSO_E_INCORRECT_COMPUTER
//
// MessageText:
//
//  This adapter is not configured for this computer.
//
#define ENTSSO_E_INCORRECT_COMPUTER      0xC0002A34L

//
// MessageId: ENTSSO_E_ACCOUNT_NOT_MAPPED_FOR_ADAPTER
//
// MessageText:
//
//  No mappings were found for this external account on this adapter.
//
#define ENTSSO_E_ACCOUNT_NOT_MAPPED_FOR_ADAPTER 0xC0002A35L

//
// MessageId: ENTSSO_E_APP_ASSIGNED_TO_ADAPTER
//
// MessageText:
//
//  The application cannot be deleted because it is currently assigned to an adapter.
//
#define ENTSSO_E_APP_ASSIGNED_TO_ADAPTER 0xC0002A36L

//
// MessageId: ENTSSO_E_TOO_MANY_UNCONFIRMED_NOTIFICATIONS
//
// MessageText:
//
//  Too many unconfirmed notifications.
//
#define ENTSSO_E_TOO_MANY_UNCONFIRMED_NOTIFICATIONS 0xC0002A37L

// HISSO error messages
//
// MessageId: ENTSSO_E_HISSO_SYSTEM_NOT_ENABLED
//
// MessageText:
//
//  The SSO system is not enabled for Host Initiated Single Sign-On.
//
#define ENTSSO_E_HISSO_SYSTEM_NOT_ENABLED 0xC0002A38L

//
// MessageId: ENTSSO_E_HISSO_APP_NOT_ENABLED
//
// MessageText:
//
//  The application is not enabled for Host Initiated Single Sign-On.
//
#define ENTSSO_E_HISSO_APP_NOT_ENABLED   0xC0002A39L

//
// MessageId: ENTSSO_E_HISSO_INVALID_CREDENTIALS
//
// MessageText:
//
//  The specified credentials are invalid or do not match those in the SSO database.
//
#define ENTSSO_E_HISSO_INVALID_CREDENTIALS 0xC0002A3AL

//
// MessageId: ENTSSO_E_HISSO_LSA_INITIALIZATION_FAILED
//
// MessageText:
//
//  Failed to connect to the LSA server.
//
#define ENTSSO_E_HISSO_LSA_INITIALIZATION_FAILED 0xC0002A3BL

//
// MessageId: ENTSSO_E_HISSO_LSA_S4U_FAILED
//
// MessageText:
//
//  The LSA server returned an error.
//
#define ENTSSO_E_HISSO_LSA_S4U_FAILED    0xC0002A3CL

//
// MessageId: ENTSSO_E_WINDOWS_UPDATE_FAILED_MORE_RECENT
//
// MessageText:
//
//  The Windows password in the SSO database is more recent.
//
#define ENTSSO_E_WINDOWS_UPDATE_FAILED_MORE_RECENT 0xC0002A3DL

//
// MessageId: ENTSSO_E_REENCRYPT_IN_PROGRESS
//
// MessageText:
//
//  SSO database re-encryption is in progress.
//
#define ENTSSO_E_REENCRYPT_IN_PROGRESS   0xC0002A3EL

//
// MessageId: ENTSSO_E_WRONG_SECRET
//
// MessageText:
//
//  Incorrect master secret.
//
#define ENTSSO_E_WRONG_SECRET            0xC0002A3FL

//
// MessageId: ENTSSO_E_EXTERNAL_UPDATE_FAILED_MORE_RECENT
//
// MessageText:
//
//  The external credentials in the SSO database are more recent.
//
#define ENTSSO_E_EXTERNAL_UPDATE_FAILED_MORE_RECENT 0xC0002A40L

//
// MessageId: ENTSSO_E_NOTIFICATION_NOT_FOUND
//
// MessageText:
//
//  The specified notification was not found.
//
#define ENTSSO_E_NOTIFICATION_NOT_FOUND  0xC0002A41L

//
// MessageId: ENTSSO_E_AMBIGUOUS_SYNC_FIELDS
//
// MessageText:
//
//  The application must have two fields or only one field must be marked for sync.
//
#define ENTSSO_E_AMBIGUOUS_SYNC_FIELDS   0xC0002A42L

//
// MessageId: ENTSSO_E_MAPPING_CONFLICT
//
// MessageText:
//
//  The external account was not updated because there is a mapping conflict.
//
#define ENTSSO_E_MAPPING_CONFLICT        0xC0002A43L

//
// MessageId: ENTSSO_E_REQUIRE_OLD_PASSWORD
//
// MessageText:
//
//  When changing the password for an external account the adapter must supply the old password.
//
#define ENTSSO_E_REQUIRE_OLD_PASSWORD    0xC0002A44L

//
// MessageId: ENTSSO_E_ADAPTER_ASSIGNED_TO_GROUP_ADAPTER
//
// MessageText:
//
//  The adapter cannot be deleted because it is currently assigned to a group adapter.
//
#define ENTSSO_E_ADAPTER_ASSIGNED_TO_GROUP_ADAPTER 0xC0002A45L

// NEW ERROR CODES FOR ENTSSO VERSION 3.0
//
// MessageId: ENTSSO_E_NO_ADAPTER
//
// MessageText:
//
//  The specified adapter does not exist.
//
#define ENTSSO_E_NO_ADAPTER              0xC0002A46L

//
// MessageId: ENTSSO_E_PSADMIN_INVALID_APP_TYPE
//
// MessageText:
//
//  Invalid application type for application. Valid application types are 'Individual', 'Group' or 'Host Group'.
//
#define ENTSSO_E_PSADMIN_INVALID_APP_TYPE 0xC0002A47L

//
// MessageId: ENTSSO_E_PSADMIN_INVALID_ADAPTER_TYPE
//
// MessageText:
//
//  Invalid application type for adapter. Valid application type is 'Password Sync Adapter'.
//
#define ENTSSO_E_PSADMIN_INVALID_ADAPTER_TYPE 0xC0002A48L

//
// MessageId: ENTSSO_E_PSADMIN_APP_ALREADY_ASSIGNED
//
// MessageText:
//
//  The application is already assigned to an adapter.
//
#define ENTSSO_E_PSADMIN_APP_ALREADY_ASSIGNED 0xC0002A49L

//
// MessageId: ENTSSO_E_NO_GROUP_ADAPTER
//
// MessageText:
//
//  The specified group adapter does not exist.
//
#define ENTSSO_E_NO_GROUP_ADAPTER        0xC0002A4AL

//
// MessageId: ENTSSO_E_PSADMIN_INVALID_GROUP_ADAPTER_TYPE
//
// MessageText:
//
//  Invalid application type for group adapter. Valid application type is 'Password Sync Group Adapter'.
//
#define ENTSSO_E_PSADMIN_INVALID_GROUP_ADAPTER_TYPE 0xC0002A4BL

//
// MessageId: ENTSSO_E_PSADMIN_ADAPTER_ALREADY_ASSIGNED
//
// MessageText:
//
//  The adapter is already assigned to a group adapter.
//
#define ENTSSO_E_PSADMIN_ADAPTER_ALREADY_ASSIGNED 0xC0002A4CL

//
// MessageId: ENTSSO_E_PSADMIN_INVALID_ADAPTER_TYPE_2
//
// MessageText:
//
//  Invalid application type for adapter. Valid application types are 'Password Sync Adapter' or 'Password Sync Group Adapter'.
//
#define ENTSSO_E_PSADMIN_INVALID_ADAPTER_TYPE_2 0xC0002A4DL

//
// MessageId: ENTSSO_E_PSADMIN_ADAPTER_SAME_COMPUTER
//
// MessageText:
//
//  The specified adapter must be on the same computer as the group adapter.
//
#define ENTSSO_E_PSADMIN_ADAPTER_SAME_COMPUTER 0xC0002A4EL

//
// MessageId: ENTSSO_E_SSO_DATABASE_EXISTS
//
// MessageText:
//
//  The specified SSO database already exists.
//
#define ENTSSO_E_SSO_DATABASE_EXISTS     0xC0002A4FL

//
// MessageId: ENTSSO_E_SSO_DATABASE_REQUIRES_UPGRADE
//
// MessageText:
//
//  The specified SSO database exists but requires an upgrade.
//
#define ENTSSO_E_SSO_DATABASE_REQUIRES_UPGRADE 0xC0002A50L

//
// MessageId: ENTSSO_E_SSO_DATABASE_NOT_CONFIGURED
//
// MessageText:
//
//  The specified SSO database is not configured.
//
#define ENTSSO_E_SSO_DATABASE_NOT_CONFIGURED 0xC0002A51L

//
// MessageId: ENTSSO_E_SSO_NOT_SSO_ADMIN
//
// MessageText:
//
//  The current user is not a member of the specified SSO Admin account.
//
#define ENTSSO_E_SSO_NOT_SSO_ADMIN       0xC0002A52L

//
// MessageId: ENTSSO_E_NO_MAPPING2
//
// MessageText:
//
//  The mapping does not exist.
//
#define ENTSSO_E_NO_MAPPING2             0xC0002A53L

//
// MessageId: ENTSSO_E_FILE_PASSWORD_TOO_SHORT
//
// MessageText:
//
//  The specified password is too short, it must be at least 7 characters.
//
#define ENTSSO_E_FILE_PASSWORD_TOO_SHORT 0xC0002A54L

// NEXT ERROR CODE (NOT NEXT EVENT!!!) GOES IMMEDIATELY ABOVE HERE
// *** FOR NEXT EVENT SEE ABOVE THIS SECTION ***
