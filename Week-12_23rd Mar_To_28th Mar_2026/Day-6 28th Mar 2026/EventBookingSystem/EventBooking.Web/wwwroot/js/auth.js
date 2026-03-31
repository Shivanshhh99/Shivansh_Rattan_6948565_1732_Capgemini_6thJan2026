const API_BASE = 'http://localhost:5000';

// Token helpers
function getToken()   { return localStorage.getItem('jwtToken'); }
function getUser()    { return JSON.parse(localStorage.getItem('userInfo') || 'null'); }
function isLoggedIn() { return !!getToken(); }

function saveAuth(data) {
  localStorage.setItem('jwtToken', data.token);
  localStorage.setItem('userInfo', JSON.stringify({ email: data.email, fullName: data.fullName }));
}

function logout() {
  localStorage.removeItem('jwtToken');
  localStorage.removeItem('userInfo');
  window.location.href = '/';
}

// Authenticated fetch wrapper
async function apiFetch(url, options = {}) {
  const token = getToken();
  const headers = { 'Content-Type': 'application/json', ...options.headers };
  if (token) headers['Authorization'] = `Bearer ${token}`;

  const resp = await fetch(`${API_BASE}${url}`, { ...options, headers });
  if (resp.status === 401) {
    logout();
    throw new Error('Session expired. Please login again.');
  }
  return resp;
}

// Update navbar based on auth state
function updateNavbar() {
  const loggedIn = isLoggedIn();
  const user = getUser();

  document.getElementById('loginBtn')?.style.setProperty('display', loggedIn ? 'none' : 'inline-block');
  document.getElementById('registerBtn')?.style.setProperty('display', loggedIn ? 'none' : 'inline-block');
  document.getElementById('logoutBtn')?.style.setProperty('display', loggedIn ? 'inline-block' : 'none');
  document.getElementById('myBookingsNav')?.style.setProperty('display', loggedIn ? 'block' : 'none');

  const greeting = document.getElementById('userGreeting');
  if (greeting && user) {
    greeting.textContent = `Hi, ${user.fullName || user.email}`;
    greeting.style.display = 'inline';
  }
}

// Toast notification
function showToast(message, type = 'success') {
  let container = document.getElementById('toast-container');
  if (!container) {
    container = document.createElement('div');
    container.id = 'toast-container';
    document.body.appendChild(container);
  }

  const icons = { success: 'bi-check-circle-fill', danger: 'bi-exclamation-triangle-fill', info: 'bi-info-circle-fill' };
  const toast = document.createElement('div');
  toast.className = `toast align-items-center text-bg-${type} border-0 show mb-2`;
  toast.setAttribute('role', 'alert');
  toast.innerHTML = `
    <div class="d-flex">
      <div class="toast-body"><i class="bi ${icons[type] || ''} me-2"></i>${message}</div>
      <button type="button" class="btn-close btn-close-white me-2 m-auto" onclick="this.parentElement.parentElement.remove()"></button>
    </div>`;
  container.appendChild(toast);
  setTimeout(() => toast.remove(), 4000);
}

document.addEventListener('DOMContentLoaded', updateNavbar);