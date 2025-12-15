using Serenity;
using Microsoft.Extensions.Configuration;

namespace AdvanceCRM
{
    public partial class Texts
    {
        private static IConfiguration _configuration;

        public Texts(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static class Db
        {
            public static class Administration
            {
                public static class Translation
                {
                    public static LocalText EntityPlural = "Translations";
                    public static LocalText Key = "Local Text Key";
                    public static LocalText SourceLanguage = "Source Language";
                    public static LocalText SourceText = "Effective Translation in Source Language";
                    public static LocalText TargetLanguage = "Target Language";
                    public static LocalText TargetText = "Effective Translation in Target Language";
                    public static LocalText CustomText = "User Translation in Target Language";
                    public static LocalText OverrideConfirmation = "Overwrite user translation with clicked text?";
                    public static LocalText SaveChangesButton = "Save Changes";
                }
            }
        }

        public static class Forms
        {
            public static class Membership
            {
                public static class ChangePassword
                {
                    public static LocalText FormTitle = "Change Password";
                    public static LocalText SubmitButton = "Change Password";
                    public static LocalText Success = "Your password is changed.";
                }

                public static class ForgotPassword
                {
                    public static LocalText FormInfo = "Please enter the e-mail.";
                    public static LocalText FormTitle = "Forgot My Password";
                    public static LocalText SubmitButton = "Reset My Password";
                    public static LocalText Success = "An e-mail with password reset instructions is sent to your e-mail address.";
                    public static LocalText BackToLogin = "I remember my password";
                }

                public static class ResetPassword
                {
                    public static LocalText EmailSubject = "Password reset request";
                    public static LocalText FormTitle = "Reset Password";
                    public static LocalText SubmitButton = "Reset Password";
                    public static LocalText Success = "Your password is changed. Please login with your new password.";
                    public static LocalText BackToLogin = "I remember my password. Don't reset it!";
                }

                public static class Login
                {
                    static Login()
                    {
                        var settings = _configuration?.GetSection("WhiteLabelConfig");
                        if (settings != null && settings["WhiteLabel"].HasValue())
                        {
                            FormTitle = "Welcome to " + settings["WhiteLabel"] + " CRM";
                        }
                    }
                    public static LocalText FormTitle = "Welcome to BizPlus CRM";
                    public static LocalText SignInButton = "Sign In";
                    public static LocalText ForgotPassword = "Forgot password?";
                    public static LocalText RememberMe = "Remember Me";
                    public static LocalText OR = "OR";
                    public static LocalText FacebookButton = "Sign in using Facebook";
                    public static LocalText GoogleButton = "Sign in using Google+";
                }

                public static class SignUp
                {
                    public static LocalText ActivateEmailSubject = "Activate Your AdvanceCRM Account";
                    public static LocalText ActivationCompleteMessage = "Your account is now activated. Use the e-mail and password you entered at sign up.";
                    public static LocalText AcceptTerms = "I agree to the terms";
                    public static LocalText BackToLogin = "I already have an account.";
                    public static LocalText ConfirmEmail = "Confirm email";
                    public static LocalText ConfirmPassword = "Confirm password";
                    public static LocalText DisplayName = "Display Name";
                    public static LocalText Email = "E-mail";
                    public static LocalText FormInfo = "Enter your details to create an account.";
                    public static LocalText FormTitle = "Sign Up";
                    public static LocalText Password = "Password";
                    public static LocalText SubmitButton = "Sign Up";
                    public static LocalText Success = "Activation instructions have been sent to your e-mail.";
                }
            }
        }

        public static class Navigation
        {
            static Navigation()
            {
                var settings = _configuration?.GetSection("WhiteLabelConfig");
                if (settings != null && settings["WhiteLabel"].HasValue())
                {
                    SiteTitle = settings["WhiteLabel"] + " CRM";
                }
            }
            public static LocalText LogoutLink = "Logout";
            public static LocalText SiteTitle = "BizPlus CRM";
        }

        public static class Site
        {
            public static class AccessDenied
            {
                public static LocalText PageTitle = "Unauthorized Access";
                public static LocalText LackPermissions = "You don't have required permissions to access this page!";
                public static LocalText NotLoggedIn = "You need to be logged in to access this page!";
                public static LocalText ClickToChangeUser = "click here to login with another user...";
                public static LocalText ClickToLogin = "clik here to return to login page...";
            }

            public static class Dashboard
            {
                static Dashboard()
                {
                    var settings = _configuration?.GetSection("WhiteLabelConfig");
                    if (settings != null && settings["WhiteLabel"].HasValue())
                    {
                        ContentDescription = settings["WhiteLabel"] + " CRM<em>(" + settings["WhiteLabelSlogan"] + ")</em>";
                    }
                }
                public static LocalText ContentDescription = "BizPlus CRM<em>(We help grow your business)</em>";
            }

            public static class BasicProgressDialog
            {
                public static LocalText CancelTitle = "Operation cancelled. Waiting for in progress calls to complete...";
                public static LocalText PleaseWait = "Please wait...";
            }

            public static class BulkServiceAction
            {
                public static LocalText AllHadErrorsFormat = "All {0} record(s) that are processed had errors!";
                public static LocalText AllSuccessFormat = "Finished processing on {0} record(s) with success.";
                public static LocalText ConfirmationFormat = "Perform this operation on {0} selected record(s)?";
                public static LocalText ErrorCount = "{0} error(s)";
                public static LocalText NothingToProcess = "Please select some records to process!";
                public static LocalText SomeHadErrorsFormat = "Finished processing on {0} record(s) with success. {1} record(s) had errors!";
                public static LocalText SuccessCount = "{0} done";
            }

            public static class UserDialog
            {
                public static LocalText EditPermissionsButton = "Edit Permissions";
                public static LocalText EditRolesButton = "Edit Roles";
            }

            public static class UserRoleDialog
            {
                public static LocalText DialogTitle = "Edit User Roles ({0})";
                public static LocalText SaveSuccess = "Updated user roles.";
            }

            public static class UserPermissionDialog
            {
                public static LocalText DialogTitle = "Edit User Permissions ({0})";
                public static LocalText SaveSuccess = "Updated user permissions.";
                public static LocalText Permission = "Permission";
                public static LocalText Grant = "Grant";
                public static LocalText Revoke = "Revoke";
            }

            public static class RolePermissionDialog
            {
                public static LocalText EditButton = "Edit Permissions";
                public static LocalText DialogTitle = "Edit Role Permissions ({0})";
                public static LocalText SaveSuccess = "Updated role permissions.";
            }

            public static class Layout
            {
                static Layout()
                {
                    var settings = _configuration?.GetSection("WhiteLabelConfig");
                    if (settings != null && settings["WhiteLabel"].HasValue())
                    {
                        WhiteLabel = settings["WhiteLabel"];
                        WhiteLabelURL = settings["WhiteLabelURL"];
                        FooterInfo = "Powered By <a href='" + WhiteLabelURL + "' target='_blank'>" + WhiteLabel + "</a>";
                    }
                }
                public static LocalText WhiteLabel = "Bizplus";
                public static LocalText WhiteLabelURL = "https://www.bizpluscrm.com";
                public static LocalText FooterCopyright = "Copyright (c) " + System.DateTime.Now.Year + ".";
                public static LocalText FooterInfo = "Powered By <a href='https://www.bizpluscrm.com' target='_blank'>BizplusCRM</a>";
                public static LocalText FooterRights = "All rights reserved.";
                public static LocalText GeneralSettings = "General Settings";
                public static LocalText Language = "Language";
                public static LocalText Theme = "Theme";
                public static LocalText ThemeBlack = "Black";
                public static LocalText ThemeBlackLight = "Black Light";
                public static LocalText ThemeBlue = "Blue";
                public static LocalText ThemeBlueLight = "Blue Light";
                public static LocalText ThemeGreen = "Green";
                public static LocalText ThemeGreenLight = "Green Light";
                public static LocalText ThemePurple = "Purple";
                public static LocalText ThemePurpleLight = "Purple Light";
                public static LocalText ThemeRed = "Red";
                public static LocalText ThemeRedLight = "Red Light";
                public static LocalText ThemeYellow = "Yellow";
                public static LocalText ThemeYellowLight = "Yellow Light";
                public static LocalText ThemeAzure = "Azure";
                public static LocalText ThemeAzureLight = "Azure Light";
                public static LocalText ThemeCosmos = "Cosmos";
                public static LocalText ThemeCosmosLight = "Cosmos Light";
                public static LocalText ThemeGlassy = "Glassy";
                public static LocalText ThemeGlassyLight = "Glassy Light";
            }

            public static class ValidationError
            {
                public static LocalText Title = "ERROR";
            }

            public static class FavoriteViewsMixin
            {
                public static LocalText FavoriteViews = "Favorite Views";
                public static LocalText SaveView = "Save View";
                public static LocalText SaveButton = "Save";
            }
        }

        public static partial class Validation
        {
            public static LocalText AuthenticationError = "Invalid username or password!";
            public static LocalText CurrentPasswordMismatch = "Your current password is not valid!";
            public static LocalText MinRequiredPasswordLength = "Entered password doesn't have enough characters (min {0})!";
            public static LocalText InvalidResetToken = "Your token to reset your password is invalid or has expired!";
            public static LocalText InvalidActivateToken = "Your token to activate your account is invalid or has expired!";
            public static LocalText CantFindUserWithEmail = "Can't find a user with that e-mail adress!";
            public static LocalText EmailInUse = "Another user with this e-mail exists!";
            public static LocalText EmailConfirm = "Emails entered doesn't match!";
            public static LocalText DeleteForeignKeyError = "Can't delete record. '{0}' table has " +
                "records that depends on this one!";
            public static LocalText SavePrimaryKeyError = "Can't save record. There is another record with the same {1} value!";
        }
    }
}