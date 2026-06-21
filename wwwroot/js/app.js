const UI_TEXTS = {
    fa: {
        heroDesc: "به پورتال مدیریت دسترسی محلی سرور خوش آمدید. در این بخش می‌توانید به صورت امن تنظیمات حساب خود را پیکربندی کنید.",
        heroBtn: "ورود به پورتال تغییر رمز",
        lblUser: "نام کاربری",
        lblOld: "رمز عبور فعلی",
        lblNew: "رمز عبور جدید",
        lblConfirm: "تکرار رمز عبور جدید",
        btnSubmit: "به‌روزرسانی رمز عبور",
        backLink: "← بازگشت به صفحه اصلی",
        mismatch: "رمز عبور جدید با تکرار آن مطابقت ندارد.",
        aboutBtn: "درباره",
        aboutTitle: "مشخصات سامانه"
    },
    en: {
        heroDesc: "Welcome to the local server access portal. In this section, you can securely configure your account settings.",
        heroBtn: "Go to Password Portal",
        lblUser: "Username",
        lblOld: "Current Password",
        lblNew: "New Password",
        lblConfirm: "Confirm New Password",
        btnSubmit: "Update Password",
        backLink: "← Back to Home",
        mismatch: "New password confirmation does not match.",
        aboutBtn: "About",
        aboutTitle: "System Information"
    }
};

function toggleLanguage() {
    let currentLang = localStorage.getItem('lang') === 'en' ? 'fa' : 'en';
    localStorage.setItem('lang', currentLang);
    location.reload();
}

function toggleAboutModal() {
    const modal = document.getElementById('aboutModal');
    modal.style.display = modal.style.display === 'none' ? 'block' : 'none';
}

async function fetchSystemSettings() {
    try {
        const response = await fetch('/api/v1/system/settings');
        return await response.json();
    } catch {
        return { version: "v1.5.0", dev_fa: "توسعه یافته", dev_en: "Developed", header_fa: "مدیریت سرور", header_en: "Server Manager", logo: "SECUREPORTAL" };
    }
}

async function initLandingLanguage() {
    let lang = localStorage.getItem('lang') || 'fa';
    document.documentElement.dir = lang === 'fa' ? 'rtl' : 'ltr';
    
    const settings = await fetchSystemSettings();

    // ست کردن لوگو و هدرهای داینامیک
    document.getElementById('mainLogo').innerText = settings.logo;
    document.getElementById('langToggleBtn').innerText = lang === 'fa' ? 'English' : 'فارسی';
    document.getElementById('heroTitle').innerText = lang === 'fa' ? settings.header_fa : settings.header_en;
    document.getElementById('heroDesc').innerText = UI_TEXTS[lang].heroDesc;
    document.getElementById('heroBtn').innerText = UI_TEXTS[lang].heroBtn;
    
    document.querySelector('.about-link-text').innerText = UI_TEXTS[lang].aboutBtn;
    document.getElementById('aboutTitle').innerText = UI_TEXTS[lang].aboutTitle;
    document.getElementById('aboutVer').innerText = "Version: " + settings.version;
    document.getElementById('aboutDev').innerText = lang === 'fa' ? settings.dev_fa : settings.dev_en;
}

async function initFormLanguage() {
    let lang = localStorage.getItem('lang') || 'fa';
    document.documentElement.dir = lang === 'fa' ? 'rtl' : 'ltr';

    const settings = await fetchSystemSettings();
    document.getElementById('formTitle').innerText = lang === 'fa' ? settings.header_fa : settings.header_en;

    document.getElementById('lblUser').innerText = UI_TEXTS[lang].lblUser;
    document.getElementById('lblOld').innerText = UI_TEXTS[lang].lblOld;
    document.getElementById('lblNew').innerText = UI_TEXTS[lang].lblNew;
    document.getElementById('lblConfirm').innerText = UI_TEXTS[lang].lblConfirm;
    document.getElementById('btnSubmit').innerText = UI_TEXTS[lang].btnSubmit;
    document.getElementById('backLink').innerText = UI_TEXTS[lang].backLink;
}

async function handlePasswordSubmit(event) {
    event.preventDefault();
    let lang = localStorage.getItem('lang') || 'fa';
    
    const username = document.getElementById('username').value;
    const oldPassword = document.getElementById('oldPassword').value;
    const newPassword = document.getElementById('newPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    const alertBox = document.getElementById('alertBox');
    const loader = document.getElementById('loader');
    const form = document.getElementById('passwordForm');

    alertBox.style.display = 'none';

    if (newPassword !== confirmPassword) {
        alertBox.className = "alert alert-danger";
        alertBox.innerText = UI_TEXTS[lang].mismatch;
        alertBox.style.display = 'block';
        return;
    }

    loader.style.display = 'block';
    form.style.opacity = '0.5';

    try {
        const response = await fetch('/api/v1/auth/change-password', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, oldPassword, newPassword, confirmPassword, lang })
        });

        const result = await response.json();
        loader.style.display = 'none';
        form.style.opacity = '1';
        alertBox.style.display = 'block';

        if (response.ok && result.success) {
            alertBox.className = "alert alert-success";
            alertBox.innerText = lang === 'fa' ? result.message_fa : result.message_en;
            form.reset();
        } else {
            alertBox.className = "alert alert-danger";
            alertBox.innerText = lang === 'fa' ? result.message_fa : result.message_en;
        }
    } catch {
        loader.style.display = 'none';
        form.style.opacity = '1';
        alertBox.className = "alert alert-danger";
        alertBox.innerText = lang === 'fa' ? "خطا در ارتباط با سرور." : "Connection error.";
        alertBox.style.display = 'block';
    }
}
