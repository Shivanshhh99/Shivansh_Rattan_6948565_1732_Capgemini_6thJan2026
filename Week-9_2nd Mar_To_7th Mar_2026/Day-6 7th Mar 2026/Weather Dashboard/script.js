// Mock weather database with conditional logic
const weatherDB = {
  london:     { temp: 14, condition: 'Rainy',   icon: '🌧️', humidity: '82%', wind: '18 km/h', theme: 'rainy' },
  paris:      { temp: 22, condition: 'Sunny',   icon: '☀️', humidity: '45%', wind: '9 km/h',  theme: 'sunny' },
  tokyo:      { temp: 19, condition: 'Cloudy',  icon: '⛅', humidity: '65%', wind: '12 km/h', theme: 'cloudy' },
  newyork:    { temp: 17, condition: 'Windy',   icon: '🌬️', humidity: '55%', wind: '32 km/h', theme: 'windy' },
  mumbai:     { temp: 34, condition: 'Sunny',   icon: '🌤️', humidity: '75%', wind: '15 km/h', theme: 'sunny' },
  sydney:     { temp: 24, condition: 'Sunny',   icon: '☀️', humidity: '48%', wind: '11 km/h', theme: 'sunny' },
  reykjavik:  { temp: 4,  condition: 'Snowy',   icon: '❄️', humidity: '88%', wind: '25 km/h', theme: 'snowy' },
  berlin:     { temp: 11, condition: 'Cloudy',  icon: '🌫️', humidity: '70%', wind: '14 km/h', theme: 'cloudy' },
  dubai:      { temp: 41, condition: 'Sunny',   icon: '🔆', humidity: '30%', wind: '8 km/h',  theme: 'sunny' },
  toronto:    { temp: 7,  condition: 'Rainy',   icon: '🌦️', humidity: '79%', wind: '20 km/h', theme: 'rainy' },
};

// Default mock for unknown cities (varies by random)
const defaultMocks = [
  { temp: 21, condition: 'Partly Cloudy', icon: '⛅', humidity: '58%', wind: '13 km/h', theme: 'cloudy' },
  { temp: 28, condition: 'Sunny',         icon: '☀️', humidity: '42%', wind: '7 km/h',  theme: 'sunny' },
  { temp: 16, condition: 'Rainy',         icon: '🌧️', humidity: '85%', wind: '22 km/h', theme: 'rainy' },
];

function handleKey(event) {
  if (event.key === 'Enter') checkWeather();
}

function quickSearch(city) {
  document.getElementById('cityInput').value = city;
  checkWeather();
}

function checkWeather() {
  const cityInput = document.getElementById('cityInput');
  const raw = cityInput.value.trim();
  if (!raw) {
    shakeInput();
    return;
  }

  const key = raw.toLowerCase().replace(/\s+/g, '');
  const data = weatherDB[key] || defaultMocks[Math.floor(Math.random() * defaultMocks.length)];
  const displayCity = toTitleCase(raw);

  updateBackground(data.theme);
  renderCard(displayCity, data);
  spawnParticles(data.theme);
}

function renderCard(city, data) {
  const card = document.getElementById('weatherCard');
  card.classList.add('has-data');
  card.innerHTML = `
    <div class="weather-main">
      <div class="city-name">📍 ${escapeHTML(city)}</div>
      <span class="weather-icon-big">${data.icon}</span>
      <div class="temperature">${data.temp}°C</div>
      <div class="condition-label">${data.condition}</div>
      <div class="weather-details">
        <div class="detail-item">
          <span class="detail-label">Humidity</span>
          <span class="detail-value">${data.humidity}</span>
        </div>
        <div class="detail-item">
          <span class="detail-label">Wind</span>
          <span class="detail-value">${data.wind}</span>
        </div>
        <div class="detail-item">
          <span class="detail-label">Feels Like</span>
          <span class="detail-value">${data.temp - 2}°C</span>
        </div>
      </div>
    </div>
  `;
}

function updateBackground(theme) {
  const dashboard = document.getElementById('dashboard');
  // Remove all weather themes
  dashboard.classList.remove('sunny', 'rainy', 'cloudy', 'snowy', 'windy');
  // Add new theme
  if (theme) dashboard.classList.add(theme);
}

function spawnParticles(theme) {
  const layer = document.getElementById('bgLayer');
  layer.innerHTML = '';

  const count = 12;
  for (let i = 0; i < count; i++) {
    const p = document.createElement('div');
    p.classList.add('particle');
    const size = 20 + Math.random() * 60;
    p.style.cssText = `
      width: ${size}px;
      height: ${size}px;
      left: ${Math.random() * 100}%;
      top: ${80 + Math.random() * 20}%;
      animation-duration: ${6 + Math.random() * 8}s;
      animation-delay: ${Math.random() * 4}s;
    `;
    layer.appendChild(p);
  }
}

function shakeInput() {
  const input = document.getElementById('cityInput');
  input.style.animation = 'none';
  input.style.border = '2px solid rgba(255,80,80,0.7)';
  setTimeout(() => { input.style.border = ''; }, 1000);
}

function toTitleCase(str) {
  return str.replace(/\w\S*/g, txt => txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase());
}

function escapeHTML(str) {
  return str.replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;');
}

// Set initial placeholder card
document.getElementById('weatherCard').innerHTML =
  '<p class="placeholder-text">🌐 Search for a city to see the weather</p>';