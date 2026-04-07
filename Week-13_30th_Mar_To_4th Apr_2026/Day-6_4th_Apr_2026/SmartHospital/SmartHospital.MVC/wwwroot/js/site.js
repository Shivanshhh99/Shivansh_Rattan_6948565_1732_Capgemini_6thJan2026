// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Sidebar toggle
document.addEventListener('DOMContentLoaded', function () {
    const sidebar = document.getElementById('sidebar');
    const toggle = document.getElementById('sidebarToggle');
    const mainContent = document.getElementById('mainContent');

    if (toggle && sidebar) {
        toggle.addEventListener('click', function () {
            if (window.innerWidth <= 768) {
                sidebar.classList.toggle('mobile-open');
            } else {
                sidebar.classList.toggle('collapsed');
                if (sidebar.classList.contains('collapsed')) {
                    mainContent.style.marginLeft = 'var(--sidebar-collapsed)';
                } else {
                    mainContent.style.marginLeft = 'var(--sidebar-width)';
                }
            }
        });
    }

    // Auto-dismiss toasts
    setTimeout(function () {
        document.querySelectorAll('.alert-toast').forEach(el => {
            el.style.opacity = '0';
            setTimeout(() => el.remove(), 500);
        });
    }, 4000);

    // Highlight active nav link
    const currentPath = window.location.pathname.toLowerCase();
    document.querySelectorAll('.sidebar-nav .nav-link').forEach(link => {
        if (link.getAttribute('href') && currentPath.includes(link.getAttribute('href').split('/')[1]?.toLowerCase())) {
            link.classList.add('active');
        }
    });

    // Password strength indicator
    const passwordField = document.getElementById('Password');
    if (passwordField) {
        passwordField.addEventListener('input', function () {
            const val = this.value;
            const strength = calculateStrength(val);
            const bar = document.getElementById('passwordStrengthBar');
            const label = document.getElementById('passwordStrengthLabel');
            if (bar && label) {
                const levels = ['', 'Weak', 'Fair', 'Good', 'Strong'];
                const colors = ['', '#dc3545', '#fd7e14', '#ffc107', '#198754'];
                const widths = ['0%', '25%', '50%', '75%', '100%'];
                bar.style.width = widths[strength];
                bar.style.background = colors[strength];
                label.textContent = levels[strength];
                label.style.color = colors[strength];
            }
        });
    }
});

function calculateStrength(password) {
    let score = 0;
    if (password.length >= 8) score++;
    if (/[A-Z]/.test(password)) score++;
    if (/\d/.test(password)) score++;
    if (/[@$!%*?&]/.test(password)) score++;
    return score;
}

// Confirm delete
function confirmDelete(form, name) {
    if (confirm(`Are you sure you want to delete "${name}"? This action cannot be undone.`)) {
        form.submit();
    }
}

// Filter doctors by department
function filterByDepartment(val) {
    const url = val ? `/Doctors?departmentId=${val}` : '/Doctors';
    window.location.href = url;
}