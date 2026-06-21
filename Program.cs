using System.IO;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// --- بخش تنظیمات هاردکد شده سیستم ---
const string APP_VERSION = "v1.5.0 (Enterprise Edition)";
const string DEVELOPER_INFO_FA = "توسعه یافته توسط علی ابوالحلاج. تمامی حقوق محفوظ است.";
const string DEVELOPER_INFO_EN = "Developed by Ali Abulhallaj. All rights reserved.";

const string HEADER_TITLE_FA = "سامانه مدیریت هوشمند سرور";
const string HEADER_TITLE_EN = "Smart Server Management System";

const string LOGO_TEXT = "SECUREPORTAL"; 

// مسیر فایل لاگ کاملاً داینامیک و قابل تغییر (مقدار پیش‌فرض مطابق درخواست شما)
const string LOG_FILE_PATH = @"C:\inetpub\logs\lusrmgr.web\error_log.txt";

// API ارسال تنظیمات به فرانت‌اند
app.MapGet("/api/v1/system/settings", () => Results.Ok(new
{
    version = APP_VERSION,
    dev_fa = DEVELOPER_INFO_FA,
    dev_en = DEVELOPER_INFO_EN,
    header_fa = HEADER_TITLE_FA,
    header_en = HEADER_TITLE_EN,
    logo = LOGO_TEXT
}));

// API پردازش فرم تغییر رمز عبور
app.MapPost("/api/v1/auth/change-password", async (PasswordChangeRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Username) || 
        string.IsNullOrWhiteSpace(request.OldPassword) || 
        string.IsNullOrWhiteSpace(request.NewPassword))
    {
        return Results.BadRequest(new { success = false, message_fa = "تمامی فیلدها الزامی هستند.", message_en = "All fields are required." });
    }

    if (request.NewPassword != request.ConfirmPassword)
    {
        return Results.BadRequest(new { success = false, message_fa = "رمز عبور جدید با تکرار آن مطابقت ندارد.", message_en = "New password confirmation does not match." });
    }

    try
    {
        using (var pc = new PrincipalContext(ContextType.Machine))
        {
            UserPrincipal user = UserPrincipal.FindByIdentity(pc, request.Username);
            if (user != null)
            {
                user.ChangePassword(request.OldPassword, request.NewPassword);
                return Results.Ok(new { success = true, message_fa = "رمز عبور با موفقیت تغییر یافت.", message_en = "Password updated successfully." });
            }
            else
            {
                LogError(request.Username, "User not found on this machine.");
                return Results.Json(new { success = false, message_fa = "کاربر مورد نظر روی این سرور یافت نشد.", message_en = "Requested user not found on this server." }, statusCode: 404);
            }
        }
    }
    catch (Exception ex)
    {
        LogError(request.Username, ex.Message);
        return Results.Json(new { success = false, message_fa = "خطا در تغییر رمز! لطفاً قوانین پیچیدگی رمز عبور ویندوز را بررسی کنید.", message_en = "Error! Please ensure the new password meets Windows complexity requirements." }, statusCode: 500);
    }
});

app.Run();

// ====================================================================
// متد ثبت لاگ در مسیر داینامیک و امن خارج از پوشه سایت
// ====================================================================
void LogError(string username, string errorMessage)
{
    try
    {
        // استخراج مسیر پوشه از روی آدرس فایل برای بررسی وجود یا عدم وجود آن
        string? directoryPath = Path.GetDirectoryName(LOG_FILE_PATH);

        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] User: {username} | Error: {errorMessage}{Environment.NewLine}";
        File.AppendAllText(LOG_FILE_PATH, logLine);
    }
    catch { }
}

public record PasswordChangeRequest(string Username, string OldPassword, string NewPassword, string ConfirmPassword, string Lang);
