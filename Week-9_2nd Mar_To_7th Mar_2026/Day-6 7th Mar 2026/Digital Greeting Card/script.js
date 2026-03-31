function revealSurprise() {
  const box = document.getElementById('surpriseBox');
  const btn = document.getElementById('surpriseBtn');

  box.classList.add('visible');
  btn.textContent = '🎉 Surprise Revealed!';
  btn.disabled = true;
  btn.style.opacity = '0.7';
  btn.style.cursor = 'default';

  launchConfetti();
}

function launchConfetti() {
  const container = document.getElementById('confetti');
  const colors = ['#FF6B6B', '#FFD93D', '#6BCB77', '#4D96FF', '#e63981', '#ff9f43'];

  for (let i = 0; i < 60; i++) {
    const piece = document.createElement('div');
    piece.classList.add('confetti-piece');
    piece.style.left = Math.random() * 100 + '%';
    piece.style.top = '0';
    piece.style.backgroundColor = colors[Math.floor(Math.random() * colors.length)];
    piece.style.animationDuration = (1.5 + Math.random() * 2) + 's';
    piece.style.animationDelay = Math.random() * 0.8 + 's';
    piece.style.width = (8 + Math.random() * 8) + 'px';
    piece.style.height = (8 + Math.random() * 8) + 'px';
    container.appendChild(piece);

    // Remove piece after animation ends
    piece.addEventListener('animationend', () => piece.remove());
  }
}