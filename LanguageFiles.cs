using System.Collections.Generic;

public static class LanguageFiles
{
    public static readonly Dictionary<string, Dictionary<string, string>> Dictionary = new()
    {
        { "fa", new Dictionary<string, string> {
            { "Title", "سامانه مدیریت هوشمند کاربران" },
            { "LandingDesc", "به پورتال مدیریت دسترسی محلی سرور خوش آمدید. در این بخش می‌توانید به صورت امن تنظیمات حساب خود را پیکربندی کنید." },
            { "BtnStart", "ورود به پورتال تغییر رمز" },
            { "ChangePassTitle", "تغییر رمز عبور کاربر محلی" },
            { "Username", "نام کاربری" },
            { "OldPass", "رمز عبور فعلی" },
            { "NewPass", "رمز عبور جدید" },
            { "ConfirmPass", "تکرار رمز عبور جدید" },
            { "BtnSubmit", "به‌روزرسانی رمز عبور" },
            { "MismatchErr", "رمز عبور جدید با تکرار آن مطابقت ندارد." },
            { "UserNotFound", "کاربر مورد نظر روی این سرور یافت نشد." },
            { "SuccessMsg", "رمز عبور با موفقیت تغییر یافت." },
            { "GeneralErr", "خطا در تغییر رمز عبور! لطفاً صحت اطلاعات و قوانین پیچیدگی رمز ویندوز را بررسی کنید." }
        }},
        { "en", new Dictionary<string, string> {
            { "Title", "Smart User Management System" },
            { "LandingDesc", "Welcome to the local server access portal. In this section, you can securely configure your account settings." },
            { "BtnStart", "Go to Password Portal" },
            { "ChangePassTitle", "Change Local User Password" },
            { "Username", "Username" },
            { "OldPass", "Current Password" },
            { "NewPass", "New Password" },
            { "ConfirmPass", "Confirm New Password" },
            { "BtnSubmit", "Update Password" },
            { "MismatchErr", "New password and confirmation do not match." },
            { "UserNotFound", "The requested user was not found on this server." },
            { "SuccessMsg", "Password updated successfully." },
            { "GeneralErr", "Error changing password! Please check inputs and Windows complexity rules." }
        }}
    };
}
