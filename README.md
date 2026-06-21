# 🛡️ Local User Password Manager (lusrmgr.web)

A modern, high-performance, and secure dual-language web portal built with **.NET 10** to allow local Windows users to securely change their passwords via a web interface without administrative privileges.

> 🇮🇷 **راهنمای فارسی در پایین صفحه قرار دارد.** (Persian documentation is available at the bottom of the page).

---

## 🇺🇸 English Documentation

### ✨ Features
*   **Secure Password Changes**: Users change their own passwords natively via the Windows SAM database without needing administrator rights.
*   **Dual-Language UI**: Seamless toggle between English (LTR) and Persian (RTL) directly from the client-side.
*   **Modern Neon Cyberpunk UI**: Sleek, fully responsive UI mimicking enterprise server environments.
*   **Hardcoded Settings**: Core configurations (Headers, Developer Credits, Versioning) are easily managed within `Program.cs`.
*   **Isolated Error Logging**: Dynamic and secure logging outside the web application directory for maximum security.

### 🏗️ Project Architecture
The project follows a modern **SPA / Decoupled Web API** structure:
*   `Program.cs`: Lightweight backend serving static assets and a secure API endpoint (`/api/v1/auth/change-password`).
*   `wwwroot/`: Complete frontend housing HTML, CSS (Neon theme & Windows native `Calibri` fonts), and AJAX asynchronous JavaScript logic.

### 🚀 Production Deployment (Windows Server 2025)

#### 1. Pre-requisites
*   Download and install the official **[.NET 10 Hosting Bundle](https://microsoft.com)** on your Windows Server 2025 to register `AspNetCoreModuleV2` within IIS.

#### 2. Local Compilation (Publish)
Run the following command in your local project terminal using VS Code:
```bash
dotnet publish -c Release -r win-x64 --self-contained false -o ./publish
```

#### 3. File Deployment & Folder Permissions
1. Copy the contents of the `./publish` directory to your server's destination (e.g., `C:\inetpub\wwwroot`).
2. Remove any default IIS placeholder files (like `iisstart.htm`).
3. **Log Directory Security**:
   * Create the default log directory: `C:\inetpub\logs\lusrmgr.web`.
   * Right-click `C:\inetpub\logs\lusrmgr.web` > **Properties** > **Security** > **Edit**.
   * Grant **Write** / **Modify** permissions to the **`IIS_IUSRS`** group.
   * *Note: Keep `C:\inetpub\wwwroot` strictly Read-Only for enhanced server security.*

#### 4. IIS Configuration (Replacing Default Web Site)
1. Open **IIS Manager**.
2. Navigate to **Application Pools**, right-click **`DefaultAppPool`** > **Basic Settings**.
3. Change **.NET CLR Version** to **No Managed Code** and click OK.
4. Restart IIS using an elevated Command Prompt (`Run as Administrator`):
   ```cmd
   iisreset
   ```
5. Access your portal at `http://your-server-ip`.

---

## 🇮🇷 مستندات راهنمای فارسی

### ✨ ویژگی‌های کلیدی
*   **تغییر امن رمز عبور**: کاربران می‌توانند رمز عبور خود را به صورت بومی در دیتابیس SAM ویندوز و بدون نیاز به دسترسی Administrator تغییر دهند.
*   **پورتال دو زبانه**: امکان جابجایی آنی و هوشمند بین زبان‌های فارسی (RTL) و انگلیسی (LTR).
*   **رابط کاربری مدرن نئونی**: طراحی شیک، ریسپانسیو و الهام گرفته از محیط‌های اتاق سرور پیشرفته.
*   **تنظیمات یکپارچه (Hardcoded)**: مدیریت آسان عناوین هدرها، نام توسعه‌دهنده و نسخه نرم‌افزار به طور مستقیم از فایل `Program.cs`.
*   **جداسازی فایل‌های لاگ**: ذخیره‌سازی هوشمند خطاهای سیستم خارج از پوشه وب‌سایت جهت تامین بالاترین سطح امنیت سرور.

### 🏗️ معماری پروژه
این پروژه بر اساس معماری استاندارد و مدرن **تفکیک کامل فرانت‌اند و بک‌اند (Web API)** توسعه یافته است:
*   فایل `Program.cs`: بخش بک‌اند دات‌نت که وظیفه میزبانی فایل‌های استاتیک و ارائه API امن تغییر رمز را بر عهده دارد.
*   پوشه `wwwroot/`: بخش فرانت‌اند شامل ساختار HTML، استایل‌های نئونی، فونت‌های استاندارد ویندوز (`Calibri`) و اسکریپت‌های ناهمگام ارتباط با سرور (AJAX).

### 🚀 راهنمای استقرار در سرور عملیاتی (Windows Server 2025)

#### ۱. پیش‌نیازها
*   بسته رسمی **[.NET 10 Hosting Bundle](https://microsoft.com)** را دانلود و روی ویندوز سرور ۲۰۲۵ نصب کنید تا ماژول `AspNetCoreModuleV2` درون IIS ثبت شود.

#### ۲. کامپایل پروژه (Publish)
دستور زیر را در ترمینال سیستم خود اجرا کنید تا پوشه خروجی ساخته شود:
```bash
dotnet publish -c Release -r win-x64 --self-contained false -o ./publish
```

#### ۳. انتقال فایل‌ها و تنظیم مجوزهای امنیتی
1. کل محتویات داخل پوشه `./publish` سیستم خود را به پوشه مقصد در سرور (مانند `C:\inetpub\wwwroot`) منتقل کنید.
2. فایل‌های پیش‌فرض مایکروسافت (مانند `iisstart.htm`) را از پوشه وب سرور حذف کنید.
3. **امنیت پوشه لاگ**:
   * مسیر `C:\inetpub\logs\lusrmgr.web` را ایجاد کنید.
   * روی پوشه ساخته شده راست‌کلیک کرده و مسیر **Properties** > **Security** > **Edit** را دنبال کنید.
   * دسترسی **Write** (نوشتن) یا **Modify** را به گروه **`IIS_IUSRS`** اعطا کنید.
   * *نکته امنیتی: پوشه اصلی وب‌سایت (`C:\inetpub\wwwroot`) را برای افزایش امنیت سرور روی حالت Read-Only (فقط خواندنی) باقی بگذارید.*

#### ۴. پیکربندی وب‌سایت پیش‌فرض در IIS
1. برنامه **IIS Manager** را باز کنید.
2. از منوی سمت چپ به بخش **Application Pools** بروید. روی **`DefaultAppPool`** راست‌کلیک کرده و **Basic Settings** را بزنید.
3. گزینه **.NET CLR Version** را روی حالت **No Managed Code** تنظیم کرده و OK کنید.
4. خط فرمان (CMD) را به صورت `Run as Administrator` باز کرده و یک‌بار وب‌سرور را ریستارت کنید:
   ```cmd
   iisreset
   ```
5. اکنون پورتال شما با وارد کردن آی‌پاد سرور در مرورگر شبکه قابل دسترسی است.
